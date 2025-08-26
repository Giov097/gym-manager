namespace GymManager_BE;

public class Payment : IEntity<long>
{
    public long Id { get; set; }

    public long FeeId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }
}