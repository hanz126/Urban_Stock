import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductoService, Producto } from '../../../../core/services/producto';
import { CategoriaService, Categoria } from '../../../../core/services/categoria';
import { ClienteService, Cliente } from '../../../../core/services/cliente';
import { Venta, RegistrarVentaRequest } from '../../../../core/services/venta';
import { AuthService } from '../../../../core/services/auth';

export interface ItemCarrito {
  producto: Producto;
  cantidad: number;
  subtotal: number;
}

export interface ReciboVenta {
  idVenta?: number;
  fecha: Date;
  vendedor: string;
  cliente: string;
  items: ItemCarrito[];
  total: number;
}

@Component({
  selector: 'app-pos-venta',
  standalone: false,
  templateUrl: './pos-venta.html',
  styleUrl: './pos-venta.css'
})
export class PosVentaComponent implements OnInit {
  productos: Producto[] = [];
  productosFiltrados: Producto[] = [];
  categorias: Categoria[] = [];
  clientes: Cliente[] = [];

  carrito: ItemCarrito[] = [];
  categoriaSeleccionada: number = 0;
  terminoBusqueda: string = '';
  clienteSeleccionadoId: number | null = null;

  loadingProductos: boolean = true;
  procesandoVenta: boolean = false;
  mensajeError: string = '';
  mensajeExito: string = '';

  mostrarModalCliente: boolean = false;
  clienteForm: FormGroup;
  guardandoCliente: boolean = false;
  errorClienteModal: string = '';

  mostrarModalRecibo: boolean = false;
  reciboActual: ReciboVenta | null = null;

  constructor(
    private productoService: ProductoService,
    private categoriaService: CategoriaService,
    private clienteService: ClienteService,
    private ventaService: Venta,
    private authService: AuthService,
    private fb: FormBuilder
  ) {
    this.clienteForm = this.fb.group({
      documento: ['', [Validators.required, Validators.minLength(5)]],
      nombreCompleto: ['', [Validators.required, Validators.minLength(3)]],
      telefono: [''],
      correo: ['', [Validators.email]]
    });
  }

  ngOnInit(): void {
    this.cargarDatosIniciales();
  }

  cargarDatosIniciales(): void {
    this.loadingProductos = true;
    this.productoService.obtenerProductos().subscribe({
      next: (data: Producto[]) => {
        this.productos = data;
        this.filtrarProductos();
        this.loadingProductos = false;
      },
      error: (err: any) => {
        this.mensajeError = 'No se pudieron cargar los productos del catálogo.';
        this.loadingProductos = false;
        console.error(err);
      }
    });

    this.categoriaService.obtenerCategorias().subscribe({
      next: (data: Categoria[]) => {
        this.categorias = data;
      },
      error: (err: any) => console.error('Error al cargar categorías:', err)
    });

    this.clienteService.obtenerClientes().subscribe({
      next: (data: Cliente[]) => {
        this.clientes = data;
      },
      error: (err: any) => console.error('Error al cargar clientes:', err)
    });
  }

  filtrarProductos(): void {
    let filtrados = this.productos;

    if (this.categoriaSeleccionada > 0) {
      filtrados = filtrados.filter(p =>
        (p.iD_Categoria || p.id_Categoria) === this.categoriaSeleccionada
      );
    }

    if (this.terminoBusqueda.trim()) {
      const q = this.terminoBusqueda.toLowerCase().trim();
      filtrados = filtrados.filter(p =>
        p.nombre.toLowerCase().includes(q) ||
        p.talla.toLowerCase().includes(q) ||
        (p.nombreCategoria && p.nombreCategoria.toLowerCase().includes(q))
      );
    }

    this.productosFiltrados = filtrados;
  }

  seleccionarCategoria(idCat: number): void {
    this.categoriaSeleccionada = idCat;
    this.filtrarProductos();
  }

  onBuscar(event: Event): void {
    this.terminoBusqueda = (event.target as HTMLInputElement).value;
    this.filtrarProductos();
  }

  getIdProducto(p: Producto): number {
    return p.iD_Producto || p.id_Producto || p.idProducto || 0;
  }

  getPrecioUnitario(p: Producto): number {
    return Number(p.precioVenta ?? p.precio ?? 0);
  }

  agregarAlCarrito(producto: Producto): void {
    if (producto.stock <= 0) {
      alert(`La prenda "${producto.nombre}" está agotada.`);
      return;
    }

    const idProd = this.getIdProducto(producto);
    const precio = this.getPrecioUnitario(producto);
    const itemExistente = this.carrito.find(item => this.getIdProducto(item.producto) === idProd);

    if (itemExistente) {
      if (itemExistente.cantidad >= producto.stock) {
        alert(`No puedes agregar más unidades. Stock disponible: ${producto.stock}.`);
        return;
      }
      itemExistente.cantidad++;
      itemExistente.subtotal = itemExistente.cantidad * precio;
    } else {
      this.carrito.push({
        producto,
        cantidad: 1,
        subtotal: precio
      });
    }
  }

