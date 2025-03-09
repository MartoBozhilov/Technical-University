using Welcome.Model;
using Welcome.Others;
using Welcome.View;
using Welcome.ViewModel;
using WelcomeExtended.Others;

namespace WelcomeExtended
{
    internal class Program
    {
        static void Main(string[] args)
        {
			try
			{
                User user = new User()
                {
                    Name = "Ivan"
    ,
                    Password = "1234"
    ,
                    UserRolesEnum = UserRolesEnum.ADMIN
                };

                var userVM = new UserViewModel(user);

                var userV = new UserView(userVM);

                userV.Display();

                userV.DisplayError();

            }
			catch (Exception e)
			{
                var log = new ActionOnError(Delegates.Log);
                log(e.Message);
			}
			finally
			{
                Console.WriteLine("Executed in any case!");
			}
        }
    }
}
