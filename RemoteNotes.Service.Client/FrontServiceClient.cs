using TechnicalStation.Service.Client.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalStation.Service.Client.Controller;
using TechnicalStation.Service.Client.Configurator.Contract;
using TechnicalStation.Service.Client.Configurator;
using TechnicalStation.Service.Domain.Data;
using RemoteNotes.Service.Client.Controller;

namespace TechnicalStation.Service.Client
{
    public class FrontServiceClient /*: IFrontServiceClient*/
    {
        private ServiceEnvironment serviceEnvironment;

        private SystemConnectionController systemConnectionController;
        private SystemEnterController systemEnterController;
        private OrderOperationController orderOperationController;
        List<OrderInfo> orderInfoList = new List<OrderInfo>();

        public FrontServiceClient()
        {
            this.ConfigureServiceSettings();
            INotificationControllerConfigurator notificationControllerConfigurator = (new NotificationControllerConfiguratorFactory()).Create();
            this.ConfigureOperationControllers(serviceEnvironment, notificationControllerConfigurator);

        }

        private void ConfigureServiceSettings()
        {
            this.serviceEnvironment = new ServiceEnvironment();
            this.serviceEnvironment.HubName = "orders";
            this.serviceEnvironment.ConnectionTimeout = new TimeSpan(0, 1, 0);
        }

        private void ConfigureOperationControllers(ServiceEnvironment serviceEnvironment, INotificationControllerConfigurator notificationConfigurator)
        {
            this.systemConnectionController = new SystemConnectionController(serviceEnvironment, notificationConfigurator);
            this.systemEnterController = new SystemEnterController(serviceEnvironment);
            //this.userOperationController = new UserOperationController(serviceEnvironment);
            this.orderOperationController = new OrderOperationController(serviceEnvironment);
        }

        public async Task ConnectAsync(string address)
        {
            await this.systemConnectionController.ConnectAsync(address);
        }

        public void Connect(string address)
        {
            this.systemConnectionController.Connect(address);
        }

        public void Disconnect()
        {
            this.systemConnectionController.Disconnect();
        }

        public async Task<List<OrderInfo>> GetOrderInfoCollection(int memberId)
        {
            return await this.orderOperationController.GetOrderInfoCollectionAsync(memberId);
        }

        public async Task<List<OrderInfo>> GetFilteredOrdersCollection(List<OrderInfo> ordersForFilterCollection) 
        {
            return await Task.Run(() =>
            {
                DateTime cStartDate = new DateTime(2020, 5, 6);
                DateTime cEndDate = new DateTime(2021, 7, 8);
                //byte[] photo = this.GetDefaultImage();
                orderInfoList.Clear();
                //OrderInfo orderInfo = new OrderInfo(0, DateTime.Now, DateTime.Now, "Undefined", "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client");
                foreach (OrderInfo order in ordersForFilterCollection)
                {
                    if (order.Start_date >= cStartDate && order.End_date <= cEndDate)
                    {
                        orderInfoList.Add(order);
                    }
                }
                return orderInfoList;
            });
        }

        public async Task<OrderInfo> AddOrderInfoAsync(OrderInfo orderInfo)
        {
            return await this.orderOperationController.AddOrderInfoAsync(orderInfo);
        }

        public async Task RemoveOrderInfoAsync(int orderId)
        {
            await this.orderOperationController.RemoveOrderInfoAsync(orderId);
        }

        //public MemberInfo Enter(string login, string password)
        //{
        //    return this.systemEnterController.SystemEnter(login, password);
        //}

        //public OperationStatusInfo Exit()
        //{
        //    return this.systemEnterController.SystemExit();
        //}

        //public async Task<OperationStatusInfo> ExitAsync()
        //{
        //    return await this.systemEnterController.SystemExitAsync();
        //}

        public async Task EnterAsync(string login, string password)
        {
           await this.systemEnterController.SystemEnterAsync(login, password);
        }
    }
}
