import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, UsuarioSesion } from '../../../core/services/auth';

@Component({
  selector: 'app-navbar',
  standalone: false,
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class NavbarComponent {
  constructor(private authService: AuthService, private router: Router) {}

  get usuario(): UsuarioSesion | null {
    return this.authService.getUsuario();
  }

  get rol(): string {
    return this.authService.getRol();
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }
}
