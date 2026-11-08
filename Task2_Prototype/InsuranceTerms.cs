namespace Lab_9;

public class InsuranceTerms : IPrototype<InsuranceTerms>
{
    public string Coverage { get; set; }
    public decimal Deductible { get; set; }

    public InsuranceTerms(string coverage, decimal deductible)
    {
        Coverage = coverage;
        Deductible = deductible;
    }

    public InsuranceTerms Clone() => new(Coverage, Deductible);
}