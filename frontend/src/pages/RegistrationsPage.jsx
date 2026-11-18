import { useState } from 'react';
import KpiCards from '../components/KpiCards.jsx';
import RegistrationFilters from '../components/RegistrationFilters.jsx';
import RegistrationTable from '../components/RegistrationTable.jsx';
import Pagination from '../components/Pagination.jsx';
import { sampleRegistrations } from '../api/sample-data.js';
import useDebounce from '../hooks/useDebounce.js';

function RegistrationsPage() {
  const [participantName, setParticipantName] = useState('');
  const [type, setType] = useState('');
  const [status, setStatus] = useState('');
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const debouncedName = useDebounce(participantName, 400);

  const filteredRegistrations = sampleRegistrations.filter((registration) => {
    const matchesName = registration.participantName
      .toLowerCase()
      .includes(debouncedName.toLowerCase());

    const matchesType = !type || registration.registrationType === type;
    const matchesStatus = !status || registration.status === status;

    return matchesName && matchesType && matchesStatus;
  });

  const totalPages = Math.ceil(filteredRegistrations.length / pageSize);

  const startIndex = (page - 1) * pageSize;

  const currentRegistrations = filteredRegistrations.slice(
    startIndex,
    startIndex + pageSize
  );

  const totalPayments = sampleRegistrations.reduce(
    (total, registration) => total + registration.amountPaid,
    0
  );

  const uncollectedPayments = sampleRegistrations.reduce(
    (total, registration) =>
      total + (registration.fee - registration.amountPaid),
    0
  );

  const confirmed = sampleRegistrations.filter(
    (registration) => registration.status === 'Approved'
  ).length;

  const checkedIn = sampleRegistrations.filter(
    (registration) => registration.status === 'Checked In'
  ).length;

  function handlePageSizeChange(newSize) {
    setPageSize(newSize);
    setPage(1);
  }

  function handleClearFilters() {
    setParticipantName('');
    setType('');
    setStatus('');
    setPage(1);
  }

  return (
    <main>
      <header>
        <h1>AutoVista Events</h1>
        <h2>Car Show Registration 2027</h2>
        <p>Sorsogon City Coliseum</p>
      </header>

      <KpiCards
        totalRegistrations={sampleRegistrations.length}
        totalPayments={totalPayments}
        uncollectedPayments={uncollectedPayments}
        confirmed={confirmed}
        checkedIn={checkedIn}
      />

      <div className="registrations-toolbar">
        <h2>Registrations</h2>

        <RegistrationFilters
          participantName={participantName}
          setParticipantName={(value) => {
            setParticipantName(value);
            setPage(1);
          }}
          type={type}
          setType={(value) => {
            setType(value);
            setPage(1);
          }}
          status={status}
          setStatus={(value) => {
            setStatus(value);
            setPage(1);
          }}
          onClear={handleClearFilters}
        />
      </div>

       {currentRegistrations.length === 0 ? (
  <div className="empty-state">
    <h3>No registrations found</h3>
    <p>
      There are no registrations matching your search or filters.
    </p>
    <button type="button" onClick={handleClearFilters}>
      Clear filters
    </button>
  </div>
) : (
  <RegistrationTable
    registrations={currentRegistrations}
    onClear={handleClearFilters}
  />
)}

      <Pagination
        page={page}
        totalPages={totalPages}
        totalItems={filteredRegistrations.length}
        pageSize={pageSize}
        onPageChange={setPage}
        onPageSizeChange={handlePageSizeChange}
      />
    </main>
  );
}

export default RegistrationsPage;
