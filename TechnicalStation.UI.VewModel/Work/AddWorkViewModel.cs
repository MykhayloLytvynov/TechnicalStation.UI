using Common.UI.Utility.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
    public class AddWorkViewModel : ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        private IFrontServiceClient frontServiceClient;

        protected RelayCommandAsync addWorkContentCommand;
        protected RelayCommandAsync cancelCommand;

        public string Mode
        {
            get
            {
                return (string)this.GetUIValue(ModeProperty);
            }
            set
            {
                SetUIValue(ModeProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register(
                "Mode",
                typeof(string),
                typeof(AddWorkViewModel),
                new PropertyMetadata(null));


        #region WorkViewModel


        public WorkViewModel WorkViewModel
        {
            get
            {
                return (WorkViewModel)this.GetUIValue(WorkViewModelProperty);
            }
            set
            {
                SetUIValue(WorkViewModelProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty WorkViewModelProperty =
            DependencyProperty.Register(
                "WorkViewModel",
                typeof(WorkViewModel),
                typeof(AddWorkViewModel),
                new PropertyMetadata(null));

        #endregion

        public AddWorkViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;


        }

        public ICommand AddWorkContentCommand
        {
            get
            {
                if (this.addWorkContentCommand == null)
                {
                    this.addWorkContentCommand = new RelayCommandAsync(() => this.AddContentOfWork());
                }
                return this.addWorkContentCommand;
            }
        }

        protected RelayCommandAsync cancelWorkCommand;

        public ICommand CancelWorkCommand
        {
            get
            {
                if (this.cancelWorkCommand == null)
                {
                    this.cancelWorkCommand = new RelayCommandAsync(() => this.CancelAsync());
                }
                return this.cancelWorkCommand;
            }
        }

        public void Load(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection)
        {
            this.Mode = "Edit";
            this.WorkViewModel = new WorkViewModel(workInfo, workerInfoCollection);
        }

        protected virtual async Task CancelAsync()
        {

            this.mainWindowController.LoadContentOrderControl();
        }

        public virtual async Task AddContentOfWork()
        {
            var workInfo = this.WorkViewModel.Extract();
            WorkInfo workInfoResult;

            if (workInfo.Id == 0)
            {
                workInfoResult = await frontServiceClient.AddWorkInfoAsync(workInfo);
            }
            else
            {
                workInfoResult = await frontServiceClient.UpdateWorkInfoAsync(workInfo);
            }

            this.WorkViewModel.Transform(workInfoResult);

            this.mainWindowController.LoadContentWorkCollectionControl();
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }

        public void LoadWorkViewModel(int orderId)
        {
            this.Mode = "Add";

            List<WorkerInfo> workerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetWorkerInfoCollectionAsync()).Result;

            this.WorkViewModel = new WorkViewModel(new WorkInfo(), workerInfoCollection);
            this.WorkViewModel.OrderId = orderId;
        }
    }
}
