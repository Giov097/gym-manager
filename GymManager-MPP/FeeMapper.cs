using System.Data;
using Microsoft.Data.SqlClient;
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

    #endregion

    private readonly IDataAccess _dataAccessConnected = new DataAccessConnected();

    public Task<Fee> Create(Fee obj)
    {
        throw new NotImplementedException();
    }

    public Task<Fee> Create(Fee obj, long userId)
    {
        var parameters = new[]
        {
            new SqlParameter("@Amount", obj.Amount),
            new SqlParameter("@StartDate",
                new DateTime(obj.StartDate.Year, obj.StartDate.Month, obj.StartDate.Day, 0, 0, 0,
                    DateTimeKind.Local)),
            new SqlParameter("@EndDate",
                new DateTime(obj.EndDate.Year, obj.EndDate.Month, obj.EndDate.Day, 0, 0, 0,
                    DateTimeKind.Local)),
            new SqlParameter("@UserId", userId)
        };

        return _dataAccessConnected.WriteProcedure("sp_CreateFee", parameters)
            .ContinueWith(newId =>
            {
                if (newId.Exception != null) throw newId.Exception;
                obj.Id = Convert.ToInt64(newId.Result!);
                return obj;
            });
    }

    public Task<Fee?> GetById(long id)
    {
        var parameters = new[] { new SqlParameter("@Id", id) };
        return _dataAccessConnected.ReadProcedure("sp_GetFeeById", parameters)
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
        return _dataAccessConnected.ReadProcedure("sp_GetAllFees")
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
        var parameters = new[]
        {
            new SqlParameter("@Id", obj.Id),
            new SqlParameter("@Amount", obj.Amount),
            new SqlParameter("@StartDate",
                new DateTime(obj.StartDate.Year, obj.StartDate.Month, obj.StartDate.Day, 0, 0, 0,
                    DateTimeKind.Local)),
            new SqlParameter("@EndDate",
                new DateTime(obj.EndDate.Year, obj.EndDate.Month, obj.EndDate.Day, 0, 0, 0,
                    DateTimeKind.Local))
        };

        return _dataAccessConnected.WriteProcedure("sp_UpdateFee", parameters)
            .ContinueWith(result =>
            {
                if (result.Exception != null) throw result.Exception;
                return result.Result != null && Convert.ToInt32(result.Result) > 0;
            });
    }

    public Task<bool> Delete(long id)
    {
        var parameters = new[] { new SqlParameter("@Id", id) };
        return _dataAccessConnected.WriteProcedure("sp_DeleteFee", parameters)
            .ContinueWith(result =>
            {
                if (result.Exception != null) throw result.Exception;
                return result.Result != null && Convert.ToInt32(result.Result) > 0;
            });
    }

    private static Fee BuildFee(DataRow row)
    {
        return new Fee
        {
            Id = (long)row[FeeId],
            StartDate = DateOnly.FromDateTime((DateTime)row[StartDate]),
            EndDate = DateOnly.FromDateTime((DateTime)row[EndDate]),
            Amount = (decimal)row[FeeAmount],
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