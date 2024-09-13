using System.Threading.Tasks;
using TechnicalStation.Service.Client.Contract;

namespace TechnicalStation.Service.Client.Stub
{
    public partial class FrontServiceClient : IFrontServiceClient
    {
             
                   

        private void Dummy()
        {
        }

        public void Connect(string address)
        {
        }

        public Task ConnectAsync(string address)
        {
            return Task.Run(() => this.Connect(address));
        }

        public void Disconnect()
        {
        }

        public async Task EnterAsync(string login, string password)
        {
            ///await this.systemEnterController.SystemEnterAsync(login, password);
        }


    }
}
