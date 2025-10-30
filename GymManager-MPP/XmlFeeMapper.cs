using System.Xml.Linq;
using System.Globalization;
using GymManager_BE;
using Microsoft.Extensions.Logging;

namespace GymManager_MPP;

public class XmlFeeMapper : IMapper<Fee, long>
{
    private readonly string _filePath;
    private readonly object _fileLock = new();

    private readonly ILogger _iLogger = LoggerFactory
        .Create(builder => builder.AddConsole())
        .CreateLogger("XmlFeeMapper");

    public XmlFeeMapper(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "fees.xml");
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
                    var doc = new XDocument(new XElement("Fees"));
                    doc.Save(_filePath);
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex, "[XmlFeeMapper] Error en EnsureFile()");
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
                _iLogger.LogError(ex, "[XmlFeeMapper] Error en LoadDoc()");
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
                _iLogger.LogError(ex, "[XmlFeeMapper] Error en SaveDoc()");
                throw;
            }
        }
    }

    #endregion

    #region Constants

    private const string Id = "id";
    private const string UserId = "UserId";
    private const string Fees = "Fees";
    private const string User = "User";
    private const string Fee = "Fee";
    private const string StartDate = "StartDate";
    private const string EndDate = "EndDate";
    private const string Amount = "Amount";
    private const string Payment = "Payment";
    private const string Type = "Type";
    private const string PaymentDate = "PaymentDate";
    private const string Status = "Status";
    private const string CardBrand = "CardBrand";
    private const string CardLast4 = "CardLast4";
    private const string ReceiptNumber = "ReceiptNumber";
    private const string? DateFormat = "yyyy-MM-dd";

    #endregion

    public Task<Fee> Create(Fee obj)
    {
        throw new NotImplementedException();
    }

    public Task<Fee> Create(Fee obj, long userId)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root;

            var maxId = root!.Descendants(Fee)
                .Select(x => (long?)x.Attribute(Id) ?? 0)
                .DefaultIfEmpty(0)
                .Max();
            var newId = maxId + 1;
            obj.Id = newId;

            var user = root.Descendants(User)
                .FirstOrDefault(u => (long?)u.Attribute("id") == userId);

            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            user.Element(Fees)!.Add(FeeToXElement(obj));
            SaveDoc(doc);
            return Task.FromResult(obj);
        }
        catch (Exception ex)
        {
            _iLogger.LogError(ex, "[XmlFeeMapper] Error en Create(...)");
            throw;
        }
    }

    public Task<Fee?> GetById(long id)
    {
        try
        {
            var doc = LoadDoc();
            var elem = doc.Root?
                .Descendants(Fee)
                .FirstOrDefault(x => (long?)x.Attribute(Id) == id);

            if (elem == null) return Task.FromResult<Fee?>(null);
            return Task.FromResult<Fee?>(XElementToFee(elem));
        }
        catch (Exception ex)
        {
            _iLogger.LogError(ex, "[XmlFeeMapper] Error en GetById({id})", id);
            throw;
        }
    }

    public Task<List<Fee>> GetAll()
    {
        try
        {
            var doc = LoadDoc();
            var list = doc.Root?
                .Descendants(Fee)
                .Select(XElementToFee)
                .ToList() ?? [];
            return Task.FromResult(list);
        }
        catch (Exception ex)
        {
            _iLogger.LogError(ex, "[XmlFeeMapper] Error en GetAll()");
            throw;
        }
    }

    public Task<bool> Delete(long id)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root;
            var elem = root?.Descendants(Fee).FirstOrDefault(x => (long?)x.Attribute(Id) == id);
            if (elem == null) return Task.FromResult(false);
            elem.Remove();
            SaveDoc(doc);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _iLogger.LogError(ex, "[XmlFeeMapper] Error en Delete({id})", id);
            throw;
        }
    }

    public Task<bool> Update(Fee obj)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root;
            var elem = root?.Descendants(Fee).FirstOrDefault(x => (long?)x.Attribute(Id) == obj.Id);
            if (elem == null) return Task.FromResult(false);

            elem.SetAttributeValue(Id, obj.Id);
            elem.SetElementValue(StartDate,
                obj.StartDate.ToString(DateFormat, CultureInfo.InvariantCulture));
            elem.SetElementValue(EndDate,
                obj.EndDate.ToString(DateFormat, CultureInfo.InvariantCulture));
            elem.SetElementValue(Amount, obj.Amount.ToString(CultureInfo.InvariantCulture));

            var paymentElem = elem.Element(Payment);
            paymentElem?.Remove();
            elem.Add(PaymentToXElement(obj.Payment));

            SaveDoc(doc);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _iLogger.LogError(ex, "[XmlFeeMapper] Error en Update(Id={Id})", obj.Id);
            throw;
        }
    }

    public Task<List<Fee>> GetByUserId(long userId)
    {
        try
        {
            var doc = LoadDoc();
            var list = doc.Root?
                .Descendants(Fee)
                .Where(f => ((long?)f.Element("UserId") ?? 0) == userId)
                .Select(XElementToFee)
                .ToList() ?? [];
            return Task.FromResult(list);
        }
        catch (Exception ex)
        {
            _iLogger.LogError(ex, "[XmlFeeMapper] Error en GetByUserId({userId})", userId);
            throw;
        }
    }

    #region BuildUtils

    private static XElement FeeToXElement(Fee f)
    {
        var nodes = new List<object>
        {
            new XAttribute(Id, f.Id),
            // new XElement(UserId, userId),
            new XElement(StartDate,
                f.StartDate.ToString(DateFormat, CultureInfo.InvariantCulture)),
            new XElement(EndDate, f.EndDate.ToString(DateFormat, CultureInfo.InvariantCulture)),
            new XElement(Amount, f.Amount.ToString(CultureInfo.InvariantCulture)),
            PaymentToXElement(f.Payment)
        };

        return new XElement(Fee, nodes);
    }

    private static XElement PaymentToXElement(Payment? p)
    {
        if (p == null) return new XElement(Payment);
        var common = new List<XElement>
        {
            new(Type, p.MethodName),
            new("Id", p.Id),
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

    private static Payment? XElementToPayment(XElement? x)
    {
        if (x == null) return null;
        var type = (string?)x.Element(Type) ?? string.Empty;
        var id = (long?)x.Element("Id") ?? 0;
        var paymentDate = ParseDateOnly((string?)x.Element(PaymentDate));
        var amount = decimal.TryParse((string?)x.Element(Amount) ?? "0", NumberStyles.Any,
            CultureInfo.InvariantCulture, out var a)
            ? a
            : 0;
        var status = (string?)x.Element(Status) ?? string.Empty;

        if (type.Equals("Card", StringComparison.OrdinalIgnoreCase))
        {
            return new CardPayment
            {
                Id = id,
                PaymentDate = paymentDate,
                Amount = amount,
                Status = status,
                Brand = (string?)x.Element(CardBrand) ?? string.Empty,
                LastFourDigits = int.TryParse((string?)x.Element(CardLast4), out var last4)
                    ? last4
                    : 0
            };
        }

        if (type.Equals("Cash", StringComparison.OrdinalIgnoreCase))
        {
            return new CashPayment
            {
                Id = id,
                PaymentDate = paymentDate,
                Amount = amount,
                Status = status,
                ReceiptNumber = (string?)x.Element(ReceiptNumber) ?? string.Empty
            };
        }

        return null;
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