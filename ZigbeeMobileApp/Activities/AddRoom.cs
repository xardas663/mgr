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
using ZigbeeMobileApp.Model;

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
            var maxTemp = FindViewById<EditText>(Resource.Id.etMaxTemp);
            var minTemp = FindViewById<EditText>(Resource.Id.etMinTemp);
            var maxHum = FindViewById<EditText>(Resource.Id.etMaxHum);
            var minHum = FindViewById<EditText>(Resource.Id.etMinHum);

            buttonBack.Click += (s, e) =>
            {
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            buttonAdd.Click += async (s, e) =>
            {
                
                var service = new RoomsService();

                if (expectedHumidity.Text == "") expectedHumidity.Text = "0";
                if (expectedTemperature.Text == "") expectedTemperature.Text = "0";
                if (maxTemp.Text == "") maxTemp.Text = "0";
                if (minTemp.Text == "") minTemp.Text = "0";
                if (maxHum.Text == "") maxHum.Text = "0";
                if (minHum.Text == "") minHum.Text = "0";

                var room = new Room()
                {
                    Name=name.Text ?? "nowePomieszczenie"+DateTime.Now.ToShortDateString(),
                    Description=description.Text ?? "",
                    ExpectedHumidity= float.Parse(expectedHumidity.Text ?? "0"),
                    ExpectedTemperature = float.Parse(expectedTemperature.Text ?? "0"),
                    MaxTemperature=float.Parse(maxTemp.Text ?? "0"),
                    MinTemperature=float.Parse(minTemp.Text ?? "0"),
                    MaxHumidity=float.Parse(maxHum.Text ?? "0"),
                    MinHumidity=float.Parse(minHum.Text ?? "0")
                };
                await service.AddRoom(room);
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };
        }
    }
}