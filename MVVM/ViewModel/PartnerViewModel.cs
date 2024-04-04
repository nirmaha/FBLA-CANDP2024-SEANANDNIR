using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Caliburn.Micro;
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

        public void RemovePartner(Partner partner)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == partner.Id)
                {
                    _items.RemoveAt(i);
                    break;
                }
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
