using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.ViewModel 
{

public class OrderViewModel : ElementViewModelBase
{
	OrderInfo orderInfo;
	public static readonly DependencyProperty IdProperty =
	DependencyProperty.Register("Id", typeof(int),
	typeof(OrderViewModel), new PropertyMetadata(null));

	public int Id
	{
	    get
	    {
	        return (int)this.GetUIValue(IdProperty);
	    }
	    set
	    {
	        SetUIValue(IdProperty, value);
	    }
	}

	public static readonly DependencyProperty CustomerIdProperty =
	DependencyProperty.Register("CustomerId", typeof(int),
	typeof(OrderViewModel), new PropertyMetadata(null));

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

	public static readonly DependencyProperty CarIdProperty =
	DependencyProperty.Register("CarId", typeof(int),
	typeof(OrderViewModel), new PropertyMetadata(null));

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

	public static readonly DependencyProperty StartDateProperty =
	DependencyProperty.Register("StartDate", typeof(DateTime),
	typeof(OrderViewModel), new PropertyMetadata(null));

	public DateTime StartDate
	{
	    get
	    {
	        return (DateTime)this.GetUIValue(StartDateProperty);
	    }
	    set
	    {
	        SetUIValue(StartDateProperty, value);
	    }
	}

	public static readonly DependencyProperty FinishDateProperty =
	DependencyProperty.Register("FinishDate", typeof(DateTime),
	typeof(OrderViewModel), new PropertyMetadata(null));

	public DateTime FinishDate
	{
	    get
	    {
	        return (DateTime)this.GetUIValue(FinishDateProperty);
	    }
	    set
	    {
	        SetUIValue(FinishDateProperty, value);
	    }
	}

	public static readonly DependencyProperty ModifyTimeProperty =
	DependencyProperty.Register("ModifyTime", typeof(DateTime),
	typeof(OrderViewModel), new PropertyMetadata(null));

	public DateTime ModifyTime
	{
	    get
	    {
	        return (DateTime)this.GetUIValue(ModifyTimeProperty);
	    }
	    set
	    {
	        SetUIValue(ModifyTimeProperty, value);
	    }
	}

		public static readonly DependencyProperty CustomerViewModelCollectionProperty =
		DependencyProperty.Register("CustomerViewModelCollection", typeof(ObservableCollection<CustomerViewModel>),
		typeof(OrderViewModel), new PropertyMetadata(null));

        public ObservableCollection<CustomerViewModel> CustomerViewModelCollection
		{
            get
            {
                return (ObservableCollection<CustomerViewModel>)this.GetUIValue(CustomerViewModelCollectionProperty);
            }
            set
            {
                SetUIValue(CustomerViewModelCollectionProperty, value);
            }
        }


	//	private ObservableCollection<CustomerViewModel> customerViewModelCollection = new ObservableCollection<CustomerViewModel>();
	//public ObservableCollection<CustomerViewModel> CustomerViewModelCollection
	//{
	//    get
	//    {
	//       return this.customerViewModelCollection;
	//    }
	//    set { this.SetProperty<ObservableCollection<CustomerViewModel>>(ref this.customerViewModelCollection, value); }
	//}
        

	private ObservableCollection<CarViewModel> carViewModelCollection = new ObservableCollection<CarViewModel>();
	public ObservableCollection<CarViewModel> CarViewModelCollection
	{
	    get
	    {
	       return this.carViewModelCollection;
	    }
	    set { this.SetProperty<ObservableCollection<CarViewModel>>(ref this.carViewModelCollection, value); }
	}
        


	public OrderViewModel(OrderInfo orderInfo, List<CustomerInfo> customerInfoCollection, List<CarInfo> carInfoCollection)
	{
		try
		{
            this.CustomerViewModelCollection = new ObservableCollection<CustomerViewModel>();
				this.Load(orderInfo, customerInfoCollection, carInfoCollection);
		}
		catch(Exception ex)
		{
			throw ex;
		}

	}

	public void Load(OrderInfo orderInfo, List<CustomerInfo> customerInfoCollection, List<CarInfo> carInfoCollection)
	{
        this.CarViewModelCollection = new ObservableCollection<CarViewModel>();
        this.CustomerViewModelCollection.Clear();

        foreach (var customerInfo in customerInfoCollection)
		{
		    this.CustomerViewModelCollection.Add(new CustomerViewModel(customerInfo));
		}

		if (customerInfoCollection.Count > 0)
		{
		    this.CustomerId = customerInfoCollection[0].Id;
		}
            
            
		foreach (var carInfo in carInfoCollection)
		{
		    this.CarViewModelCollection.Add(new CarViewModel(carInfo));
		}

		if (carInfoCollection.Count > 0)
		{
		    this.CarId = carInfoCollection[0].Id;
		}
            


		this.orderInfo = orderInfo;
		this.Transform(this.orderInfo);
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
