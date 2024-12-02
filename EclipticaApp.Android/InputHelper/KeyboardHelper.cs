using Android.App;
using Android.Content;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Ecliptica;
using Ecliptica.InputHandler;

namespace EclipticaApp.Android.InputHelper
{
    internal class KeyboardHelper : IKeyboardHelper
    {
        #region Fields
        private readonly EditText _editText;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of keyboard helper
        /// </summary>
        public KeyboardHelper()
        {
            var context = ActivityEcliptica.AppContext;
            if (context is Activity activity)
            {
                // Create an invisible EditText
                _editText = new EditText(context)
                {
                    Visibility = ViewStates.Invisible,
                    LayoutParameters = new ViewGroup.LayoutParams(1, 1)
                };

                // Set the properties of the EditText
                _editText.SetMaxLines(1);
                _editText.InputType = InputTypes.ClassText;
                _editText.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(12) });

                // Add a listener to the EditText
                _editText.AfterTextChanged += (sender, args) =>
                {
                    EclipticaGame.PlayerName = _editText.Text;
                };

                activity.RunOnUiThread(() =>
                {
                    activity.AddContentView(_editText, new ViewGroup.LayoutParams(1, 1));
                });
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to show the keyboard
        /// </summary>
        public void ShowKeyboard()
        {
            var context = ActivityEcliptica.AppContext;
            var inputMethodManager = (InputMethodManager)context.GetSystemService(Context.InputMethodService);

            if (context is Activity activity)
            {
                activity.RunOnUiThread(() =>
                {
                    _editText.Visibility = ViewStates.Visible;
                    _editText.RequestFocus();

                    inputMethodManager.ShowSoftInput(_editText, ShowFlags.Implicit);
                });
            }

            if (_editText.Parent == null)
            {
                if (context is Activity activity1)
                {
                    activity1.RunOnUiThread(() =>
                    {
                        activity1.AddContentView(_editText, new ViewGroup.LayoutParams(1, 1));
                    });
                }
            }
        }

        /// <summary>
        /// Method to hide the keyboard
        /// </summary>
        public void HideKeyboard()
        {
            var context = ActivityEcliptica.AppContext;
            var inputMethodManager = (InputMethodManager)context.GetSystemService(Context.InputMethodService);

            if (context is Activity activity)
            {
                activity.RunOnUiThread(() =>
                {
                    inputMethodManager.HideSoftInputFromWindow(_editText.WindowToken, HideSoftInputFlags.None);
                    _editText.Visibility = ViewStates.Gone;
                });
            }
        }
        #endregion
    }
}
