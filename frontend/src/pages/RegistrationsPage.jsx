
import { useState, useEffect } from 'react';
import KpiCards from '../components/KpiCards.jsx';
import RegistrationFilters from '../components/RegistrationFilters.jsx';
import RegistrationTable from '../components/RegistrationTable.jsx';
import Pagination from '../components/Pagination.jsx';
import {
  getRegistrations,
  getRegistrationSummary,
} from '../api/registrationsApi.js';
import useDebounce from '../hooks/useDebounce.js';

function RegistrationsPage() {
  const [participantName, setParticipantName] = useState('');
  const [type, setType] = useState('');
  const [status, setStatus] = useState('');

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const [registrations, setRegistrations] = useState([]);
  const [summary, setSummary] = useState({
    totalRegistrations: 0,
    totalPayments: 0,
    uncollectedPayments: 0,
    confirmed: 0,
    checkedIn: 0,
  });

  const [totalPages, setTotalPages] = useState(0);
  const [totalCount, setTotalCount] = useState(0);

  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  const [retryCount, setRetryCount] = useState(0);

  const debouncedName = useDebounce(participantName, 400);

  useEffect(() => {
    const controller = new AbortController();

    async function loadDashboard() {
      try {
        setIsLoading(true);
        setError('');

        const [registrationData, summaryData] = await Promise.all([
          getRegistrations({
            participantName: debouncedName,
            type,
            status,
            page,
            pageSize,
            signal: controller.signal,
          }),
          getRegistrationSummary({
            signal: controller.signal,
          }),
        ]);

        setRegistrations(registrationData.items);
        setTotalPages(registrationData.totalPages);
        setTotalCount(registrationData.totalCount);
        setSummary(summaryData);
      } catch (err) {
        if (err.name !== 'AbortError') {
          setError(err.message || 'Unable to load dashboard data.');
        }
      } finally {
        if (!controller.signal.aborted) {
          setIsLoading(false);
        }
      }
    }

    loadDashboard();

    return () => controller.abort();
  }, [debouncedName, type, status, page, pageSize, retryCount]);

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

  function handleRetry() {
    setRetryCount((count) => count + 1);
  }

  return (
    <main>
      <header>
        <h1>AutoVista Events</h1>
        <h2>Car Show Registration 2027</h2>
        <p>Sorsogon City Coliseum</p>
      </header>

      <KpiCards
        totalRegistrations={summary.totalRegistrations}
        totalPayments={summary.totalPayments}
        uncollectedPayments={summary.uncollectedPayments}
        confirmed={summary.confirmed}
        checkedIn={summary.checkedIn}
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

      {isLoading ? (
        <p className="loading-message">
          Loading registrations...
        </p>
      ) : error ? (
        <div className="error-state">
          <h3>Something went wrong</h3>
          <p>{error}</p>
          <button type="button" onClick={handleRetry}>
            Try again
          </button>
        </div>
      ) : registrations.length === 0 ? (
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
          registrations={registrations}
          onClear={handleClearFilters}
        />
      )}

      <Pagination
        page={page}
        totalPages={totalPages}
        totalItems={totalCount}
        pageSize={pageSize}
        onPageChange={setPage}
        onPageSizeChange={handlePageSizeChange}
      />
    </main>
  );
}

export default RegistrationsPage;
