using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

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

        /// <summary>
        /// This function removes the <paramref name="partner"/> for the ObservableCollection.
        /// </summary>
        /// <param name="partner">The partner to remove.</param>
        public void RemovePartner(Partner partner)
        {
            // Loops through the collection and deletes the selected partner.
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

        /// <summary>
        /// This function triggers a property changed event.
        /// </summary>
        /// <param name="propertyName">The name of the property to notify.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
