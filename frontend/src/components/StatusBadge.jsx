function StatusBadge({ status }) {
  const displayStatus =
    status === 'CheckedIn' ? 'Checked In' : status || 'Unknown';

  const statusClass = status
    ? status === 'CheckedIn'
      ? 'checked-in'
      : status.toLowerCase().replace(/\s+/g, '-')
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
