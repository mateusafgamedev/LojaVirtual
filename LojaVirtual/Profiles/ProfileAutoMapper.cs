using AutoMapper;
using LojaVirtual.DTO.EnderecoDto;
using LojaVirtual.Models;

namespace LojaVirtual.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<EnderecoModel, EditarEnderecoDto>().ReverseMap();
        }

    }
}
