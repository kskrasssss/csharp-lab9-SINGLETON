namespace Lab_9;

public class EndZone : IPrototype<EndZone>
{
    public string Name { get; set; }
    public decimal ExtraFee { get; set; }

    public EndZone(string name, decimal extraFee)
    {
        Name = name;
        ExtraFee = extraFee;
    }

    public EndZone Clone() => new(Name, ExtraFee);
}