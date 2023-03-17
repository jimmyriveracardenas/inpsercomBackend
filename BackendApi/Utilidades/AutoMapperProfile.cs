using AutoMapper;
using BackendApi.DTOs;
using BackendApi.Models;
using System.Globalization;

namespace BackendApi.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol,RolDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino => destino.NombreRol,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino => destino.FechaCreacion,
                opt => opt.MapFrom(origen => origen.FechaCreacion.Value.ToString("dd/MM/yyyy")));


            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.FechaCreacion,
                opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaCreacion,"dd/MM/yyyy",CultureInfo.InvariantCulture))
                );
            #endregion
        }

    }
}
