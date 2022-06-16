namespace Vue.Core.Application.Dtos;

public class PerformanceDto
{
    public int id { get; set; }
    public string oid { get; set; }
    public int cinema_id { get; set; }
    public int movie_id { get; set; }
    public int auditorium_id { get; set; }
    public int has_2d { get; set; }
    public int has_3d { get; set; }
    public int has_dbox { get; set; }
    public int has_xd { get; set; }
    public int has_atmos { get; set; }
    public int has_dolbycinema { get; set; }
    public int has_hfr { get; set; }
    public int has_ov { get; set; }
    public int has_nl { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public int has_break { get; set; }
    public int visible { get; set; }
    public int disabled { get; set; }
    public int is_edited { get; set; }
    public int occupied_seats { get; set; }
    public int total_seats { get; set; }
    public int manual_seat_selection { get; set; }
    public object price { get; set; }
    public object full_price { get; set; }
    public object reservation_fee { get; set; }
    public object ticket_fee { get; set; }
    public object has_rental_3d_glasses { get; set; }
    public object cinema { get; set; }
    public object cinema_name { get; set; }
    public object auditorium_name { get; set; }
    public object special_category { get; set; }
    public object variant_name { get; set; }
    public object variant_slug { get; set; }
    public object prices { get; set; }
}