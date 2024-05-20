using System.Collections.Generic;

namespace LibraBll.DTOs.Pos
{
    public class PosGetDTO
    {
        public int? PosId { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        //FullAddress contains City and Pos Address
        //public string FullAddress { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; }
        public string ConnectionType { get; set; }
        public string MorningProgram { get; set; } = string.Empty;
        public string AfternoonProgram { get; set; } = string.Empty;
        public List<string> DaysClosed { get; set; }
        public string InsertDate { get; set; }
    }
}