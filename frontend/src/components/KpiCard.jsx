
function KpiCard({ title, value, description }) {
  return (
    <article className="kpi-card" aria-label={`${title}: ${value}`}>
      <h3>{title}</h3>
      <p className="kpi-value">{value}</p>
      {description && <span>{description}</span>}
    </article>
  );
}

export default KpiCard;
