using RideManagementDriverUI.AppPermissions;
using RideManagementDriverUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RideManagementDriverUI.ViewModels
{
    public class LocationPermissionViewModel : BaseViewModel
    {
        public ICommand EnableLocationCommand { get; }

        private bool _isBusy;

        public LocationPermissionViewModel()
        {
            EnableLocationCommand = new Command(async () => await RequestLocationPermission());
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private async Task RequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                // Request permission if not granted
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                IsBusy = true; // Start the loading indicator
                if (status == PermissionStatus.Granted)
                {
                    // Location permission granted, proceed
                    if (!await LocationPermission.CheckAndConfirmCurrentLocation())
                        Application.Current.MainPage = new LocationDetectionView();
                    else
                        Application.Current.MainPage = new AppShell();
                }
                else
                {
                    // Permission denied, check if permanently denied
                    if (status == PermissionStatus.Denied)
                    {
                        // Show an alert to inform the user
                        await Application.Current.MainPage.DisplayAlert("Permission Denied",
                            "Location access is denied. Please enable it from your device settings.",
                            "OK");

                        Application.Current.Quit();
                    }
                    else if (status == PermissionStatus.Restricted)
                    {
                        // Handle restricted status if necessary
                        await Application.Current.MainPage.DisplayAlert("Permission Restricted",
                            "Location access is restricted. Please enable it from your device settings.",
                            "OK");

                        Application.Current.Quit();
                    }
                }

                //IsBusy = false;  // Stop the loading indicator
            }
            else
            {
                // Permission already granted, proceed directly
                if (!await LocationPermission.CheckAndConfirmCurrentLocation())
                    Application.Current.MainPage = new LocationDetectionView();
                else
                    Application.Current.MainPage = new AppShell();
            }
        }
    }
}
