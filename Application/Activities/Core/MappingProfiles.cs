using AutoMapper;
using Domain;

namespace Application.Activities.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //It accesses the same file?
            CreateMap<Activity, Activity>();
            
        }
    }
}