namespace Lab_9;

public class PersonalSettings
{
    public const int DefaultSeatPosition = 5;

    public string? MultimediaAccount { get; set; }
    public List<string> NavigatorAddresses { get; } = new();
    public int SeatPosition { get; set; } = DefaultSeatPosition;

    public void Reset()
    {
        MultimediaAccount = null;
        NavigatorAddresses.Clear();
        SeatPosition = DefaultSeatPosition;
    }

    public override string ToString() =>
        $"акаунт: {MultimediaAccount ?? "(немає)"}, адрес у навігаторі: {NavigatorAddresses.Count}, сидіння: {SeatPosition}";
}