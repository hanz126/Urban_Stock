import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PosVentaComponent } from './components/pos-venta/pos-venta';

const routes: Routes = [
  {
    path: '',
    component: PosVentaComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PosRoutingModule {}