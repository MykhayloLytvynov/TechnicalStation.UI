using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Extensions;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
    public class WorkerViewModel : ElementViewModelBase
    {
        public int Id
        {
            get
            {
                return (int)this.GetUIValue(idProperty);
            }
            set
            {
                SetUIValue(idProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty idProperty =
            DependencyProperty.Register(
            "Id",
            typeof(int),
            typeof(WorkerViewModel),
            new PropertyMetadata(null));

        public string Name
        {
            get
            {
                return (string)this.GetUIValue(NameProperty);
            }
            set
            {
                SetUIValue(NameProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(
            "Name",
            typeof(string),
            typeof(WorkerViewModel),
            new PropertyMetadata(null));
        private WorkerInfo workerInfo;

        public WorkerViewModel(int id, string firstName)
        {
            this.Id = id;
            this.Name = firstName;
        }

        public WorkerViewModel(WorkerInfo workerInfo)
        {
            try
            {
                this.Load(workerInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load(WorkerInfo workerInfo)
        {
            this.workerInfo = workerInfo;
            this.Transform(workerInfo);
        }

        public void Transform(WorkerInfo workerInfo)
        {
             workerInfo.CopyProperties(this);
        }

        public WorkerInfo Extract()
        {
            this.CopyProperties(workerInfo);

            return this.workerInfo;
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
