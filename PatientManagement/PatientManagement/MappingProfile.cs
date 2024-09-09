using AutoMapper;
using PatientManagement.Models;
using PatientManagement.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PatientModel, Patient>()
           .ReverseMap();
    }
}
