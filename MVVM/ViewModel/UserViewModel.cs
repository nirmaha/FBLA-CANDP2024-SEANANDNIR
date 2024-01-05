using System.Collections.ObjectModel;
using System.ComponentModel;

using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _items;

        public ObservableCollection<User> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public UserViewModel()
        {
            _items = new ObservableCollection<User>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
