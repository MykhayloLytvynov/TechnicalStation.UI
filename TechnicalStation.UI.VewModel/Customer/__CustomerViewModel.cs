using System;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
    public class CustomerViewModel : ElementViewModelBase
    {
        private CustomerInfo customerInfo;
        public int Id
        {
            get
            {
                return (int)this.GetUIValue(idProperty);
            }
            set
            {
                SetUIValue(idProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty idProperty =
            DependencyProperty.Register(
            "Id",
            typeof(int),
            typeof(CustomerViewModel),
            new PropertyMetadata(null));

        public string Name
        {
            get
            {
                return (string)this.GetUIValue(NameProperty);
            }
            set
            {
                SetUIValue(NameProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(
            "Name",
            typeof(string),
            typeof(CustomerViewModel),
            new PropertyMetadata(null));


        public CustomerViewModel(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public CustomerViewModel(CustomerInfo customerInfo)
        {
            try
            {
                this.Load(customerInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(CustomerInfo customerInfo)
        {
            this.customerInfo = customerInfo;

            this.Transform(this.customerInfo);
        }

        public CustomerInfo Extract()
        {
            customerInfo.Id = this.Id;
            customerInfo.Name = this.Name;


            return this.customerInfo;
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }

        private void Transform(CustomerInfo customerInfo)
        {
            customerInfo.CopyProperties(this);
        }
    }
}
