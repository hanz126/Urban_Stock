import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PosRoutingModule } from './pos-routing-module';
import { PosVentaComponent } from './components/pos-venta/pos-venta';

@NgModule({
  declarations: [
    PosVentaComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PosRoutingModule
  ]
})
export class PosModule {}