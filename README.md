# Car Show Registration System

Car Show 2027 registration system for AutoVista Events (Sorsogon City Coliseum).
Course: Integrative Programming 2, CCDI Sorsogon Campus.

## Team

| Member | Role |
|---|---|
| John Jared Laura | Backend Developer |
| Sheila Mae Haboc | Frontend Developer |

## Tech stack

- **Backend:** C# / .NET 10, Vertical Slice Architecture, EF Core, SQLite, FluentValidation, Scalar
- **Frontend:** React (Vite)

## Run the backend

Requires the .NET 10 SDK.

    cd backend
    dotnet run --project Api

- API: http://localhost:5205
- Scalar docs: http://localhost:5205/scalar/v1
- The SQLite database (`carshow.db`) is created and seeded with 25 registrations on first run.

## Run the frontend

    cd frontend
    npm install
    npm run dev

The dashboard runs at http://localhost:5173 (allowed by the API's CORS policy).

## API endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | /api/registrations | Create a registration |
| GET | /api/registrations | List (paged; filters: participantName, type, status) |
| GET | /api/registrations/{id} | Get one registration |
| GET | /api/registrations/summary | KPI totals |