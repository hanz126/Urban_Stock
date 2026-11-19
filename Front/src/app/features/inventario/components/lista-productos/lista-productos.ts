import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductoService, Producto } from '../../../../core/services/producto';
import { CategoriaService, Categoria } from '../../../../core/services/categoria';

@Component({
  selector: 'app-lista-productos',
  standalone: false,
  templateUrl: './lista-productos.html',
  styleUrl: './lista-productos.css'
})
export class ListaProductos implements OnInit {
  productos: Producto[] = [];
  categorias: Categoria[] = [];
  loading: boolean = true;
  errorMessage: string = '';

  // Control del Modal
  isModalOpen: boolean = false;
  isEditMode: boolean = false;
  productoForm: FormGroup;
  currentProductId: number | null = null;

  constructor(
    private productoService: ProductoService,
    private categoriaService: CategoriaService,
    private fb: FormBuilder
  ) {
    this.productoForm = this.fb.group({
      iD_Categoria: ['', Validators.required],
      nombre: ['', Validators.required],
      talla: ['', Validators.required],
      precioCosto: [0, [Validators.required, Validators.min(0)]],
      precioVenta: [0, [Validators.required, Validators.min(0)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      stockMinimo: [3, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.cargarProductos();
    this.cargarCategorias();
  }

  cargarProductos(): void {
    this.loading = true;
    this.productoService.obtenerProductos().subscribe({
      next: (data) => {
        this.productos = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'No se pudo conectar con el servidor para cargar el inventario.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  cargarCategorias(): void {
    this.categoriaService.obtenerCategorias().subscribe({
      next: (data) => this.categorias = data,
      error: (err) => console.error('Error cargando categorías', err)
    });
  }

  abrirModalCrear(): void {
    this.isEditMode = false;
    this.currentProductId = null;
    this.productoForm.reset({ stockMinimo: 3 });
    this.isModalOpen = true;
  }

  abrirModalEditar(p: Producto): void {
    this.isEditMode = true;
    this.currentProductId = p.iD_Producto || null;
    this.productoForm.patchValue({
      iD_Categoria: p.iD_Categoria,
      nombre: p.nombre,
      talla: p.talla,
      precioCosto: p.precioCosto,
      precioVenta: p.precioVenta,
      stock: p.stock,
      stockMinimo: p.stockMinimo
    });
    this.isModalOpen = true;
  }

  cerrarModal(): void {
    this.isModalOpen = false;
  }

  guardarProducto(): void {
    if (this.productoForm.invalid) {
      this.productoForm.markAllAsTouched();
      return;
    }

    const productoData = this.productoForm.value;

    if (this.isEditMode && this.currentProductId) {
      productoData.iD_Producto = this.currentProductId;
      this.productoService.actualizarProducto(this.currentProductId, productoData).subscribe({
        next: () => {
          this.cargarProductos();
          this.cerrarModal();
        },
        error: (err) => alert('Error al actualizar el producto')
      });
    } else {
      this.productoService.crearProducto(productoData).subscribe({
        next: () => {
          this.cargarProductos();
          this.cerrarModal();
        },
        error: (err) => alert('Error al crear el producto')
      });
    }
  }

  eliminarProducto(id?: number): void {
    if (!id) return;
    if (confirm('¿Estás seguro de que deseas eliminar este producto?')) {
      this.productoService.eliminarProducto(id).subscribe({
        next: () => {
          this.productos = this.productos.filter(p => p.iD_Producto !== id);
        },
        error: (err) => alert('Error al intentar eliminar el producto.')
      });
    }
  }
}