using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TechnicalStation.UI.VewModel.Base
{
    public class BindableBase : DependencyObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private delegate object GetValueDelegate(DependencyProperty property);

        private delegate void SetValueDelegate(DependencyProperty property, object value);

        protected object GetUIValue(DependencyProperty property)
        {
            return Dispatcher.Invoke(new GetValueDelegate(GetValue), property);
        }


        protected void SetUIValue(DependencyProperty property, object value)
        {
            Dispatcher.Invoke(new SetValueDelegate(SetValue), property, value);
        }
        protected void SetProperty<T>(ref T member, T value, string propertyName = null)
        {
            member = value;
            this.RaiseNotification(propertyName);
        }

        public void RaiseNotification(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
