using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace ZigbeeMobileApp
{


    class ListViewRoomsAdapter : BaseAdapter
    {
        private Context _context;
        private List<ListViewRoomsRow> _items;
        private LayoutInflater _inflater;

        public ListViewRoomsAdapter(Context context, List<ListViewRoomsRow> items, LayoutInflater inflater)
        {
            _context = context;
            _items = items;
            _inflater = inflater;
        }
        //public override ListViewRoomsRow this[int position] => _items[position];

        public override int Count => _items.Count;

        public override long GetItemId(int position)
        { return 0; }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.listViewRoomsRow, null, false);
            }
            var txtRoomName = row.FindViewById<TextView>(Resource.Id.lblRoomName);
            var txtHumidity = row.FindViewById<TextView>(Resource.Id.lblHumidity);
            var txtTemperature = row.FindViewById<TextView>(Resource.Id.lblTemperature);
            var txtExpectedHumidity = row.FindViewById<TextView>(Resource.Id.lblExpectedHumidity);
            var txtExpectedTemperature = row.FindViewById<TextView>(Resource.Id.lblExpectedTemperature);

            txtRoomName.Text = "Nazwa pokoju: "+_items[position].RoomName;
            txtHumidity.Text = "Wilgotność: "+_items[position].Humidity + "%";
            txtTemperature.Text ="Temperatura: "+ _items[position].Temperature + "  °C";
            txtExpectedHumidity.Text = "Optymalna wilgotność: " + _items[position].ExpectedHumidity +  " %";
            txtExpectedTemperature.Text = "Optymalna temperatura: " + _items[position].ExpectedTemperature+ "  °C";
            
            return row;
        }
    }
}