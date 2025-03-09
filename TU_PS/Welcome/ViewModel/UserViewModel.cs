using Welcome.Model;
using Welcome.Others;

namespace Welcome.ViewModel
{
    public class UserViewModel
    {
        private User _user { get; set; }

        public string Name
        {
            get => _user.Name; 
            set
            {

                _user.Name = value;
            }
        }
        public string Password
        {
            get => _user.Password;
            
            set
            {

                _user.Password = value;
            }
        }


        public UserRolesEnum UserRolesEnum
        {
            get => _user.UserRolesEnum;

            set
            {

                _user.UserRolesEnum = value;
            }
        }

        public UserViewModel(User user)
        {
            this._user = user;
        }
    }
}
