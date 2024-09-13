using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TechnicalStation.Service.Client;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Context;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
    public class OrderViewModel : ElementViewModelBase
    {
        private OrderInfo orderInfo;
        int id;

        public OrderViewModel(OrderInfo orderInfo, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection)
        {
            try
            {
                 this.Load(orderInfo, carInfoCollection, customerInfoCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(OrderInfo orderInfo, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection)
        {
            this.CarViewModelCollection = new ObservableCollection<CarViewModel>();
            this.CustomerViewModelCollection = new ObservableCollection<CustomerViewModel>();

            foreach (var customerInfo in customerInfoCollection)
            {
                this.CustomerViewModelCollection.Add(new CustomerViewModel(customerInfo));
            }

            foreach (var carInfo in carInfoCollection)
            {
                this.CarViewModelCollection.Add(new CarViewModel(carInfo));
            }

            this.orderInfo = orderInfo;
            this.Transform(this.orderInfo);            
            

        }


        #region End_date


        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public DateTime End_date
        {
            get
            {
                return (DateTime)this.GetUIValue(End_dateProperty);
            }
            set
            {
                SetUIValue(End_dateProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty End_dateProperty =
            DependencyProperty.Register(
            "End_date",
            typeof(DateTime),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion

        #region StartDate

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public DateTime Start_date
        {
            get
            {
                return (DateTime)this.GetUIValue(Start_dateProperty);
            }
            set
            {
                SetUIValue(Start_dateProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty Start_dateProperty =
            DependencyProperty.Register(
            "Start_date",
            typeof(DateTime),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion
        #region Car_produser

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public string Car_produser
        {
            get
            {
                return (string)this.GetUIValue(Car_produserProperty);
            }
            set
            {
                SetUIValue(Car_produserProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty Car_produserProperty =
            DependencyProperty.Register(
            "Car_produser",
            typeof(string),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion
        #region Car_model

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public string Car_model
        {
            get
            {
                return (string)this.GetUIValue(Car_modelProperty);
            }
            set
            {
                SetUIValue(Car_modelProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty Car_modelProperty =
            DependencyProperty.Register(
            "Car_model",
            typeof(string),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion
        #region Car_year_of_creation

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public DateTime Car_year_of_creation
        {
            get
            {
                return (DateTime)this.GetUIValue(Car_year_of_creationProperty);
            }
            set
            {
                SetUIValue(Car_year_of_creationProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty Car_year_of_creationProperty =
            DependencyProperty.Register(
            "Car_year_of_creation",
            typeof(DateTime),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion
        #region Firstname_of_client

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public string Firstname_of_client
        {
            get
            {
                return (string)this.GetUIValue(Firstname_of_clientProperty);
            }
            set
            {
                SetUIValue(Firstname_of_clientProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty Firstname_of_clientProperty =
            DependencyProperty.Register(
            "Firstname_of_client",
            typeof(string),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion
        #region PublishTime

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public string Secondname_of_client
        {
            get
            {
                return (string)this.GetUIValue(Secondname_of_clientProperty);
            }
            set
            {
                SetUIValue(Secondname_of_clientProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty Secondname_of_clientProperty =
            DependencyProperty.Register(
            "Secondname_of_client",
            typeof(string),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion

        #region PublishTime

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public string Patronymic_of_client
        {
            get
            {
                return (string)this.GetUIValue(Patronymic_of_clientProperty);
            }
            set
            {
                SetUIValue(Patronymic_of_clientProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty Patronymic_of_clientProperty =
            DependencyProperty.Register(
            "Patronymic_of_client",
            typeof(string),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        #endregion

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
            typeof(OrderViewModel),
            new PropertyMetadata(null));


        public int CustomerId
        {
            get
            {
                return (int)this.GetUIValue(CustomerIdProperty);
            }
            set
            {
                SetUIValue(CustomerIdProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty CustomerIdProperty =
            DependencyProperty.Register(
            "CustomerId",
            typeof(int),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        public int CarId
        {
            get
            {
                return (int)this.GetUIValue(CarIdProperty);
            }
            set
            {
                SetUIValue(CarIdProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty CarIdProperty =
            DependencyProperty.Register(
            "CarId",
            typeof(int),
            typeof(OrderViewModel),
            new PropertyMetadata(null));

        private ObservableCollection<CustomerViewModel> customerViewModelCollection = new ObservableCollection<CustomerViewModel>();
       
        public ObservableCollection<CustomerViewModel> CustomerViewModelCollection
        {
            get
            {
                return this.customerViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<CustomerViewModel>>(ref this.customerViewModelCollection, value); }
        }

        private ObservableCollection<CarViewModel> carViewModelCollection = new ObservableCollection<CarViewModel>();
        public ObservableCollection<CarViewModel> CarViewModelCollection
        {
            get
            {
                return this.carViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<CarViewModel>>(ref this.carViewModelCollection, value); }
        }

        public void Transform(OrderInfo orderInfo)
        {
            orderInfo.CopyProperties(this);
        }

        public OrderInfo Extract()
        {
            this.CopyProperties(orderInfo);

            return this.orderInfo;
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
