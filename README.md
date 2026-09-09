# LibroFacil - Sistema de Gestión de Libros (Web API)

API RESTful desarrollada en **.NET** aplicando los principios de **Clean Architecture**, **Domain-Driven Design (DDD)** básico, **Entity Framework Core (Code-First)** y **Microsoft SQL Server**.

---

## Arquitectura del Proyecto

El sistema está estructurado en 4 capas desacopladas respetando la regla de dependencia hacia adentro:

* **`LibroFacil.Domain`**: Contiene la entidad principal `Libro` y las definiciones puras del dominio sin dependencias externas.
* **`LibroFacil.Application`**: Contiene la lógica de negocio (`LibroService`), las interfaces abstracción de persistencia (`ILibroRepository`) y las validaciones de las reglas del sistema.
* **`LibroFacil.Infrastructure`**: Implementa la persistencia de datos con **Entity Framework Core**, gestiona el contexto de base de datos (`LibroFacilDbContext`), las migraciones y la comunicación con **SQL Server**.
* **`LibroFacil.Api`**: Expone las rutas HTTP mediante `LibrosController`, maneja las peticiones y respuestas JSON, y configura el contenedor de Inyección de Dependencias.

---

##  Tecnologías y Herramientas

* **Lenguaje:** C# / .NET
* **Framework Web:** ASP.NET Core Web API
* **ORM:** Entity Framework Core (SqlServer, Tools, Design)
* **Base de Datos:** Microsoft SQL Server (`LibroFacilDb`)
* **Pruebas de API:** Postman
* **IDE/Editor:** Visual Studio Code

---

## Reglas de Negocio Implementadas

La API valida obligatoriamente las siguientes reglas de negocio antes de modificar el estado de la base de datos:

1. **ISBN:** Obligatorio y único en el sistema.
2. **Campos Requeridos:** El **Título** y **Autor** no pueden ser vacíos ni nulos.
3. **Año de Publicación:** Debe ser un valor estrictamente mayor a 0 y no superior al año actual.
4. **Stock:** Debe ser un número entero mayor o igual a 0 (no se permiten valores negativos).

---

## Pasos para Configurar y Ejecutar

### 1. Requisitos Previos
* .NET SDK instalado.
* Instancia local de SQL Server (`SQLEXPRESS`) activa.
* Servicio **SQL Server Browser** en ejecución.

### 2. Configurar la Cadena de Conexión
Verifica que en el archivo `src/LibroFacil.Api/appsettings.json` la cadena apunte a tu servidor local:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\SQLEXPRESS;Database=LibroFacilDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Aplicar las Migraciones en SQL Server
Aplica el modelo Code-First para crear la base de datos `LibroFacilDb` y la tabla `Libros`:

```bash
dotnet ef database update --project src/LibroFacil.Infrastructure/LibroFacil.Infrastructure.csproj --startup-project src/LibroFacil.Api/LibroFacil.Api.csproj
```

### 4. Iniciar la Web API
Ejecuta el proyecto desde la raíz del repositorio:

```bash
dotnet run --project src/LibroFacil.Api/LibroFacil.Api.csproj
```

---

## 🔌 Endpoints de la API

La API responde en la ruta base: `http://localhost:<PUERTO>/api/libros`

| Método | Endpoint | Descripción | Código Éxito |
| :--- | :--- | :--- | :--- |
| **GET** | `/api/libros` | Obtiene el listado completo de libros | `200 OK` |
| **GET** | `/api/libros/{id}` | Consulta un libro por su ID | `200 OK` |
| **POST** | `/api/libros` | Registra un nuevo libro validando las reglas de negocio | `201 Created` |
| **PUT** | `/api/libros/{id}` | Actualiza la información de un libro existente | `204 No Content` |
| **DELETE** | `/api/libros/{id}` | Elimina un libro de la base de datos por su ID | `204 No Content` |

---

## Ejemplo de Petición JSON (POST / PUT)

```json
{
  "isbn": "9780132350884",
  "titulo": "Clean Code",
  "autor": "Robert C. Martin",
  "anioPublicacion": 2008,
  "stock": 5
}
```