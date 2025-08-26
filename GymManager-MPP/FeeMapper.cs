using System.Data;
using GymManager_BE;
using GymManager_DAL;

namespace GymManager_MPP;

public class FeeMapper : IMapper<Fee, long>
{
    private readonly IDataAccess _dataAccess = new DataAccess();

    public Task<Fee> Create(Fee obj)
    {
        var query =
            $"INSERT INTO fees (amount, start_date, end_date, user_id) VALUES ({obj.Amount}, '{obj.StartDate:yyyy-MM-dd}', '{obj.EndDate:yyyy-MM-dd}', '{obj.UserId}'); SELECT SCOPE_IDENTITY();";
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
             	p.status 
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
                             	p.status 
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
            $"UPDATE fees SET amount = {obj.Amount}, start_date = '{obj.StartDate}', end_date = '{obj.EndDate}', user_id = {obj.Payment.Id} WHERE id = {obj.Id};";
        return _dataAccess.Write(query)
            .ContinueWith(result => result.Result != null);
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
            Id = (long)row["fee_id"],
            StartDate = DateOnly.FromDateTime((DateTime)row["start_date"]),
            EndDate = DateOnly.FromDateTime((DateTime)row["end_date"]),
            Amount = (decimal)row["fee_amount"],
            UserId = (long)row["user_id"],
            Payment = (row["payment_id"] == DBNull.Value
                ? null
                : BuildPayment(row))!
        };
    }

    private static Payment BuildPayment(DataRow row)
        {
            return new Payment
            {
                Id = (long)row["payment_id"],
                FeeId = row["fee_id"] != DBNull.Value ? (long)row["fee_id"] : 0,
                PaymentDate = row["payment_date"] != DBNull.Value
                    ? DateOnly.FromDateTime((DateTime)row["payment_date"])
                    : default,
                Amount = row["payment_amount"] != DBNull.Value
                    ? (decimal)row["payment_amount"]
                    : 0,
                Status = row["status"] != DBNull.Value
                    ? (string)row["status"]
                    : string.Empty
            };
        }

}