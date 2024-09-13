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
        readonly List<CustomerInfo> customerInfoList = new List<CustomerInfo>();
        int customerIdCounter = 1;

        public async Task<List<CustomerInfo>> GetCustomerInfoCollectionAsync()
        {
            if (customerInfoList.Count == 0)
            {
                customerInfoList.Add(new CustomerInfo() {Id = 1, FirstName = "First", LastName = "LastName", Address = "Addess", PhoneNumber = "095671876"});
                customerInfoList.Add(new CustomerInfo() { Id = 2, FirstName = "First2", LastName = "LastName2", Address = "Addess2", PhoneNumber = "095671876" });
            }

            return await Task.Run(() =>
            {
                return customerInfoList;
            });
        }

        public async Task<CustomerInfo> AddCustomerInfoAsync(CustomerInfo customerInfo)
        {
            return await Task.Run(() =>
            {
                customerInfo.Id = customerIdCounter++;
                customerInfoList.Add(customerInfo);
                return customerInfo;
            });
        }

        public async Task<CustomerInfo> UpdateCustomerInfoAsync(CustomerInfo customerInfo)
        {
            return await Task.Run(() =>
            {
                var collection = this.customerInfoList.Where(o => o.Id == customerInfo.Id).ToList();
                if (collection.Count == 0)
                {
                    throw new Exception($"Customer {customerInfo.Id} is not found");
                }
                else
                {
                    var existedCustomerInfo = collection[0];
                    existedCustomerInfo.CopyProperties<CustomerInfo>(customerInfo);

                    return existedCustomerInfo;
                }
            });
        }

        public Task RemoveCustomerInfoAsync(int customerId)
        {
            return Task.Run(() =>
            {
                var collection = this.customerInfoList.Where(w => w.Id == customerId).ToList();
                if (collection.Count > 0)
                {
                    var existedCustomerInfo = collection[0];
                    this.customerInfoList.Remove(existedCustomerInfo);
                }
            });
        }
    }
}
