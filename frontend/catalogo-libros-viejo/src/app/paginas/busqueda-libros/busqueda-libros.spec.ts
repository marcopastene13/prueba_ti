import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusquedaLibros } from './busqueda-libros';

describe('BusquedaLibros', () => {
  let component: BusquedaLibros;
  let fixture: ComponentFixture<BusquedaLibros>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusquedaLibros]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusquedaLibros);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
