using RideManagementDriverUI.AppPermissions;
using RideManagementDriverUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RideManagementDriverUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // Command to send OTP
        public ICommand SendOtpCommand { get; }
        public ICommand VerifyOtpCommand { get; }
        public ICommand ResendOtpCommand { get; }

        private double _pageOpacity = 1;

        private string _phoneNumber;
        private string _otp;

        private bool _isPhoneEntryVisible = true;
        private bool _isOtpVerificationVisible = false;
        private string _timerText = "Resend OTP in 25 seconds";
        private bool _isResendOtpButtonVisible = false;
        private bool _isTimerVisible = false;
        private int countdownTime = 25;

        private bool _isBusy;

        private string _otpDigit1;
        private string _otpDigit2;
        private string _otpDigit3;
        private string _otpDigit4;
        private string _otpDigit5;
        private string _otpDigit6;

        public LoginViewModel()
        {
            SendOtpCommand = new Command(async () => await SendOTPAsync());
            VerifyOtpCommand = new Command(async () => await VerifyOTPAsync());
            ResendOtpCommand = new Command(async () => await ResendOtpAsync());
        }

        public string OtpDigit1
        {
            get => _otpDigit1;
            set => SetProperty(ref _otpDigit1, value);
        }

        public string OtpDigit2
        {
            get => _otpDigit2;
            set => SetProperty(ref _otpDigit2, value);
        }

        public string OtpDigit3
        {
            get => _otpDigit3;
            set => SetProperty(ref _otpDigit3, value);
        }

        public string OtpDigit4
        {
            get => _otpDigit4;
            set => SetProperty(ref _otpDigit4, value);
        }

        public string OtpDigit5
        {
            get => _otpDigit5;
            set => SetProperty(ref _otpDigit5, value);
        }

        public string OtpDigit6
        {
            get => _otpDigit6;
            set => SetProperty(ref _otpDigit6, value);
        }

        public double PageOpacity
        {
            get => _pageOpacity;
            set
            {
                _pageOpacity = value;
                OnPropertyChanged(nameof(PageOpacity));
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string OTP
        {
            get => _otp;
            set
            {
                _otp = value;
                OnPropertyChanged(nameof(OTP));
            }
        }

        public bool IsPhoneEntryVisible
        {
            get => _isPhoneEntryVisible;
            set
            {
                _isPhoneEntryVisible = value;
                OnPropertyChanged(nameof(IsPhoneEntryVisible));
            }
        }

        public bool IsOtpVerificationVisible
        {
            get => _isOtpVerificationVisible;
            set
            {
                _isOtpVerificationVisible = value;
                OnPropertyChanged(nameof(IsOtpVerificationVisible));
            }
        }

        public string TimerText
        {
            get => _timerText;
            set
            {
                _timerText = value;
                OnPropertyChanged(nameof(TimerText));
            }
        }

        public bool IsResendOtpButtonVisible
        {
            get => _isResendOtpButtonVisible;
            set
            {
                _isResendOtpButtonVisible = value;
                OnPropertyChanged(nameof(IsResendOtpButtonVisible));
            }
        }

        public bool IsTimerVisible
        {
            get => _isTimerVisible;
            set
            {
                _isTimerVisible = value;
                OnPropertyChanged(nameof(IsTimerVisible));
            }
        }

        // Method to send OTP
        private async Task SendOTPAsync()
        {
            IsPhoneEntryVisible = false;
            IsOtpVerificationVisible = true;
            IsTimerVisible = true;

            // Add your OTP sending logic here (e.g., using Twilio API)
            // Simulate OTP sending process
            OTP = "123456";

            await StartOtpCountdown();
        }

        // Method to verify OTP
        private async Task VerifyOTPAsync()
        {
            PageOpacity = 0.5;
            IsBusy = true;  // Start the loading indicator
            var CompleteOtp = $"{OtpDigit1}{OtpDigit2}{OtpDigit3}{OtpDigit4}{OtpDigit5}{OtpDigit6}";

            // Simulate OTP verification delay
            await Task.Delay(2000);  // Replace this with actual OTP verification logic
            // Add your OTP verification logic here
            if (OTP == CompleteOtp) // Replace with actual OTP check
            {
                if (await LocationPermission.CheckAndRequestLocationPermission())
                {
                    if (!await LocationPermission.CheckAndConfirmCurrentLocation())
                        Application.Current.MainPage = new LocationDetectionView();
                    else
                        Application.Current.MainPage = new AppShell();

                }
                else
                {
                    Application.Current.MainPage = new LocationPermissionView();
                }

                // Navigate to next page after successful OTP verification
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid OTP", "OK");
            }

            IsBusy = false;  // Stop the loading indicator
            PageOpacity = 1;
        }

        private async Task ResendOtpAsync()
        {
            countdownTime = 25;
            IsResendOtpButtonVisible = false;
            IsTimerVisible = true;
            TimerText = $"Resend OTP in {countdownTime} seconds";
            await StartOtpCountdown();
        }

        private async Task StartOtpCountdown()
        {
            for (int i = countdownTime; i >= 0; i--)
            {
                TimerText = $"Resend OTP in {i} seconds";
                await Task.Delay(1000);
            }

            IsResendOtpButtonVisible = true;
            IsTimerVisible = false;
        }
    }
}
