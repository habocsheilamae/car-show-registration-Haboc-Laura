
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
    <div
      className="table-container"
      role="region"
      aria-label="Registration records"
      tabIndex={0}
    >
      <table>
        <caption className="visually-hidden">
          Car show participant registration records
        </caption>

        <thead>
          <tr>
            <th scope="col">Reg. No.</th>
            <th scope="col">Date</th>
            <th scope="col">Participant</th>
            <th scope="col">Car</th>
            <th scope="col">Type</th>
            <th scope="col">Status</th>
            <th scope="col">Fee / Paid</th>
            <th scope="col">Payment</th>
          </tr>
        </thead>

        <tbody>
          {registrations.map((registration) => (
            <tr key={registration.id}>
              <td>{registration.registrationNumber}</td>
              <td>
                {new Date(registration.registrationDate).toLocaleDateString('en-PH')}
              </td>
              <td>{registration.participantName}</td>
              <td>{registration.carMakeModel}</td>
              <td>{registration.registrationType}</td>
              <td>
                <StatusBadge status={registration.status} />
              </td>
              <td>
                ₱{Number(registration.registrationFee).toLocaleString('en-PH')}
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
