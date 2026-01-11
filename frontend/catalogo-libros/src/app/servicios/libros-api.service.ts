import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Libro {
  idExterno: string;
  titulo: string;
  autores: string[];
  anioPrimeraPublicacion?: number;
  urlPortada?: string;
}

@Injectable({
  providedIn: 'root'
})
export class LibrosApiService {

 private readonly baseUrl = 'https://congenial-space-spoon-r4w4w79v57v72wprv-5050.app.github.dev/api/libros';

  constructor(private http: HttpClient) { }

  buscar(texto: string): Observable<Libro[]> {
    const params = new HttpParams().set('texto', texto);
    return this.http.get<Libro[]>(`${this.baseUrl}/buscar`, { params });
  }
}
