import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Categoria {
  iD_Categoria?: number;
  nombreCategoria: string;
}

@Injectable({ providedIn: 'root' })
export class CategoriaService {
  private apiUrl = 'http://localhost:5190/api/Categorias';
  constructor(private http: HttpClient) {}

  obtenerCategorias(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(this.apiUrl);
  }

  crearCategoria(cat: Categoria): Observable<Categoria> {
    return this.http.post<Categoria>(this.apiUrl, cat);
  }
}