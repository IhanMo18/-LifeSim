# 🧬 LifeSim - Simulador de Vida Evolutiva y Guerra

**LifeSim** es un juego de simulación de vida artificial donde dos jugadores crean y evolucionan sus propias razas de criaturas. Estas razas compiten por sobrevivir, reproducirse y evolucionar durante 30 años simulados. Al final, las razas entran en guerra y solo una prevalecerá.

---

## 🎮 Concepto General

- Dos jugadores crean una raza personalizada (stats, nombre, color).
- Cada criatura se reproduce, envejece, muta y sobrevive.
- Cada 5 años los jugadores pueden evolucionar su raza.
- A los 30 años, las razas entran en guerra automáticamente.
- Gana la raza con mayor supervivencia al final del combate.

---

## 🧱 Arquitectura

El proyecto sigue Clean Architecture:

```
LifeSim/
│
├── LifeSim.Core           # Entidades de dominio (Raza, Criatura, ADN)
├── LifeSim.Application    # Casos de uso y lógica de aplicación
├── LifeSim.Infrastructure # Acceso a datos, simulación y tiempo
└── LifeSim.API            # API REST (Endpoints de control)
```

---

## 📦 Tecnologías

- **.NET 8**
- **ASP.NET Core** (API REST)
- **MediatR** (CQRS)
- **Inyección de dependencias**
- **LiteDB o JSON storage** (Persistencia opcional)
- **HostedService** para simulación por tiempo

---

## 🚀 Casos de uso principales

- `POST /api/race/create` → Crear nueva raza
- `POST /api/world/tick` → Avanza un año
- `POST /api/race/evolve` → Mejorar stats cada 5 años
- `POST /api/world/start-war` → Fuerza inicio del combate
- `GET /api/world/state` → Estado actual del mundo

---

## ⚔️ Reglas del Juego

- **Inicio**: cada jugador crea su raza con puntos distribuidos en 5 stats.
- **Simulación**: se avanza por años. Criaturas nacen, comen, mutan y mueren.
- **Evolución**: cada 5 años, el jugador puede subir un stat de su raza.
- **Evento**: al año 30, inicia la guerra entre razas.
- **Combate**: criaturas se enfrentan al encontrarse, usando stats como fuerza y velocidad.

---

## 🧠 Patrones de diseño usados

- **Strategy**: comportamiento de criaturas (mutación, combate)
- **Factory**: creación de criaturas y razas
- **Observer**: eventos globales del mundo
- **ECS (Entity-Component-System)**: comportamiento modular por sistema

---

## 🔧 Cómo correr el proyecto

```bash
# Requiere .NET 8 SDK

dotnet restore

# Ejecutar el backend
cd LifeSim.API
dotnet run
```

---

## ✨ A futuro

- Frontend visualizador del mundo en Blazor o Unity WebGL
- Sistema de ranking entre jugadores
- Eventos aleatorios globales (plagas, mutaciones)
- Balanceo y personalización de habilidades

---

## 👥 Autores

- **David**: lógica de criaturas, ADN, reproducción
- **Ihan**: lógica del mundo, combate, eventos, API

