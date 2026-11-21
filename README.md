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

| # | Method | Endpoint | Description | Success | Errors |
|---|---|---|---|---|---|
| 1 | POST | /api/registrations | Make a new registration | 201 Created | 400, 409 |
| 2 | GET | /api/registrations | Show all registrations (paged) | 200 OK | 400 |
| 3 | GET | /api/registrations/{id} | Find registration by ID | 200 OK | 404 |
| 4 | GET | /api/registrations?type=Competitor | Find by type | 200 OK | 400 |
| 5 | GET | /api/registrations?status=Confirmed | Find by status | 200 OK | 400 |
| 6 | GET | /api/registrations?participantName=juan | Find by participant name (contains, case-insensitive) | 200 OK | 400 |
| 7 | GET | /api/registrations/summary | KPI totals for the dashboard | 200 OK | — |

NOTE:
Endpoints 2, 4, 5 and 6 are the same route with optional query parameters 
(`participantName`, `type`, `status`, `page`, `pageSize`). Filters work alone and combined.