namespace GymManager_BE;

public class Payment : IEntity<long>
{
    public long Id { get; set; }

    // public long FeeId { get; set; } //TODO: quitar

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }

    public virtual string MethodName => "Generic";

    public virtual string Summary()
    {
        return $"{MethodName} | {PaymentDate:yyyy-MM-dd} | {Amount:C} | {Status}";
    }

    public virtual bool Validate(out string? reason)
    {
        if (Amount <= 0)
        {
            reason = "El monto debe ser mayor que 0.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Status))
        {
            reason = "El estado no puede estar vacío.";
            return false;
        }

        reason = null;
        return true;
    }

    public override string ToString() => Summary();
}