using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.Control.Customer;
using TechnicalStation.UI.Control.Worker;
using TechnicalStation.UI.VewModel;
using TechnicalStation.UI.VewModel.Customer;
using TechnicalStation.UI.VewModel.Worker;
using TechnicalStation.UI.ViewModel;

namespace TechnicalStation.UI.Control
{
    public class ControlFactory : IControlFactory
    {
        readonly Dictionary<Type, object> controlCollection = new Dictionary<Type, object>();

        private IViewModelFactory viewModelFactory;

        public ControlFactory(IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            //this.controlCollection.Add(typeof(LoginControl), new LoginControl(this.viewModelFactory.Create<LoginViewModel>()));
            controlCollection.Add(typeof(DashboardControl), new DashboardControl(this.viewModelFactory.Create<DashboardViewModel>()));
            controlCollection.Add(typeof(OrderEditorControl), new OrderEditorControl(this.viewModelFactory.Create<OrderEditorViewModel>()));
            controlCollection.Add(typeof(AddOrderControl), new AddOrderControl(this.viewModelFactory.Create<AddOrderViewModel>()));
            //controlCollection.Add(typeof(ContentOrderControl), new ContentOrderControl(this.viewModelFactory.Create<OrderCollectionViewModel>()));
            controlCollection.Add(typeof(OrderFilterControl), new OrderFilterControl(this.viewModelFactory.Create<OrderFilterViewModel>()));
            controlCollection.Add(typeof(AddWorkControl), new AddWorkControl(this.viewModelFactory.Create<AddWorkViewModel>()));
            controlCollection.Add(typeof(AddCarControl), new AddCarControl(this.viewModelFactory.Create<AddCarViewModel>()));
            controlCollection.Add(typeof(AddCustomerControl), new AddCustomerControl(this.viewModelFactory.Create<AddCustomerViewModel>()));
            controlCollection.Add(typeof(WorkerEditorControl), new WorkerEditorControl(this.viewModelFactory.Create<WorkerEditorViewModel> ()));
            controlCollection.Add(typeof(AddWorkerControl), new AddWorkerControl(this.viewModelFactory.Create<AddWorkerViewModel>()));
            controlCollection.Add(typeof(CustomerEditorControl), new CustomerEditorControl(this.viewModelFactory.Create<CustomerEditorViewModel>()));
        }

        public T Create<T>()
        {
            Type type = typeof(T);

            if (!this.controlCollection.ContainsKey(type))
            {
                throw new MissingMemberException(type.ToString() + " is missing in the control model collection");
            }

            return (T)this.controlCollection[type];
        }
    }
}
