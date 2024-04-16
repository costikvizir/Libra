using System;
using System.Collections.Generic;

namespace LibraBll.DTOs.Pos
{
    public class PosPostDTO
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int ConnectionType { get; set; }
        public string MorningOpening { get; set; }
        public string MorningClosing { get; set; }
        public string AfternoonOpening { get; set; }
        public string AfternoonClosing { get; set; }
        public List<string> DaysClosed { get; set; }
        public DateTime InsertDate { get; set; }
    }
}