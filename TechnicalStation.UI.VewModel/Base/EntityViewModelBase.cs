using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel.Base
{
    public abstract class EntityViewModelBase<TInfo> : ElementViewModelBase
    {
        TInfo infoObject;


    }
}
