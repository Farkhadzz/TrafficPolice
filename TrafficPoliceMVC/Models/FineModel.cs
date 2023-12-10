namespace TrafficPoliceMVC.Models;

public class FineModel
{
    public int Id { get; set; }
    public string CarNumber { get; set; }
    public string CarName { get; set; }
    public double? Price { get; set; }
    public FineType Type { get; set; }
}

public enum FineType
{
    ParkingViolation,
    Speeding,
}