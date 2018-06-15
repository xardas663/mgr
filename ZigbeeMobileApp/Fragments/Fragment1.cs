using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZigbeeMobileApp.Activities;
using ZigbeeMobileApp.Services;

namespace ZigbeeMobileApp.Fragments
{
    public class Fragment1 : Fragment
    {
        private static List<ListViewRoomsRow> rowList = new List<ListViewRoomsRow>();
        private ListView mListView;
        private ListViewRoomsAdapter adapter;
        private ProgressBar progressBar;
        private readonly DataRecieverService dataRecieverService = new DataRecieverService();

        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            await GetRoomsAndBindToList(savedInstanceState);
        }       

        public static Fragment1 NewInstance()
        {
            var frag1 = new Fragment1 { Arguments = new Bundle() };
            return frag1;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment1, null);

            mListView = view.FindViewById<ListView>(Resource.Id.roomsList);
            progressBar = view.FindViewById<ProgressBar>(Resource.Id.pbStatus);
            var btnAddRoom = view.FindViewById<Button>(Resource.Id.btnAddRoom);
            var btnShowMap = view.FindViewById<Button>(Resource.Id.btnShowMap);
            var btnSettings = view.FindViewById<Button>(Resource.Id.btnSettings);
            var btnInfo = view.FindViewById<Button>(Resource.Id.btnInfo);

            adapter = new ListViewRoomsAdapter(Context, rowList, inflater);
    
            mListView.ItemLongClick += (o, e) =>
            {
                var item = rowList[e.Position];
                var itemJson = JsonConvert.SerializeObject(item); 
           
                var nextActivity = new Intent(view.Context, typeof(EditRoom));
                nextActivity.PutExtra("item", itemJson);
                StartActivity(nextActivity);
            };
            btnAddRoom.Click += (o, e) =>
            {
                var nextActivity = new Intent(view.Context, typeof(AddRoom));             
                StartActivity(nextActivity);
            };
            btnShowMap.Click += (o, e) =>
            {
                var nextActivity = new Intent(view.Context, typeof(ShowMap));                
                StartActivity(nextActivity);
            };
            btnInfo.Click += (o, e) =>
            {
                var nextActivity = new Intent(view.Context, typeof(Info));
                StartActivity(nextActivity);
            };
            btnSettings.Click += (o, e) =>
            {
                var nextActivity = new Intent(view.Context, typeof(Settings));
                StartActivity(nextActivity);
            };


            return view;
        }
        private async Task GetRoomsAndBindToList(Bundle savedInstanceState)
        {
            rowList = await dataRecieverService.GetRooms();
            var inflater = GetLayoutInflater(savedInstanceState);
            adapter = new ListViewRoomsAdapter(Context, rowList, inflater);
            mListView.Adapter = adapter;
            adapter.NotifyDataSetChanged();
            progressBar.Visibility = ViewStates.Invisible;
        }
    }
}