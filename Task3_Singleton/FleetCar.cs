namespace Lab_9;

public enum CarStatus
{
    Free,
    Reserved,
    InRent
}

public class FleetCar
{
    public string Plate { get; }
    public double Latitude { get; }
    public double Longitude { get; }
    public CarStatus Status { get; internal set; } = CarStatus.Free;
    public string? ReservedFor { get; internal set; }

    public FleetCar(string plate, double latitude, double longitude)
    {
        Plate = plate;
        Latitude = latitude;
        Longitude = longitude;
    }
}