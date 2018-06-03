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
using Core;
using System.Threading.Tasks;
using ZigbeeMobileApp.Repository;

namespace ZigbeeMobileApp.Services
{
    public class RoomsService : IRoomsService
    {              
     
        public async Task AddRoom(Room room)
        {
            var _roomsRepository = new RoomsRepository();
            await _roomsRepository.AddRoom(room);
        }

        public async Task EditRoom(Room room)
        {
            var _roomsRepository = new RoomsRepository();
            await _roomsRepository.EditRoom(room);
        }
    }

    public interface IRoomsService
    {
        Task EditRoom(Room room);
        Task AddRoom(Room room);
    }
}