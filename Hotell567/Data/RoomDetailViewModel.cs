using CommunityToolkit.Mvvm.ComponentModel;
using Hotell567.Models;

namespace Hotell567.Data
{
    [QueryProperty(nameof(Room), "Room")]
    public partial class RoomDetailViewModel : BaseViewModel
    {
        public RoomDetailViewModel()
        {

        }

        [ObservableProperty]
        Room room;
    }
}
