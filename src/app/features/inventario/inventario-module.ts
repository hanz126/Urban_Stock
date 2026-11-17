import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InventarioRoutingModule } from './inventario-routing-module';
import { ListaProductos } from './components/lista-productos/lista-productos';

@NgModule({
  declarations: [ListaProductos],
  imports: [CommonModule, InventarioRoutingModule],
})
export class InventarioModule {}
