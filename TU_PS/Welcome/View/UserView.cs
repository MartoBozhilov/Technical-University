using Welcome.ViewModel;

namespace Welcome.View
{
    public class UserView
    {
        private UserViewModel _viewModel { get; set; }

        public UserView(UserViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public void Display()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine($"User: {this._viewModel.Name}");
            Console.WriteLine($"Role: {this._viewModel.UserRolesEnum }");
        }

        public void DisplayError()
        {
            throw new NotImplementedException();
        }
    }
}
