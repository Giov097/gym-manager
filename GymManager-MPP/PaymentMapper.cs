using System.Data;
using GymManager_BE;
using GymManager_DAL;

namespace GymManager_MPP;

public class PaymentMapper : IMapper<Payment, long>
{
    private readonly IDataAccess _dataAccess = DataAccess.Instance;

    public Task<Payment> Create(Payment obj)
    {
        var paymentMethod = obj.GetType() == typeof(CardPayment) ? "Card" : "Cash";
        var cardLast4 = obj is CardPayment card ? card.LastFourDigits.ToString() : "NULL";
        var cardBrand = obj is CardPayment card2 ? card2.Brand : "NULL";
        var receiptNumber = obj is CashPayment cash ? cash.ReceiptNumber : "NULL";
        var query =
            $"""
             INSERT INTO payments (fee_id, payment_date, amount, payment_method, status, card_last4, card_brand, receipt_number) 
             VALUES (
                     '{obj.FeeId}',
                     '{obj.PaymentDate:yyyy-MM-dd}',
                     {obj.Amount},
                     '{paymentMethod}',
                     '{obj.Status}',
                     '{cardLast4}',
                     '{cardBrand}',
                     '{receiptNumber}');
             SELECT SCOPE_IDENTITY();
             """;
        return _dataAccess.Write(query)
            .ContinueWith(newId =>
            {
                obj.Id = decimal.ToInt64((decimal)newId.Result!);
                return obj;
            });
    }

    public Task<Payment?> GetById(long id)
    {
        var query = $"SELECT * FROM payments WHERE id = '{id}';";
        return _dataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                var row = dataSet.Result.Tables[0].Rows[0];
                return BuildPayment(row);
            });
    }

    public Task<List<Payment>> GetAll()
    {
        const string query = "SELECT * FROM payments;";
        return _dataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                var payments = new List<Payment>();
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                {
                    return payments;
                }

                payments.AddRange(from DataRow row in dataSet.Result.Tables[0].Rows
                    select BuildPayment(row));
                return payments;
            });
    }

    public Task<bool> Update(Payment obj)
    {
        var paymentMethod = obj.GetType() == typeof(CardPayment) ? "Card" : "Cash";
        var cardLast4 = obj is CardPayment card ? card.LastFourDigits.ToString() : null;
        var cardBrand = obj is CardPayment card2 ? card2.Brand : null;
        var receiptNumber = obj is CashPayment cash ? cash.ReceiptNumber : null;

        var query =
            $"""
             UPDATE payments
             SET payment_date = '{obj.PaymentDate:yyyy-MM-dd}',
                 amount = {obj.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                 status = '{obj.Status}',
                 payment_method = '{paymentMethod}',
                 card_last4 = '{cardLast4}',
                 card_brand = '{cardBrand}',
                 receipt_number = '{receiptNumber}'
             WHERE id = {obj.Id};
             """;
        return _dataAccess.Write(query)
            .ContinueWith(result => result.Status == TaskStatus.RanToCompletion);
    }

    public Task<bool> Delete(long id)
    {
        var query = $"DELETE FROM payments WHERE id = {id};";
        return _dataAccess.Write(query)
            .ContinueWith(_ => true);
    }

    public Task<List<Payment>> Search(DateOnly from, DateOnly to, long userId)
    {
        if (from == null && to == null && userId == 0)
        {
            return GetAll();
        }

        var conditions = new List<string>();
        if (from != null)
        {
            conditions.Add($"payment_date >= '{from:yyyy-MM-dd}'");
        }

        if (to != null)
        {
            conditions.Add($"payment_date <= '{to:yyyy-MM-dd}'");
        }

        if (userId != 0)
        {
            conditions.Add($"fee_id IN (SELECT id FROM fees WHERE user_id = {userId})");
        }

        var whereClause = conditions.Count > 0
            ? "WHERE " + string.Join(" AND ", conditions)
            : string.Empty;
        var query = $"SELECT * FROM payments {whereClause};";
        return _dataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                var payments = new List<Payment>();
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                {
                    return payments;
                }

                payments.AddRange(from DataRow row in dataSet.Result.Tables[0].Rows
                    select BuildPayment(row));
                return payments;
            });
    }

    #region BuildUtils

    private static Payment BuildPayment(DataRow row)
    {
        return row["payment_method"].ToString() switch
        {
            "Card" => new CardPayment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row["payment_date"]),
                Amount = (decimal)row["amount"],
                Status = (string)row["status"],
                Brand =
                    row["card_brand"] != DBNull.Value ? (string)row["card_brand"] : string.Empty,
                LastFourDigits = row["card_last4"] != DBNull.Value
                    ? int.Parse((string)row["card_last4"])
                    : 0
            },
            "Cash" => new CashPayment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row["payment_date"]),
                Amount = (decimal)row["amount"],
                Status = (string)row["status"],
                ReceiptNumber = row["receipt_number"] != DBNull.Value
                    ? (string)row["receipt_number"]
                    : string.Empty
            },
            _ => new Payment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row["payment_date"]),
                Amount = (decimal)row["amount"],
                Status = (string)row["status"]
            }
        };
    }

    #endregion
}