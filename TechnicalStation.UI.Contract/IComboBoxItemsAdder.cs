using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Domain.Data;

namespace TechnicalStation.UI.Contract
{
    public interface IComboBoxItemsAdder
    {
        void SetCollectionValue(ObservableCollection<OrderInfo> orderInfoCollection);
    }
}
