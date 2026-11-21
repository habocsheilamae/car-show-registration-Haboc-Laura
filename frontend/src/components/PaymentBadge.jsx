function PaymentBadge({ status }) {
  const paymentClass = status?.toLowerCase() || 'unknown';

  return (
    <span className={`payment-badge payment-${paymentClass}`}>
      {status || 'Unknown'}
    </span>
  );
}

export default PaymentBadge;