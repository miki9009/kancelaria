using AutoMapper;
using Kancelaria.API.Models;
using Kancelaria.API.Dtos;
using System.Linq;

namespace Kancelaria.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            
            CreateMap<User, UserForListDto>();

            // CreateMap<Case, PhotosForDetailedDto>();
        }
    }
}