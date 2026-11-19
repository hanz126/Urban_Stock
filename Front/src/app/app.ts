import { Component, signal } from '@angular/core';
import { AuthService } from './core/services/auth';

@Component({
  selector: 'app-root',
  standalone: false,
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('urban-stock');

  constructor(private authService: AuthService) {}

  estaLogueado(): boolean {
    return this.authService.estaAutenticado();
  }
}