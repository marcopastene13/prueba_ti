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

  private readonly baseUrl = 'http://localhost:5050/api/libros';

  constructor(private http: HttpClient) { }

  buscar(texto: string): Observable<Libro[]> {
    const params = new HttpParams().set('texto', texto);
    return this.http.get<Libro[]>(`${this.baseUrl}/buscar`, { params });
  }
}
