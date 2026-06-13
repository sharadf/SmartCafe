using AutoMapper;
public class AppProfile : Profile
{
    public AppProfile()
    {
        // --- Menu ---
        CreateMap<MenuItem, MenuItemDto>().ReverseMap();
        CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();

        CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();

        // --- Table ---
        CreateMap<CafeTable, TableDto>().ReverseMap();
        CreateMap<CafeTable, CreateTableDto>().ReverseMap();
        // Добавил маппинг для UpdateTableDto, так как он был в списке сущностей
        CreateMap<CafeTable, UpdateTableDto>().ReverseMap();

        // --- Reservation ---
        // AutoMapper автоматически смапит Table.Number в TableNumber благодаря Flattening
        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Table.Number))
            .ReverseMap();


        // --- Order ---
        // Маппинг для элементов заказа, чтобы OrderDto мог корректно заполнить свой список Items
        CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();

        CreateMap<CreateOrderDto, Order>()
        .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items))
        .ReverseMap();

        CreateMap<Order, OrderDto>()
            // Если в будущем захочешь автоматически мапить OrderItems -> Items,
            // имя свойства отличается (OrderItems vs Items), поэтому явно указываем источник:
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
            .ReverseMap()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));


        //// --- Auth ---
        CreateMap<RegisterDto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}
