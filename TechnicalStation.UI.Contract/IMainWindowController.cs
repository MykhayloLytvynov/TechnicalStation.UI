using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Domain.Data;

namespace TechnicalStation.UI.Contract
{
    public interface IMainWindowController
    {
        void Exit();
        void RegisterControls(IControlFactory controlFactory);
        void LoadDashboard();

        // Worker
        void LoadWorkerEditorControl();

        void LoadAddWorkerControl(WorkerInfo workerInfo);
        void LoadContentWorkerCollectionControl(List<WorkerInfo> workerInfoCollection);
        void LoadContentWorkerCollectionControl(WorkerInfo workerInfo);


        // Order
        void LoadEditorControl();
        void LoadAddOrderControl(OrderInfo orderInfo, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection);
        void LoadContentOrderControl(OrderInfo orderInfoResult);//, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection
        void LoadContentOrderControl(List<OrderInfo> orderInfoCollection, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection);
                
        void LoadFilteredOrderCollection(List<OrderInfo> orderFilteredCollection, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection);
        void RemoveFromList(OrderInfo orderInfo);
        List<OrderInfo> GetOrderInfoCollection();
        ObservableCollection<OrderInfo> GetOrderInfoObsCollection();
        void LoadContentOrderControl();
        void LoadOrderFilterControl();

        // Work
        void LoadWorkCollectionControl(List<OrderInfo> orderInfoCollection);
        void LoadContentWorkCollectionControl();
        void LoadUpdateWorkControl(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection);
        void LoadUpdateWorkerControl(WorkerInfo workerInfo);
        void LoadAddWorkControl(int orderId);

        // Customer
        void LoadAddCustomerControl(CustomerInfo customerInfo);
        void LoadUpdateCarControl(CarInfo carInfo);
        void LoadContentCustomerControl();
        void LoadContentCustomerControl(List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection);
        void LoadContentCustomerControl(CustomerInfo customerInfo);


        // Car
        void LoadAddCarControl(CarInfo carInfo);
        void LoadAddCarControl(int customerId);
        void LoadContentCarCollectionControl();
        //void LoadUpdateCarControl(CarInfo carInfo);


    }
}
