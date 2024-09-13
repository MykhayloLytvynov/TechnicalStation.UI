using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Control;
using TechnicalStation.UI.Control.Worker;

namespace TechnicalStation.UI.Shell
{
    public partial class MainWindowController
    {
        public void LoadContentWorkerCollectionControl(WorkerInfo workerInfo)
        {
            WorkerEditorControl editControl = this.controlManager.GetControl("WorkerEditorControl") as WorkerEditorControl;

            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                                    editControl.editorViewModel.AddWorker(workerInfo);

                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "WorkerEditorControl");
        }

        public void LoadContentWorkerCollectionControl(List<WorkerInfo> workerInfoCollection)
        {
            WorkerEditorControl editControl = this.controlManager.GetControl("WorkerEditorControl") as WorkerEditorControl;

            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                        editControl.editorViewModel.Load(workerInfoCollection);
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "WorkerEditorControl");
        }

        public void LoadAddWorkerControl(WorkerInfo workerInfo)
        {
            AddWorkerControl addWorkerCollectionControl = this.controlManager.GetControl("AddWorkerControl") as AddWorkerControl;
            //addOrderControl.SetFocus();
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    addWorkerCollectionControl.addWorkerViewModel.LoadWorkerViewModel(workerInfo);
                }));
            this.controlManager.Place("DashboardControl", "EditControlRegion", "AddWorkerControl");
        }

        public void LoadUpdateWorkerControl(WorkerInfo workerInfo)
        {
            AddWorkerControl addWorkerCollectionControl = this.controlManager.GetControl("AddWorkerControl") as AddWorkerControl;
            //addOrderControl.SetFocus();
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    addWorkerCollectionControl.addWorkerViewModel.Load(workerInfo);
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "AddWorkerControl");
        }

        public void LoadWorkerEditorControl()
        {
            this.controlManager.Place("DashboardControl", "EditControlRegion", "WorkerEditorControl");
        }

    }
}
