namespace Hotell567.Models
{
    public class Reservation
    {
        public int res_id { get; set; }
        public int res_room_id { get; set; }
        public int res_user_id { get; set; }
        public DateTime res_check_in_date { get; set; }
        public DateTime res_check_out_date { get; set; }
        public decimal res_total_price { get; set; }
    }
}
