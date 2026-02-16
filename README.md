# Real-time Call Center Dashboard

A real-time monitoring dashboard for call centers, built with **Angular 19** and **.NET 10**. This application provides live updates of agent statuses, queue statistics, and key performance indicators (KPIs) without requiring page reloads.

## 🚀 Key Features

- **Real-time Updates**: changes in agent status and queue data are pushed instantly to all connected clients using **SignalR**.
- **Live Timers**: Duration and Wait times update every second on the client side.
- **Background Simulation**: A hosted background service simulates a live call center environment by randomly changing agent statuses and generating/processing calls (Answered/Abandoned).
- **Search**: Efficient server-side filtering using **Sieve**.
- **Clean Architecture**: structured following domain-driven design principles for maintainability and scalability.

## 🛠️ Technology Stack

### Backend (.NET 10)
- **ASP.NET Core Web API**: The core framework.
- **SignalR**: For real-time bi-directional communication.
- **BackgroundService (IHostedService)**: Handles the simulation logic for generating data.
- **Sieve**: For advanced filtering, sorting, and pagination.
- **Clean Architecture**: Separated into `Domain`, `Application`, `Infrastructure`, and `API` layers.

### Frontend (Angular 19)
- **Angular 19**: The latest version of the framework.
- **TailwindCSS**: For rapid and responsive UI styling.
- **SignalR Client**: To consume real-time events.
- **Standalone Components**: Modern Angular architecture.

## 📂 Project Structure

```
├── client/                 # Angular 19 Frontend
├── src/
│   ├── Dashboard.API/      # Entry point, Controllers, SignalR Hubs
│   ├── Dashboard.Application/ # Business logic, DTOs, Interfaces
│   ├── Dashboard.Domain/   # Core Entities, Enums
│   └── Dashboard.Infrastructure/ # Data access, Repositories
```

## 🚦 Getting Started

### Prerequisites
- Node.js (Latest LTS)
- .NET 10 SDK

### Running the Backend

1. Navigate to the API folder:
   ```bash
   cd src/Dashboard.API
   ```
2. Run the application:
   ```bash
   dotnet run
   ```
   The API will start on `http://localhost:5081`.

### Running the Frontend

1. Navigate to the client folder:
   ```bash
   cd client
   ```
2. Install dependencies (first time only):
   ```bash
   npm install
   ```
3. Start the development server:
   ```bash
   npm start
   ```
   The application will be available at `http://localhost:4200`.

## 🔄 How it Works

1. **Simulation**: The `DashboardBroadcastService` runs in the background. Every few seconds, it updates random agents and processes calls in the queue.
2. **Broadcasting**: When data changes, the service uses `DashboardHub` to broadcast updated `AgentDTO` and `QueueDTO` objects to all connected clients.
3. **Client Updates**: The Angular application listens for these events and updates the `StatsGrid`, `AgentTable`, and `QueueTable` instantly.
4. **Live Timers**: The frontend calculates the duration since `LastStatusChange` or `OldestCallCreatedAt` locally to show a ticking timer, ensuring the UI feels alive.