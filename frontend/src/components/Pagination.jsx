function Pagination({
  page,
  totalPages,
  totalItems = 0,
  pageSize = 10,
  onPageChange,
  onPageSizeChange,
}) {
  const startItem = totalItems === 0 ? 0 : (page - 1) * pageSize + 1;
  const endItem = Math.min(page * pageSize, totalItems);

  return (
    <div className="pagination-container">
      <div className="pagination">
        <button
          onClick={() => onPageChange(page - 1)}
          disabled={page === 1}
        >
          Previous
        </button>

        {Array.from({ length: totalPages }, (_, index) => index + 1).map(
          (pageNumber) => (
            <button
              key={pageNumber}
              className={page === pageNumber ? 'active-page' : ''}
              onClick={() => onPageChange(pageNumber)}
            >
              {pageNumber}
            </button>
          )
        )}

        <button
          onClick={() => onPageChange(page + 1)}
          disabled={page === totalPages || totalPages === 0}
        >
          Next
        </button>
      </div>

      <div className="pagination-info">
        <span>
          Showing {startItem}–{endItem} of {totalItems}
        </span>

        <label>
          Per page:
          <select
            value={pageSize}
            onChange={(event) =>
              onPageSizeChange(Number(event.target.value))
            }
          >
            <option value={5}>5</option>
            <option value={10}>10</option>
            <option value={25}>25</option>
          </select>
        </label>
      </div>
    </div>
  );
}

export default Pagination;