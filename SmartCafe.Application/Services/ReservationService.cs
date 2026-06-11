using AutoMapper;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ITableRepository _tableRepository;

    private readonly IMapper _mapper;

    public ReservationService(
        IReservationRepository reservationRepository,
        ITableRepository tableRepository,
        IMapper mapper
    )
    {
        _reservationRepository = reservationRepository;
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<List<ReservationDto>> GetAllAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();

        return _mapper.Map<List<ReservationDto>>(reservations);
    }

    public async Task<ReservationDto?> GetByIdAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);

        if (reservation == null)
            return null;

        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<List<ReservationDto>> GetCustomerReservationsAsync(Guid customerId)
    {
        var reservations = await _reservationRepository.GetAllAsync();

        var customerReservations = reservations.Where(x => x.CustomerId == customerId).ToList();

        return _mapper.Map<List<ReservationDto>>(customerReservations);
    }

    public async Task CreateAsync(Guid customerId, CreateReservationDto dto)
    {
        // Валидация базовой логики дат
        if (dto.StartDateTime >= dto.EndDateTime)
            throw new ArgumentException("End date must be after start date");

        if (dto.StartDateTime < DateTime.UtcNow)
            throw new ArgumentException("Cannot reserve in the past");

        var table = await _tableRepository.GetByIdAsync(dto.TableId);

        if (table == null)
            throw new Exception("Table not found");

        var reservations = await _reservationRepository.GetByTableIdAsync(dto.TableId);

        bool hasConflict = reservations.Any(x =>
            x.Status != ReservationStatus.Cancelled
            && dto.StartDateTime < x.EndDateTime
            && dto.EndDateTime > x.StartDateTime
        );

        if (hasConflict)
            throw new Exception("Table is already reserved");

        // var reservation = new Reservation
        // {
        //     Id = Guid.NewGuid(),

        //     CustomerId = customerId,

        //     TableId = dto.TableId,

        //     StartDateTime = dto.StartDateTime,

        //     EndDateTime = dto.EndDateTime,

        //     Status = ReservationStatus.Confirmed,
        // };

        var reservation = _mapper.Map<Reservation>(dto);

        reservation.Id = Guid.NewGuid();
        reservation.CustomerId = customerId;
        reservation.Status = ReservationStatus.Confirmed;

        await _reservationRepository.AddAsync(reservation);

        table.Status = TableStatus.Reserved;
        await _tableRepository.UpdateAsync(table);
    }

    public async Task CancelAsync(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId);

        if (reservation == null)
            throw new Exception("Reservation not found");

        if (reservation.Status == ReservationStatus.Cancelled)
            throw new Exception("Reservation is already cancelled");

        reservation.Status = ReservationStatus.Cancelled;
        await _reservationRepository.UpdateAsync(reservation);

        var table = await _tableRepository.GetByIdAsync(reservation.TableId);
        if (table != null)
        {
            var now = DateTime.UtcNow;
            if (now >= reservation.StartDateTime && now <= reservation.EndDateTime)
            {
                table.Status = TableStatus.Empty;
                await _tableRepository.UpdateAsync(table);
            }
        }
    }
}
