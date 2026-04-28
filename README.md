# 🏦 Calculadora de Préstamos Premium

Este proyecto es una aplicación web moderna desarrollada con **ASP.NET Core 9.0** que permite a los usuarios calcular cuotas de préstamos basadas en su edad, monto solicitado y plazo en meses. La arquitectura sigue un patrón de **3 capas** (UI/Controllers, Services, Data) y combina **MVC** con una **Web API** robusta.

## 🚀 Características

- **Interfaz de Usuario Premium**: Diseño elegante con Glassmorphism, animaciones fluidas y totalmente responsive.
- **Validación por Edad**: El sistema aplica reglas de negocio basadas en la edad del solicitante (18-25 años).
- **Tasas Dinámicas**: Las tasas de interés se ajustan automáticamente según el rango de edad.
- **Tabla de Amortización**: Visualización detallada de los pagos mensuales y el balance restante.
- **Arquitectura Limpia**: Separación clara de responsabilidades entre lógica de negocio y acceso a datos.

## 🛠️ Tecnologías

- **Backend**: .NET 9.0 (ASP.NET Core MVC + Web API)
- **Frontend**: HTML5, Vanilla CSS3 (Glassmorphism), JavaScript (Fetch API)
- **Framework de Estilo**: Bootstrap 5.3 (incluido mediante CDN/Assets estáticos)
- **Patrones**: Repository Pattern, Dependency Injection (DI)

## 📋 Requisitos Previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado en su sistema.
- Un editor de código como VS Code o Visual Studio 2022.

## ⚙️ Configuración y Ejecución

### 1. Clonar o descargar el proyecto
Si ya tienes los archivos en tu carpeta:

### 2. Restaurar dependencias
Abre una terminal en la raíz del proyecto y ejecuta:
```bash
dotnet restore
```

### 3. Compilar el proyecto
```bash
dotnet build
```

### 4. Ejecutar la aplicación
```bash
dotnet run
```
La aplicación estará disponible en `http://localhost:5000` (o el puerto indicado en la consola).

## 📂 Estructura del Proyecto

- **/Controllers**: 
  - `HomeController`: Maneja la navegación y las vistas MVC.
  - `CalculoController`: Controlador API que procesa los cálculos de préstamos.
- **/Services**: 
  - `PrestamoService`: Contiene la lógica de negocio, reglas de edad y cálculos matemáticos.
- **/Data**: 
  - `PrestamoRepository`: Encargado del acceso a datos (actualmente configurado como Mock para pruebas rápidas).
- **/Models**: 
  - Definición de clases para solicitudes, respuestas y tablas de amortización.
- **/Views**: 
  - Interfaz de usuario principal.

## 🔌 API Endpoints

### Calcular Cuota
- **URL**: `/api/calculo/cuota`
- **Método**: `POST`
- **Body**:
```json
{
  "fechaNacimiento": "1995-05-15",
  "monto": 50000,
  "meses": 12
}
```

## 📝 Reglas de Negocio Implementadas
- **Edad < 18**: El sistema deniega la solicitud.
- **Edad 18-25**: El sistema permite el cálculo con tasas específicas.
- **Edad > 25**: El sistema solicita evaluación en sucursal.
- **Plazos permitidos**: 3, 6, 9 y 12 meses.

---
© 2026 Sistema de Gestión de Préstamos Personales.
