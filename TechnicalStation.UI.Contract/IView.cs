using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalStation.UI.Contract
{
    public interface IView
    {
        /// <summary>
        /// The set focus.
        /// </summary>
        void SetFocus(int index = 0);

        /// <summary>
        /// The show error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        //void ShowError(string error);

        //void ClearError();
    }
}
