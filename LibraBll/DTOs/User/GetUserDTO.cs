using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.DTOs.User
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Telephone { get; set; }
        public int UserTypeId { get; set; }
    }
}