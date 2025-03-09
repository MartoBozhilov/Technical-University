using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;

namespace Welcome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var user = new User()
            {
                Name = "Ivan"
                ,Password = "1234"
                ,UserRolesEnum = Others.UserRolesEnum.ADMIN
            };

            var userVM = new UserViewModel(user);

            var userV = new UserView(userVM);

            userV.Display();

            Console.ReadKey();
        }
    }
}
