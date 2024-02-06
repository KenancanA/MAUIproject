using Hotell567.Data;
using Hotell567.Logic;
using Hotell567.Models;
using System.Diagnostics;

namespace Hotell567.MVVM;

public partial class RoomsPage : ContentPage
{
    public RoomsViewModel roomsViewModel { get; set; } = new();


    public RoomsPage()
    {
        InitializeComponent();
        BindingContext = roomsViewModel;
        RoomTypePicker.SelectedIndex = 0;
    }

    private async void LearnMoreBtnClicked(object sender, EventArgs e)
    {

        var room = ((VisualElement)sender).BindingContext as Room;

        Debug.WriteLine("Opening room: " + room.room_type + "!");

        if (room == null) return;

        await Shell.Current.GoToAsync(nameof(RoomDetailPage), true, new Dictionary<string, object>
        {
            {"Room", room}
        });

    }


    private void GetRooms(object sender, EventArgs e)
    {
        
        if (roomsViewModel.IsBusy) return;

        roomsViewModel.IsBusy = true;
        AppManager.roomFiltering.UpdateRoomAndReservationList();

        string roomType = RoomTypePicker.SelectedItem.ToString();
        int minPrice;
        int maxPrice;
        DateTime startDate;
        DateTime endDate;

        int minResult;
        if (int.TryParse(MinPrice.Text, out minResult))
        {
            Debug.WriteLine("Min price: " + minResult);
            minPrice = minResult;
        }
        else if (string.IsNullOrEmpty(MinPrice.Text))
        {
            minPrice = 0;
        }
        else
        {
            DisplayAlert("Error", "Enter a proper minimum price", "Okay");
            roomsViewModel.IsBusy = false;
            return;
        }

        int maxResult;
        if (int.TryParse(MaxPrice.Text, out maxResult))
        {
            Debug.WriteLine("Max price: " + maxResult);
            maxPrice = maxResult;
        }
        else if (string.IsNullOrEmpty(MaxPrice.Text))
        {
            maxPrice = 0;
        }
        else
        {
            DisplayAlert("Error", "Enter a proper maximum price", "Oopsie");
            roomsViewModel.IsBusy = false;
            return;
        }

        if (StartDate.Date <= DateTime.Today || EndDate.Date <= DateTime.Today)
        {
            DisplayAlert("Dates are in the past!", "Check-in or check-out dates cannot be in the past! You also can't book for today!", "I am very sorry..");
            roomsViewModel.IsBusy = false;
            return;
        }

        if (EndDate.Date < StartDate.Date)
        {
            DisplayAlert("Error", "Check-out date can't be before check-in date!", "Sorry, I'm dumb..");
            roomsViewModel.IsBusy = false;
            return;
        }

        startDate = StartDate.Date;
        endDate = EndDate.Date;

        Debug.WriteLine($"Searching rooms with type {roomType}, minimum price of {minPrice}, maximum price of {maxPrice}, starting from {StartDate.Date} and ending at {EndDate.Date}!");

        List<Room> filteredRooms = AppManager.roomFiltering.FilterRooms(roomType, minPrice, maxPrice, startDate, endDate);

        roomsViewModel.AddRoomsToList(filteredRooms);

        AppManager.selectedCheckInDate = startDate;
        AppManager.selectedCheckOutDate = endDate;

        roomsViewModel.IsBusy = false;
    }
}
