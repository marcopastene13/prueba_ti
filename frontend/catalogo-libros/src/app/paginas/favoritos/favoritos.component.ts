import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FavoritosApiService, Favorito } from '../../servicios/favoritos-api.service';

@Component({
  selector: 'app-favoritos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './favoritos.component.html',
  styleUrls: ['./favoritos.component.css']
})
export class FavoritosComponent implements OnInit {

  favoritos: Favorito[] = [];
  estaCargando = false;
  mensajeError: string | null = null;

  constructor(private favoritosApi: FavoritosApiService) {}

  ngOnInit(): void {
    this.cargarFavoritos();
  }

  cargarFavoritos(): void {
    this.mensajeError = null;
    this.estaCargando = true;

    this.favoritosApi.listar().subscribe({
      next: (lista) => {
        this.favoritos = lista;
        this.estaCargando = false;
      },
      error: () => {
        this.mensajeError = 'No se pudieron obtener los favoritos.';
        this.estaCargando = false;
      }
    });
  }

  eliminar(favorito: Favorito): void {
    this.mensajeError = null;

    this.favoritosApi.eliminar(favorito.id).subscribe({
      next: () => {
        this.favoritos = this.favoritos.filter(f => f.id !== favorito.id);
      },
      error: () => {
        this.mensajeError = 'No se pudo eliminar el favorito.';
      }
    });
  }
}
