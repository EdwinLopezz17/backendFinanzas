namespace MonoLineAPI.Request;

public class Datas
{
    public int Client_dni { get; set; }
    public string Money { get; set; }
    public int IdProperty { get; set; }
    public double InitialFee { get; set; }
    public bool IsSustainable { get; set; }
    public bool GoodPayerBonus { get; set; }
    public double InterestRate { get; set; }
    public int PartialGracePeriod { get; set; }
    public int FullGracePeriod { get; set; }
    public int NumberOfYears { get; set; }
    public int PaymentFrequency { get; set; }
    public double cok { get; set; }
}