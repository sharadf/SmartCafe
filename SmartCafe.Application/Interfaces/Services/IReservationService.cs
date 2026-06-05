public interface IReservationService
{
    Task<List<ReservationDto>> GetAllAsync();

    Task<ReservationDto?> GetByIdAsync(Guid id);

    Task<List<ReservationDto>> GetCustomerReservationsAsync(Guid customerId);

    Task CreateAsync(Guid customerId, CreateReservationDto dto);

    Task CancelAsync(Guid reservationId);
}
