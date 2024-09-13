using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
	public class CarViewModel : ElementViewModelBase
	{

		public static readonly DependencyProperty IdProperty =
		DependencyProperty.Register("Id", typeof(int),
		typeof(CarViewModel), new PropertyMetadata(null));

		public string Id
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
		typeof(CarViewModel), new PropertyMetadata(null));

		public string CustomerId
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

		public static readonly DependencyProperty ProducerProperty =
		DependencyProperty.Register("Producer", typeof(string),
		typeof(CarViewModel), new PropertyMetadata(null));

		public string Producer
		{
			get
			{
				return (string)this.GetUIValue(ProducerProperty);
			}
			set
			{
				SetUIValue(ProducerProperty, value);
			}
		}

		public static readonly DependencyProperty ModelProperty =
		DependencyProperty.Register("Model", typeof(string),
		typeof(CarViewModel), new PropertyMetadata(null));

		public string Model
		{
			get
			{
				return (string)this.GetUIValue(ModelProperty);
			}
			set
			{
				SetUIValue(ModelProperty, value);
			}
		}

		public static readonly DependencyProperty ColorProperty =
		DependencyProperty.Register("Color", typeof(string),
		typeof(CarViewModel), new PropertyMetadata(null));

		public string Color
		{
			get
			{
				return (string)this.GetUIValue(ColorProperty);
			}
			set
			{
				SetUIValue(ColorProperty, value);
			}
		}

		public static readonly DependencyProperty NumberProperty =
		DependencyProperty.Register("Number", typeof(string),
		typeof(CarViewModel), new PropertyMetadata(null));

		public string Number
		{
			get
			{
				return (string)this.GetUIValue(NumberProperty);
			}
			set
			{
				SetUIValue(NumberProperty, value);
			}
		}

		public static readonly DependencyProperty YearProperty =
		DependencyProperty.Register("Year", typeof(int),
		typeof(CarViewModel), new PropertyMetadata(null));

		public string Year
		{
			get
			{
				return (int)this.GetUIValue(YearProperty);
			}
			set
			{
				SetUIValue(YearProperty, value);
			}
		}

		public static readonly DependencyProperty ModifyTimeProperty =
		DependencyProperty.Register("ModifyTime", typeof(DateTime),
		typeof(CarViewModel), new PropertyMetadata(null));

		public string ModifyTime
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





		public CarViewModel(CarInfo carInfo)
		{
			try
			{
				this.Load(carInfo);
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public void Load(CarInfo carInfo)
		{
			this.carInfo = carInfo;
			this.Transform(this.carInfo);
		}

		public void Transform(CarInfo carInfo)
		{
			carInfo.CopyProperties(this);
		}

		public CarInfo Extract()
		{
			this.CopyProperties(carInfo);

			return this.carInfo;
		}

		protected override string GetValidationError(string property)
		{
			return string.Empty;
		}
	}

	//public class CarViewModel : ElementViewModelBase
	//{
	//    CarInfo carInfo;
	//    public CarViewModel(CarInfo carInfo)
	//    {
	//        try
	//        {
	//            this.Load(carInfo);
	//        }
	//        catch (Exception ex)
	//        {
	//            throw ex;
	//        }

	//    }
	//    public int Id
	//    {
	//        get
	//        {
	//            return (int)this.GetUIValue(idProperty);
	//        }
	//        set
	//        {
	//            SetUIValue(idProperty, value);
	//        }
	//    }

	//    /// <summary>
	//    /// The Publish time property.
	//    /// </summary>
	//    public static readonly DependencyProperty idProperty =
	//        DependencyProperty.Register(
	//        "Id",
	//        typeof(int),
	//        typeof(CarViewModel),
	//        new PropertyMetadata(null));

	//    public string CarModel
	//    {
	//        get
	//        {
	//            return (string)this.GetUIValue(carModelProperty);
	//        }
	//        set
	//        {
	//            SetUIValue(carModelProperty, value);
	//        }
	//    }

	//    /// <summary>
	//    /// The Publish time property.
	//    /// </summary>
	//    public static readonly DependencyProperty carModelProperty =
	//        DependencyProperty.Register(
	//        "CarModel",
	//        typeof(string),
	//        typeof(CarViewModel),
	//        new PropertyMetadata(null));

	//    public void Load(CarInfo carInfo)
	//    {
	//        this.carInfo = carInfo;

	//        this.Transform(this.carInfo);
	//    }

	//    private void Transform(CarInfo carInfo)
	//    {
	//        this.Id = carInfo.Id;
	//        this.CarModel = carInfo.CarModel;
	//    }

	//    public CarInfo Extract()
	//    {
	//        carInfo.Id = this.Id;
	//        carInfo.CarModel=this.CarModel;


	//        return this.carInfo;
	//    }

	//    public CarViewModel(int id, string carModel)
	//    {
	//        this.Id = id;
	//        this.CarModel = carModel;
	//    }

	//    protected override string GetValidationError(string property)
	//    {
	//        return string.Empty;
	//    }
	//}

}
