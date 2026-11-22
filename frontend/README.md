# AutoVista Events — Car Show Registration Dashboard

A React-based frontend dashboard for managing participant registrations for **AutoVista Events Car Show 2027**, to be held at Sorsogon City Coliseum.

## Project Overview

The Car Show Registration Dashboard provides an organized interface for viewing and monitoring participant registrations, participant information, registration statuses, and payment records.

This project is developed as part of the Integrative Programming 2 course.

The frontend is integrated with a **C#/.NET backend REST API** to retrieve actual registration records and dashboard summary information.

## Technologies Used

- React JS
- Vite
- JavaScript
- HTML5
- CSS3
- REST API
- Fetch API
- Git and GitHub

## Features

- Responsive registration dashboard
- Five KPI summary cards:
  - Total Registrations
  - Total Payments
  - Uncollected Payments
  - Confirmed Registrations
  - Checked In Participants
- Registration records table connected to the backend API
- Search participants by name
- Filter by registration type
- Filter by registration status
- Debounced search input (400ms)
- API-based pagination
- Loading state
- Empty state
- Error state with retry functionality
- Clear filters functionality
- Accessible labels and navigation
- Dynamic dashboard summary from the backend API

## Backend API Integration

The frontend communicates with the backend through HTTP GET requests.

### API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/registrations` | Retrieves registration records with filtering and pagination |
| GET | `/api/registrations/summary` | Retrieves dashboard KPI summary data |

### Supported Query Parameters

The registrations endpoint supports:

- `participantName` — Search by participant name
- `type` — Filter by registration type
- `status` — Filter by registration status
- `page` — Current page number
- `pageSize` — Number of records per page

### API Configuration

The frontend uses an environment variable to identify the backend API base URL.

Create a file named `.env.development` inside the `frontend/` directory:

```env
VITE_API_BASE_URL=http://localhost:5205
```

**Note:** The `.env.development` file is for local development and should not be committed to GitHub.

## Project Structure

```text
frontend/
├── public/
├── src/
│   ├── api/
│   │   ├── registrationsApi.js
│   │   └── sample-data.js
│   ├── components/
│   │   ├── KpiCard.jsx
│   │   ├── KpiCards.jsx
│   │   ├── Pagination.jsx
│   │   ├── PaymentBadge.jsx
│   │   ├── RegistrationFilters.jsx
│   │   ├── RegistrationTable.jsx
│   │   └── StatusBadge.jsx
│   ├── hooks/
│   │   └── useDebounce.js
│   ├── pages/
│   │   └── RegistrationsPage.jsx
│   ├── App.jsx
│   ├── App.css
│   ├── index.css
│   └── main.jsx
├── index.html
├── package.json
└── vite.config.js
```

## Installation and Setup

### Prerequisites

- Node.js
- npm
- Visual Studio Code (recommended)
- Running backend API

### 1. Navigate to the frontend directory

```bash
cd frontend
```

If you have already opened the frontend folder directly, you can skip this command.

### 2. Install dependencies

```bash
npm install
```

### 3. Configure the API URL

Create `frontend/.env.development` and add:

```env
VITE_API_BASE_URL=http://localhost:5205
```

### 4. Start the development server

```bash
npm run dev
```

Open the local URL displayed in your terminal to view the dashboard.

**Important:** Make sure the backend API is running and accessible at the configured URL before using the dashboard.

## Current Development Status

The frontend dashboard is integrated with the .NET backend REST API.

Registration records and dashboard summary information are retrieved from the backend instead of relying on the sample registration data.

The dashboard supports API-based searching, filtering, pagination, and user interface states for loading, empty results, and errors.

## Repository

GitHub: [Car Show Registration Project](https://github.com/habocsheilamae/car-show-registration-Haboc-Laura)

Frontend Branch: `feature/frontend-dashboard`

## Project Information

Developed for Integrative Programming 2 — Car Show Registration System Project.