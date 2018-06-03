using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZigbeeMobileApp.Services;
using Core;
using Newtonsoft.Json;

namespace ZigbeeMobileApp.Activities
{
    [Activity(Label = "EditRoom")]
    public class EditRoom : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditRoom);
            var buttonBack = FindViewById<Button>(Resource.Id.btnBackAdd);
            var buttonEdit = FindViewById<Button>(Resource.Id.btnEditRoom);

            string itemJson = Intent.GetStringExtra("item") ?? "0";
            var item = JsonConvert.DeserializeObject<ListViewRoomsRow>(itemJson);
            
            var name = FindViewById<EditText>(Resource.Id.etName);
            var description = FindViewById<EditText>(Resource.Id.etDescription);
            var expectedHumidity = FindViewById<EditText>(Resource.Id.etExpectedHumidity);
            var expectedTemperature = FindViewById<EditText>(Resource.Id.etExpectedTemperature);
            var tolerant = FindViewById<EditText>(Resource.Id.etTolerant);

            name.Text = item.RoomName;
            description.Text = "description";
            expectedHumidity.Text = item.ExpectedHumidity;
            expectedTemperature.Text = item.ExpectedTemperature;
            tolerant.Text = "123";


            buttonBack.Click += (s, e) =>
            {
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            buttonEdit.Click += async (s, e) =>
            {
                var service = new RoomsService();
                var room = new Room()
                {
                    Id = Int32.Parse(item.RoomId),
                    Name = name.Text,
                    Description = description.Text,
                    ExpectedHumidity = float.Parse(expectedHumidity.Text),
                    ExpectedTemperature = float.Parse(expectedTemperature.Text)
                };
                await service.EditRoom(room);
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            // Create your application here
        }
    }
}