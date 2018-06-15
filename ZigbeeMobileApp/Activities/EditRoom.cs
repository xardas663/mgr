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
using Newtonsoft.Json;
using System.Threading.Tasks;
using ZigbeeMobileApp.Model;

namespace ZigbeeMobileApp.Activities
{
    [Activity(Label = "EditRoom")]
    public class EditRoom : Activity
    {
        private Spinner spinnerAvailableHumiditySensors;
        private Spinner spinnerAvailableTemperatureSensors;
        private Spinner spinnerCurrentHumiditySensors;
        private Spinner spinnerCurrentTemperatureSensors;

        private ArrayAdapter<string> spinnerHumidityAdapter;
        private ArrayAdapter<string> spinnerTemperatureAdapter;
        private ArrayAdapter<string> currentHumiditySensorsAdapter;
        private ArrayAdapter<string> currentTemperatureSensorsAdapter;


        private List<string> availableHumiditySensorsNames;
        private List<string> availableTemperatureSensorsNames;
        private List<string> currentHumiditySensorsNames;
        private List<string> currentTemperatureSensorsNames;
        DataRecieverService dataRecieverService = new DataRecieverService();

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditRoom);    
            var itemJson = Intent.GetStringExtra("item") ?? "0";
            var item = JsonConvert.DeserializeObject<ListViewRoomsRow>(itemJson);
            
            var name = FindViewById<EditText>(Resource.Id.etName);
            var description = FindViewById<EditText>(Resource.Id.etDescription);
            var expectedHumidity = FindViewById<EditText>(Resource.Id.etExpectedHumidity);
            var expectedTemperature = FindViewById<EditText>(Resource.Id.etExpectedTemperature);  
            var buttonBack = FindViewById<Button>(Resource.Id.btnBackAdd);
            var buttonEdit = FindViewById<Button>(Resource.Id.btnEditRoom);
            var buttonRemove = FindViewById<Button>(Resource.Id.btnRemoveRoom);
            var maxTemp = FindViewById<EditText>(Resource.Id.etMaxTemp);
            var minTemp = FindViewById<EditText>(Resource.Id.etMinTemp);
            var maxHum = FindViewById<EditText>(Resource.Id.etMaxHum);
            var minHum = FindViewById<EditText>(Resource.Id.etMinHum);
            var btnAddHumSensor = FindViewById<Button>(Resource.Id.btnAddHumSensor);
            var btnAddTempSensor = FindViewById<Button>(Resource.Id.btnAddTempSensor);           
            spinnerAvailableHumiditySensors = FindViewById<Spinner>(Resource.Id.spinnerHumiditySensors);
            spinnerAvailableTemperatureSensors = FindViewById<Spinner>(Resource.Id.spinnerTemperatureSensors);
            spinnerCurrentHumiditySensors = FindViewById<Spinner>(Resource.Id.spinnerCurrentHumSensors);
            spinnerCurrentTemperatureSensors = FindViewById<Spinner>(Resource.Id.spinnerCurrentTempSensors);
            

            await CreateHumiditySensorsList(item);
            await CreateTemperatureSensorsList(item);

            name.Text = item.RoomName;
            description.Text = item.Description;
            expectedHumidity.Text = item.ExpectedHumidity;
            expectedTemperature.Text = item.ExpectedTemperature;
            maxTemp.Text = item.MaxTemperature;
            minTemp.Text = item.MinTemperature;
            maxHum.Text = item.MinHumidity;
            minHum.Text = item.MinHumidity;

            btnAddHumSensor.Click += (s, e) =>
            {
                if (spinnerAvailableHumiditySensors.SelectedItem != null)
                {
                    var selectedItem = spinnerAvailableHumiditySensors.SelectedItem.ToString();
                    availableHumiditySensorsNames.Remove(selectedItem);
                    currentHumiditySensorsNames.Add(selectedItem);
                    RefreshHumiditySensorsList();
                }
            };
               

            btnAddTempSensor.Click += (s, e) =>
            {
                if(spinnerAvailableTemperatureSensors.SelectedItem != null)
                {
                    var selectedItem = spinnerAvailableTemperatureSensors.SelectedItem.ToString();
                    availableTemperatureSensorsNames.Remove(selectedItem);
                    currentTemperatureSensorsNames.Add(selectedItem);
                    RefreshTemperatureSensorsList();
                }
            };
           


