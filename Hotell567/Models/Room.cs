namespace Hotell567.Models
{
    public class Room
    {
        public int room_id { get; set; }
        public int room_number { get; set; }
        public string room_type { get; set; }
        public string room_image_name { get; set; }
        public string room_description { get; set; }
        public string room_availability { get; set; }
        public decimal room_price_per_night { get; set; }
    }
}

