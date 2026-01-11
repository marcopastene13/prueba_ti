import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Favorito {
  id: number;
  idExterno: string;
  titulo: string;
  autores?: string;
  anioPrimeraPublicacion?: number;
  urlPortada?: string;
}

export interface CrearFavoritoRequest {
  idExterno: string;
  titulo: string;
  autores?: string;
  anioPrimeraPublicacion?: number;
  urlPortada?: string;
}

@Injectable({
  providedIn: 'root'
})
export class FavoritosApiService {

  private readonly baseUrl = 'https://congenial-space-spoon-r4w4w79v57v72wprv-5050.app.github.dev/api/favoritos';

  constructor(private http: HttpClient) { }

  listar(): Observable<Favorito[]> {
    return this.http.get<Favorito[]>(this.baseUrl);
  }

  agregar(request: CrearFavoritoRequest): Observable<any> {
    return this.http.post(this.baseUrl, request);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
