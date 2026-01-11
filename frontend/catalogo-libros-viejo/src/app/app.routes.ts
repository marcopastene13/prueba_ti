import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'busqueda', pathMatch: 'full' },
  {
    path: 'busqueda',
    loadComponent: () =>
      import('./paginas/busqueda-libros/busqueda-libros.component')
        .then(m => m.BusquedaLibrosComponent)
  },
  {
    path: 'favoritos',
    loadComponent: () =>
      import('./paginas/favoritos/favoritos.component')
        .then(m => m.FavoritosComponent)
  }
];
