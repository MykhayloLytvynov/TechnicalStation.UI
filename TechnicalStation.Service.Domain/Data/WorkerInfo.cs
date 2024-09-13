using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalStation.Service.Domain.Data
{
    public class WorkerInfo
    {
        private int id;
        private string name;

        public WorkerInfo()
        {
        }

        public WorkerInfo(int id,
            string name)
        {

            this.id = id;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
