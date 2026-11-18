const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

// Get registrations with filters and pagination
export async function getRegistrations({
  participantName = '',
  type = '',
  status = '',
  page = 1,
  pageSize = 10,
} = {}) {
  const params = new URLSearchParams({
    participantName,
    type,
    status,
    page: String(page),
    pageSize: String(pageSize),
  });

  const response = await fetch(
    `${API_BASE_URL}/api/registrations?${params}`
  );

  if (!response.ok) {
    throw new Error('Failed to fetch registrations.');
  }

  return response.json();
}

// Get dashboard summary for KPI cards
export async function getRegistrationSummary() {
  const response = await fetch(
    `${API_BASE_URL}/api/registrations/summary`
  );

  if (!response.ok) {
    throw new Error('Failed to fetch registration summary.');
  }

  return response.json();
}
