using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiLesson_7_ManagerPassword
{
    public class PasswordModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayPassword));
                    OnPropertyChanged(nameof(ToggleIcon));
                }
            }
        }

        public string DisplayPassword => IsVisible ? Password : new string('*', Password?.Length ?? 0);

        public string ToggleIcon => IsVisible ? "👁" : "◡";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}