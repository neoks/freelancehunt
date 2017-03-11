using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Freelancehunt_Messenger.Models.based
{
    public class SelectedItem : NotifyPropertyChanged
    {
        private bool isSelectedItem;

        public bool IsSelectedItem
        {
            get
            {
                return isSelectedItem;
            }
            set
            {
                isSelectedItem = value;
                OnPropertyChanged();
            }
        }
    }



    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
