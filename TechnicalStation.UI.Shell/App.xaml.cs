using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.Control;
using TechnicalStation.Service.Client.Contract;

namespace TechnicalStation.UI.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IApplication
    {
        private void AppStartUp(object sender, StartupEventArgs args)
        {
            try
            {
                IMainWindowController mainWindowController = new MainWindowController(this);


                // Root of assembly
                // IFrontServiceClient frontServiceClient = new TechnicalStation.Service.Client.Stub.FrontServiceClient();
                 IFrontServiceClient frontServiceClient = new TechnicalStation.Service.Client.FrontServiceClient();

                //1.Подставление реализации FrontServiceClient который работает с сервером
                //IFrontServiceClient frontServiceClient = new TechnicalStation.Service.Client.FrontServiceClient(); //new RemoteNotes.Service.Client.Stub.FrontServiceClient();
                //2.адрес сервера берется из файл appSettings в папке configuration и передается в свойство NameValueCollection класса
                //ConfigurationManager
                string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
                Task.Run(async ()=> await frontServiceClient.ConnectAsync(serviceUrl)).Wait(); // todo: whether is not connected

                //IValidationRuleFactory validationRuleFactory = new ValidationRuleFactory();
                //3.Передача serviceUrl и реализацию frontServiceClient во ViewModelFactory
                IViewModelFactory viewModelFactory = new ViewModelFactory(mainWindowController, frontServiceClient, serviceUrl);
                IControlFactory controlFactory = new ControlFactory(viewModelFactory);
                mainWindowController.RegisterControls(controlFactory);
                mainWindowController.LoadDashboard();
                //mainWindowController.LoadContentOrderControl();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AppExit(object sender, ExitEventArgs args)
        {
            this.Shutdown();
        }

        void IApplication.Exit()
        {
            this.Shutdown();
        }
    }
}

