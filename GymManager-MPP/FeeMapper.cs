using System.Data;
using GymManager_BE;
using GymManager_DAL;

namespace GymManager_MPP;

public class FeeMapper : IMapper<Fee, long>
{
    #region Constants

    private const string FeeId = "fee_id";
    private const string PaymentId = "payment_id";
    private const string Status = "status";
    private const string PaymentDate = "payment_date";
    private const string PaymentMethod = "payment_method";
    private const string PaymentAmount = "payment_amount";
    private const string CardBrand = "card_brand";
    private const string CardLast4 = "card_last4";
    private const string ReceiptNumber = "receipt_number";
    private const string StartDate = "start_date";
    private const string EndDate = "end_date";
    private const string FeeAmount = "fee_amount";
    private const string UserId = "user_id";

    #endregion

    private readonly IDataAccess _dataAccess = DataAccess.Instance;


    public Task<Fee> Create(Fee obj)
    {
      throw new NotImplementedException();
    }

    public Task<Fee> Create(Fee obj, long userId)
    {
        var query =
            $"INSERT INTO fees (amount, start_date, end_date, user_id) VALUES ({obj.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)}, '{obj.StartDate:yyyy-MM-dd}', '{obj.EndDate:yyyy-MM-dd}', '{userId}'); SELECT SCOPE_IDENTITY();";
        return _dataAccess.Write(query)
            .ContinueWith(newId =>
            {
                obj.Id = decimal.ToInt64((decimal)newId.Result!);
                return obj;
            });
    }

    public Task<Fee?> GetById(long id)
    {
        var query =
            $"""
             SELECT
             	f.id as 'fee_id',
             	f.start_date,
             	f.end_date,
             	f.amount as 'fee_amount',
             	f.user_id,
             	p.id as 'payment_id',
             	p.amount as 'payment_amount',
             	p.payment_date,
             	p.payment_method,
             	p.status,
             	p.card_last4,
             	p.card_brand,
             	p.receipt_number
             FROM
             	fees f
             FULL OUTER JOIN payments p ON
             	f.id = p.fee_id
             WHERE f.id = '{id}';
             """;
        return _dataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                var row = dataSet.Result.Tables[0].Rows[0];
                return BuildFee(row);
            });
    }

    public Task<List<Fee>> GetAll()
    {
        const string query = """
                             SELECT
                             	f.id as 'fee_id',
                             	f.start_date,
                             	f.end_date,
                             	f.amount as 'fee_amount',
                             	f.user_id,
                             	p.id as 'payment_id',
                             	p.amount as 'payment_amount',
                             	p.payment_date,
                             	p.payment_method,
                             	p.status,
                             	p.card_last4,
                                p.card_brand,
                                p.receipt_number
                             FROM
                             	fees f
                             FULL OUTER JOIN payments p ON
                             	f.id = p.fee_id;
                             """;
        return _dataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                var fees = new List<Fee>();
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                {
                    return fees;
                }

                fees.AddRange(from DataRow row in dataSet.Result.Tables[0].Rows
                    select BuildFee(row));
                return fees;
            });
    }

    public Task<bool> Update(Fee obj)
    {
        var query =
            $"""
             UPDATE fees
             SET amount = {obj.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                 start_date = '{obj.StartDate:yyyy-MM-dd}',
                 end_date = '{obj.EndDate:yyyy-MM-dd}'
             WHERE id = {obj.Id};
             """;
        return _dataAccess.Write(query)
            .ContinueWith(result => result.Status == TaskStatus.RanToCompletion);
    }

    public Task<bool> Delete(long id)
    {
        var query = $"DELETE FROM fees WHERE id = {id};";
        return _dataAccess.Write(query)
            .ContinueWith(_ => true);
    }

    private static Fee BuildFee(DataRow row)
    {
        return new Fee
        {
            Id = (long)row[FeeId],
            StartDate = DateOnly.FromDateTime((DateTime)row[StartDate]),
            EndDate = DateOnly.FromDateTime((DateTime)row[EndDate]),
            Amount = (decimal)row[FeeAmount],
            // UserId = (long)row[UserId],
            Payment = (row[PaymentId] == DBNull.Value
                ? null
                : BuildPayment(row))!
        };
    }

    #region BuildUtils

    private static Payment BuildPayment(DataRow row)
    {
        return row[PaymentMethod].ToString() switch
        {
            "Card" => BuildCardPayment(row),
            "Cash" => BuildCashPayment(row),
            _ => BuildBasePayment(row)
        };
    }

    private static Payment BuildBasePayment(DataRow row)
    {
        return new Payment
        {
            Id = (long)row[PaymentId],
            // FeeId = row[FeeId] != DBNull.Value ? (long)row[FeeId] : 0,
            PaymentDate =
                row[PaymentDate] != DBNull.Value
                    ? DateOnly.FromDateTime((DateTime)row[PaymentDate])
                    : default,
            Amount = row[PaymentAmount] != DBNull.Value
                ? (decimal)row[PaymentAmount]
                : 0,
            Status = row[Status] != DBNull.Value ? (string)row[Status] : string.Empty
        };
    }

    private static CashPayment BuildCashPayment(DataRow row)
    {
        return new CashPayment
        {
            Id = (long)row[PaymentId],
            // FeeId = row[FeeId] != DBNull.Value ? (long)row[FeeId] : 0,
            PaymentDate =
                row[PaymentDate] != DBNull.Value
                    ? DateOnly.FromDateTime((DateTime)row[PaymentDate])
                    : default,
            Amount =
                row[PaymentAmount] != DBNull.Value ? (decimal)row[PaymentAmount] : 0,
            Status = row[Status] != DBNull.Value ? (string)row[Status] : string.Empty,
            ReceiptNumber = row[ReceiptNumber] != DBNull.Value
                ? (string)row[ReceiptNumber]
                : string.Empty
        };
    }

    private static CardPayment BuildCardPayment(DataRow row)
    {
        return new CardPayment
        {
            Id = (long)row[PaymentId],
            // FeeId = row[FeeId] != DBNull.Value ? (long)row[FeeId] : 0,
            PaymentDate =
                row[PaymentDate] != DBNull.Value
                    ? DateOnly.FromDateTime((DateTime)row[PaymentDate])
                    : default,
            Amount =
                row[PaymentAmount] != DBNull.Value ? (decimal)row[PaymentAmount] : 0,
            Status = row[Status] != DBNull.Value ? (string)row[Status] : string.Empty,
            Brand =
                row[CardBrand] != DBNull.Value
                    ? (string)row[CardBrand]
                    : string.Empty,
            LastFourDigits = row[CardLast4] != DBNull.Value
                ? int.Parse((string)row[CardLast4])
                : 0
        };
    }

    #endregion
}