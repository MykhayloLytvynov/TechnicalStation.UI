using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalStation.UI.Contract
{
    public interface ICollectionView : IView
    {
        object SelectedItem { get; }
    }
}
