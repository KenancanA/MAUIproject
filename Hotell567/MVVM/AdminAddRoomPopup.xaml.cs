using CommunityToolkit.Maui.Views;
using Hotell567.Logic;
using Hotell567.Models;

namespace Hotell567.MVVM;

public partial class AdminAddRoomPopup : Popup
{
    private FileResult chosenFile;
    private string chosenFileName;
    private Stream fileByteStream;
    private byte[] bytes;

    public AdminAddRoomPopup()
    {
        InitializeComponent();
    }

    private void CancelBtnClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void AddBtnClicked(object sender, EventArgs e)
    {
        if (AppManager.roomFactory.IsPriceValid(PricePerNight.Text) == false)
        {
            Status.Text = "Failed: Price must be a number greater than 0!";
            return;
        }

        if (AppManager.roomFactory.IsValidDescription(Description.Text) == false)
        {
            Status.Text = "Failed: Description text must not be empty!";
        }

        if (chosenFile == null)
        {
            Status.Text = "Failed: No image chosen!";
            return;
        }

        if (AppManager.roomFactory.IsValidRoomNumber(RoomNumber.Text).Result == false)
        {
            Status.Text = "Failed: Room number invalid or already in use!";
            return;
        }

        Room newRoom = new Room
        {
            room_type = RoomTypePicker.SelectedItem.ToString(),
            room_image_name = chosenFileName,
            room_number = int.Parse(RoomNumber.Text),
            room_description = Description.Text,
            room_price_per_night = decimal.Parse(PricePerNight.Text)
        };

        try
        {
            AppManager.roomDatabase.AddRoom(newRoom);

            var imagePath = Path.Combine(AppManager.imageFolder, chosenFileName);


            using (var stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            Close();
        }
        catch (Exception exception)
        {
            Console.WriteLine("Failed to add room to database: " + exception);
            Status.Text = "Failed to add room to database!";
            throw;
        }
    }

    private async void UploadImageBtnClicked(object sender, EventArgs e)
    {
        chosenFile = await FilePicker.PickAsync();

        if (chosenFile == null) return;

        if (chosenFile.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) == false)
        {
            Status.Text = "Failed: Only .jpg files are allowed!";
            return;
        }
        else
        {
            Status.Text = "";
        }


        chosenFileName = chosenFile.FileName;
        fileByteStream = chosenFile.OpenReadAsync().Result;
        bytes = new byte[fileByteStream.Length];
        await fileByteStream.ReadAsync(bytes, 0, (int)fileByteStream.Length);
        ImageBtn.Text = chosenFileName;
    }
}