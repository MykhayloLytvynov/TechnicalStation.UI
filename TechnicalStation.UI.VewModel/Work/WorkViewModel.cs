using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.ViewModel 
{

public class WorkViewModel : ElementViewModelBase
{
	WorkInfo workInfo;
	public static readonly DependencyProperty IdProperty =
	DependencyProperty.Register("Id", typeof(int),
	typeof(WorkViewModel), new PropertyMetadata(null));

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

	public static readonly DependencyProperty OrderIdProperty =
	DependencyProperty.Register("OrderId", typeof(int),
	typeof(WorkViewModel), new PropertyMetadata(null));

	public int OrderId
	{
	    get
	    {
	        return (int)this.GetUIValue(OrderIdProperty);
	    }
	    set
	    {
	        SetUIValue(OrderIdProperty, value);
	    }
	}

	public static readonly DependencyProperty WorkerIdProperty =
	DependencyProperty.Register("WorkerId", typeof(int),
	typeof(WorkViewModel), new PropertyMetadata(null));

	public int WorkerId
	{
	    get
	    {
	        return (int)this.GetUIValue(WorkerIdProperty);
	    }
	    set
	    {
	        SetUIValue(WorkerIdProperty, value);
	    }
	}

	public static readonly DependencyProperty StartDateProperty =
	DependencyProperty.Register("StartDate", typeof(DateTime),
	typeof(WorkViewModel), new PropertyMetadata(null));

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
	typeof(WorkViewModel), new PropertyMetadata(null));

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

	public static readonly DependencyProperty CostProperty =
	DependencyProperty.Register("Cost", typeof(double),
	typeof(WorkViewModel), new PropertyMetadata(null));

	public double Cost
	{
	    get
	    {
	        return (double)this.GetUIValue(CostProperty);
	    }
	    set
	    {
	        SetUIValue(CostProperty, value);
	    }
	}

	public static readonly DependencyProperty SupplyExpensesProperty =
	DependencyProperty.Register("SupplyExpenses", typeof(double),
	typeof(WorkViewModel), new PropertyMetadata(null));

	public double SupplyExpenses
	{
	    get
	    {
	        return (double)this.GetUIValue(SupplyExpensesProperty);
	    }
	    set
	    {
	        SetUIValue(SupplyExpensesProperty, value);
	    }
	}

	public static readonly DependencyProperty WorkExpensesProperty =
	DependencyProperty.Register("WorkExpenses", typeof(double),
	typeof(WorkViewModel), new PropertyMetadata(null));

	public double WorkExpenses
	{
	    get
	    {
	        return (double)this.GetUIValue(WorkExpensesProperty);
	    }
	    set
	    {
	        SetUIValue(WorkExpensesProperty, value);
	    }
	}

	public static readonly DependencyProperty DescriptionProperty =
	DependencyProperty.Register("Description", typeof(string),
	typeof(WorkViewModel), new PropertyMetadata(null));

	public string Description
	{
	    get
	    {
	        return (string)this.GetUIValue(DescriptionProperty);
	    }
	    set
	    {
	        SetUIValue(DescriptionProperty, value);
	    }
	}

	public static readonly DependencyProperty NotesProperty =
	DependencyProperty.Register("Notes", typeof(string),
	typeof(WorkViewModel), new PropertyMetadata(null));

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
	typeof(WorkViewModel), new PropertyMetadata(null));

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




	private ObservableCollection<WorkerViewModel> workerViewModelCollection = new ObservableCollection<WorkerViewModel>();
	public ObservableCollection<WorkerViewModel> WorkerViewModelCollection
	{
	    get
	    {
	       return this.workerViewModelCollection;
	    }
	    set { this.SetProperty<ObservableCollection<WorkerViewModel>>(ref this.workerViewModelCollection, value); }
	}
        


	public WorkViewModel(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection)
	{
		try
		{
			this.Load(workInfo, workerInfoCollection);
		}
		catch(Exception ex)
		{
			throw ex;
		}

	}

	public void Load(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection)
	{
        this.WorkerViewModelCollection = new ObservableCollection<WorkerViewModel>();


		foreach (var workerInfo in workerInfoCollection)
		{
		    this.WorkerViewModelCollection.Add(new WorkerViewModel(workerInfo));
		}

		if (workerInfoCollection.Count > 0)
		{
		    this.WorkerId = workerInfoCollection[0].Id;
		}
            


		this.workInfo = workInfo;
		this.Transform(this.workInfo);
	}

	public void Transform(WorkInfo workInfo)
	{
		workInfo.CopyProperties(this);
	}

	public WorkInfo Extract()
	{
		this.CopyProperties(workInfo);

		return this.workInfo;
	}

	protected override string GetValidationError(string property)
	{
		return string.Empty;
	}
}		
}
