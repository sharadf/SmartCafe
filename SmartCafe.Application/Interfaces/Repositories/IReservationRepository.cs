public interface IReservationRepository
{
    Task<List<Reservation>> GetAllAsync();

    Task<Reservation?> GetByIdAsync(Guid id);

    Task<List<Reservation>> GetByTableIdAsync(Guid tableId);

    Task AddAsync(Reservation reservation);

    Task UpdateAsync(Reservation reservation);

    Task DeleteAsync(Guid id);
}
