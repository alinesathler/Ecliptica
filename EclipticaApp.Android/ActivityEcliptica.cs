using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Ecliptica.Games;
using Ecliptica.InputHandler;
using EclipticaApp.Android.InputHelper;
using Microsoft.Xna.Framework;

namespace EclipticaApp.Android
{
	[Activity(
		Label = "@string/app_name",
		MainLauncher = true,
		Icon = "@drawable/icon",
		AlwaysRetainTaskState = true,
		LaunchMode = LaunchMode.SingleInstance,
		ScreenOrientation = ScreenOrientation.Landscape,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
	)]
	public class ActivityEcliptica : AndroidGameActivity
	{
		private Ecliptica.EclipticaGame _game;
		private View _view;
        public static Context AppContext;

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			_game = new(Platform.Android);
			_view = _game.Services.GetService(typeof(View)) as View;

            AppContext = this;

            // Register the dependency
            ServiceLocator.Register<IKeyboardHelper>(new KeyboardHelper());

            SetContentView(_view);
			_game.Run();
		}
    }
}
