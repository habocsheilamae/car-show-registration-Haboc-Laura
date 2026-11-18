import KpiCard from './KpiCard.jsx';

function KpiCards({
  totalRegistrations = 0,
  totalPayments = 0,
  uncollectedPayments = 0,
  confirmed = 0,
  checkedIn = 0,
}) {
  const cards = [
    {
      title: 'Total Registration',
      value: totalRegistrations,
    },
    {
      title: 'Total Payments',
      value: `₱${Number(totalPayments).toLocaleString('en-PH')}`,
    },
    {
      title: 'Uncollected Payments',
      value: `₱${Number(uncollectedPayments).toLocaleString('en-PH')}`,
    },
    {
      title: 'Confirmed',
      value: confirmed,
    },
    {
      title: 'Checked In',
      value: checkedIn,
    },
  ];

  return (
    <section className="kpi-cards">
      {cards.map((card) => (
        <KpiCard
          key={card.title}
          title={card.title}
          value={card.value}
          description=""
        />
      ))}
    </section>
  );
}

export default KpiCards;