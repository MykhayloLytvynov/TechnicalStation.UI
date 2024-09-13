using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Common.UI.Utility;

namespace TechnicalStation.UI.ViewModel.Base
{
    public abstract class ViewModelBase : DependencyObject, IDataErrorInfo
    {
        protected readonly List<string> validatablePropertyCollection = new List<string>();


        private delegate object GetValueDelegate(DependencyProperty property);

        private delegate void SetValueDelegate(DependencyProperty property, object value);

        protected object GetUIValue(DependencyProperty property)
        {
            return Dispatcher.Invoke(new GetValueDelegate(GetValue), property);
        }


        protected void SetUIValue(DependencyProperty property, object value)
        {
            Dispatcher.Invoke(new SetValueDelegate(SetValue), property, value);
        }


        protected virtual bool IsValid
        {
            get
            {
                foreach (string property in this.validatablePropertyCollection)
                {
                    if (!string.IsNullOrEmpty(this.GetValidationError(property)))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        //protected byte[] GetDefaultImage()
        //{
        //    var location = System.Reflection.Assembly.GetExecutingAssembly().Location;

        //    // Todo: here we should place the call of default photo with "No photo" text.
        //    var fileToRead = Path.GetDirectoryName(location) + "\\images\\no-image-available.png";
        //    byte[] defaultImage = FileHelper.ReadBytesFromFile(fileToRead);

        //    return defaultImage;
        //}

        protected abstract string GetValidationError(string property);

        #region IDataErrorInfo

        /// <summary>
        /// The i data error info.this.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string IDataErrorInfo.this[string property]
        {
            get
            {
                string error = this.GetValidationError(property);
                return error;
            }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }



        #endregion IDataErrorInfo
    }
}
