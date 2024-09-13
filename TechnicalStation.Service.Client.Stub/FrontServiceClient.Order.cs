using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Client.Stub.Extensions;
using TechnicalStation.Service.Domain.Data;

namespace TechnicalStation.Service.Client.Stub
{
    public partial class FrontServiceClient
    {
        List<OrderInfo> orderInfoList = new List<OrderInfo>();
        int orderIdCounter = 1;

        public async Task<List<OrderInfo>> GetOrderInfoCollectionAsync()
        {
            return await Task.Run(()=>
            {
                return orderInfoList;
            });
        }

        public async Task<List<OrderInfo>> GetFilteredOrdersCollection(List<OrderInfo> ordersForFilterCollection, string firstNameValue, DateTime startDateValue, DateTime finishDateValue)
        {
            return await Task.Run(() =>
            {
                //DateTime cStartDate = new DateTime(2020,5,6);
                //DateTime cEndDate = new DateTime(2021, 7, 8);
                //byte[] photo = this.GetDefaultImage();
                orderInfoList.Clear();
                //OrderInfo orderInfo = new OrderInfo(0, DateTime.Now, DateTime.Now, "Undefined", "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client");
                foreach (OrderInfo order in ordersForFilterCollection)
                {
                    if (order.StartDate >= startDateValue && order.FinishDate <= finishDateValue && order.CustomerId==14)
                    {
                        orderInfoList.Add(order);
                    }
                }
                return orderInfoList;
            });
        }

        public async Task<OrderInfo> AddOrderInfoAsync(OrderInfo orderInfo)
        {
            return await Task.Run(() =>
            {
                return new OrderInfo(orderIdCounter++, orderInfo.CustomerId, orderInfo.CarId, orderInfo.StartDate, orderInfo.FinishDate, DateTime.Now);
            });
        }

        public async Task<OrderInfo> UpdateOrderInfoAsync(OrderInfo orderInfo)
        {
            return await Task.Run(() =>
            {
                var collection = this.orderInfoList.Where(o => o.Id == orderInfo.Id).ToList();
                if (collection.Count == 0)
                {
                    throw new Exception($"Order {orderInfo.Id} is not found");
                }
                else
                {
                    var existedOrderInfo = collection[0];
                    existedOrderInfo.CopyProperties<OrderInfo>(orderInfo);

                    return existedOrderInfo;
                }
            });
        }

        public Task RemoveOrderInfoAsync(int orderId)
        {
            return Task.Run(()=>
            {
                var collection = this.orderInfoList.Where(o => o.Id == orderId).ToList();
                if (collection.Count > 0)
                {
                    var existedOrderInfo = collection[0];
                    this.orderInfoList.Remove(existedOrderInfo);
                    this.workInfoList.RemoveAll(w => w.OrderId == orderId);

                }
            });
        }
    }
}
