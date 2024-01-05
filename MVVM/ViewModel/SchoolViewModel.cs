using System.Collections.ObjectModel;
using System.ComponentModel;

using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.ViewModel
{
    public class SchoolViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<School> _items;

        public ObservableCollection<School> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public SchoolViewModel()
        {
            _items = new ObservableCollection<School>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

