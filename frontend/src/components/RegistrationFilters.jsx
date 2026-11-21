
function RegistrationFilters({
  participantName,
  setParticipantName,
  type,
  setType,
  status,
  setStatus,
  onClear,
}) {
  return (
    <section
      className="registration-filters"
      aria-label="Registration filters"
    >
      <input
        type="text"
        placeholder="Search by participant name..."
        aria-label="Search participant by name"
        value={participantName}
        onChange={(event) => setParticipantName(event.target.value)}
      />

      <select
        aria-label="Filter by registration type"
        value={type}
        onChange={(event) => setType(event.target.value)}
      >
        <option value="">All Types</option>
        <option value="Exhibitor">Exhibitor</option>
        <option value="Competitor">Competitor</option>
        <option value="VIP">VIP</option>
      </select>

      <select
        aria-label="Filter by registration status"
        value={status}
        onChange={(event) => setStatus(event.target.value)}
      >
        <option value="">All Statuses</option>
        <option value="Confirmed">Confirmed</option>
        <option value="Pending">Pending</option>
        <option value="CheckedIn">Checked In</option>
        <option value="Cancelled">Cancelled</option>
      </select>

      <button
        type="button"
        onClick={onClear}
        aria-label="Clear all registration filters"
      >
        Clear Filters
      </button>
    </section>
  );
}

export default RegistrationFilters;
