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
        List<WorkerInfo> workerInfoList = new List<WorkerInfo>();
        int workerIdCounter = 1;

        public async Task<List<WorkerInfo>> GetWorkerInfoCollectionAsync()
        {
            //if (workerInfoList.Count == 0)
            //{
            //    workerInfoList.Add(new WorkerInfo() { Id = 1, Name = "Worker 1" });
            //    workerInfoList.Add(new WorkerInfo() { Id = 2, Name = "Worker 2" });
            //}
            return await Task.Run(() =>
            {
                return workerInfoList;
            });
        }

        public async Task<WorkerInfo> AddWorkerInfoAsync(WorkerInfo workerInfo)
        {
            return await Task.Run(() =>
            {
                workerInfo.Id = workerIdCounter++;
                workerInfoList.Add(workerInfo);
                return workerInfo;
            });
        }

        public async Task<WorkerInfo> UpdateWorkerInfoAsync(WorkerInfo workerInfo)
        {
            return await Task.Run(() =>
            {
                var collection = this.workerInfoList.Where(o => o.Id == workerInfo.Id).ToList();
                if (collection.Count == 0)
                {
                    throw new Exception($"Worker {workerInfo.Id} is not found");
                }
                else
                {
                    var existedWorkerInfo = collection[0];
                    existedWorkerInfo.CopyProperties<WorkerInfo>(workerInfo);

                    return existedWorkerInfo;
                }
            });
        }

        public Task RemoveWorkerInfoAsync(int workerId)
        {
            return Task.Run(() =>
            {
                var collection = this.workerInfoList.Where(w => w.Id == workerId).ToList();
                if (collection.Count > 0)
                {
                    var existedWorkerInfo = collection[0];
                    this.workerInfoList.Remove(existedWorkerInfo);
                }
            });
        }
    }
}
