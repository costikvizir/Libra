using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.DTOs
{
	public class LoginUserDTO
	{
		[Required(ErrorMessage = "Missing User Name")]
		public string UserName { get; set;}

		[Required(ErrorMessage = "Missing Password")]
		[DataType(DataType.Password)]
		public string Password { get; set;}
	}
}
