# AutoVista Events — Car Show Registration Dashboard

A React-based frontend dashboard for managing participant registrations for **AutoVista Events Car Show 2027**, to be held at Sorsogon City Coliseum.

## Project Overview

This dashboard provides an organized interface for viewing and monitoring car show registrations, participant information, registration statuses, and payment records.

This project is developed as part of the Integrative Programming 2 course.

## Technologies Used

- React JS
- Vite
- JavaScript
- HTML5
- CSS3
- Git and GitHub

## Features

- Responsive registration dashboard
- Five KPI summary cards:
  - Total Registrations
  - Total Payments
  - Uncollected Payments
  - Confirmed Registrations
  - Checked In Participants
- Registration records table
- Search participants by name
- Filter by registration type
- Filter by registration status
- Debounced search input
- Pagination
- Loading state
- Empty state
- Error state UI
- Clear filters functionality
- Accessible labels and navigation

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

### 1. Navigate to the frontend directory

```bash
cd frontend
```

If you have already opened the frontend folder directly, you can skip this command.

### 2. Install dependencies

```bash
npm install
```

### 3. Start the development server

```bash
npm run dev
```

Open the local URL displayed in your terminal to view the dashboard.

## Current Development Status

The frontend dashboard currently uses sample registration data for development and testing.

Backend API integration is pending verification of the backend endpoints and response format.

## Repository

GitHub: [Car Show Registration Project](https://github.com/habocsheilamae/car-show-registration-Haboc-Laura)

Frontend Branch: `feature/frontend-dashboard`

---

Developed for Integrative Programming 2 — Car Show Registration System Project.