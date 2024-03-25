using System;

namespace LibraBll.Common
{
    public class PosDTO
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string ConnectionType { get; set; }
        public TimeSpan? MorningOpening { get; set; }
        public TimeSpan? MorningClosing { get; set; }
        public TimeSpan? AfternoonOpening { get; set; }
        public TimeSpan? AfternoonClosing { get; set; }
        public string DaysClosed { get; set; }
        public DateTime InsertDate { get; set; }
    }
}