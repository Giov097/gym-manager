namespace GymManager_BE;

public class CashPayment : Payment
{
    public string ReceiptNumber { get; set; }

    public override string MethodName => "Efectivo";

    public override string Summary()
    {
        var receipt = string.IsNullOrWhiteSpace(ReceiptNumber)
            ? "sin recibo"
            : $"recibo {ReceiptNumber}";
        return $"{MethodName} | {PaymentDate:yyyy-MM-dd} | {Amount:C} | {Status} | {receipt}";
    }

    public override bool Validate(out string? reason)
    {
        if (!base.Validate(out reason)) return false;

        if (string.IsNullOrWhiteSpace(ReceiptNumber))
        {
            reason = "El pago en efectivo debe tener número de recibo.";
            return false;
        }

        return true;
    }
}