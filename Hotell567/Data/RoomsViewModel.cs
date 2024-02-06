using CommunityToolkit.Mvvm.Input;
using Hotell567.Models;
using Hotell567.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Hotell567.Data;

public partial class RoomsViewModel : BaseViewModel
{
    public ObservableCollection<Room> Rooms { get; } = new();

    public RoomsViewModel()
    {
        Title = "Rooms";
    }

    [RelayCommand]
    private async Task GoToDetails(Room room)
    {
        if (room == null)
            return;

        await Shell.Current.GoToAsync(nameof(RoomDetailPage), true, new Dictionary<string, object>
        {
            {"Room", room }
        });
    }

    public void AddRoomsToList(List<Room> roomsToAdd)
    {
        if (Rooms.Count != 0) Rooms.Clear();

        foreach (var room in roomsToAdd)
        {
            Debug.WriteLine($"Added room {room.room_id} to the list on the rooms page!");
            Rooms.Add(room);
        }
        Debug.WriteLine("Rooms on the page: " + Rooms.Count);
    }
}