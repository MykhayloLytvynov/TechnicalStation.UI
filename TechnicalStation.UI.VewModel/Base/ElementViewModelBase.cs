using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.UI.Contract;

namespace TechnicalStation.UI.ViewModel.Base
{
    public abstract class ElementViewModelBase : ViewModelBase
    {
        protected IView view;

        public IView View
        {
            get { return view; }
            set { view = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T member, T value, string propertyName = null)
        {
            member = value;
            this.RaiseNotification(propertyName);
        }

        protected void RaiseNotification(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
