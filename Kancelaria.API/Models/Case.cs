using System;

namespace Kancelaria.API.Models
{
    public class Case
    {
        public int Id { get; set; }

        public string CaseName { get; set; }   

        public string Signature { get; set;}

        public string Leader {get; set;}

        public bool Closed {get;set;}

        public DateTime DateAdded { get; set; }

        public int? UserID { get; set; } 

        public int? CaseDetailID { get; set; }


    }
}