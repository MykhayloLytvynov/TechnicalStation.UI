using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel.Base
{
    public abstract class CollectionViewModelBase : ViewModelBase
    {
        private ICollectionView collectionView;

        public ICollectionView CollectionView
        {
            get { return this.collectionView; }
            set { this.collectionView = value; }
        }
    }
}
