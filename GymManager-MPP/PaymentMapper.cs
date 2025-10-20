using System.Data;
using GymManager_BE;
using GymManager_DAL;
using GymManager_DDAL;

namespace GymManager_MPP;

public class PaymentMapper : IMapper<Payment, long>
{
    private readonly IDisconnectedDataAccess _dataAccess = DataAccessDisconnected.Instance;

    # region Constants

    private const string FeeId = "fee_id";
    private const string PaymentDate = "payment_date";
    private const string Amount = "amount";
    private const string PaymentMethod = "payment_method";
    private const string Status = "status";
    private const string CardLast4 = "card_last4";
    private const string CardBrand = "card_brand";
    private const string ReceiptNumber = "receipt_number";
    private const string Payments = "payments";

    #endregion

    public Task<Payment> Create(Payment obj)
    {
        throw new NotImplementedException();
    }

    public async Task<Payment> Create(Payment obj, long feeId)
    {
        var paymentMethod = obj.GetType() == typeof(CardPayment) ? "Card" : "Cash";

        var dataSet = await _dataAccess.Read("SELECT * FROM payments WHERE 1 = 0;", Payments);
        var table = dataSet.Tables[0];

        var row = table.NewRow();
        row[FeeId] = feeId;
        row[PaymentDate] = obj.PaymentDate.ToDateTime(TimeOnly.MinValue);
        row[Amount] = obj.Amount;
        row[PaymentMethod] = paymentMethod;
        row[Status] = obj.Status;
        if (obj is CardPayment card)
        {
            row[CardLast4] = card.LastFourDigits.ToString();
            row[CardBrand] = card.Brand ?? (object)DBNull.Value;
            row[ReceiptNumber] = DBNull.Value;
        }
        else if (obj is CashPayment cash)
        {
            row[ReceiptNumber] = cash.ReceiptNumber ?? (object)DBNull.Value;
            row[CardLast4] = DBNull.Value;
            row[CardBrand] = DBNull.Value;
        }
        else
        {
            row[CardLast4] = DBNull.Value;
            row[CardBrand] = DBNull.Value;
            row[ReceiptNumber] = DBNull.Value;
        }

        table.Rows.Add(row);

        var affected = await _dataAccess.Write(dataSet);

        var idObj = table.Rows[0]["id"];
        if (idObj != DBNull.Value && idObj != null)
        {
            obj.Id = Convert.ToInt64(idObj);
        }

        return obj;
    }

    public Task<Payment?> GetById(long id)
    {
        var query = $"SELECT * FROM payments WHERE id = '{id}';";
        return _dataAccess.Read(query, Payments)
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
        return _dataAccess.Read(query, Payments)
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

    public async Task<bool> Update(Payment obj)
    {
        var ds = await _dataAccess.Read($"SELECT * FROM payments WHERE id = {obj.Id};", Payments);
        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            return false;
        }

        var row = ds.Tables[0].Rows[0];
        row[PaymentDate] = obj.PaymentDate.ToDateTime(TimeOnly.MinValue);
        row[Amount] = obj.Amount;
        row[Status] = obj.Status;

        var paymentMethod = obj.GetType() == typeof(CardPayment) ? "Card" : "Cash";
        row[PaymentMethod] = paymentMethod;

        if (obj is CardPayment card)
        {
            row[CardLast4] = card.LastFourDigits.ToString();
            row[CardBrand] = card.Brand ?? (object)DBNull.Value;
            row[ReceiptNumber] = DBNull.Value;
        }
        else if (obj is CashPayment cash)
        {
            row[ReceiptNumber] = cash.ReceiptNumber ?? (object)DBNull.Value;
            row[CardLast4] = DBNull.Value;
            row[CardBrand] = DBNull.Value;
        }
        else
        {
            row[CardLast4] = DBNull.Value;
            row[CardBrand] = DBNull.Value;
            row[ReceiptNumber] = DBNull.Value;
        }

        row.AcceptChanges();
        row.SetModified();

        var affected = await _dataAccess.Write(ds);
        return affected > 0;
    }

    public async Task<bool> Delete(long id)
    {
        var ds = await _dataAccess.Read($"SELECT * FROM payments WHERE id = {id};", Payments);
        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            return false;
        }

        ds.Tables[0].Rows[0].Delete();

        var affected = await _dataAccess.Write(ds);
        return affected > 0;
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
        return _dataAccess.Read(query, Payments)
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
        return row[PaymentMethod].ToString() switch
        {
            "Card" => new CardPayment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row[PaymentDate]),
                Amount = (decimal)row[Amount],
                Status = (string)row[Status],
                Brand =
                    row[CardBrand] != DBNull.Value ? (string)row[CardBrand] : string.Empty,
                LastFourDigits = row[CardLast4] != DBNull.Value
                    ? int.Parse((string)row[CardLast4])
                    : 0
            },
            "Cash" => new CashPayment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row[PaymentDate]),
                Amount = (decimal)row[Amount],
                Status = (string)row[Status],
                ReceiptNumber = row[ReceiptNumber] != DBNull.Value
                    ? (string)row[ReceiptNumber]
                    : string.Empty
            },
            _ => new Payment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row[PaymentDate]),
                Amount = (decimal)row[Amount],
                Status = (string)row[Status]
            }
        };
    }

    #endregion
}