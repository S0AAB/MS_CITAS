using AutoMapper;
using MS_CITAS.Application.Dto;
using MS_CITAS.Domain.Models;



namespace MS_CITAS.Application.Mappings
{
	public class CitaMapping: Profile
    {
        public CitaMapping()
        {
            CreateMap<Citas, CitaDto>().ReverseMap();
        }
    }
	
}