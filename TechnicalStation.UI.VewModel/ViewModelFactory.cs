using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.VewModel;
using TechnicalStation.UI.VewModel.Context;
using TechnicalStation.UI.VewModel.Customer;
using TechnicalStation.UI.VewModel.Worker;

namespace TechnicalStation.UI.ViewModel
{
    public class ViewModelFactory : IViewModelFactory
    {
        readonly Dictionary<Type, object> viewModelCollection = new Dictionary<Type, object>();

        public ViewModelFactory(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient, /*IValidationRuleFactory validationRuleFactory,*/ string serviceUrl)
        {
            //4.добавление в колекцию вью моделей, создание обьекта LoginViewModel
            /*this.viewModelCollection.Add(typeof(LoginViewModel),
                new LoginViewModel(
                    mainWindowController,
                    frontServiceClient,
                    validationRuleFactory.Create<IEnterOperationValidationRule>(),
                    serviceUrl));*/

            this.viewModelCollection.Add(typeof(DashboardViewModel), new DashboardViewModel(mainWindowController,frontServiceClient, serviceUrl));
            this.viewModelCollection.Add(typeof(OrderEditorViewModel), new OrderEditorViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(AddOrderViewModel), new AddOrderViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(OrderFilterViewModel), new OrderFilterViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(AddWorkViewModel), new AddWorkViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(AddCarViewModel), new AddCarViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(AddCustomerViewModel), new AddCustomerViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(WorkerEditorViewModel), new WorkerEditorViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(AddWorkerViewModel), new AddWorkerViewModel(mainWindowController, frontServiceClient));
            this.viewModelCollection.Add(typeof(CustomerEditorViewModel), new CustomerEditorViewModel(mainWindowController, frontServiceClient));


            DataContext.FrontServiceClient = frontServiceClient;
            DataContext.LoadData();

            //this.viewModelCollection.Add(typeof(OrderCollectionViewModel), new OrderCollectionViewModel());
        }

        public T Create<T>()
        {
            Type type = typeof(T);

            if (!this.viewModelCollection.ContainsKey(type))
            {
                throw new MissingMemberException(type.ToString() + "is missing in the view model collection");
            }

            return (T)this.viewModelCollection[type];
        }
    }
}
