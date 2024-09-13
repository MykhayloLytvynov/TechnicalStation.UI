using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalStation.Service.Client.Configurator.Contract
{
    public interface INotificationControllerConfigurator
    {
        void Configure(HubConnection hubConnection);
    }
}
