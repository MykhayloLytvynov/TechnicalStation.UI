using TechnicalStation.Service.Client.Configurator.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalStation.Service.Client.Configurator
{
    class NotificationControllerConfiguratorFactory
    {
        public INotificationControllerConfigurator Create()
        {
            return new NotificationControllerConfigurator();
        }
    }
}
