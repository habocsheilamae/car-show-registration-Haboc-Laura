
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
      title: 'Total Registrations',
      value: Number(totalRegistrations).toLocaleString('en-PH'),
      description: 'All registered participants',
    },
    {
      title: 'Total Payments',
      value: `₱${Number(totalPayments).toLocaleString('en-PH')}`,
      description: 'Payments received',
    },
    {
      title: 'Uncollected Payments',
      value: `₱${Number(uncollectedPayments).toLocaleString('en-PH')}`,
      description: 'Remaining balances',
    },
    {
      title: 'Confirmed',
      value: Number(confirmed).toLocaleString('en-PH'),
      description: 'Approved registrations',
    },
    {
      title: 'Checked In',
      value: Number(checkedIn).toLocaleString('en-PH'),
      description: 'Participants checked in',
    },
  ];

  return (
    <section className="kpi-cards" aria-label="Registration summary">
      {cards.map((card) => (
        <KpiCard
          key={card.title}
          title={card.title}
          value={card.value}
          description={card.description}
        />
      ))}
    </section>
  );
}

export default KpiCards;
