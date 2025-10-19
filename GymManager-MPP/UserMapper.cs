using System.Data;
using GymManager_BE;
using GymManager_DAL;
using GymManager_DDAL;

namespace GymManager_MPP;

public class UserMapper : IMapper<User, long>
{
    private readonly IDataAccess _dataAccess = new DataAccessConnected();
    private readonly IDisconnectedDataAccess _disconnectedDataAccess = DataAccessDisconnected.Instance;


    public Task<User> Create(User obj)
    {
        var query =
            $"INSERT INTO users (first_name, last_name, email, password) VALUES ('{obj.FirstName}', '{obj.LastName}', '{obj.Email}', '{obj.Password}'); SELECT SCOPE_IDENTITY();";
        return _dataAccess.Write(query)
            .ContinueWith(newId =>
            {
                obj.Id = decimal.ToInt64((decimal)newId.Result!);
                var findRolesQuery =
                    $"SELECT * FROM roles WHERE role_name IN ('{string.Join("','", obj.UserRoles.Select(r => r.ToString()))}');";
                var rolesDataSet = _disconnectedDataAccess.Read(findRolesQuery).Result;

                foreach (DataRow row in rolesDataSet.Tables[0].Rows)
                {
                    var insertRolesQuery =
                        $"INSERT INTO user_roles (user_id, role_id) VALUES ({obj.Id}, {row["id"]});";
                    _dataAccess.Write(insertRolesQuery).Wait();
                }

                return obj;
            });
    }

    public Task<User?> GetById(long id)
    {
        var query = $"""
                     SELECT
                         u.id,
                         u.first_name,
                         u.last_name,
                         u.email,
                         u.password,
                         r.role_name,
                         f.id AS fee_id,
                         f.start_date,
                         f.end_date,
                         f.amount,
                         f.user_id AS fee_user_id,
                         p.id as payment_id,
                         p.amount as payment_amount,
                         p.payment_date,
                         p.payment_method,
                         p.status as payment_status,
                         p.card_last4,
                         p.card_brand,
                         p.receipt_number
                         FROM users u
                         LEFT JOIN user_roles ur ON u.id = ur.user_id
                         LEFT JOIN roles r ON ur.role_id = r.id
                         LEFT JOIN fees f ON u.id = f.user_id
                         LEFT JOIN payments p ON
                         f.id = p.fee_id
                        WHERE u.id = {id};
                     """;

        return _disconnectedDataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                    return null;

                User? user = null;

                foreach (DataRow row in dataSet.Result.Tables[0].Rows)
                {
                    user ??= BuildUser(row);

                    if (row["role_name"] != DBNull.Value)
                    {
                        MapRoles(row, user);
                    }

                    if (row["fee_id"] != DBNull.Value)
                    {
                        MapFees(row, user, id);
                    }
                }

                return user;
            });
    }

    public Task<User?> GetByEmail(string email)
    {
        var query =
            $"SELECT * FROM [GymManager].[dbo].[users] WHERE email = '{email}';";
        return _disconnectedDataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                var row = dataSet.Result.Tables[0].Rows[0];
                return BuildUser(row);
            });
    }


    public Task<User?> GetByFeeId(long feeId)
    {
        var query =
            $"""
             SELECT u.* from users u 
             INNER JOIN fees f 
             ON u.id = f.user_id
             WHERE f.id = {feeId};
             """;
        return _disconnectedDataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                if (dataSet.Result.Tables.Count == 0 || dataSet.Result.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                var row = dataSet.Result.Tables[0].Rows[0];
                return BuildUser(row);
            });
    }

    public Task<List<User>> GetAll()
    {
        const string query = """
                             SELECT
                             	u.id AS user_id,
                             	u.first_name,
                             	u.last_name,
                             	u.email,
                             	u.password,
                             	r.role_name,
                             	f.id AS fee_id,
                             	f.start_date,
                             	f.end_date,
                             	f.amount,
                             	f.user_id AS fee_user_id,
                             	p.id as payment_id,
                             	p.amount as payment_amount,
                             	p.payment_date,
                             	p.payment_method,
                             	p.status as payment_status,
                             	p.card_last4,
                             	p.card_brand,
                             	p.receipt_number
                             FROM
                             	users u
                             LEFT JOIN user_roles ur ON
                             	u.id = ur.user_id
                             LEFT JOIN roles r ON
                             	ur.role_id = r.id
                             LEFT JOIN fees f ON
                             	u.id = f.user_id
                             LEFT JOIN payments p ON
                             	f.id = p.fee_id;
                             """;

        return _disconnectedDataAccess.Read(query)
            .ContinueWith(dataSet =>
            {
                var usersDict = new Dictionary<long, User>();

                foreach (DataRow row in dataSet.Result.Tables[0].Rows)
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
                        MapFees(row, user, userId);
                    }
                }

                return usersDict.Values.ToList();
            });
    }

    public Task<bool> Delete(long id)
    {
        var query = $"DELETE FROM users WHERE id = {id};";
        return _dataAccess.Write(query)
            .ContinueWith(result => result.Result != null);
    }

    public Task<bool> Update(User obj)
    {
        var query =
            $"UPDATE users SET first_name = '{obj.FirstName}', last_name = '{obj.LastName}', email = '{obj.Email}', password = '{obj.Password}' WHERE id = {obj.Id};";
        return _dataAccess.Write(query)
            .ContinueWith(result => result.Result != null)
            .ContinueWith(updateResult =>
            {
                if (!updateResult.Result) return false;
                var deleteRolesQuery = $"DELETE FROM user_roles WHERE user_id = {obj.Id};";
                _dataAccess.Write(deleteRolesQuery).Wait();
                foreach (var role in obj.UserRoles)
                {
                    var rolesQuery =
                        $"INSERT INTO user_roles (user_id, role_id) VALUES ({obj.Id}, {(int)role});";
                    _dataAccess.Write(rolesQuery).Wait();
                }

                return true;
            });
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

    private static void MapFees(DataRow row, User user, long userId)
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