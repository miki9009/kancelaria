using System;
using System.Collections.Generic;
using Kancelaria.API.Models;

namespace Kancelaria.API.Dtos
{
    public class UserForDetailsDto
    {
        public int Id{get;set;}
        public string UserName { get; set; }
        public ICollection<CaseDto> Photos { get; set; }
    }
}