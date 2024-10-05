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

        public LoginViewModel()
        {
            SendOtpCommand = new Command(async () => await SendOTPAsync());
            VerifyOtpCommand = new Command(async () => await VerifyOTPAsync());
            ResendOtpCommand = new Command(async () => await ResendOtpAsync());
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
            // Add your OTP sending logic here (e.g., using Twilio API)
            // Simulate OTP sending process
            IsPhoneEntryVisible = false;
            IsOtpVerificationVisible = true;
            IsTimerVisible = true;

            await StartOtpCountdown();
        }

        // Method to verify OTP
        private async Task VerifyOTPAsync()
        {
            PageOpacity = 0.5;
            IsBusy = true;  // Start the loading indicator

            // Simulate OTP verification delay
            await Task.Delay(2000);  // Replace this with actual OTP verification logic
            // Add your OTP verification logic here
            if (OTP == "123456") // Replace with actual OTP check
            {
                Application.Current.MainPage = new AppShell();
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
