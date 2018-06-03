using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace ZigbeeMobileApp
{
    class ListViewDataAdapter : BaseAdapter
    {
        private Context _context;
        private List<ListViewDataRow> _items;
        private LayoutInflater _inflater;

        public ListViewDataAdapter(Context context, List<ListViewDataRow> items, LayoutInflater inflater)
        {
            _context = context;
            _items = items;
            _inflater = inflater;
        }
        //public override ListViewDataRow this[int position] =>  _items[position];

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
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.listViewRow, null, false);
            }
            var txtRoomName = row.FindViewById<TextView>(Resource.Id.txtRoomName);
            var txtSensorId = row.FindViewById<TextView>(Resource.Id.txtSensorId);
            var txtValue = row.FindViewById<TextView>(Resource.Id.txtValue);
            var txtDate = row.FindViewById<TextView>(Resource.Id.txtDate);

            txtRoomName.Text = _items[position].RoomName;
            txtSensorId.Text = _items[position].SensorName;
            txtValue.Text = _items[position].Value;
            txtDate.Text = _items[position].Date;

            if (position == 0)
            {
                txtRoomName.TextSize = 15;
                txtSensorId.TextSize = 15;
                txtValue.TextSize = 15;
                txtDate.TextSize = 15;
                txtRoomName.SetTextColor(Android.Graphics.Color.Black);
                txtSensorId.SetTextColor(Android.Graphics.Color.Black);
                txtValue.SetTextColor(Android.Graphics.Color.Black);
                txtDate.SetTextColor(Android.Graphics.Color.Black);
            }
           
            

            return row;
        }
    }
}