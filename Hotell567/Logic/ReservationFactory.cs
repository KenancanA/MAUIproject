
using Hotell567.Models;
using System.Diagnostics;

namespace Hotell567.Logic
{
    public class ReservationFactory
    {
        public ReservationFactory()
        {
            Debug.WriteLine("ReservationFactory created!");
        }


        public async void CreateReservation(DateTime CheckInDate, DateTime CheckOutDate, int RoomId, int UserId)
        {
            decimal price = await CalculateReservationTotalPrice(CheckInDate, CheckOutDate, RoomId);

            var reservation = new Reservation
            {
                res_check_in_date = CheckInDate,
                res_check_out_date = CheckOutDate,
                res_room_id = RoomId,
                res_user_id = UserId,
                res_total_price = price
            };

            AppManager.reservationDatabase.AddReservation(reservation);
        }

        public bool IsCheckInDateValid(DateTime CheckInDate)
        {
            if (CheckInDate.Date < DateTime.Now.Date) return false;
            return true;
        }

        public bool AreDatesValid(DateTime CheckInDate, DateTime CheckOutDate)
        {
            if (CheckInDate.Date > CheckOutDate.Date) return false;
            return true;
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var roomReservations = await GetReservationsForRoom(roomId);

            foreach (var reservation in roomReservations)
            {
                if (checkInDate >= reservation.res_check_in_date && checkInDate <= reservation.res_check_out_date)
                {
                    return false;
                }
                if (checkOutDate >= reservation.res_check_in_date && checkOutDate <= reservation.res_check_out_date)
                {
                    return false;
                }
            }

            return true;
        }

        
        private async Task<List<Reservation>> GetReservationsForRoom(int roomId)
        {
            var allReservations = await AppManager.reservationDatabase.ListReservations();
            var specificRoomReservations = new List<Reservation>();


            foreach (Reservation reservation in allReservations)
            {
                if (reservation.res_room_id == roomId)
                {
                    specificRoomReservations.Add(reservation);
                }
            }

            return specificRoomReservations;
        }

        public async Task<decimal> CalculateReservationTotalPrice(DateTime checkInDate, DateTime checkOutDate, int roomId)
        {
            decimal totalPrice = 0;

            var room = await AppManager.roomDatabase.GetRoomById(roomId);

            for (var date = checkInDate; date < checkOutDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    totalPrice += 1.5m * room.room_price_per_night;
                }
                else
                {
                    totalPrice += room.room_price_per_night;
                }
            }

            return totalPrice;
        }
    }
}
