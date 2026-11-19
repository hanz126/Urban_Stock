import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared-module';
import { InventarioRoutingModule } from './inventario-routing-module';
import { ListaProductos } from './components/lista-productos/lista-productos';

@NgModule({
  declarations: [
    ListaProductos
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    InventarioRoutingModule
  ],
})
export class InventarioModule {}
