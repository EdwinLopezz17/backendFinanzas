namespace MonolineInfraestructure.models;

public class Flows
{
    public int Id { get; set; }
    public int Number { get; set; }
    public double TEP { get; set; }
    public string GracePeriod { get; set; }
    public double InitialBalance { get; set; }
    public double Interest { get; set; }
    public double Fee { get; set; }
    public double Amortization { get; set; }
    public double SD { get; set; }
    public double STR { get; set; }
    public double FinalBalance { get; set; }
    public double Flow { get; set; }
    
    public int CreditId { get; set; }
    
}