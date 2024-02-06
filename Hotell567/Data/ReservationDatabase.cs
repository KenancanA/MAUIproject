using Hotell567.Logic;
using Hotell567.Models;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;

namespace Hotell567.Data
{
    public class ReservationDatabase
    {
        private DateTimeFormatInfo dateTimeFormatInfo;

        public ReservationDatabase()
        {
            dateTimeFormatInfo = new DateTimeFormatInfo();

            Debug.WriteLine("ReservationDatabase connector created");
        }

        
        public async Task<List<Reservation>> ListReservations()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                var command = new SQLiteCommand("SELECT * FROM Reservations", _connection);
                var reader = await command.ExecuteReaderAsync();
                var reservations = new List<Reservation>();

                while (await reader.ReadAsync())
                {
                    reservations.Add(new Reservation
                    {
                        res_id = reader["res_id"].GetHashCode(),
                        res_room_id = reader["res_room_id"].GetHashCode(),
                        res_user_id = reader["res_user_id"].GetHashCode(),
                        res_check_in_date = DateTime.Parse(reader["res_check_in_date"].ToString()),
                        res_check_out_date = DateTime.Parse(reader["res_check_out_date"].ToString()),
                        res_total_price = decimal.Parse(reader["res_total_price"].ToString())
                    });
                }

                return reservations;
            }
        }


        
        public async void AddReservation(Reservation reservation)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                var command = new SQLiteCommand("INSERT INTO Reservations (res_room_id, res_user_id, res_check_in_date, res_check_out_date, res_total_price) VALUES (@res_room_id, @res_user_id, @res_check_in_date, @res_check_out_date, @res_total_price)", _connection);
                command.Parameters.AddWithValue("@res_room_id", reservation.res_room_id);
                command.Parameters.AddWithValue("@res_user_id", reservation.res_user_id);
                command.Parameters.AddWithValue("@res_check_in_date", reservation.res_check_in_date.ToString("d"));
                command.Parameters.AddWithValue("@res_check_out_date", reservation.res_check_out_date.ToString("d"));
                command.Parameters.AddWithValue("@res_total_price", reservation.res_total_price);
                await command.ExecuteNonQueryAsync();

                Debug.WriteLine("Reservation added to database!");
            }
        }
    }
}
