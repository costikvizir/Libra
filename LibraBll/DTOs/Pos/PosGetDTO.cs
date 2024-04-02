﻿using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.DTOs.Pos
{
	public class PosGetDTO
	{
		public string Name { get; set; }
		public string Telephone { get; set; }
		public string Cellphone { get; set; }
		public string City { get; set; }

		//FullAddress contains City and Pos Address
		public string FullAddress { get; set; }
		public string Model { get; set; }
		public string Brand { get; set; }
		public string ConnectionType { get; set; }
		public string MorningProgram { get; set; }
		public string AfternoonProgram { get; set; }
		public List<string> DaysClosed { get; set; }
		public string InsertDate { get; set; }
	}
}
