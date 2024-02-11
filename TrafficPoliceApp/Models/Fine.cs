namespace TrafficPoliceApp.Models;

public class Fine {
    public int Id { get; set; }
    public string FineName { get; set; }
    public string CarNumber { get; set; }
    public string CarModel { get; set; }
    public decimal Price { get; set; }
}