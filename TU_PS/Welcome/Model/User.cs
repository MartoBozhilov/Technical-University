using Welcome.Others;

namespace Welcome.Model
{
    public class User
    {
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserRolesEnum UserRolesEnum { get; set; } 
    }
}
