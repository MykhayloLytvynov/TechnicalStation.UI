using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalStation.Service.Client.Settings
{
    public class ServiceEnvironment
    {
        public string HubName { get; set; } = "orders";
        public TimeSpan ConnectionTimeout { get; set; } = new TimeSpan(0, 0, 30);

        public TimeSpan OperationTimeout { get; set; } = new TimeSpan(0, 0, 30);

        public HubConnection Connection { get; set; }
    }
}
