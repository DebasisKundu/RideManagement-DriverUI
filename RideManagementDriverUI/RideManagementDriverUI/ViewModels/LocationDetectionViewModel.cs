using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RideManagementDriverUI.ViewModels
{
    public class LocationDetectionViewModel : BaseViewModel
    {
        private string _cityName;
        private string _mapImageSource;
        private bool _isBusy;

        public ICommand ConfirmLocationCommand { get; }

        public LocationDetectionViewModel()
        {
            ConfirmLocationCommand = new Command(async () => await ConfirmLocationAsync());

            DetectLocationAsync();
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                _cityName = value;
                OnPropertyChanged(nameof(CityName)); // Notify property changed
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public string MapImageSource
        {
            get => _mapImageSource;
            set
            {
                _mapImageSource = value;
                OnPropertyChanged(nameof(MapImageSource));
            }
        }

        private async Task DetectLocationAsync()
        {
            IsBusy = true; // Show ActivityIndicator
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    string apiKey = "AIzaSyAr8N6ndXAA0W2NDf9lUgu5HXN5Cd1zxIA";
                    string latitude = location.Latitude.ToString();
                    string longitude = location.Longitude.ToString();

                    // Google Maps Static Image URL
                    MapImageSource = $"https://maps.googleapis.com/maps/api/staticmap?center={latitude},{longitude}&zoom=15&size=600x300&markers=color:red%7C{latitude},{longitude}&key={apiKey}";

                    // Get the city name using reverse geocoding
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        CityName = $"{placemark.SubLocality}, {placemark.Locality}"; // Set the city name                        
                    }
                    else
                    {
                        CityName = "Unknown Location";
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Location Error",
                        "Unable to detect location. Please try again.", "OK");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Application.Current.MainPage.DisplayAlert("Feature Not Supported", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await Application.Current.MainPage.DisplayAlert("Permission Error", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false; // Hide ActivityIndicator
            }
        }

        private async Task ConfirmLocationAsync()
        {
            // Logic to handle location confirmation
            await Application.Current.MainPage.DisplayAlert("Location Confirmed",
                $"You are located in {CityName}.", "OK");

            // Navigate to the next page after confirmation
            await Shell.Current.GoToAsync("//NextPage");
        }

    }
}