            buttonBack.Click += (s, e) =>
            {
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            buttonEdit.Click += async (s, e) =>
            {
                var service = new RoomsService();
                var humiditySensors = new List<HumiditySensor>();
                var temperatureSensors = new List<TemperatureSensor>();

                foreach (var humiditySensorName in currentHumiditySensorsNames)
                {
                    humiditySensors.Add(new HumiditySensor() { Name = humiditySensorName });
                }

                foreach (var temperatureSensorName in currentTemperatureSensorsNames)
                {
                    temperatureSensors.Add(new TemperatureSensor() { Name = temperatureSensorName });
                }

                if (expectedHumidity.Text == "") expectedHumidity.Text = "0";
                if (expectedTemperature.Text == "") expectedTemperature.Text = "0";
                if (maxTemp.Text == "") maxTemp.Text = "0";
                if (minTemp.Text == "") minTemp.Text = "0";
                if (maxHum.Text == "") maxHum.Text = "0";
                if (minHum.Text == "") minHum.Text = "0";

                var room = new Room()
                {
                    Id = Int32.Parse(item.RoomId),
                    Name = name.Text,
                    Description = description.Text,
                    ExpectedHumidity = float.Parse(expectedHumidity.Text),
                    ExpectedTemperature = float.Parse(expectedTemperature.Text),
                    MaxTemperature = float.Parse(maxTemp.Text),
                    MinTemperature = float.Parse(minTemp.Text),
                    MaxHumidity = float.Parse(maxHum.Text),
                    MinHumidity = float.Parse(minHum.Text),
                    HumiditySensors = humiditySensors,
                    TemperatureSensors = temperatureSensors
                };
                await service.EditRoom(room);
                var nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            };

            buttonRemove.Click += (s, e) =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle($"Potwierdź usunięcie pokoju {item.RoomName}");
                alert.SetMessage("Usunięcia pokoju nie będzie można cofnąć");
                alert.SetPositiveButton("Usuń", async (senderAlert, args) => 
                {
                    var service = new RoomsService();
                    await service.DeleteRoom(item.RoomName);
                    var nextActivity = new Intent(this, typeof(MainActivity));
                    StartActivity(nextActivity);
                });

                alert.SetNegativeButton("Anuluj", (senderAlert, args) => {
                    Toast.MakeText(this, "Anulowano!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            };
        }

        private void RefreshTemperatureSensorsList()
        {
            spinnerTemperatureAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, availableTemperatureSensorsNames);
            currentTemperatureSensorsAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, currentTemperatureSensorsNames);
            spinnerAvailableTemperatureSensors.Adapter = null;
            spinnerCurrentTemperatureSensors.Adapter = null;
            spinnerCurrentTemperatureSensors.Adapter = currentTemperatureSensorsAdapter;
            spinnerAvailableTemperatureSensors.Adapter = spinnerTemperatureAdapter;

            RunOnUiThread(() =>
            {
                spinnerTemperatureAdapter.NotifyDataSetChanged();
                currentTemperatureSensorsAdapter.NotifyDataSetChanged();
            });
        }

        private void RefreshHumiditySensorsList()
        {
            spinnerHumidityAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, availableHumiditySensorsNames);
            currentHumiditySensorsAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, currentHumiditySensorsNames);
            spinnerCurrentHumiditySensors.Adapter = currentHumiditySensorsAdapter;
            spinnerAvailableHumiditySensors.Adapter = spinnerHumidityAdapter;
            RunOnUiThread(() =>
            {
                spinnerHumidityAdapter.NotifyDataSetChanged();
                currentHumiditySensorsAdapter.NotifyDataSetChanged();
            });
        }

        private async Task CreateHumiditySensorsList(ListViewRoomsRow item)
        {
            var tavailableHumiditySensors = await dataRecieverService.GetAllHumiditySensors();
            availableHumiditySensorsNames = tavailableHumiditySensors.Select(x => x.Name).ToList();
            availableHumiditySensorsNames = availableHumiditySensorsNames.Except(item.HumiditySensors).ToList();
            currentHumiditySensorsNames = item.HumiditySensors.ToList();
            RefreshHumiditySensorsList();
        }

        private async Task CreateTemperatureSensorsList(ListViewRoomsRow item)
        {
            var tavailableTemperatureSensors = await dataRecieverService.GetAllTemperatureSensors();
            availableTemperatureSensorsNames = tavailableTemperatureSensors.Select(x => x.Name).ToList();
            availableTemperatureSensorsNames = availableTemperatureSensorsNames.Except(item.TemperatureSensors).ToList();
            currentTemperatureSensorsNames = item.TemperatureSensors.ToList();
            RefreshTemperatureSensorsList();
        }
    }
}