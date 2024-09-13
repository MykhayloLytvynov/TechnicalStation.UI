using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
    public class WorkCollectionViewModel : ElementViewModelBase/*BindableBase*/
    {
        WorkInfo workInfo;
        private int id;

        public WorkCollectionViewModel(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection)
        {
            try
            {
                this.Load(workInfo, workerInfoCollection);
            }
            catch (Exception ex)
            {
                throw ex;
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

        public void Load(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection)
        {
            this.WorkerViewModelCollection = new ObservableCollection<WorkerViewModel>();

            foreach (var workerInfo in workerInfoCollection)
            {
                this.WorkerViewModelCollection.Add(new WorkerViewModel(workerInfo));
            }

            this.workInfo = workInfo;

            this.Transform(this.workInfo);
        }

        //public int Id
        //{
        //    get { return id; }
        //    set { this.SetProperty<int>(ref this.id, value); }
        //}

        //private int orderId;
        //public int OrderId
        //{
        //    get { return orderId; }
        //    set { this.SetProperty<int>(ref this.orderId, value); }
        //}

        //private double cost;
        //public double Cost
        //{
        //    get { return cost; }
        //    set { this.SetProperty<double>(ref this.cost, value); }

        //}
        //private string description;
        //public string Description
        //{
        //    get { return description; }
        //    set { this.SetProperty<string>(ref this.description, value); }
        //}

        //public int WorkerId
        //{
        //    get
        //    {
        //        return (int)this.GetUIValue(WorkerIdProperty);
        //    }
        //    set
        //    {
        //        SetUIValue(WorkerIdProperty, value);
        //    }
        //}

        ///// <summary>
        ///// The Publish time property.
        ///// </summary>
        //public static readonly DependencyProperty WorkerIdProperty =
        //    DependencyProperty.Register(
        //    "WorkerId",
        //    typeof(int),
        //    typeof(WorkCollectionViewModel),
        //    new PropertyMetadata(null));

        public void Transform(WorkInfo workInfo)
        {
            //this.Id = workInfo.Id;
            //this.OrderId = workInfo.OrderId;
            //this.Cost = workInfo.Cost;
            //this.WorkerId = workInfo.WorkerId;
            //this.Description = workInfo.Description;

            workInfo.CopyProperties(this);
        }

        public WorkInfo Extract()
        {
            //workInfo.Id = this.Id;
            //workInfo.OrderId = this.OrderId;
            //workInfo.Cost = this.Cost;
            //workInfo.Description = this.Description;

            this.CopyProperties(workInfo);

            return this.workInfo;
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
