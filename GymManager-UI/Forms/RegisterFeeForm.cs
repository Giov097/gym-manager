using GymManager_BLL.Impl;

namespace GymManager.Forms;

using GymManager_BE;
using GymManager_BLL;

public partial class RegisterFeeForm : Form
{
    private readonly IUserService _userService;
    private readonly IFeeService _feeService;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentService _cashPaymentService;
    private readonly IPaymentService _cardPaymentService;

    public RegisterFeeForm(IUserService userService, IFeeService feeService,
        IPaymentService paymentService, IPaymentService cashPaymentService,
        IPaymentService cardPaymentService)
    {
        _userService = userService;
        _feeService = feeService;
        _paymentService = paymentService;
        _cashPaymentService = cashPaymentService;
        _cardPaymentService = cardPaymentService;
        InitializeComponent();
        LoadUsers();
        paymentTypeCombo.SelectedIndexChanged += PaymentTypeCombo_SelectedIndexChanged!;
        paymentCheck.CheckedChanged += PaymentCheck_CheckedChanged!;
        btnOk.Click += BtnOk_Click!;
        btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;
        PaymentFieldsVisibility();
    }

    private async void LoadUsers()
    {
        try
        {
            var users = await _userService.GetUsers();
            userCombo.DataSource = users;
            userCombo.DisplayMember = "FullName";
            userCombo.ValueMember = "Id";
        }
        catch (Exception e)
        {
            MessageBox.Show("Error al cargar usuarios: " + e.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PaymentCheck_CheckedChanged(object sender, EventArgs e)
    {
        PaymentFieldsVisibility();
    }

    private void PaymentTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        PaymentFieldsVisibility();
    }

    private void PaymentFieldsVisibility()
    {
        var showPayment = paymentCheck.Checked;
        paymentTypeCombo.Enabled = showPayment;
        paymentDatePicker.Enabled = showPayment;
        paymentAmountTxt.Enabled = showPayment;

        cashPanel.Visible = showPayment && paymentTypeCombo.SelectedItem?.ToString() == "Efectivo";
        cardPanel.Visible = showPayment && paymentTypeCombo.SelectedItem?.ToString() == "Tarjeta";
    }

    private async void BtnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (userCombo.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un usuario.", "Validación", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(amountTxt.Text) ||
                !decimal.TryParse(amountTxt.Text, out var amt) || amt <= 0)
            {
                MessageBox.Show("Ingrese un monto válido.", "Validación", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (startDatePicker.Value.Date >= endDatePicker.Value.Date)
            {
                MessageBox.Show("La fecha de inicio debe ser anterior a la fecha de fin.",
                    "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fee = new Fee
            {
                StartDate = DateOnly.FromDateTime(startDatePicker.Value),
                EndDate = DateOnly.FromDateTime(endDatePicker.Value),
                Amount = amt
            };

            if (paymentCheck.Checked)
            {
                Payment payment;
                if (paymentTypeCombo.SelectedItem?.ToString() == "Efectivo")
                {
                    payment = new CashPayment
                    {
                        PaymentDate = DateOnly.FromDateTime(paymentDatePicker.Value),
                        Amount = decimal.TryParse(paymentAmountTxt.Text, out var pay) ? pay : 0,
                        Status = "Pagado",
                        ReceiptNumber = receiptTxt.Text
                    };
                }
                else // Tarjeta
                {
                    payment = new CardPayment
                    {
                        PaymentDate = DateOnly.FromDateTime(paymentDatePicker.Value),
                        Amount = decimal.TryParse(paymentAmountTxt.Text, out var pay) ? pay : 0,
                        Status = "Pagado",
                        Brand = brandTxt.Text,
                        LastFourDigits = int.TryParse(lastFourTxt.Text, out var digits) ? digits : 0
                    };
                }

                var savedFee = await _feeService.AddFee(fee,
                    userCombo.SelectedValue is long userId ? userId : 0);
                switch (payment)
                {
                    case CashPayment cashPayment:
                        await _cashPaymentService.AddPayment(cashPayment, savedFee.Id);
                        break;
                    case CardPayment cardPayment:
                        await _cardPaymentService.AddPayment(cardPayment, savedFee.Id);
                        break;
                    default:
                        throw new InvalidOperationException("Tipo de pago no soportado.");
                }
            }
            else
            {
                await _feeService.AddFee(fee, userCombo.SelectedValue is long userId ? userId : 0);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception exception)
        {
            MessageBox.Show("Error al registrar la cuota: " + exception.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}