namespace GymManager_BE;

public class Fee : IEntity<long>
{
    public long Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal Amount { get; set; }

    public Payment Payment { get; set; }
}