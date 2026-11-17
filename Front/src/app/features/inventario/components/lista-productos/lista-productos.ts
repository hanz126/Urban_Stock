import { Component, OnInit } from '@angular/core';
import { ProductoService, Producto } from '../../../../core/services/producto';

@Component({
  selector: 'app-lista-productos',
  standalone: false,
  templateUrl: './lista-productos.html',
  styleUrl: './lista-productos.css'
})
export class ListaProductos implements OnInit {
  productos: Producto[] = [];
  loading: boolean = true;
  errorMessage: string = '';

  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.cargarProductos();
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
}
