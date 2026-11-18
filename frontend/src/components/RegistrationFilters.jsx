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
    <section className="registration-filters">
      <input
        type="text"
        placeholder="Search participant..."
        value={participantName}
        onChange={(event) => setParticipantName(event.target.value)}
      />

      <select
        value={type}
        onChange={(event) => setType(event.target.value)}
      >
        <option value="">All Types</option>
        <option value="Regular">Regular</option>
        <option value="VIP">VIP</option>
      </select>

      <select
        value={status}
        onChange={(event) => setStatus(event.target.value)}
      >
        <option value="">All Statuses</option>
        <option value="Approved">Approved</option>
        <option value="Pending">Pending</option>
        <option value="Checked In">Checked In</option>
        <option value="Rejected">Rejected</option>
      </select>

      <button type="button" onClick={onClear}>
        Clear
      </button>
    </section>
  );
}

export default RegistrationFilters;
