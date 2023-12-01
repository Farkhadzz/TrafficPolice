namespace TrafficPolice.Models;

public enum FineType
{
    Parking,
    Speeding,
}
public class Fine {
    public int Id { get; set; }
    public string CarNumber { get; set; }
    public string CarName { get; set; }
    public double? Price { get; set; }
    public FineType Type { get; set; }

    public Fine(int id, string carNumber, string carName, double? price, FineType type)
        {
            Id = id;
            CarNumber = carNumber;
            CarName = carName;
            Price = price;
            Type = type;
        }
}