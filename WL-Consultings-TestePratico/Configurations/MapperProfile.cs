using AutoMapper;
using WL_Consultings_TestePratico.Models.DTOs.Carteira;
using WL_Consultings_TestePratico.Models.DTOs.Transacao;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Models.Enums;

namespace WL_Consultings_TestePratico.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Usuario
            CreateMap<Usuario, UsuarioReadDto>().ReverseMap();
            CreateMap<Usuario, UsuarioCreateDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDto>().ReverseMap();

            // Carteira
            CreateMap<Carteira, CarteiraReadDto>()
                .ForMember(dest => dest.Moeda, opt => opt.MapFrom(src => Enum.Parse<Moeda>(src.Moeda)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<StatusCarteira>(src.Status)))
                .ReverseMap()
                .ForMember(dest => dest.Moeda, opt => opt.MapFrom(src => src.Moeda.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // Transacao
            CreateMap<Transacao, TransacaoReadDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<StatusTransacao>(src.Status)))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => Enum.Parse<TipoTransacao>(src.Tipo)))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));


        }
    }
}
