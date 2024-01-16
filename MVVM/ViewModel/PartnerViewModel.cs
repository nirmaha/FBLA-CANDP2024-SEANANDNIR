using System.Collections.ObjectModel;
using System.ComponentModel;

using EduPartners.MVVM.Model;

namespace EduPartners.MVVM.ViewModel
{
    public class PartnerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Partner> _items;

        public ObservableCollection<Partner> Items
        {
            get { return _items; }
            set 
            { 
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public PartnerViewModel()
        {
            _items = new ObservableCollection<Partner>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
