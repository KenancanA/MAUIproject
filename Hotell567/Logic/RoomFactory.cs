using Hotell567.Models;
using System.Diagnostics;

namespace Hotell567.Logic
{
    public class RoomFactory
    {
        public RoomFactory()
        {
            Debug.WriteLine("RoomFactory connector created");
        }

        public async Task<bool> IsValidRoomNumber(string roomNumber)
        {
            if (int.TryParse(roomNumber, out int roomNumberAsInt))
            {
                if (roomNumberAsInt > 0)
                {
                    List<Room> rooms = await AppManager.roomDatabase.ListRooms();

                    foreach (var room in rooms)
                    {
                        if (room.room_number == roomNumberAsInt)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public bool IsValidDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return false;
            }

            return true;
        }

        public bool IsPriceValid(string price)
        {
            if (decimal.TryParse(price, out decimal priceAsDecimal))
            {
                if (priceAsDecimal > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
