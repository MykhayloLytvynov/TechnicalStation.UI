using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Domain.Base;
using TechnicalStation.Service.Domain.Data;

namespace TechnicalStation.Service.Client.Contract
{
    public interface IFrontServiceClient
    {
        //void Connect(string address);

        Task ConnectAsync(string address);

        //void Disconnect();

        /// <summary>
        /// Allows the user to enter the system. Returns minimal user data object.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task EnterAsync(string login, string password);

        // Order
        Task<List<OrderInfo>> GetOrderInfoCollectionAsync();

        Task<List<OrderInfo>> GetFilteredOrdersCollection(List<OrderInfo> ordersForFilterCollection,string firstNameValue,DateTime startDateValue,DateTime finishDateValue);
        Task<OrderInfo> AddOrderInfoAsync(OrderInfo orderInfo);
        Task<OrderInfo> UpdateOrderInfoAsync(OrderInfo orderInfo);
        Task RemoveOrderInfoAsync(int orderId);

        // Work
        Task<List<WorkInfo>> GetWorkInfoCollectionAsync();
        Task<WorkInfo> AddWorkInfoAsync(WorkInfo workInfo);
        Task<WorkInfo> UpdateWorkInfoAsync(WorkInfo workInfo);
        Task RemoveWorkInfoAsync(int orderId);

        // Worker
        Task<List<WorkerInfo>> GetWorkerInfoCollectionAsync();
        Task<WorkerInfo> AddWorkerInfoAsync(WorkerInfo workerInfo);
        Task<WorkerInfo> UpdateWorkerInfoAsync(WorkerInfo workerInfo);
        Task RemoveWorkerInfoAsync(int workerId);


        // Car
        Task<List<CarInfo>> GetCarInfoCollectionAsync();
        Task<CarInfo> AddCarInfoAsync(CarInfo carInfo);
        Task<CarInfo> UpdateCarInfoAsync(CarInfo carInfo);
        Task RemoveCarInfoAsync(int carId);

        // Customer
        Task<List<CustomerInfo>> GetCustomerInfoCollectionAsync();
        Task<CustomerInfo> AddCustomerInfoAsync(CustomerInfo customerInfo);

        Task<CustomerInfo> UpdateCustomerInfoAsync(CustomerInfo customerInfo);

        Task RemoveCustomerInfoAsync(int customerId);
    }
}
