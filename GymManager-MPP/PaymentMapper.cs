using System.Data;
using GymManager_BE;
using GymManager_DAL;

namespace GymManager_MPP;

public class PaymentMapper : IMapper<Payment, long>
{
    private readonly IDataAccess _dataAccess = new DataAccess();

    public Task<Payment> Create(Payment obj)
    {
        var paymentMethod = typeof(Payment) == typeof(CardPayment) ? "Card" : "Cash";
        var query =
            $"INSERT INTO payments (fee_id, payment_date, amount, payment_method, status) VALUES ('{obj.FeeId}','{obj.PaymentDate:yyyy-MM-dd}', {obj.Amount}, '{paymentMethod}', '{obj.Status}'); SELECT SCOPE_IDENTITY();";
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
        var query =
            $"UPDATE payments SET payment_date = '{obj.PaymentDate}', amount = {obj.Amount}, status = '{obj.Status}' WHERE id = {obj.Id};";
        return _dataAccess.Write(query)
            .ContinueWith(result => result.Result != null);
    }

    public Task<bool> Delete(long id)
    {
        var query = $"DELETE FROM payments WHERE id = {id};";
        return _dataAccess.Write(query)
            .ContinueWith(_ => true);
    }


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
                    row.Table.Columns.Contains("brand") ? (string)row["brand"] : string.Empty,
                LastFourDigits = row.Table.Columns.Contains("last_four_digits")
                    ? (int)row["last_four_digits"]
                    : 0
            },
            "Cash" => new CashPayment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row["payment_date"]),
                Amount = (decimal)row["amount"],
                Status = (string)row["status"],
                ReceiptNumber = row.Table.Columns.Contains("receipt_number")
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
}