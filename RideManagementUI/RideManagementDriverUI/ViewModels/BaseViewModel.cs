using RideManagementUI.Infrastructures;

namespace RideManagementUI.ViewModels
{
    public class BaseViewModel : BaseObservable
    {
        private Visibility _loaderVisibility;

        private ContentView _currentControl;

        private Visibility _gridVisibility;
        private Visibility _formVisibility;

        public BaseViewModel()
        {
            LoaderVisibility = Visibility.Collapsed;
        }


        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged(nameof(LoaderVisibility));
            }
        }


        public ContentView CurrentControl
        {
            get => _currentControl;
            set
            {
                _currentControl = value;
                OnPropertyChanged(nameof(CurrentControl));
            }
        }

        public Visibility GridVisibility
        {
            get
            {
                return _gridVisibility;
            }
            set
            {
                _gridVisibility = value;
                OnPropertyChanged(nameof(GridVisibility));
            }
        }

        public Visibility FormVisibility
        {
            get
            {
                return _formVisibility;
            }
            set
            {
                _formVisibility = value;
                OnPropertyChanged(nameof(FormVisibility));
            }
        }

        public virtual Task InitializeAsync(object? parameter) => Task.FromResult(false);
    }
}
