import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UsuarioSesion {
  iD_Usuario?: number;
  id_Usuario?: number;
  idUsuario?: number;
  nombre: string;
  correo: string;
  rol: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5190/api/Usuarios/login';

  constructor(private http: HttpClient) {}

  login(credentials: { correo: string; contrasena: string }): Observable<any> {
    return this.http.post<any>(this.apiUrl, credentials);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
  }

  getUsuario(): UsuarioSesion | null {
    const data = localStorage.getItem('usuario');
    return data ? JSON.parse(data) : null;
  }

  getRol(): string {
    const usuario = this.getUsuario();
    return usuario?.rol || '';
  }

  estaAutenticado(): boolean {
    return !!localStorage.getItem('token');
  }
}