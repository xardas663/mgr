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
using System.Threading.Tasks;
using ZigbeeMobileApp.Model;

namespace ZigbeeMobileApp.Fragments
{
    public class Fragment4 : Android.Support.V4.App.Fragment
    {
        private List<PlotData> plotDataTemperature= new List<PlotData>();
        private List<PlotData> plotDataHumidity = new List<PlotData>();
        private PlotView plotView;
        CheckBox cbDaily;
        CheckBox cbMonthly;
        CheckBox cbYearly;
        private static bool isHumidity = false;
        private static bool isTemperature = true;
   
        private List<HumiditySensor> humiditySensors;
        private List<TemperatureSensor> temperatureSensors;
        private Spinner spinner;
        private ArrayAdapter<string> spinnerHumidityAdapter;
        private ArrayAdapter<string> spinnerTemperatureAdapter;
        private string date = DateTime.Now.ToString("yyyy-MM-dd");
        DataRecieverService dataRecieverService = new DataRecieverService();

        Room room;

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);      
            await CreateTemperatureSensorsList();
        }

        public static Fragment4 NewInstance()
        {
            var frag4 = new Fragment4 { Arguments = new Bundle() };
            return frag4;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment4, null);
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            plotView = view.FindViewById<PlotView>(Resource.Id.plot_view);
            cbDaily = view.FindViewById<CheckBox>(Resource.Id.cbDaily);
            cbMonthly = view.FindViewById<CheckBox>(Resource.Id.cbMonthly);
            cbYearly = view.FindViewById<CheckBox>(Resource.Id.cbYearly);
            var cbTemperature = view.FindViewById<CheckBox>(Resource.Id.cbTemperature);
            var cbHumidity = view.FindViewById<CheckBox>(Resource.Id.cbHumidity);

            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerSensors);           
            var btnDraw = view.FindViewById<Button>(Resource.Id.btnDraw);

            cbDaily.Click += (o, e) =>
            {
                if (cbDaily.Checked == true)
                {
                    cbMonthly.Checked = false;
                    cbYearly.Checked = false;
                }
            };

            cbMonthly.Click += (o, e) =>
            {
                if (cbMonthly.Checked == true)
                {
                    cbDaily.Checked = false;
                    cbYearly.Checked = false;
                }                
            };

            cbYearly.Click += (o, e) =>
            {
                if (cbYearly.Checked == true)
                {
                    cbDaily.Checked = false;
                    cbMonthly.Checked = false;
                }
            };

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


            btnDraw.Click += async (o, e) =>
            {                       
                await CreatePlotModel();
            };

            return view;
        }


        private async Task<Dictionary<string, double>> GetDataForSeries()
        {
            var sensor = spinner.SelectedItem.ToString();
            room = await dataRecieverService.GetRoomForGivenSensorName(sensor, isHumidity);

            if (isHumidity)
            {
                                
                if (cbDaily.Checked)
                {
                    try
                    {
                        var results = await dataRecieverService.GetHumidityAvgDaily(sensor);

                        return results.ToDictionary(x => x.Day.ToString() + "/" + x.Month.ToString(), x => Convert.ToDouble(x.AvgHumidity));
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                   
                }
                else if (cbMonthly.Checked)
                {
                    var results = await dataRecieverService.GetHumidityAvgMonthly(sensor);
                    return results.ToDictionary(x => x.Month.ToString(), x => Convert.ToDouble(x.AvgHumidity));
                }
                else if (cbYearly.Checked)
                {
                    var results = await dataRecieverService.GetHumidityAvgYearly(sensor);
                    return results.ToDictionary(x => x.Year.ToString(), x => Convert.ToDouble(x.AvgHumidity));
                }
                else
                {
                    return null;
                }
            }

            else
            {                
                if (cbDaily.Checked)
                {
                    var results = await dataRecieverService.GetTemperatureAvgDaily(sensor);

                    return results.ToDictionary(x => x.Day.ToString() + "/" + x.Month.ToString(), x => Convert.ToDouble(x.AvgTemperature));
                }
                else if (cbMonthly.Checked)
                {
                    var results = await dataRecieverService.GetTemperatureAvgMonthly(sensor);
                    return results.ToDictionary(x => x.Month.ToString(), x => Convert.ToDouble(x.AvgTemperature));
                }
                else if (cbYearly.Checked)
                {
                    var results = await dataRecieverService.GetTemperatureAvgYearly(sensor);
                    return results.ToDictionary(x => x.Year.ToString(), x => Convert.ToDouble(x.AvgTemperature));
                }
                else
                {
                    return null;
                }
            }
           
        }

        private async Task CreatePlotModel()
        {
            var results = await GetDataForSeries();
            var names = results.Keys.ToArray();
            string title;
            if (isHumidity)
            {
                if (cbDaily.Checked)
                {
                    title = "Wartoœci œrednie dzienne wilgotnoœci";
                }
                else if (cbMonthly.Checked)
                {
                    title = "Wartoœci œrednie miesiêczne wilgotnoœci";
                }
                else
                {
                    title = "Wartoœci œrednie roczne wilgotnoœci";
                }
                
            }
            else
            {
                if (cbDaily.Checked)
                {
                    title = "Wartoœci œrednie dzienne temperatury";
                }
                else if (cbMonthly.Checked)
                {
                    title = "Wartoœci œrednie miesiêczne temperatury";
                }
                else
                {
                    title = "Wartoœci œrednie roczne temperatury";
                }
            }
            var model = new PlotModel { Title = title };

            // A ColumnSeries requires a CategoryAxis on the x-axis.
            model.Axes.Add(new CategoryAxis
            {
                ItemsSource = names
            });
            var series = new ColumnSeries();

            foreach (var value in results.Values)
            {
                ColumnItem item;
                if (isHumidity)
                {
                    if (value > room.MaxHumidity)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(255, 0, 0);
                    }
                    else if (value < room.MinHumidity)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(0, 0, 255);
                    }
                    else if(value> room.ExpectedHumidity + 2 && value < room.MaxHumidity)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(200, 70, 10);
                    }

                    else if (value < room.ExpectedHumidity - 2 && value > room.MinHumidity)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(40, 70, 120);
                    }

                    else
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(0, 255, 0);
                    }               
                    series.LabelFormatString = "{0:.0}%";
                }

                else
                {
                    if (value > room.MaxTemperature)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(255, 0, 0);
                    }
                    else if (value < room.MinTemperature)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(0, 0, 255);
                    }
                    else if (value > room.ExpectedTemperature + 1 && value < room.MaxTemperature)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(200, 70, 10);
                    }

                    else if (value < room.ExpectedTemperature - 1 && value > room.MinTemperature)
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(40, 70, 120);
                    }

                    else
                    {
                        item = new ColumnItem(value);
                        item.Color = OxyColor.FromRgb(0, 255, 0);
                    }
                    series.LabelFormatString = "{0:.0}°C";
                }
                series.LabelPlacement = LabelPlacement.Inside;
                series.Items.Add(item);
            }
            model.Series.Add(series);         
            plotView.Model = model;

        }
      
        private async Task CreateHumiditySensorsList()
        {
            humiditySensors = await dataRecieverService.GetAllHumiditySensors();
            var sensorsNames = humiditySensors.Select(x => x.Name).ToList();

            spinnerHumidityAdapter = new ArrayAdapter<string>(Context,
                Android.Resource.Layout.SimpleSpinnerItem, sensorsNames);
            spinnerHumidityAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = null;
            spinner.Adapter = spinnerHumidityAdapter;
            spinnerHumidityAdapter.NotifyDataSetChanged();
        }

        private async Task CreateTemperatureSensorsList()
        {
            temperatureSensors = await dataRecieverService.GetAllTemperatureSensors();
            var sensorsNames = temperatureSensors.Select(x => x.Name).ToList();

            spinnerTemperatureAdapter = new ArrayAdapter<string>(Context,
                Android.Resource.Layout.SimpleSpinnerItem, sensorsNames);
            spinnerTemperatureAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = null;
            spinner.Adapter = spinnerTemperatureAdapter;
            spinnerTemperatureAdapter.NotifyDataSetChanged();
        }

    }
}