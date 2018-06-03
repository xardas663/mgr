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
using ZigbeeMobileApp.Fragments;
using ZigbeeMobileApp.Services;
using Core;

namespace ZigbeeMobileApp.Activities
{
    [Activity(Label = "AddRoom")]
    public class AddRoom : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddRoom);
            var buttonBack = FindViewById<Button>(Resource.Id.btnBackAdd);
            var buttonAdd = FindViewById<Button>(Resource.Id.btnAddRoom);
            var name = FindViewById<EditText>(Resource.Id.etName);
            var description = FindViewById<EditText>(Resource.Id.etDescription);
            var expectedHumidity = FindViewById<EditText>(Resource.Id.etExpectedHumidity);
            var expectedTemperature = FindViewById<EditText>(Resource.Id.etExpectedTemperature);
            var tolerant = FindViewById<EditText>(Resource.Id.etTolerant);

            buttonBack.Click += (s, e) =>
            {
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            buttonAdd.Click += async (s, e) =>
            {
                var service = new RoomsService();
                var room = new Room()
                {
                    Name=name.Text,
                    Description=description.Text,
                    ExpectedHumidity= float.Parse(expectedHumidity.Text),
                    ExpectedTemperature = float.Parse(expectedTemperature.Text)                   
                };
                await service.AddRoom(room);
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            // Create your application here
        }
    }
}