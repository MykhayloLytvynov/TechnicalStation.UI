using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalStation.Service.Domain.Data
{
    public class WorkInfo : ICloneable
    {
        private int id;
        private int orderId;
        private double cost;
        private string description;
        private int workerId;
        public WorkInfo(int id, int orderId, double cost, string description, int workerId)
        {
            this.id = id;
            this.orderId = orderId;
            this.cost = cost;
            this.description = description;
            this.workerId = workerId;
        }

        public WorkInfo()
        {
           
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public int WorkerId
        {
            get => workerId; set => workerId = value;
        }

        public int Id
        {
            get => id; set => id = value;
        }
        
        public int OrderId
        {
            get => orderId; set => orderId = value;
        }

        public double Cost
        {
            get => cost; set => cost = value;

        }
       
        public string Description
        {
            get => description; set => description = value;
        }
    }
}
