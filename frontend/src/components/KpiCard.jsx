function KpiCard({ title, value, description }) {
  return (
    <div className="kpi-card">
      <h3>{title}</h3>
      <p className="kpi-value">{value}</p>
      <span>{description}</span>
    </div>
  );
}

export default KpiCard;
