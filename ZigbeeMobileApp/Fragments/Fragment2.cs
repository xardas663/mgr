using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZigbeeMobileApp.Repository;
using ZigbeeMobileApp.Services;

namespace ZigbeeMobileApp.Fragments
{
    public class Fragment2 : Fragment
    {
        private static List<ListViewDataRow> rowList = new List<ListViewDataRow>();
        private ListView mListView;
        private ListViewDataAdapter adapter;
        private Spinner spinner;
        private ArrayAdapter<string> spinnerAdapter;
        private static bool isTemperature = true;
        private static bool isHumidity;
        private List<HumiditySensor> humiditySensors;
        private List<TemperatureSensor> temperatureSensors;
        private static int maxResults = 6;
        private static int amount=6;
        private string date="all";
        private string sensorName="all";

        TextView _dateDisplay;
        Button _dateSelectButton;

        private readonly DataRecieverService dataRecieverService = new DataRecieverService();

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);               
            rowList = await dataRecieverService.GetDesiredDataFromApiForListView(isTemperature, isHumidity, amount, date, sensorName);
            if (isTemperature)
            {
                await CreateTemperatureSensorsList();
            }
            else
            {
                await CreateHumiditySensorsList();
            }

            mListView.Adapter = null;
            mListView.Adapter = adapter;
            adapter.NotifyDataSetChanged();

            // Create your fragment here
        }

        private async Task CreateHumiditySensorsList()
        {
            humiditySensors = await dataRecieverService.GetAllHumiditySensors();
            var sensorsNames = humiditySensors.Select(x => x.Name).ToList();
            sensorsNames.Add("Wszystkie");
            spinnerAdapter = new ArrayAdapter<string>(Context,
                Android.Resource.Layout.SimpleSpinnerItem, sensorsNames);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = null;
            spinner.Adapter = spinnerAdapter;
            spinnerAdapter.NotifyDataSetChanged();
        }

        private async Task CreateTemperatureSensorsList()
        {
            temperatureSensors = await dataRecieverService.GetAllTemperatureSensors();
            var sensorsNames = temperatureSensors.Select(x => x.Name).ToList();
            sensorsNames.Add("Wszystkie");
            spinnerAdapter = new ArrayAdapter<string>(Context,
                Android.Resource.Layout.SimpleSpinnerItem, sensorsNames);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = null;
            spinner.Adapter = spinnerAdapter;
            spinnerAdapter.NotifyDataSetChanged();
        }

        public static Fragment2 NewInstance()
        {
            var frag2 = new Fragment2 { Arguments = new Bundle() };
            return frag2;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment2, null);
            
            mListView = view.FindViewById<ListView>(Resource.Id.temphumlist);         
            adapter = new ListViewDataAdapter(Context,rowList, inflater);
            var seekBar = view.FindViewById<SeekBar>(Resource.Id.sbAmount);
            var resetDate = view.FindViewById<Button>(Resource.Id.btnResetDate);
            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerSensors);

            _dateDisplay = view.FindViewById<TextView>(Resource.Id.txtDate);
            _dateSelectButton = view.FindViewById<Button>(Resource.Id.btnSelectDate);
            _dateSelectButton.Click += DateSelect_OnClick;

            
            resetDate.Click += (o, e) => date = "all";

            seekBar.ProgressChanged += SeekBar_ProgressChanged;
            
            mListView.Adapter = adapter;
            var button = view.FindViewById<Button>(Resource.Id.btnFilter);
            button.Click += async (o, e) =>
            {                
                sensorName = spinner.SelectedItem.ToString();
                if (sensorName == "Wszystkie") sensorName = "all";
                rowList = await dataRecieverService.GetDesiredDataFromApiForListView(isTemperature, isHumidity, amount, date, sensorName);
                adapter = new ListViewDataAdapter(Context, rowList, inflater);
                mListView.Adapter = adapter;
                adapter.NotifyDataSetChanged();
            };

            var cbTemperature = view.FindViewById<CheckBox>(Resource.Id.cbTemperature);
            var cbHumidity = view.FindViewById<CheckBox>(Resource.Id.cbHumidity);

            cbTemperature.Click += async (o, e) => {
                if (cbTemperature.Checked)
                {
                    cbHumidity.Checked = false;
                    isHumidity = false;
                    isTemperature = true;
                    await CreateTemperatureSensorsList();
                    Toast.MakeText(Context, "Selected", ToastLength.Short).Show();
                }                                 
            };

            
            cbHumidity.Click += async (o, e) => {
                if (cbHumidity.Checked)
                {
                    cbTemperature.Checked = false;
                    isTemperature = false;
                    isHumidity = true;
                    await CreateHumiditySensorsList();
                    Toast.MakeText(Context, "Selected", ToastLength.Short).Show();
                }                                    
            };

            return view;
        }

        private void SeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            amount = e.Progress;
            amount = 1 + maxResults * e.Progress / 100;
        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _dateDisplay.Text = time.ToLongDateString();
                date = time.ToString("yyyy-MM-dd");    
            });


            frag.Show(this.Activity.FragmentManager, DatePickerFragment.TAG);
        }
    }
}