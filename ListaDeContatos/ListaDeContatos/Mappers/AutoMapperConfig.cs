using AutoMapper;
using ListaDeContatos.Models;
using ListaDeContatos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Mappers
{
    public class AutoMapperConfig : AutoMapper.Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioEditarViewModel>()                
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.NomeUsuario));

            CreateMap<Usuario, UsuarioViewModel>()                
                .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.NomeUsuario));

            CreateMap<Contato, ContatoViewModel>()
                .ReverseMap();
        }
    }
}
