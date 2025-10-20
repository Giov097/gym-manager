using System.Data;
using Microsoft.Data.SqlClient;
using GymManager_BE;
using GymManager_DAL;

namespace GymManager_MPP;

public class UserMapper : IMapper<User, long>
{
    private readonly IDataAccess _dataAccess = new DataAccessConnected();


    public async Task<User> Create(User obj)
    {
        var pFirst = new SqlParameter("@FirstName", SqlDbType.NVarChar, 100)
            { Value = obj.FirstName };
        var pLast = new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = obj.LastName };
        var pEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = obj.Email };
        var pPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 255)
            { Value = obj.Password };
        var pNewId = new SqlParameter("@NewId", SqlDbType.BigInt)
            { Direction = ParameterDirection.Output };

        await _dataAccess.WriteProcedure("dbo.usp_CreateUser", [
            pFirst, pLast, pEmail, pPassword, pNewId
        ]);

        obj.Id = Convert.ToInt64(pNewId.Value);

        var namesTable = new DataTable();
        namesTable.Columns.Add("Value", typeof(string));
        foreach (var r in obj.UserRoles.Select(x => x.ToString()))
        {
            namesTable.Rows.Add(r);
        }

        var pNames = new SqlParameter("@Names", SqlDbType.Structured)
        {
            TypeName = "dbo.StringList",
            Value = namesTable
        };

        var rolesDataSet = await _dataAccess.ReadProcedure("dbo.usp_GetRolesByNames", [pNames]);

        var roleIdsTable = new DataTable();
        roleIdsTable.Columns.Add("Value", typeof(int));
        foreach (DataRow row in rolesDataSet.Tables[0].Rows)
        {
            roleIdsTable.Rows.Add((int)row["id"]);
        }

        if (roleIdsTable.Rows.Count > 0)
        {
            var pUserId = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = obj.Id };
            var pRoleIds = new SqlParameter("@RoleIds", SqlDbType.Structured)
            {
                TypeName = "dbo.IntList",
                Value = roleIdsTable
            };

            await _dataAccess.WriteProcedure("dbo.usp_InsertUserRolesBulk", [pUserId, pRoleIds]);
        }

        return obj;
    }

    public async Task<User?> GetById(long id)
    {
        var pId = new SqlParameter("@Id", SqlDbType.BigInt) { Value = id };
        var dataSet = await _dataAccess.ReadProcedure("dbo.usp_GetUserById", [pId]);

        if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            return null;

        User? user = null;

        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            user ??= BuildUser(row);

            if (row["role_name"] != DBNull.Value)
            {
                MapRoles(row, user);
            }

            if (row["fee_id"] != DBNull.Value)
            {
                MapFees(row, user);
            }
        }

        return user;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var pEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = email };
        var dataSet = await _dataAccess.ReadProcedure("dbo.usp_GetUserByEmail", [pEmail]);

        if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
        {
            return null;
        }

        var row = dataSet.Tables[0].Rows[0];
        return BuildUser(row);
    }


    public async Task<User?> GetByFeeId(long feeId)
    {
        var pFeeId = new SqlParameter("@FeeId", SqlDbType.BigInt) { Value = feeId };
        var dataSet = await _dataAccess.ReadProcedure("dbo.usp_GetUserByFeeId", [pFeeId]);

        if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
        {
            return null;
        }

        var row = dataSet.Tables[0].Rows[0];
        return BuildUser(row);
    }

    public async Task<List<User>> GetAll()
    {
        var dataSet = await _dataAccess.ReadProcedure("dbo.usp_GetAllUsers");

        var usersDict = new Dictionary<long, User>();

        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            var userId = (long)row["user_id"];
            if (!usersDict.TryGetValue(userId, out var user))
            {
                user = new User
                {
                    Id = userId,
                    FirstName = row["first_name"].ToString() ?? string.Empty,
                    LastName = row["last_name"].ToString() ?? string.Empty,
                    Email = row["email"].ToString() ?? string.Empty,
                    Password = row["password"].ToString() ?? string.Empty,
                    UserRoles = new List<UserRole>(),
                    Fees = new List<Fee>()
                };
                usersDict[userId] = user;
            }

            if (row["role_name"] != DBNull.Value)
            {
                MapRoles(row, user);
            }

            if (row["fee_id"] != DBNull.Value)
            {
                MapFees(row, user);
            }
        }

        return usersDict.Values.ToList();
    }

    public async Task<bool> Delete(long id)
    {
        var pId = new SqlParameter("@Id", SqlDbType.BigInt) { Value = id };
        var result = await _dataAccess.WriteProcedure("dbo.usp_DeleteUser", [pId]);

        if (result == null) return false;
        return Convert.ToInt32(result) > 0;
    }

    public async Task<bool> Update(User obj)
    {
        var pId = new SqlParameter("@Id", SqlDbType.BigInt) { Value = obj.Id };
        var pFirst = new SqlParameter("@FirstName", SqlDbType.NVarChar, 100)
            { Value = obj.FirstName };
        var pLast = new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = obj.LastName };
        var pEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = obj.Email };
        var pPassword = new SqlParameter("@Password", SqlDbType.NVarChar, 255)
            { Value = obj.Password };

        var updateResult = await _dataAccess.WriteProcedure("dbo.usp_UpdateUser", [
            pId, pFirst, pLast, pEmail, pPassword
        ]);

        if (updateResult == null || Convert.ToInt32(updateResult) == 0) return false;

        var pDeleteUserId = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = obj.Id };
        await _dataAccess.WriteProcedure("dbo.usp_DeleteUserRoles", [pDeleteUserId]);

        var roleIdsTable = new DataTable();
        roleIdsTable.Columns.Add("Value", typeof(int));
        foreach (var role in obj.UserRoles)
        {
            roleIdsTable.Rows.Add((int)role + 1);
        }

        if (roleIdsTable.Rows.Count > 0)
        {
            var pUserId = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = obj.Id };
            var pRoleIds = new SqlParameter("@RoleIds", SqlDbType.Structured)
            {
                TypeName = "dbo.IntList",
                Value = roleIdsTable
            };

            await _dataAccess.WriteProcedure("dbo.usp_InsertUserRolesBulk", [pUserId, pRoleIds]);
        }

        return true;
    }

    #region BuildUtils

    private static User BuildUser(DataRow row)
    {
        return new User
        {
            Id = (long)row["id"],
            FirstName = row["first_name"].ToString() ?? string.Empty,
            LastName = row["last_name"].ToString() ?? string.Empty,
            Email = row["email"].ToString() ?? string.Empty,
            Password = row["password"].ToString() ?? string.Empty,
            UserRoles = new List<UserRole>(),
            Fees = new List<Fee>()
        };
    }

    private static void MapFees(DataRow row, User user)
    {
        var feeId = (long)row["fee_id"];

        if (user.Fees.Any(f => f.Id == feeId))
        {
            return;
        }

        var fee = new Fee
        {
            Id = feeId,
            StartDate = row["start_date"] != DBNull.Value
                ? DateOnly.FromDateTime((DateTime)row["start_date"])
                : default,
            EndDate = row["end_date"] != DBNull.Value
                ? DateOnly.FromDateTime((DateTime)row["end_date"])
                : default,
            Amount = row["amount"] != DBNull.Value ? (decimal)row["amount"] : 0,
            Payment = MapPayment(row)
        };
        user.Fees = user.Fees.Append(fee).ToList();
    }

    private static void MapRoles(DataRow row, User user)
    {
        var roleName = row["role_name"].ToString();
        if (!string.IsNullOrEmpty(roleName) &&
            user.UserRoles.All(r => r.ToString() != roleName))
        {
            user.UserRoles = user.UserRoles.Append(Enum.Parse<UserRole>(roleName))
                .ToList();
        }
    }

    private static Payment MapPayment(DataRow row)
    {
        return row["payment_method"].ToString() switch
        {
            "Card" => new CardPayment
            {
                Id = (long)row["payment_id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row["payment_date"]),
                Amount = (decimal)row["payment_amount"],
                Status = (string)row["payment_status"],
                Brand =
                    row["card_brand"] != DBNull.Value ? (string)row["card_brand"] : string.Empty,
                LastFourDigits = row["card_last4"] != DBNull.Value
                    ? int.Parse((string)row["card_last4"])
                    : 0
            },
            "Cash" => new CashPayment
            {
                Id = (long)row["payment_id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row["payment_date"]),
                Amount = (decimal)row["payment_amount"],
                Status = (string)row["payment_status"],
                ReceiptNumber = row["receipt_number"] != DBNull.Value
                    ? (string)row["receipt_number"]
                    : string.Empty
            },
            _ => new Payment
            {
                Id = (long)row["id"],
                PaymentDate = DateOnly.FromDateTime((DateTime)row["payment_date"]),
                Amount = (decimal)row["payment_amount"],
                Status = (string)row["payment_status"]
            }
        };
    }

    #endregion
}