using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecliptica.InputHandler
{
    public interface IKeyboardHelper
    {
        #region Methods
        /// <summary>
        /// Method to show the keyboard
        /// </summary>
        void ShowKeyboard();

        /// <summary>
        /// Method to hide the keyboard
        /// </summary>
        void HideKeyboard();
        #endregion
    }
}
