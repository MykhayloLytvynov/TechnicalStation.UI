using Common.UI.Utility.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel.Worker
{
    public class AddWorkerViewModel : ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        private IFrontServiceClient frontServiceClient;
        protected RelayCommandAsync addWorkerContentCommand;
        protected RelayCommandAsync cancelCommand;
        //private List<OrderInfo> orderInfoCollection = new List<OrderInfo>();
        public AddWorkerViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;

            List<WorkerInfo> carInfoCollection = Task.Run(async () => await this.frontServiceClient.GetWorkerInfoCollectionAsync()).Result;
            

            this.WorkerViewModel = new WorkerViewModel(new WorkerInfo());
        }

        public ICommand CancelCommand
        {
            get
            {
                if (this.cancelCommand == null)
                {
                    this.cancelCommand = new RelayCommandAsync(() => this.CancelAsync());
                }

                return this.cancelCommand;
            }
        }

        protected virtual async Task CancelAsync()
        {

            this.mainWindowController.LoadWorkerEditorControl();
        }

        public ICommand AddWorkerCommand
        {
            get
            {
                if (this.addWorkerContentCommand == null)
                {
                    this.addWorkerContentCommand = new RelayCommandAsync(() => this.AddWorkerAsync());
                }
                return this.addWorkerContentCommand;
            }
        }

        #region OrderViewModel

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public WorkerViewModel WorkerViewModel
        {
            get
            {
                return (WorkerViewModel)this.GetUIValue(WorkerViewModelProperty);
            }
            set
            {
                SetUIValue(WorkerViewModelProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty WorkerViewModelProperty =
            DependencyProperty.Register(
                "WorkerViewModel",
                typeof(WorkerViewModel),
                typeof(AddWorkerViewModel),
                new PropertyMetadata(null));

        #endregion

        protected virtual async Task AddWorkerAsync()
        {
            var workerInfo = this.WorkerViewModel.Extract();
            WorkerInfo workerInfoResult;

            if (workerInfo.Id == 0)
            {
                workerInfoResult = await frontServiceClient.AddWorkerInfoAsync(workerInfo);
            }
            else
            {
                workerInfoResult = await frontServiceClient.UpdateWorkerInfoAsync(workerInfo);
            }

            //this.WorkerViewModel.Transform(workerInfoResult);

            this.mainWindowController.LoadContentWorkerCollectionControl(workerInfoResult);
        }

        public void LoadWorkerViewModel(WorkerInfo workerInfo)
        {
            //this.Mode = "Add";

            //List<WorkerInfo> workerInfoCollection = this.frontServiceClient.GetWorkerInfoCollection();

            this.WorkerViewModel = new WorkerViewModel(workerInfo);
            //this.WorkViewModel.OrderId = orderId;
        }

        public void Load(WorkerInfo workerInfo)
        {
            //this.Mode = "Add";

            //List<WorkerInfo> workerInfoCollection = this.frontServiceClient.GetWorkerInfoCollection();

            this.WorkerViewModel = new WorkerViewModel(workerInfo);
            //this.WorkViewModel.OrderId = orderId;
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
