using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using TechnicalStation.Service.Client.Settings;
using TechnicalStation.Service.Domain.Base;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.Service.Domain.Enum;

namespace TechnicalStation.Service.Client.Controller
{
    public class OrderOperationController
    {
        private CancellationTokenSource cts;
        private ServiceEnvironment serviceEnvironment;

        public OrderOperationController(ServiceEnvironment serviceEnvironment)
        {
            this.serviceEnvironment = serviceEnvironment;
        }

        public async Task<List<OrderInfo>> GetOrderInfoCollectionAsync(int orderId)
        {
            try
            {
                this.cts = new CancellationTokenSource(this.serviceEnvironment.OperationTimeout);

                OperationStatusInfo operationStatusInfo = await this.serviceEnvironment.Connection.InvokeCoreAsync<OperationStatusInfo>("getOrderInfoCollection", new object[] { orderId }, this.cts.Token);

                if (operationStatusInfo.OperationStatus == OperationStatus.Done)
                {
                    string attachedObjectText = operationStatusInfo.AttachedObject.ToString();
                    List<OrderInfo> orderInfoCollection = JsonConvert.DeserializeObject<List<OrderInfo>>(attachedObjectText);
                    return orderInfoCollection;
                }
                else
                {
                    throw new Exception(operationStatusInfo.AttachedInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Get notes collection for {orderId} cannot be performed. {ex.Message}", ex);
            }
        }

        public async Task<OrderInfo> AddOrderInfoAsync(OrderInfo orderInfo)
        {
            try
            {
                this.cts = new CancellationTokenSource(this.serviceEnvironment.OperationTimeout);

                OperationStatusInfo operationStatusInfo = await this.serviceEnvironment.Connection.InvokeCoreAsync<OperationStatusInfo>("addOrderInfo", new object[] { orderInfo }, this.cts.Token);

                if (operationStatusInfo.OperationStatus == OperationStatus.Done)
                {
                    string attachedObjectText = operationStatusInfo.AttachedObject.ToString();
                    OrderInfo orderInfoResult = JsonConvert.DeserializeObject<OrderInfo>(attachedObjectText);
                    return orderInfoResult;
                }
                else
                {
                    throw new Exception(operationStatusInfo.AttachedInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Add note cannot be performed. {ex.Message}", ex);
            }
        }

        public async Task RemoveOrderInfoAsync(int orderId)
        {
            try
            {
                this.cts = new CancellationTokenSource(this.serviceEnvironment.OperationTimeout);

                OperationStatusInfo operationStatusInfo = await this.serviceEnvironment.Connection.InvokeCoreAsync<OperationStatusInfo>("removeOrderInfo", new object[] { orderId }, this.cts.Token);

                if (operationStatusInfo.OperationStatus != OperationStatus.Done)
                {
                    throw new Exception(operationStatusInfo.AttachedInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Remove note cannot be performed. {ex.Message}", ex);
            }
        }
    }
}
