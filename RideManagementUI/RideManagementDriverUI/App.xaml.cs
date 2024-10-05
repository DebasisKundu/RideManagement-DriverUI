using RideManagementUI.Views;

namespace RideManagementUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }
    }
}
