namespace GymManager_BE;

public class CardPayment : Payment
{
    public string Brand { get; set; }

    public int LastFourDigits { get; set; }

    public override string MethodName => "Tarjeta";

    public override string Summary()
    {
        var brand = string.IsNullOrWhiteSpace(Brand) ? "marca desconocida" : Brand;
        var last4 = LastFourDigits is >= 0 and <= 9999
            ? LastFourDigits.ToString("D4")
            : "####";
        return
            $"{MethodName} ({brand} ****{last4}) | {PaymentDate:yyyy-MM-dd} | {Amount:C} | {Status}";
    }

    public override bool Validate(out string? reason)
    {
        if (!base.Validate(out reason)) return false;

        if (string.IsNullOrWhiteSpace(Brand))
        {
            reason = "La marca de la tarjeta no puede estar vacía.";
            return false;
        }

        if (LastFourDigits is < 0 or > 9999)
        {
            reason = "Los últimos cuatro dígitos de la tarjeta son inválidos.";
            return false;
        }

        return true;
    }
}