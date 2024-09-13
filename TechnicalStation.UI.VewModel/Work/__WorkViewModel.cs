using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel.Work
{
    public class WorkViewModel : ElementViewModelBase
    {
        private WorkInfo workInfo;

        private ObservableCollection<WorkerViewModel> workerViewModelCollection = new ObservableCollection<WorkerViewModel>();
        public ObservableCollection<WorkerViewModel> WorkerViewModelCollection
        {
            get
            {
                return this.workerViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<WorkerViewModel>>(ref this.workerViewModelCollection, value); }
        }

        /// <summary>
        /// Gets or sets the NickName.
        /// </summary>
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

        /// <summary>
        /// The NickName property.
        /// </summary>
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int),
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

        /// <summary>
        /// The NickName property.
        /// </summary>
        public static readonly DependencyProperty OrderIdProperty =
            DependencyProperty.Register("OrderId", typeof(int),
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

        /// <summary>
        /// The NickName property.
        /// </summary>
        public static readonly DependencyProperty WorkerIdProperty =
            DependencyProperty.Register("WorkerId", typeof(int),
                typeof(WorkViewModel), new PropertyMetadata(null));

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

        public void Load(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection)
        {

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
