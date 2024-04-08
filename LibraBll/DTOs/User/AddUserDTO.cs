using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.DTOs.User
{ 
	public class AddUserDTO
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Telephone { get; set; }
		public string Role { get; set; }
	}
}
