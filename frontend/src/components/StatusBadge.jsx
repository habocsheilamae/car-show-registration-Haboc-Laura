
function StatusBadge({ status }) {
  const displayStatus = status || 'Unknown';
  const statusClass = status
    ? status.toLowerCase().replace(/\s+/g, '-')
    : 'unknown';

  return (
    <span
      className={`status-badge status-${statusClass}`}
      role="status"
      aria-label={`Registration status: ${displayStatus}`}
    >
      {displayStatus}
    </span>
  );
}

export default StatusBadge;
