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
        List<WorkInfo> workInfoList = new List<WorkInfo>();
        int workIdCounter = 1;

        public async Task<List<WorkInfo>> GetWorkInfoCollectionAsync()
        {
            return await Task.Run(() =>
            {
                return workInfoList;
            });
        }

        public async  Task<WorkInfo> AddWorkInfoAsync(WorkInfo workInfo)
        {
            return await Task.Run(() =>
            {
                //var newWorkInfo = workInfo.Clone() as WorkInfo;
                //newWorkInfo.Id = workIdCounter++;
                //workInfoList.Add(newWorkInfo);

                //return newWorkInfo;

                workInfo.Id = workIdCounter++;
                workInfoList.Add(workInfo);

                return workInfo;
            });
        }

        public async Task<WorkInfo> UpdateWorkInfoAsync(WorkInfo workInfo)
        {
            return await Task.Run(() =>
            {
                var collection = this.workInfoList.Where(o => o.Id == workInfo.Id).ToList();
                if (collection.Count == 0)
                {
                    throw new Exception($"Work {workInfo.Id} is not found");
                }
                else
                {
                    var existedWorkInfo = collection[0];
                    existedWorkInfo.CopyProperties<WorkInfo>(workInfo);

                    return existedWorkInfo;
                }
            });
        }

        public Task RemoveWorkInfoAsync(int workId)
        {
            return Task.Run(() =>
            {
                var collection = this.workInfoList.Where(w => w.Id == workId).ToList();
                if (collection.Count > 0)
                {
                    var existedWorkInfo = collection[0];
                    this.workInfoList.Remove(existedWorkInfo);
                }
            });
        }
    }
}
