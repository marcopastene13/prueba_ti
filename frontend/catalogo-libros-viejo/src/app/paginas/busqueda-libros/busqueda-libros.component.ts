import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LibrosApiService, Libro } from '../../servicios/libros-api.service';
import { FavoritosApiService } from '../../servicios/favoritos-api.service';

@Component({
  selector: 'app-busqueda-libros',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './busqueda-libros.component.html',
  styleUrls: ['./busqueda-libros.component.css']
})
export class BusquedaLibrosComponent {

  textoBusqueda = '';
  estaBuscando = false;
  libros: Libro[] = [];
  mensajeError: string | null = null;
  mensajeExito: string | null = null;

  constructor(
    private librosApi: LibrosApiService,
    private favoritosApi: FavoritosApiService
  ) {}

  buscar(): void {
    this.mensajeError = null;
    this.mensajeExito = null;

    const texto = this.textoBusqueda.trim();
    if (!texto) {
      this.mensajeError = 'Ingresa un texto para buscar.';
      return;
    }

    this.estaBuscando = true;
    this.librosApi.buscar(texto).subscribe({
      next: (resultado) => {
        this.libros = resultado;
        this.estaBuscando = false;
      },
      error: () => {
        this.mensajeError = 'Ocurrió un error al buscar libros.';
        this.estaBuscando = false;
      }
    });
  }

  agregarAFavoritos(libro: Libro): void {
    this.mensajeError = null;
    this.mensajeExito = null;

    const autores = libro.autores && libro.autores.length > 0
      ? libro.autores.join(', ')
      : undefined;

    this.favoritosApi.agregar({
      idExterno: libro.idExterno,
      titulo: libro.titulo,
      autores,
      anioPrimeraPublicacion: libro.anioPrimeraPublicacion ?? undefined,
      urlPortada: libro.urlPortada
    }).subscribe({
      next: () => {
        this.mensajeExito = 'Libro agregado a favoritos.';
      },
      error: (err) => {
        const mensaje = err?.error?.mensaje as string | undefined;
        this.mensajeError = mensaje ?? 'No se pudo agregar el favorito.';
      }
    });
  }
}
