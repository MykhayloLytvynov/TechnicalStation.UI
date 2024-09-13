using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Client.Configurator.Contract;

namespace TechnicalStation.Service.Client.Configurator
{
    class NotificationControllerConfigurator : INotificationControllerConfigurator
    {
        public NotificationControllerConfigurator()
        {

        }

        public void Configure(HubConnection hubConnection)
        {
            hubConnection.On<string>("Notify", (message) =>
            {
                System.Console.Write($"Received notification: {message}\r\n");
            });
        }

    }
}
