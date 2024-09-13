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
        List<CarInfo> carInfoList = new List<CarInfo>();
        int carIdCounter = 1;

        public async Task<List<CarInfo>> GetCarInfoCollectionAsync()
        {
            if(carInfoList.Count == 0)
            {
                carInfoList.Add(new CarInfo() { Id = 1, Model = "Model1", Producer = "Producer1", Color = "Gray", CustomerId = 1, Number = "12344", Year = 1987});
                carInfoList.Add(new CarInfo() { Id = 1, Model = "Model1", Producer = "Producer1", Color = "Gray", CustomerId = 1, Number = "12344", Year = 1987 });
               // carInfoList.Add(new CarInfo() { Id = 2, Model = "Model2" });
            }

            return await Task.Run(() =>
            {
                return carInfoList;
            });
        }

        public async Task<CarInfo> AddCarInfoAsync(CarInfo carInfo)
        {
            return await Task.Run(() =>
            {
                carInfo.Id = carIdCounter++;
                carInfoList.Add(carInfo);
                return carInfo;
            });
        }

        public async Task<CarInfo> UpdateCarInfoAsync(CarInfo carInfo)
        {
            return await Task.Run(() =>
            {
                var collection = this.carInfoList.Where(o => o.Id == carInfo.Id).ToList();
                if (collection.Count == 0)
                {
                    throw new Exception($"Car {carInfo.Id} is not found");
                }
                else
                {
                    var existedCarInfo = collection[0];
                    existedCarInfo.CopyProperties<CarInfo>(carInfo);

                    return existedCarInfo;
                }
            });
        }

        public Task RemoveCarInfoAsync(int carId)
        {
            return Task.Run(() =>
            {
                var collection = this.carInfoList.Where(w => w.Id == carId).ToList();
                if (collection.Count > 0)
                {
                    var existedCarInfo = collection[0];
                    this.carInfoList.Remove(existedCarInfo);
                }
            });
        }
    }
}