  incrementarCantidad(item: ItemCarrito): void {
    if (item.cantidad >= item.producto.stock) {
      alert(`Stock máximo disponible alcanzado (${item.producto.stock} unidades).`);
      return;
    }
    item.cantidad++;
    item.subtotal = item.cantidad * this.getPrecioUnitario(item.producto);
  }

  decrementarCantidad(item: ItemCarrito): void {
    if (item.cantidad > 1) {
      item.cantidad--;
      item.subtotal = item.cantidad * this.getPrecioUnitario(item.producto);
    } else {
      this.eliminarDelCarrito(item);
    }
  }

  eliminarDelCarrito(item: ItemCarrito): void {
    const idProd = this.getIdProducto(item.producto);
    this.carrito = this.carrito.filter(i => this.getIdProducto(i.producto) !== idProd);
  }

  vaciarCarrito(): void {
    if (this.carrito.length === 0) return;
    if (confirm('¿Estás seguro de vaciar el carrito actual?')) {
      this.carrito = [];
    }
  }

  get totalVenta(): number {
    return this.carrito.reduce((sum, item) => sum + item.subtotal, 0);
  }

  get totalPrendas(): number {
    return this.carrito.reduce((sum, item) => sum + item.cantidad, 0);
  }

  confirmarVenta(): void {
    if (this.carrito.length === 0) {
      alert('Debes agregar al menos un producto al carrito de venta.');
      return;
    }

    const usuario = this.authService.getUsuario();
    const idUsuario = usuario?.iD_Usuario || usuario?.id_Usuario || usuario?.idUsuario || 1;

    const request: RegistrarVentaRequest = {
      venta: {
        iD_Usuario: idUsuario,
        iD_Cliente: this.clienteSeleccionadoId ? Number(this.clienteSeleccionadoId) : null,
        totalVenta: this.totalVenta
      },
      detalles: this.carrito.map(item => ({
        iD_Producto: this.getIdProducto(item.producto),
        cantidad: item.cantidad,
        precioUnitario: this.getPrecioUnitario(item.producto),
        subtotal: item.subtotal
      }))
    };

    this.procesandoVenta = true;
    this.mensajeError = '';
    this.mensajeExito = '';

    this.ventaService.registrarVenta(request).subscribe({
      next: (res: any) => {
        this.procesandoVenta = false;
        const idVentaGenerada = res?.iD_Venta || res?.id_Venta || res?.idVenta || Math.floor(1000 + Math.random() * 9000);

        let nombreCliente = 'Cliente Ocasional / Mostrador';
        if (this.clienteSeleccionadoId) {
          const cli = this.clientes.find(c => (c.iD_Cliente || c.id_Cliente || c.idCliente) === Number(this.clienteSeleccionadoId));
          if (cli) nombreCliente = `${cli.nombreCompleto} (CC: ${cli.documento})`;
        }

        this.reciboActual = {
          idVenta: idVentaGenerada,
          fecha: new Date(),
          vendedor: usuario?.nombre || 'Vendedor Urban',
          cliente: nombreCliente,
          items: [...this.carrito],
          total: this.totalVenta
        };

        this.mostrarModalRecibo = true;
        this.carrito = [];
        this.clienteSeleccionadoId = null;
        this.recargarProductos();
      },
      error: (err: any) => {
        this.procesandoVenta = false;
        console.error('Error al registrar venta:', err);
        const backendMsg = err.error?.mensaje || err.error?.message || 'Error al procesar la venta en el servidor.';
        this.mensajeError = backendMsg;
        alert(`No se pudo procesar la venta: ${backendMsg}`);
      }
    });
  }

  recargarProductos(): void {
    this.productoService.obtenerProductos().subscribe({
      next: (data: Producto[]) => {
        this.productos = data;
        this.filtrarProductos();
      }
    });
  }

  imprimirRecibo(): void {
    window.print();
  }

  cerrarRecibo(): void {
    this.mostrarModalRecibo = false;
    this.reciboActual = null;
  }

  abrirModalCliente(): void {
    this.clienteForm.reset();
    this.errorClienteModal = '';
    this.mostrarModalCliente = true;
  }

  cerrarModalCliente(): void {
    this.mostrarModalCliente = false;
    this.clienteForm.reset();
    this.errorClienteModal = '';
  }

  guardarNuevoCliente(): void {
    if (this.clienteForm.invalid) {
      this.clienteForm.markAllAsTouched();
      return;
    }

    this.guardandoCliente = true;
    this.errorClienteModal = '';

    const nuevoCliente: Cliente = this.clienteForm.value;

    this.clienteService.crearCliente(nuevoCliente).subscribe({
      next: (creado: Cliente) => {
        this.guardandoCliente = false;
        this.clientes.push(creado);
        this.clienteSeleccionadoId = creado.iD_Cliente || creado.id_Cliente || creado.idCliente || null;
        this.cerrarModalCliente();
        alert(`Cliente "${creado.nombreCompleto}" registrado y seleccionado con éxito.`);
      },
      error: (err: any) => {
        this.guardandoCliente = false;
        this.errorClienteModal = 'No se pudo registrar el cliente. Verifique que el documento no esté duplicado.';
        console.error(err);
      }
    });
  }
}