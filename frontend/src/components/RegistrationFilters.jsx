
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
        <option value="Regular">Regular</option>
        <option value="VIP">VIP</option>
      </select>

      <select
        aria-label="Filter by registration status"
        value={status}
        onChange={(event) => setStatus(event.target.value)}
      >
        <option value="">All Statuses</option>
        <option value="Approved">Approved</option>
        <option value="Pending">Pending</option>
        <option value="Checked In">Checked In</option>
        <option value="Rejected">Rejected</option>
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
