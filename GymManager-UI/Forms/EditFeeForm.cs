using GymManager_BE;
using GymManager_BLL;

namespace GymManager.Forms;

public partial class EditFeeForm : Form
{
    private readonly IUserService _userService;
    private readonly IFeeService _feeService;
    private readonly IPaymentService _paymentService;
    private readonly Fee _fee;

    public EditFeeForm(IUserService userService, IFeeService feeService,
        IPaymentService paymentService, Fee fee)
    {
        _userService = userService;
        _feeService = feeService;
        _paymentService = paymentService;
        _fee = fee;
        InitializeComponent();
        LoadUsers();
        LoadFeeData();
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
            userCombo.DisplayMember = "FirstName";
            userCombo.ValueMember = "Id";
            userCombo.SelectedValue = _fee.UserId;
        }
        catch (Exception e)
        {
            MessageBox.Show("Error al cargar usuarios: " + e.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadFeeData()
    {
        startDatePicker.Value = _fee.StartDate.ToDateTime(TimeOnly.MinValue);
        endDatePicker.Value = _fee.EndDate.ToDateTime(TimeOnly.MinValue);
        amountTxt.Text = _fee.Amount.ToString("0.00");

        if (_fee.Payment != null)
        {
            paymentCheck.Checked = true;
            paymentDatePicker.Value = _fee.Payment.PaymentDate.ToDateTime(TimeOnly.MinValue);
            paymentAmountTxt.Text = _fee.Payment.Amount.ToString("0.00");
            switch (_fee.Payment)
            {
                case CashPayment cash:
                    paymentTypeCombo.SelectedIndex = 0;
                    receiptTxt.Text = cash.ReceiptNumber;
                    break;
                case CardPayment card:
                    paymentTypeCombo.SelectedIndex = 1;
                    brandTxt.Text = card.Brand;
                    lastFourTxt.Text = card.LastFourDigits.ToString();
                    break;
            }
        }
        else
        {
            paymentCheck.Checked = false;
            paymentTypeCombo.SelectedIndex = 0;
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
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _fee.StartDate = DateOnly.FromDateTime(startDatePicker.Value);
            _fee.EndDate = DateOnly.FromDateTime(endDatePicker.Value);
            _fee.Amount = amt;
            _fee.UserId = (long)userCombo.SelectedValue;

            if (paymentCheck.Checked)
            {
                Payment payment;
                if (paymentTypeCombo.SelectedItem?.ToString() == "Efectivo")
                {
                    payment = new CashPayment
                    {
                        Id = _fee.Payment?.Id ?? 0,
                        FeeId = _fee.Id,
                        PaymentDate = DateOnly.FromDateTime(paymentDatePicker.Value),
                        Amount = decimal.TryParse(paymentAmountTxt.Text, out var pay) ? pay : 0,
                        Status = "Pagado",
                        ReceiptNumber = receiptTxt.Text
                    };
                }
                else
                {
                    payment = new CardPayment
                    {
                        Id = _fee.Payment?.Id ?? 0,
                        FeeId = _fee.Id,
                        PaymentDate = DateOnly.FromDateTime(paymentDatePicker.Value),
                        Amount = decimal.TryParse(paymentAmountTxt.Text, out var pay) ? pay : 0,
                        Status = "Pagado",
                        Brand = brandTxt.Text,
                        LastFourDigits = int.TryParse(lastFourTxt.Text, out var digits) ? digits : 0
                    };
                }

                if (_fee.Payment == null)
                {
                    var savedPayment = await _paymentService.AddPayment(payment);
                    _fee.Payment = savedPayment;
                }
                else
                {
                    var savedPayment = await _paymentService.UpdatePayment(payment.Id, payment);
                    _fee.Payment = savedPayment;
                }
            }
            else
            {
                if (_fee.Payment != null)
                {
                    await _paymentService.DeletePayment(_fee.Payment.Id);
                    _fee.Payment = null;
                }
            }

            await _feeService.UpdateFee(_fee.Id, _fee);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al editar la cuota: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}