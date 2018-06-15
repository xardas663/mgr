using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using ZigbeeMobileApp.Model;
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
        private ListView mListView;
        private ListViewDataAdapter adapter;
        private Spinner spinner;
        private ArrayAdapter<string> spinnerAdapter;
        private TextView dateDisplay;
        private Button dateSelectButton;

        private static List<ListViewDataRow> rowList = new List<ListViewDataRow>();
        private List<HumiditySensor> humiditySensors;
        private List<TemperatureSensor> temperatureSensors;      

        private static bool isTemperature = true;
        private static bool isHumidity;
        private static int maxResults = 100;
        private static int amount=6;
        private string date="all";
        private string sensorName="all";      

        private readonly DataRecieverService dataRecieverService = new DataRecieverService();

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            rowList = await dataRecieverService.GetDesiredDataFromApiForListView(isTemperature, isHumidity, amount, date, sensorName);
            await GenerateSensorsList();

            mListView.Adapter = null;
            mListView.Adapter = adapter;
            adapter.NotifyDataSetChanged();
            // Create your fragment here
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
            dateDisplay = view.FindViewById<TextView>(Resource.Id.txtDate);
            dateSelectButton = view.FindViewById<Button>(Resource.Id.btnSelectDate);
            var button = view.FindViewById<Button>(Resource.Id.btnFilter);
            var cbTemperature = view.FindViewById<CheckBox>(Resource.Id.cbTemperature);
            var cbHumidity = view.FindViewById<CheckBox>(Resource.Id.cbHumidity);          
            
            mListView.Adapter = adapter;

            dateSelectButton.Click += DateSelect_OnClick;
            seekBar.ProgressChanged += SeekBar_ProgressChanged;
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

            button.Click += async (o, e) =>
            {
                sensorName = spinner.SelectedItem.ToString();
                if (sensorName == "Wszystkie") sensorName = "all";
                rowList = await dataRecieverService.GetDesiredDataFromApiForListView(isTemperature, isHumidity, amount, date, sensorName);
                adapter = new ListViewDataAdapter(Context, rowList, inflater);
                mListView.Adapter = adapter;
                adapter.NotifyDataSetChanged();
            };

            resetDate.Click += (o, e) => 
            {
                dateDisplay.Text = "Wszystkie daty";
                date = "all";
            }; 

            return view;
        }

        private void SeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            amount = e.Progress;
            amount = 1 + maxResults * e.Progress / 100;
        }

        private async Task GenerateSensorsList()
        {
            if (isTemperature)
            {
                await CreateTemperatureSensorsList();
            }
            else
            {
                await CreateHumiditySensorsList();
            }
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

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                dateDisplay.Text = time.ToLongDateString();
                date = time.ToString("yyyy-MM-dd");    
            });

            frag.Show(Activity.FragmentManager, DatePickerFragment.TAG);
        }
    }
}