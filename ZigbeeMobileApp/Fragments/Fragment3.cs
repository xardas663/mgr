using Android.OS;
using Android.Views;
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using ZigbeeMobileApp.Services;
using System.Linq;
using Android.Widget;
using System;
using Core;
using System.Threading.Tasks;

namespace ZigbeeMobileApp.Fragments
{
    public class Fragment3 : Android.Support.V4.App.Fragment
    {
        private List<PlotData> plotDataTemperature= new List<PlotData>();
        private List<PlotData> plotDataHumidity = new List<PlotData>();
        private PlotView plotView;
        private static bool isTemp=true;
        private static bool isHumidity=true;
        private List<HumiditySensor> humiditySensors;
        private List<TemperatureSensor> temperatureSensors;
        private Spinner spinnerHumidity;
        private Spinner spinnerTemperature;
        private ArrayAdapter<string> spinnerHumidityAdapter;
        private ArrayAdapter<string> spinnerTemperatureAdapter;
        TextView _dateDisplay;
        Button _dateSelectButton;
        private string date = DateTime.Now.ToString("yyyy-MM-dd");
        DataRecieverService dataRecieverService = new DataRecieverService();
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);          
            await CreateHumiditySensorsList();
            await CreateTemperatureSensorsList();
            
            // Create your fragment here
        }

        public static Fragment3 NewInstance()
        {
            var frag3 = new Fragment3 { Arguments = new Bundle() };
            return frag3;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment3, null);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            plotView = view.FindViewById<PlotView>(Resource.Id.plot_view);
            var cbTemperature = view.FindViewById<CheckBox>(Resource.Id.cbTemperature);
            var cbHumidity = view.FindViewById<CheckBox>(Resource.Id.cbHumidity);
            spinnerHumidity = view.FindViewById<Spinner>(Resource.Id.spinnerHumiditySensors);
            spinnerTemperature = view.FindViewById<Spinner>(Resource.Id.spinnerTemperatureSensors);
            
            var btnDraw = view.FindViewById<Button>(Resource.Id.btnDraw);
            btnDraw.Click += async (o, e) => 
            {                            
                if (isTemp)
                {
                    var tempSensorName = spinnerTemperature.SelectedItem.ToString();
                    plotDataTemperature = await dataRecieverService.GetTemperatureFromApiForPlot(date, tempSensorName);
                }
                if (isHumidity)
                {
                    var humSensorName = spinnerHumidity.SelectedItem.ToString();
                    plotDataHumidity = await dataRecieverService.GetHumidityFromApiForPlot(date, humSensorName);
                }               
               
                plotView.Model = CreatePlotModel(isTemp, isHumidity);
                plotView.InvalidatePlot(true);
            };
       

            _dateDisplay = view.FindViewById<TextView>(Resource.Id.txtDate);
            _dateSelectButton = view.FindViewById<Button>(Resource.Id.btnSelectDate);
            _dateSelectButton.Click += DateSelect_OnClick;
            _dateDisplay.Text = date;

            cbTemperature.Click += (o, e) =>
            {
                if (cbTemperature.Checked)
                {                                    
                    isTemp = true;
                    Toast.MakeText(Context, "Selected", ToastLength.Short).Show();
                }
                else
                {
                    isTemp = false;
                }              
            };

            cbHumidity.Click += (o, e) =>
            {
                if (cbHumidity.Checked)
                {
                    isHumidity = true;
                    Toast.MakeText(Context, "Selected", ToastLength.Short).Show();
                }
                else
                {
                    isHumidity = false;
                }       
            };


            return view;
        }

        private PlotModel CreatePlotModel(bool temp = true, bool humidity = true)
        {
            if (plotDataHumidity.Count > 1 || plotDataTemperature.Count > 1)
            {
                var plotModel = new PlotModel { Title = "" };


                var minTemp = plotDataTemperature.Min(x => x.Value);
                var maxTemp = plotDataTemperature.Max(x => x.Value);

                var minHum = plotDataHumidity.Min(x => x.Value);
                var maxHum = plotDataHumidity.Max(x => x.Value);
                plotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm:ss" });             

                LineSeries series1 = new LineSeries();
                LineSeries series2 = new LineSeries();

                if (temp)
                {
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = maxTemp + 5, Minimum = minTemp - 5, Key = "Temp", Title = "Temperatura [°C]" });

                    series1 = new LineSeries
                    {
                        Title = "Temperatura",
                        MarkerType = MarkerType.Circle,
                        MarkerSize = 5,
                        MarkerStroke = OxyColors.White,
                        YAxisKey = "Temp"
                    };

                    
                    foreach (var item in plotDataTemperature)
                    {
                        series1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Date), item.Value));
                    }

                    plotModel.Series.Add(series1);
                }
                if (humidity)
                {
                    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Right, Maximum = maxHum + 5, Minimum = minHum - 5, Key = "Hum", Title = "Wilgotnoœæ [%]" });

                    series2 = new LineSeries
                    {
                        Title = "Wilgotnoœæ",
                        MarkerType = MarkerType.Circle,
                        MarkerSize = 5,
                        MarkerStroke = OxyColors.Red,
                        YAxisKey = "Hum"
                    };

                    foreach (var item in plotDataHumidity)
                    {
                        series2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Date), item.Value));
                    }

                    plotModel.Series.Add(series2);
                }

                return plotModel;
            }

            else return new PlotModel();
            
        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _dateDisplay.Text = time.ToLongDateString();
                date= time.ToString("yyyy-MM-dd");
            });            

            frag.Show(Activity.FragmentManager, DatePickerFragment.TAG);
        }

        private async Task CreateHumiditySensorsList()
        {
            humiditySensors = await dataRecieverService.GetAllHumiditySensors();
            var sensorsNames = humiditySensors.Select(x => x.Name).ToList();
            
            spinnerHumidityAdapter = new ArrayAdapter<string>(Context,
                Android.Resource.Layout.SimpleSpinnerItem, sensorsNames);
            spinnerHumidityAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerHumidity.Adapter = null;
            spinnerHumidity.Adapter = spinnerHumidityAdapter;
            spinnerHumidityAdapter.NotifyDataSetChanged();
        }

        private async Task CreateTemperatureSensorsList()
        {
            temperatureSensors = await dataRecieverService.GetAllTemperatureSensors();
            var sensorsNames = temperatureSensors.Select(x => x.Name).ToList();
            
            spinnerTemperatureAdapter = new ArrayAdapter<string>(Context,
                Android.Resource.Layout.SimpleSpinnerItem, sensorsNames);
            spinnerTemperatureAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerTemperature.Adapter = null;
            spinnerTemperature.Adapter = spinnerTemperatureAdapter;
            spinnerTemperatureAdapter.NotifyDataSetChanged();
        }

    }
}