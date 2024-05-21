using System.Collections.Generic;

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
        public List<string> Roles { get; set; }
    }
}