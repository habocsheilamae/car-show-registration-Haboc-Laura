import StatusBadge from './StatusBadge.jsx';
import PaymentBadge from './PaymentBadge.jsx';

function RegistrationTable({ registrations = [], onClear }) {
  if (registrations.length === 0) {
    return (
      <div className="empty-state">
        <h3>No registrations match your filters</h3>
        <p>Try changing your search or clearing the filters.</p>
        <button type="button" onClick={onClear}>
          Clear filters
        </button>
      </div>
    );
  }

  return (
    <div className="table-container">
      <table>
        <thead>
          <tr>
            <th>Reg. No.</th>
            <th>Date</th>
            <th>Participant</th>
            <th>Car</th>
            <th>Type</th>
            <th>Status</th>
            <th>Fee / Paid</th>
            <th>Payment</th>
          </tr>
        </thead>

        <tbody>
          {registrations.map((registration) => (
            <tr key={registration.id}>
              <td>{registration.registrationNumber}</td>
              <td>{registration.registrationDate}</td>
              <td>{registration.participantName}</td>
              <td>{registration.carMake} {registration.carModel}</td>
              <td>{registration.registrationType}</td>
              <td>
                <StatusBadge status={registration.status} />
              </td>
              <td>
                ₱{Number(registration.fee).toLocaleString('en-PH')}
                {' / '}
                ₱{Number(registration.amountPaid).toLocaleString('en-PH')}
              </td>
              <td>
                <PaymentBadge status={registration.paymentStatus} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default RegistrationTable;
