
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ZigbeeMobileApp.Services;

namespace ZigbeeMobileApp.Activities
{
    [Activity(Label = "ShowMap")]
    public class Settings : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);

            var email = FindViewById<EditText>(Resource.Id.etEmail);
            var buttonBack = FindViewById<Button>(Resource.Id.btnBackMap);
            var buttonSave = FindViewById<Button>(Resource.Id.btnSave);
            buttonBack.Click += (s, e) =>
            {
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            buttonSave.Click += async (s, e) =>
            {
                var service = new SettingsService();
                await service.ChangeSetting("email", email.Text);
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);

            };
        }
    }
}