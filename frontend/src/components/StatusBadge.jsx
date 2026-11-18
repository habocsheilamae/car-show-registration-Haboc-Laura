function StatusBadge({ status }) {
  const statusClass = status?.toLowerCase() || 'unknown';

  return (
    <span className={`status-badge status-${statusClass}`}>
      {status || 'Unknown'}
    </span>
  );
}

export default StatusBadge;