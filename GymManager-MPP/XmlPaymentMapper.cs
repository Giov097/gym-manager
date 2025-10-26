using System.Xml.Linq;
using System.Globalization;
using GymManager_BE;

namespace GymManager_MPP;

public class XmlPaymentMapper : IMapper<Payment, long>
{
    private readonly string _filePath;
    private readonly object _fileLock = new();

    public XmlPaymentMapper(string? filePath = null)
    {
        _filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "payments.xml");
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
                    var doc = new XDocument(new XElement("Payments"));
                    doc.Save(_filePath);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[XmlPaymentMapper] Error en EnsureFile(): {ex}");
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
                Console.Error.WriteLine($"[XmlPaymentMapper] Error en LoadDoc(): {ex}");
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
                Console.Error.WriteLine($"[XmlPaymentMapper] Error en SaveDoc(): {ex}");
                throw;
            }
        }
    }

    #endregion

    #region Constants

    private const string Id = "id";
    private const string Payments = "Payments";
    private const string PaymentElem = "Payment";
    private const string FeeId = "FeeId";
    private const string PaymentDate = "PaymentDate";
    private const string Amount = "Amount";
    private const string Type = "Type";
    private const string Status = "Status";
    private const string CardLast4 = "CardLast4";
    private const string CardBrand = "CardBrand";
    private const string ReceiptNumber = "ReceiptNumber";
    private const string DateFormat = "yyyy-MM-dd";

    #endregion

    public Task<Payment> Create(Payment obj)
    {
        throw new NotImplementedException();
    }

    public Task<Payment> Create(Payment obj, long feeId)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root ?? new XElement(Payments);

            var maxId = root.Elements(PaymentElem)
                .Select(x => (long?)x.Attribute(Id) ?? 0)
                .DefaultIfEmpty(0)
                .Max();
            var newId = maxId + 1;
            obj.Id = newId;

            root.Add(PaymentToXElement(obj, feeId));
            SaveDoc(doc);
            return Task.FromResult(obj);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(
                $"[XmlPaymentMapper] Error en Create(..., feeId={feeId}): {ex}");
            throw;
        }
    }

    public Task<Payment?> GetById(long id)
    {
        try
        {
            var doc = LoadDoc();
            var elem = doc.Root?
                .Elements(PaymentElem)
                .FirstOrDefault(x => (long?)x.Attribute(Id) == id);

            if (elem == null) return Task.FromResult<Payment?>(null);
            return Task.FromResult<Payment?>(XElementToPayment(elem));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlPaymentMapper] Error en GetById({id}): {ex}");
            throw;
        }
    }

    public Task<List<Payment>> GetAll()
    {
        try
        {
            var doc = LoadDoc();
            var list = doc.Root?
                .Elements(PaymentElem)
                .Select(XElementToPayment)
                .ToList() ?? new List<Payment>();
            return Task.FromResult(list);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlPaymentMapper] Error en GetAll(): {ex}");
            throw;
        }
    }

    public Task<bool> Update(Payment obj)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root;
            var elem = root?.Elements(PaymentElem)
                .FirstOrDefault(x => (long?)x.Attribute(Id) == obj.Id);
            if (elem == null) return Task.FromResult(false);

            elem.SetElementValue(PaymentDate,
                obj.PaymentDate.ToString(DateFormat, CultureInfo.InvariantCulture));
            elem.SetElementValue(Amount, obj.Amount.ToString(CultureInfo.InvariantCulture));
            elem.SetElementValue(Status, obj.Status ?? string.Empty);

            var typeVal = obj.GetType() == typeof(CardPayment) ? "Card" :
                obj.GetType() == typeof(CashPayment) ? "Cash" : "Generic";
            elem.SetElementValue(Type, typeVal);

            elem.Element(CardBrand)?.Remove();
            elem.Element(CardLast4)?.Remove();
            elem.Element(ReceiptNumber)?.Remove();

            if (obj is CardPayment card)
            {
                elem.Add(new XElement(CardBrand, card.Brand ?? string.Empty));
                elem.Add(new XElement(CardLast4, card.LastFourDigits.ToString("D4")));
            }
            else if (obj is CashPayment cash)
            {
                elem.Add(new XElement(ReceiptNumber, cash.ReceiptNumber ?? string.Empty));
            }

            SaveDoc(doc);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlPaymentMapper] Error en Update(Id={obj.Id}): {ex}");
            throw;
        }
    }

    public Task<bool> Delete(long id)
    {
        try
        {
            var doc = LoadDoc();
            var root = doc.Root;
            var elem = root?.Elements(PaymentElem)
                .FirstOrDefault(x => (long?)x.Attribute(Id) == id);
            if (elem == null) return Task.FromResult(false);
            elem.Remove();
            SaveDoc(doc);
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XmlPaymentMapper] Error en Delete({id}): {ex}");
            throw;
        }
    }

    public Task<List<Payment>> Search(DateOnly from, DateOnly to, long userId)
    {
        try
        {
            var payments = new List<Payment>();
            var doc = LoadDoc();
            if (doc.Root == null) return Task.FromResult(payments);

            var elems = doc.Root.Elements(PaymentElem).AsEnumerable();

            if (from != default)
            {
                elems = elems.Where(e =>
                {
                    var pd = ParseDateOnly((string?)e.Element(PaymentDate));
                    return pd != default && pd >= from;
                });
            }

            if (to != default)
            {
                elems = elems.Where(e =>
                {
                    var pd = ParseDateOnly((string?)e.Element(PaymentDate));
                    return pd != default && pd <= to;
                });
            }

            if (userId != 0)
            {
                var feesPath = Path.Combine(AppContext.BaseDirectory, "fees.xml");
                if (!File.Exists(feesPath))
                {
                    // No fees file -> no results for user filter
                    return Task.FromResult(new List<Payment>());
                }

                var feesDoc = XDocument.Load(feesPath);
                var feeIds = feesDoc.Root?
                    .Elements("Fee")
                    .Where(f => ((long?)f.Element("UserId") ?? 0) == userId)
                    .Select(f => (long?)f.Attribute("id") ?? 0)
                    .Where(id => id != 0)
                    .ToHashSet() ?? new HashSet<long>();

                if (feeIds.Count == 0) return Task.FromResult(new List<Payment>());

                elems = elems.Where(e => feeIds.Contains((long?)e.Element(FeeId) ?? 0));
            }

            payments.AddRange(elems.Select(XElementToPayment));
            return Task.FromResult(payments);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(
                $"[XmlPaymentMapper] Error en Search(from={from}, to={to}, userId={userId}): {ex}");
            throw;
        }
    }

    #region BuildUtils

    private static XElement PaymentToXElement(Payment p, long feeId)
    {
        var nodes = new List<object>
        {
            new XAttribute(Id, p.Id),
            new XElement(FeeId, feeId),
            new XElement(PaymentDate,
                p.PaymentDate.ToString(DateFormat, CultureInfo.InvariantCulture)),
            new XElement(Amount, p.Amount.ToString(CultureInfo.InvariantCulture)),
            new XElement(Status, p.Status ?? string.Empty)
        };

        var typeVal = p.GetType() == typeof(CardPayment) ? "Card" :
            p.GetType() == typeof(CashPayment) ? "Cash" : "Generic";
        nodes.Add(new XElement(Type, typeVal));

        if (p is CardPayment c)
        {
            nodes.Add(new XElement(CardBrand, c.Brand ?? string.Empty));
            nodes.Add(new XElement(CardLast4, c.LastFourDigits.ToString("D4")));
        }
        else if (p is CashPayment ca)
        {
            nodes.Add(new XElement(ReceiptNumber, ca.ReceiptNumber ?? string.Empty));
        }

        return new XElement(PaymentElem, nodes);
    }

    private static Payment XElementToPayment(XElement x)
    {
        var type = (string?)x.Element(Type) ?? string.Empty;
        var id = (long?)x.Attribute(Id) ?? 0;
        var paymentDate = ParseDateOnly((string?)x.Element(PaymentDate));
        var amount = decimal.TryParse((string?)x.Element(Amount) ?? "0", NumberStyles.Any,
            CultureInfo.InvariantCulture, out var a)
            ? a
            : 0;
        var status = (string?)x.Element(Status) ?? string.Empty;

        return type switch
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
            _ => new Payment
            {
                Id = id,
                PaymentDate = paymentDate,
                Amount = amount,
                Status = status
            }
        };
    }

    private static DateOnly ParseDateOnly(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return default;
        if (DateOnly.TryParseExact(s, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var d) || DateOnly.TryParse(s, out d))
            return d;
        return default;
    }

    #endregion
}