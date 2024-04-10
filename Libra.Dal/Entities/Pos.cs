using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
    public class Pos : BaseEntity
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int ConnectionTypeId { get; set; }
        public int? MorningOpening { get; set; }
        public int? MorningClosing { get; set; }
        public int? AfternoonOpening { get; set; }
        public int? AfternoonClosing { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime InsertDate { get; set; }
        public City City { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public ICollection<Issue> Issues { get; set; }
        public ICollection<PosWeekDay> PosWeekDays { get; set; }
    }
}