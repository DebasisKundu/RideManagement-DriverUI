using RideManagementDriverUI.Views;

namespace RideManagementDriverUI
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
