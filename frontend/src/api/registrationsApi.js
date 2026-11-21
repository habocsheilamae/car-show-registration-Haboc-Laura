const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export async function getRegistrations({
  participantName = '',
  type = '',
  status = '',
  page = 1,
  pageSize = 10,
  signal,
} = {}) {
  const params = new URLSearchParams({
  page: String(page),
  pageSize: String(pageSize),
});

if (participantName.trim()) {
  params.set('participantName', participantName.trim());
}

if (type) {
  params.set('type', type);
}

if (status) {
  params.set('status', status);
}

  const response = await fetch(
    `${API_BASE_URL}/api/registrations?${params}`,
    { signal }
  );

  if (!response.ok) {
    throw new Error('Failed to fetch registrations.');
  }

  return response.json();
}

export async function getRegistrationSummary({ signal } = {}) {
  const response = await fetch(
    `${API_BASE_URL}/api/registrations/summary`,
    { signal }
  );

  if (!response.ok) {
    throw new Error('Failed to fetch registration summary.');
  }

  return response.json();
}