using System.Xml.Linq;
using System.Globalization;
using GymManager_BE;

namespace GymManager_MPP;

public class XmlUserMapper : IMapper<User, long>
{
    private readonly string _filePath;
    private readonly object _fileLock = new();

    public XmlUserMapper(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "users.xml");
        EnsureFile();
    }

    #region FileManagement

    private void EnsureFile()
    {
        lock (_fileLock)
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    var doc = new XDocument(new XElement(Users));
                    doc.Save(_filePath);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[XmlUserMapper] Error en EnsureFile(): {ex}");
                throw;
            }
        }
    }

    private XDocument LoadDoc()
    {
        lock (_fileLock)
        {
            try
            {
                return XDocument.Load(_filePath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[XmlUserMapper] Error en LoadDoc(): {ex}");
                throw;
            }
        }
    }

    private void SaveDoc(XDocument doc)
    {
        lock (_fileLock)
        {
            try
            {
                doc.Save(_filePath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[XmlUserMapper] Error en SaveDoc(): {ex}");
                throw;
            }
        }
    }

    #endregion

    #region Constants

    private const string Users = "Users";
    private const string User = "User";
    private const string Id = "id";
    private const string FirstName = "FirstName";
    private const string LastName = "LastName";
    private const string Email = "Email";
    private const string Password = "Password";
    private const string Roles = "Roles";
    private const string Role = "Role";
    private const string Fees = "Fees";
    private const string Fee = "Fee";
    private const string StartDate = "StartDate";
    private const string EndDate = "EndDate";
    private const string Amount = "Amount";
    private const string Payment = "Payment";
    private const string Type = "Type";
    private const string PaymentId = "Id";
    private const string PaymentDate = "PaymentDate";
    private const string Status = "Status";
    private const string CardBrand = "CardBrand";
    private const string CardLast4 = "CardLast4";
    private const string ReceiptNumber = "ReceiptNumber";
    private const string? DateFormat = "yyyy-MM-dd";

    #endregion

    public Task<User> Create(User obj)
    {
        try
        {
            var doc = LoadDoc();
            var users = doc.Root ?? new XElement(Users);

            var maxId = users.Elements(User)
                .Select(x => (long?)x.Attribute("id") ?? 0)
                .DefaultIfEmpty(0)
                .Max();
            var newId = maxId + 1;
            obj.Id = newId;

            var userElem = new XElement(User,
                new XAttribute(Id, obj.Id),
                new XElement(FirstName, obj.FirstName ?? string.Empty),
                new XElement(LastName, obj.LastName ?? string.Empty),
                new XElement(Email, obj.Email ?? string.Empty),
                new XElement(Password, obj.Password ?? string.Empty),
                new XElement(Roles,
                    (obj.UserRoles ?? new List<UserRole>())
                    .Select(r => new XElement(Role, r.ToString()))
                ),
                new XElement(Fees,
                    (obj.Fees ?? new List<Fee>())
                    .Select(FeeToXElement)
                )
            );

            users.Add(userElem);
            SaveDoc(doc);

            return Task.FromResult(obj);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlUserMapper] Error en Create(...): {ex}");
            throw;
        }
    }

    public Task<User?> GetById(long id)
    {
        try
        {
            var doc = LoadDoc();
            var elem = doc.Root?
                .Elements(User)
                .FirstOrDefault(x => (long?)x.Attribute(Id) == id);

            if (elem == null) return Task.FromResult<User?>(null);
            return Task.FromResult<User?>(XElementToUser(elem));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlUserMapper] Error en GetById({id}): {ex}");
            throw;
        }
    }

    public Task<User?> GetByEmail(string email)
    {
        try
        {
            var doc = LoadDoc();
            var elem = doc.Root?
                .Elements(User)
                .FirstOrDefault(x => string.Equals((string?)x.Element(Email) ?? string.Empty, email,
                    StringComparison.OrdinalIgnoreCase));

            if (elem == null) return Task.FromResult<User?>(null);
            return Task.FromResult<User?>(XElementToUser(elem));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlUserMapper] Error en GetByEmail({email}): {ex}");
            throw;
        }
    }

    public Task<User?> GetByFeeId(long feeId)
    {
        try
        {
            var doc = LoadDoc();
            var elem = doc.Root?
                .Elements(User)
                .FirstOrDefault(u => u.Element(Fees)?
                    .Elements(Fee)
                    .Any(f => (long?)f.Attribute(Id) == feeId) == true);

            if (elem == null) return Task.FromResult<User?>(null);
            return Task.FromResult<User?>(XElementToUser(elem));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlUserMapper] Error en GetByFeeId({feeId}): {ex}");
            throw;
        }
    }

    public Task<List<User>> GetAll()
    {
        try
        {
            var doc = LoadDoc();
            var list = doc.Root?
                .Elements(User)
                .Select(XElementToUser)
                .ToList() ?? new List<User>();
            return Task.FromResult(list);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlUserMapper] Error en GetAll(): {ex}");
            throw;
        }
    }

    public Task<bool> Delete(long id)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root;
            var elem = root?.Elements(User).FirstOrDefault(x => (long?)x.Attribute(Id) == id);
            if (elem == null) return Task.FromResult(false);
            elem.Remove();
            SaveDoc(doc);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlUserMapper] Error en Delete({id}): {ex}");
            throw;
        }
    }

    public Task<bool> Update(User obj)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root;
            var elem = root?.Elements(User).FirstOrDefault(x => (long?)x.Attribute(Id) == obj.Id);
            if (elem == null) return Task.FromResult(false);

            elem.SetElementValue(FirstName, obj.FirstName ?? string.Empty);
            elem.SetElementValue(LastName, obj.LastName ?? string.Empty);
            elem.SetElementValue(Email, obj.Email ?? string.Empty);
            elem.SetElementValue(Password, obj.Password ?? string.Empty);

            var rolesElem = elem.Element(Roles);
            rolesElem?.Remove();
            elem.Add(new XElement(Roles,
                (obj.UserRoles ?? new List<UserRole>()).Select(r =>
                    new XElement(Role, r.ToString()))
            ));

            var feesElem = elem.Element(Fees);
            feesElem?.Remove();
            elem.Add(new XElement(Fees,
                (obj.Fees ?? new List<Fee>()).Select(FeeToXElement)
            ));

            SaveDoc(doc);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlUserMapper] Error en Update(Id={obj.Id}): {ex}");
            throw;
        }
    }


    #region BuildUtils

    private static XElement FeeToXElement(Fee f)
    {
        return new XElement(Fee,
            new XAttribute(Id, f.Id),
            new XElement(StartDate,
                f.StartDate.ToString(DateFormat, CultureInfo.InvariantCulture)),
            new XElement(EndDate, f.EndDate.ToString(DateFormat, CultureInfo.InvariantCulture)),
            new XElement(Amount, f.Amount.ToString(CultureInfo.InvariantCulture)),
            PaymentToXElement(f.Payment)
        );
    }

    private static XElement PaymentToXElement(Payment p)
    {
        if (p == null) return new XElement(Payment);
        var common = new List<XElement>
        {
            new(Type, p.MethodName),
            new(Id, p.Id),
            new(PaymentDate,
                p.PaymentDate.ToString(DateFormat, CultureInfo.InvariantCulture)),
            new(Amount, p.Amount.ToString(CultureInfo.InvariantCulture)),
            new(Status, p.Status ?? string.Empty)
        };

        if (p is CardPayment c)
        {
            common.Add(new XElement(CardBrand, c.Brand ?? string.Empty));
            common.Add(new XElement(CardLast4, c.LastFourDigits.ToString("D4")));
        }
        else if (p is CashPayment ca)
        {
            common.Add(new XElement(ReceiptNumber, ca.ReceiptNumber ?? string.Empty));
        }

        return new XElement(Payment, common);
    }

    private static User XElementToUser(XElement e)
    {
        var user = new User
        {
            Id = (long?)e.Attribute(Id) ?? 0,
            FirstName = (string?)e.Element(FirstName) ?? string.Empty,
            LastName = (string?)e.Element(LastName) ?? string.Empty,
            Email = (string?)e.Element(Email) ?? string.Empty,
            Password = (string?)e.Element(Password) ?? string.Empty,
            UserRoles = e.Element(Roles)?
                .Elements(Role)
                .Select(r => Enum.Parse<UserRole>((string)r))
                .ToList() ?? [],
            Fees = e.Element(Fees)?
                .Elements(Fee)
                .Select(XElementToFee)
                .ToList() ?? []
        };

        return user;
    }


    private static Fee XElementToFee(XElement x)
    {
        var fee = new Fee
        {
            Id = (long?)x.Attribute(Id) ?? 0,
            StartDate = ParseDateOnly((string?)x.Element(StartDate)),
            EndDate = ParseDateOnly((string?)x.Element(EndDate)),
            Amount = decimal.TryParse((string?)x.Element(Amount) ?? "0", NumberStyles.Any,
                CultureInfo.InvariantCulture, out var a)
                ? a
                : 0,
            Payment = XElementToPayment(x.Element(Payment))
        };
        return fee;
    }

    private static Payment XElementToPayment(XElement? x)
    {
        if (x == null) return null!;
        var type = (string?)x.Element(Type) ?? string.Empty;
        var id = (long?)x.Element(Id) ?? 0;
        var paymentDate = ParseDateOnly((string?)x.Element(PaymentDate));
        var amount = decimal.TryParse((string?)x.Element(Amount) ?? "0", NumberStyles.Any,
            CultureInfo.InvariantCulture, out var a)
            ? a
            : 0;
        var status = (string?)x.Element(Status) ?? string.Empty;

        return (type switch
        {
            var t when t.Equals("Card", StringComparison.OrdinalIgnoreCase) =>
                new CardPayment
                {
                    Id = id,
                    PaymentDate = paymentDate,
                    Amount = amount,
                    Status = status,
                    Brand = (string?)x.Element(CardBrand) ?? string.Empty,
                    LastFourDigits = int.TryParse((string?)x.Element(CardLast4), out var last4)
                        ? last4
                        : 0
                } as Payment,
            var t when t.Equals("Cash", StringComparison.OrdinalIgnoreCase) =>
                new CashPayment
                {
                    Id = id,
                    PaymentDate = paymentDate,
                    Amount = amount,
                    Status = status,
                    ReceiptNumber = (string?)x.Element(ReceiptNumber) ?? string.Empty
                } as Payment,
            _ => null
        })!;
    }

    private static DateOnly ParseDateOnly(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return default;
        if (DateOnly.TryParseExact(s, DateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var d))
            return d;
        if (DateOnly.TryParse(s, out d)) return d;
        return default;
    }

    #endregion
}