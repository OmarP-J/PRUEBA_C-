# 🏦 Calculadora de Préstamos — ASP.NET Core MVC

Aplicación web para calcular cuotas de préstamos personales basándose en la edad del solicitante, el monto y el plazo. Desarrollada con **ASP.NET Core 9.0** siguiendo una arquitectura de **3 capas** que combina **MVC** con una **Web API REST**.

---

## 🛠️ Tecnologías

| Capa | Tecnología |
|------|------------|
| Backend | C# / ASP.NET Core 9.0 (MVC + Web API) |
| Base de datos | SQL Server (con Procedimientos Almacenados) |
| Frontend | HTML5, Bootstrap 5, JavaScript (Fetch API) |
| Patrones | Repository Pattern, Dependency Injection |

---

## 📂 Estructura del Proyecto

```
CalculadoraPrestamosApp/
├── Controllers/
│   ├── HomeController.cs        # Sirve la vista principal (MVC)
│   └── CalculoController.cs     # Endpoint API de cálculo
├── Services/
│   └── PrestamoService.cs       # Lógica de negocio y reglas de validación
├── Data/
│   └── PrestamoRepository.cs    # Acceso a datos vía Procedimientos Almacenados
├── Models/
│   ├── Prestamo.cs              # PrestamoRequest, PrestamoResponse, CuotaAmortizacion
│   └── ErrorViewModel.cs
├── Views/
│   ├── Home/Index.cshtml        # Interfaz de usuario
│   └── Shared/
│       ├── _Layout.cshtml
│       └── Error.cshtml
├── wwwroot/                     # Archivos estáticos (Bootstrap, jQuery, CSS)
├── appsettings.json             # Cadena de conexión a SQL Server
└── Program.cs                   # Configuración de servicios y middleware
```

---

## ⚙️ Configuración

### 1. Prerrequisitos
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (Express o superior)

### 2. Base de datos
Ejecutar el script SQL para crear la base de datos `DB_Credito` con las tablas `EdadTasa`, `PlazoMeses` y `LogConsultas`, y los procedimientos almacenados:
- `usp_ObtenerTasaPorEdad`
- `usp_ValidarPlazo`
- `usp_InsertarLogConsulta`

### 3. Cadena de conexión
Editar `appsettings.json` con el nombre de tu servidor SQL:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR\\SQLEXPRESS;Database=DB_Credito;Integrated Security=true;TrustServerCertificate=True;"
}
```

### 4. Ejecutar la aplicación
```bash
dotnet restore
dotnet run
```

La aplicación estará disponible en `https://localhost:{puerto}`.

---

## 🔌 API Endpoint

### `POST /api/calculo/cuota`

**Body (JSON):**
```json
{
  "fechaNacimiento": "2000-05-15",
  "monto": 50000,
  "meses": 12
}
```

**Respuesta exitosa:**
```json
{
  "exito": true,
  "mensaje": "Cálculo exitoso",
  "cuota": 4375.00,
  "tablaAmortizacion": [...]
}
```

---

## 📋 Reglas de Negocio

### Fórmula de cálculo
```
Cuota = (Monto × Tasa) / Cantidad de meses
```

### Tasas por edad
| Edad | Tasa |
|------|------|
| 18   | 1.20 |
| 19   | 1.18 |
| 20   | 1.16 |
| 21   | 1.14 |
| 22   | 1.12 |
| 23   | 1.10 |
| 24   | 1.08 |
| 25   | 1.05 |

### Plazos permitidos
`3`, `6`, `9` y `12` meses.

### Validaciones
- **Edad < 18:** *"Lo sentimos, aún no cuenta con la edad para solicitar este producto."*
- **Edad > 25:** *"Favor pasar por una de nuestras sucursales para evaluar su caso."*
- **Plazo inválido:** Solo se aceptan los valores de la tabla anterior.

### Registro de consultas
Cada cálculo exitoso queda registrado en la tabla `LogConsultas` con: Id, Fecha, Edad, Monto, Meses, Valor Cuota e IP del cliente.

---

© 2026 Sistema de Gestión de Préstamos Personales
