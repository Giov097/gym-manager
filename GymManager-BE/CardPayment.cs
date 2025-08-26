namespace GymManager_BE;

public class CardPayment : Payment
{
    public string Brand { get; set; }

    public int LastFourDigits { get; set; }

}