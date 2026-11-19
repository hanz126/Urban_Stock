import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DetalleVentaDTO {
  iD_Producto?: number;
  id_Producto?: number;
  cantidad: number;
  precioUnitario: number;
  subtotal?: number;
  nombreProducto?: string;
  talla?: string;
}

export type DetalleVenta = DetalleVentaDTO;

export interface RegistrarVentaRequest {
  venta: {
    iD_Usuario?: number;
    id_Usuario?: number;
    iD_Cliente?: number | null;
    id_Cliente?: number | null;
    totalVenta?: number;
  };
  detalles: DetalleVentaDTO[];
}

@Injectable({
  providedIn: 'root'
})
export class Venta {
  private apiUrl = 'http://localhost:5190/api/Ventas';

  constructor(private http: HttpClient) {}

  obtenerVentas(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  registrarVenta(payload: RegistrarVentaRequest): Observable<any> {
    return this.http.post<any>(this.apiUrl, payload);
  }
}