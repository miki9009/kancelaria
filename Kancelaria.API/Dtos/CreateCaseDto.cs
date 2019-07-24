using System;

namespace Kancelaria.API.Dtos
{
    public class CreateCaseDto
    {
        public string CaseName { get; set; }
        public string Signature { get; set; }

        public string Leader {get;set;}
        public string MyProperty { get; set; }
        public int UserID {get;set;}

        public DateTime DateAdded {get; set; }
        
        public bool Closed {get; set;}


        public int CaseDetailID {get;set;}

    }
}