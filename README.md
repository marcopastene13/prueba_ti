# Catálogo de libros + Favoritos (Prueba técnica VISTA TI)

Este README incluye: **requisitos**, cómo ejecutar backend y frontend, cómo ejecutar el DDL y configurar la *connection string*, cómo correr tests y un resumen de decisiones técnicas.[file:1]

Aplicación web que permite buscar libros en una API REST pública y guardar favoritos en una base de datos relacional SQL Server.[file:1]  
El objetivo es demostrar legibilidad, estructura, orden del código y capacidad de análisis utilizando Angular, .NET Web API y SQL Server.[file:1]

---

## Requisitos

- **Frontend:** Angular 21 (compatible con el requisito de Angular 20+).[file:1]  
- **Backend:** C# .NET Web API (SDK .NET 8 o compatible).  
- **Base de datos:** SQL Server relacional.  
- **Tests:** xUnit en backend.  
- **Opcional:**  
  - GitHub Codespaces.  
  - Docker para levantar SQL Server dentro de Codespaces.

---

## Estructura del repositorio

```txt
prueba_ti/
  backend/
    PruebaTi.Api/              # API REST en .NET
    PruebaTi.Api.Tests/        # Tests de backend
  frontend/
    catalogo-libros/           # Aplicación Angular
  database/
    schema.sql                 # Script DDL (SQL Server)
  README.md

 
Cómo ejecutar el DDL y configurar la connection string
Opción 1: SQL Server local
1.	Tener SQL Server instalado (Express o Developer).
2.	Abrir tu herramienta de administración (SSMS, Azure Data Studio, etc.).
3.	Ejecutar el script:
-- Abrir y ejecutar:
database/schema.sql

4.	Verificar:
o	Base de datos PruebaTiLibros.
o	Tablas Usuarios y Favoritos.
En backend/PruebaTi.Api/appsettings.json configurar:
"ConnectionStrings": {
  "BaseDeDatos": "Server=localhost;Database=PruebaTiLibros;Trusted_Connection=True;TrustServerCertificate=True;"
}

Opción 2: SQL Server en Docker (Codespaces)
1.	Crear contenedor:
docker run -e "ACCEPT_EULA=Y" \
  -e "SA_PASSWORD=Your_password123" \
  -p 1433:1433 \
  --name sql_prueba_ti \
  -d mcr.microsoft.com/mssql/server:2022-latest

2.	Copiar el script:
cd /workspaces/prueba_ti
docker cp ./database/schema.sql sql_prueba_ti:/var/opt/mssql/schema.sql

3.	Instalar mssql-tools18 en el contenedor (solo una vez):
docker exec -it sql_prueba_ti /bin/bash
apt-get update
apt-get install -y mssql-tools18 unixodbc-dev
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc
exit

4.	Ejecutar el DDL:
docker exec -it sql_prueba_ti /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "Your_password123" -C \
  -i /var/opt/mssql/schema.sql

5.	Configurar connection string para Codespaces:
"ConnectionStrings": {
  "BaseDeDatos": "Server=localhost,1433;Database=PruebaTiLibros;User Id=sa;Password=Your_password123;TrustServerCertificate=True;"
}

Nota: si la base de datos no está levantada, los endpoints de favoritos (/api/favoritos) fallarán con error de conexión a SQL Server.[file:1]
 
Cómo ejecutar el backend
Proyecto: backend/PruebaTi.Api
1.	Ir a la carpeta del backend:
cd backend/PruebaTi.Api

2.	Restaurar dependencias:
dotnet restore

3.	Ejecutar la API:
dotnet run

4.	La consola mostrará las URLs, por ejemplo:
o	https://localhost:7182
o	http://localhost:5050
Usa una de esas URLs como baseUrl en el frontend.
 
Cómo ejecutar el frontend
Proyecto: frontend/catalogo-libros
1.	Ir a la carpeta del frontend:
cd frontend/catalogo-libros

2.	Instalar dependencias:
npm install

3.	Ejecutar la aplicación:
ng serve

4.	Acceder desde el navegador:
o	Local: http://localhost:4200.
o	Codespaces: Angular mostrará una URL del tipo https://<TU-FRONT>-4200.app.github.dev.
En los servicios Angular configura el baseUrl apuntando al backend, por ejemplo:
// Libros
private readonly baseUrlLibros = 'https://localhost:7182/api/libros';
// Favoritos
private readonly baseUrlFavoritos = 'https://localhost:7182/api/favoritos';
// o las URLs equivalentes en Codespaces

 
Cómo correr los tests
Proyecto de tests: backend/PruebaTi.Api.Tests
1.	Ir a la carpeta de tests:
cd backend/PruebaTi.Api.Tests

2.	Ejecutar:
dotnet test

Los tests cubren casos sugeridos por la prueba:[file:1]
•	Agregar favorito evita duplicados.
•	Validación de request inválido (faltan campos obligatorios).
•	Normalización/mapeo desde la API externa (Open Library) a DTO propio.
•	Eliminar favorito inexistente devuelve una respuesta clara.
•	Listar favoritos retorna los favoritos del usuario.
 
Descripción técnica y decisiones breves
Funcionalidades principales
•	Búsqueda en API pública:
o	API usada: Open Library (https://openlibrary.org/search.json?q=...), que no requiere API Key.[file:1]
o	El backend llama a Open Library, normaliza la respuesta y expone un endpoint propio para el frontend.[file:1]
•	Favoritos en base de datos:
o	Botón “Agregar a favoritos” en los resultados de búsqueda.
o	Vista “Mis favoritos” que lista lo guardado desde el backend/BD.
o	Se puede eliminar un favorito desde la misma vista.
Modelo de datos (SQL Server)
El script database/schema.sql define:[file:1]
•	Base de datos PruebaTiLibros.
•	Tabla Usuarios (Id, Nombre).
•	Tabla Favoritos (Id, UsuarioId, IdExterno, Titulo, Autores, AnioPrimeraPublicacion, UrlPortada).
•	Constraint único UNIQUE (UsuarioId, IdExterno) para evitar duplicados por usuario.[file:1]
Al guardar un favorito se almacena información suficiente para listar sin volver a consultar la API externa (id externo, título, autores, año, portada).[file:1]
Usuario / sesión
Para simplificar autenticación, se utiliza un usuario fijo (UsuarioId = 1), opción permitida explícitamente por la prueba técnica.[file:1]
Arquitectura del backend
•	Capas / carpetas principales:
o	Dominio/Entidades: Usuario, Favorito.
o	Infraestructura/Contexto: AplicacionDbContext (EF Core).
o	Infraestructura/ClientesExternos: cliente HTTP a Open Library (ILibrosApiCliente, OpenLibraryApiCliente).
o	Aplicacion/Dto: DTOs para libros y favoritos.
o	Aplicacion/Servicios: IServicioLibros, ServicioLibros, IServicioFavoritos, ServicioFavoritos.
o	Controladores: LibrosController, FavoritosController.
•	Separación de responsabilidades:
o	Controladores: sólo gestionan HTTP (request/response).
o	Servicios: contienen la lógica de negocio (validaciones, reglas de favoritos, etc.).
o	Cliente externo: encapsula el consumo de la API de Open Library.
o	Contexto EF Core: abstrae el acceso a la base de datos.[file:1]
Endpoints implementados
Basados en lo sugerido por el enunciado:[file:1]
•	GET /api/libros/buscar?texto=...
o	Consulta a Open Library y devuelve libros normalizados.
•	GET /api/favoritos
o	Lista favoritos del usuario fijo.
•	POST /api/favoritos
o	Agrega un nuevo favorito validando duplicados y campos obligatorios.
•	DELETE /api/favoritos/{id}
o	Elimina un favorito si existe; en caso contrario devuelve un mensaje claro.
Frontend (Angular)
•	Pantalla de búsqueda:
o	Input de texto, botón “Buscar”, listado de resultados, botón “Agregar a favoritos”.
•	Pantalla de favoritos:
o	Lista libros almacenados en BD, botón “Eliminar”.
El diseño es mínimo pero usable, en línea con que la prueba no asigna puntaje a estética.[file:1]
 
Notas finales
•	El proyecto está pensado para ejecutarse tanto en entorno local como en GitHub Codespaces (con SQL Server en Docker).
