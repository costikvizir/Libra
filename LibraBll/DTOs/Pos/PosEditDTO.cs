using System.Collections.Generic;

namespace LibraBll.DTOs.Pos
{
    public class PosEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }

        //FullAddress contains City and Pos Address
        public string FullAddress { get; set; }

        public string Model { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; }
        public int ConnectionType { get; set; }
        public string MorningOpening { get; set; }
        public string MorningClosing { get; set; }
        public string AfternoonOpening { get; set; }
        public string AfternoonClosing { get; set; }

        // public string MorningProgram { get; set; }
        // public string AfternoonProgram { get; set; }
        public List<string> DaysClosed { get; set; }

        // public string ModificationDate { get; set; }
    }
}