using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.ViewModel 
{

public class WorkerViewModel : ElementViewModelBase
{
	WorkerInfo workerInfo;
	public static readonly DependencyProperty IdProperty =
	DependencyProperty.Register("Id", typeof(int),
	typeof(WorkerViewModel), new PropertyMetadata(null));

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

	public static readonly DependencyProperty FirstNameProperty =
	DependencyProperty.Register("FirstName", typeof(string),
	typeof(WorkerViewModel), new PropertyMetadata(null));

	public string FirstName
	{
	    get
	    {
	        return (string)this.GetUIValue(FirstNameProperty);
	    }
	    set
	    {
	        SetUIValue(FirstNameProperty, value);
	    }
	}

	public static readonly DependencyProperty LastNameProperty =
	DependencyProperty.Register("LastName", typeof(string),
	typeof(WorkerViewModel), new PropertyMetadata(null));

	public string LastName
	{
	    get
	    {
	        return (string)this.GetUIValue(LastNameProperty);
	    }
	    set
	    {
	        SetUIValue(LastNameProperty, value);
	    }
	}

	public static readonly DependencyProperty AddressProperty =
	DependencyProperty.Register("Address", typeof(string),
	typeof(WorkerViewModel), new PropertyMetadata(null));

	public string Address
	{
	    get
	    {
	        return (string)this.GetUIValue(AddressProperty);
	    }
	    set
	    {
	        SetUIValue(AddressProperty, value);
	    }
	}

	public static readonly DependencyProperty PhoneNumberProperty =
	DependencyProperty.Register("PhoneNumber", typeof(string),
	typeof(WorkerViewModel), new PropertyMetadata(null));

	public string PhoneNumber
	{
	    get
	    {
	        return (string)this.GetUIValue(PhoneNumberProperty);
	    }
	    set
	    {
	        SetUIValue(PhoneNumberProperty, value);
	    }
	}

	public static readonly DependencyProperty NotesProperty =
	DependencyProperty.Register("Notes", typeof(string),
	typeof(WorkerViewModel), new PropertyMetadata(null));

	public string Notes
	{
	    get
	    {
	        return (string)this.GetUIValue(NotesProperty);
	    }
	    set
	    {
	        SetUIValue(NotesProperty, value);
	    }
	}

	public static readonly DependencyProperty ModifyTimeProperty =
	DependencyProperty.Register("ModifyTime", typeof(DateTime),
	typeof(WorkerViewModel), new PropertyMetadata(null));

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





	public WorkerViewModel(WorkerInfo workerInfo)
	{
		try
		{
			this.Load(workerInfo);
		}
		catch(Exception ex)
		{
			throw ex;
		}

	}

	public void Load(WorkerInfo workerInfo)
	{



		this.workerInfo = workerInfo;
		this.Transform(this.workerInfo);
	}

	public void Transform(WorkerInfo workerInfo)
	{
		workerInfo.CopyProperties(this);
	}

	public WorkerInfo Extract()
	{
		this.CopyProperties(workerInfo);

		return this.workerInfo;
	}

	protected override string GetValidationError(string property)
	{
		return string.Empty;
	}
}		
}
