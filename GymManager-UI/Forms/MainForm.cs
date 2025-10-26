using GymManager_BE;
using GymManager_BLL;
using GymManager_BLL.Impl;
using Microsoft.Extensions.Logging;

namespace GymManager.Forms;

public partial class MainForm : Form
{
    #region Services

    private readonly IUserService _userService;
    private readonly IUserService _xmlUserService;
    private readonly IFeeService _feeService;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentService _cashPaymentService;
    private readonly IPaymentService _cardPaymentService;
    private readonly ILogger _logger;

    #endregion

    #region Constants

    private const string? SuccessCaption = "Éxito";
    private const string? ErrorCaption = "Error";
    private const string Alumno = "Alumno";
    private const string Estado = "Estado";
    private const string Metodo = "Metodo";
    private const string Fecha = "Fecha";
    private const string Monto = "Monto";
    private const string Efectivo = "Efectivo";
    private const string Tarjeta = "Tarjeta";
    private const string Desde = "Desde";
    private const string Hasta = "Hasta";
    private const string Pagado = "Pagado";

    #endregion

    public MainForm(IUserService userService, IUserService xmlUserService, IFeeService feeService,
        IPaymentService paymentService, IPaymentService cashPaymentService,
        IPaymentService cardPaymentService)
    {
        var factory = LoggerFactory.Create(builder => builder.AddConsole());
        _logger = factory.CreateLogger("MainForm");
        _userService = userService;
        _feeService = feeService;
        _paymentService = paymentService;
        _cashPaymentService = cashPaymentService;
        _cardPaymentService = cardPaymentService;
        _xmlUserService = xmlUserService;
        InitializeComponent();
        greetingLbl.Enabled = true;
        greetingLbl.Visible = true;
        greetingLbl.Text = $"¡Hola, {SessionManager.CurrentUser!.FirstName}!";
        greetingLbl.Click += (_, _) => greetingMenu.Show(greetingLbl, 0, greetingLbl.Height);
        Load += MainForm_Load!;
        feesGridView.CellClick += FeesGridView_CellClick!;
        newUserBtn.Click += BtnAddUser_Click!;
        editUserBtn.Click += BtnEditUser_Click!;
        logoutMenuItem.Click += (_, _) =>
        {
            SessionManager.CurrentUser = null;
            var loginForm = new LoginForm();
            loginForm.Show();
            Hide();
        };
        exitMenuItem.Click += (_, _) => Application.Exit();
        registerFeeBtn.Click += RegisterFeeBtn_Click!;
        editFeeBtn.Click += EditFeeBtn_Click!;
        myDataTabControl.SelectedIndexChanged += MyDataTabControl_SelectedIndexChanged!;

        var monthLabel = new Label { Text = "Mes:", Location = new Point(10, 10), AutoSize = true };
        var monthComboBox = new ComboBox
        {
            Location = new Point(70, 10),
            Size = new Size(100, 23),
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        var yearLabel = new Label { Text = "Año:", Location = new Point(190, 10), AutoSize = true };
        var yearComboBox = new ComboBox
        {
            Location = new Point(240, 10),
            Size = new Size(100, 23),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        var currentYear = DateTime.Now.Year;
        for (var y = currentYear - 5; y <= currentYear + 1; y++)
            yearComboBox.Items.Add(y);
        for (var m = 1; m <= 12; m++)
            monthComboBox.Items.Add(
                new DateTime(1, m, 1, 0, 0, 0, DateTimeKind.Local).ToString("MMMM"));
        monthComboBox.SelectedIndex = DateTime.Now.Month - 1;
        yearComboBox.SelectedItem = currentYear;

        var fromLabel = new Label
            { Text = "Desde:", Location = new Point(10, 10), AutoSize = true };
        var fromDatePicker = new DateTimePicker
        {
            Location = new Point(70, 10),
            Size = new Size(120, 23),
            Format = DateTimePickerFormat.Short
        };
        var toLabel = new Label { Text = "Hasta:", Location = new Point(210, 10), AutoSize = true };
        var toDatePicker = new DateTimePicker
        {
            Location = new Point(260, 10),
            Size = new Size(120, 23),
            Format = DateTimePickerFormat.Short
        };
        var methodLabel = new Label
            { Text = "Método:", Location = new Point(10, 40), AutoSize = true };
        var methodComboBox = new ComboBox
        {
            Location = new Point(70, 40),
            Size = new Size(120, 23),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        methodComboBox.Items.AddRange([Efectivo, Tarjeta]);
        methodComboBox.SelectedIndex = 0;

        btnGenerateReport.Click += BtnGenerateReport_Click!;

        var userLabel = new Label
            { Text = "Alumno:", Location = new Point(10, 10), AutoSize = true };
        var userComboBox = new ComboBox
        {
            Location = new Point(70, 10),
            Size = new Size(250, 23),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        Task.Run(async () =>
        {
            var users = await _userService.GetUsers();
            Invoke(() =>
            {
                userComboBox.Items.Clear();
                foreach (var u in users)
                {
                    userComboBox.Items.Add(new
                        { Text = $"{u.FirstName} {u.LastName}", Value = u.Id });
                }

                userComboBox.DisplayMember = "Text";
                userComboBox.ValueMember = "Value";
                if (userComboBox.Items.Count > 0)
                    userComboBox.SelectedIndex = 0;
            });
        });

        reportParamsPanel.Controls.Clear();
        reportParamsPanel.Controls.Add(monthLabel);
        reportParamsPanel.Controls.Add(monthComboBox);
        reportParamsPanel.Controls.Add(yearLabel);
        reportParamsPanel.Controls.Add(yearComboBox);

        var rangeFromLabel = new Label
            { Text = "Desde:", Location = new Point(10, 10), AutoSize = true };
        var rangeFromDatePicker = new DateTimePicker
        {
            Location = new Point(70, 10),
            Size = new Size(120, 23),
            Format = DateTimePickerFormat.Short
        };
        var rangeToLabel = new Label
            { Text = "Hasta:", Location = new Point(210, 10), AutoSize = true };
        var rangeToDatePicker = new DateTimePicker
        {
            Location = new Point(260, 10),
            Size = new Size(120, 23),
            Format = DateTimePickerFormat.Short
        };

        reportTypeComboBox.SelectedIndexChanged += (_, _) =>
        {
            reportParamsPanel.Controls.Clear();
            switch (reportTypeComboBox.SelectedItem?.ToString())
            {
                case "Recaudación mensual":
                    reportParamsPanel.Controls.Add(monthLabel);
                    reportParamsPanel.Controls.Add(monthComboBox);
                    reportParamsPanel.Controls.Add(yearLabel);
                    reportParamsPanel.Controls.Add(yearComboBox);
                    break;
                case "Pagos por método":
                    reportParamsPanel.Controls.Add(fromLabel);
                    reportParamsPanel.Controls.Add(fromDatePicker);
                    reportParamsPanel.Controls.Add(toLabel);
                    reportParamsPanel.Controls.Add(toDatePicker);
                    reportParamsPanel.Controls.Add(methodLabel);
                    reportParamsPanel.Controls.Add(methodComboBox);
                    break;
                case "Historial de pagos de un alumno":
                    reportParamsPanel.Controls.Add(userLabel);
                    reportParamsPanel.Controls.Add(userComboBox);
                    break;
                case "Alumnos con más deuda":
                case "Cuotas impagas":
                    break;
                case "Pagos por rango de fechas":
                    reportParamsPanel.Controls.Add(rangeFromLabel);
                    reportParamsPanel.Controls.Add(rangeFromDatePicker);
                    reportParamsPanel.Controls.Add(rangeToLabel);
                    reportParamsPanel.Controls.Add(rangeToDatePicker);
                    break;
                default:
                    break;
            }
        };
        reportParamsPanel.Controls.Clear();
        switch (reportTypeComboBox.SelectedItem?.ToString())
        {
            case "Recaudación mensual":
                reportParamsPanel.Controls.Add(monthLabel);
                reportParamsPanel.Controls.Add(monthComboBox);
                reportParamsPanel.Controls.Add(yearLabel);
                reportParamsPanel.Controls.Add(yearComboBox);
                break;
            case "Pagos por método":
                reportParamsPanel.Controls.Add(fromLabel);
                reportParamsPanel.Controls.Add(fromDatePicker);
                reportParamsPanel.Controls.Add(toLabel);
                reportParamsPanel.Controls.Add(toDatePicker);
                reportParamsPanel.Controls.Add(methodLabel);
                reportParamsPanel.Controls.Add(methodComboBox);
                break;
            case "Historial de pagos de un alumno":
                reportParamsPanel.Controls.Add(userLabel);
                reportParamsPanel.Controls.Add(userComboBox);
                break;
            case "Pagos por rango de fechas":
                reportParamsPanel.Controls.Add(rangeFromLabel);
                reportParamsPanel.Controls.Add(rangeFromDatePicker);
                reportParamsPanel.Controls.Add(rangeToLabel);
                reportParamsPanel.Controls.Add(rangeToDatePicker);
                break;
            default:
                break;
        }
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            var users = await _userService.GetUsers();
            FillUsersDataGrid(users);
            var fees = await _feeService.GetFees();
            FillFeesDataGrid(fees);
        }
        catch (Exception exception)
        {
            MessageBox.Show("Error al cargar datos: " + exception.Message, ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FillFeesDataGrid(List<Fee> fees)
    {
        feesGridView.DataSource = fees.Select(f => new
        {
            f.Id,
            f.StartDate,
            f.EndDate,
            f.Amount,
            PaymentStatus = f.Payment?.Status,
            ViewUser = "👁️"
        }).ToList();

        feesGridView.Columns["Id"]!.HeaderText = "ID";
        feesGridView.Columns["Id"]!.Width = 50;
        feesGridView.Columns["StartDate"]!.HeaderText = Desde;
        feesGridView.Columns["StartDate"]!.Width = 100;
        feesGridView.Columns["EndDate"]!.HeaderText = Hasta;
        feesGridView.Columns["EndDate"]!.Width = 100;
        feesGridView.Columns["Amount"]!.HeaderText = Monto;
        feesGridView.Columns["Amount"]!.Width = 80;
        feesGridView.Columns["ViewUser"]!.HeaderText = "Usuario";
        feesGridView.Columns["ViewUser"]!.Width = 80;
        feesGridView.Columns["PaymentStatus"]!.HeaderText = "Estado Pago";
        feesGridView.Columns["PaymentStatus"]!.Width = 100;
    }

    private void FillUsersDataGrid(List<User> users)
    {
        usersGridView.DataSource = users.Select(u => new
        {
            u.Id,
            u.FirstName,
            u.LastName,
            u.Email,
            Roles = string.Join(", ", u.UserRoles.Select(r => r.GetRoleName()))
        }).ToList();

        usersGridView.Columns["Id"]!.HeaderText = "ID";
        usersGridView.Columns["Id"]!.Width = 50;
        usersGridView.Columns["FirstName"]!.HeaderText = "Nombre";
        usersGridView.Columns["FirstName"]!.Width = 120;
        usersGridView.Columns["LastName"]!.HeaderText = "Apellido";
        usersGridView.Columns["LastName"]!.Width = 120;
        usersGridView.Columns["Email"]!.HeaderText = "Correo";
        usersGridView.Columns["Email"]!.Width = 180;
        usersGridView.Columns["Roles"]!.HeaderText = "Roles";
        usersGridView.Columns["Roles"]!.Width = 120;
    }

    private async void FeesGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e is { RowIndex: >= 0, ColumnIndex: >= 0 } &&
                feesGridView.Columns[e.ColumnIndex].Name == "ViewUser")
            {
                var feeId = (long)feesGridView.Rows[e.RowIndex].Cells["Id"].Value;
                var user = await _userService.GetUserByFeeId(feeId);

                var userForm = new UserDetailsForm(user);
                userForm.ShowDialog();
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show("Error al cargar datos del usuario: " + exception.Message, ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnAddUser_Click(object sender, EventArgs e)
    {
        try
        {
            var createForm = new CreateUserForm();
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                var newUser = createForm.CreatedUser;
                await _userService.CreateUser(newUser!);
                try
                {
                    await _xmlUserService.CreateUser(newUser!);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error duplicating user: {Message}", ex.Message);
                }

                MessageBox.Show("Usuario creado con éxito", SuccessCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                var users = await _userService.GetUsers();
                FillUsersDataGrid(users);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al crear el usuario: {ex.Message}", ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnEditUser_Click(object sender, EventArgs e)
    {
        try
        {
            if (usersGridView.CurrentRow == null) return;
            var userId = (long)usersGridView.CurrentRow.Cells["Id"].Value;
            var user = await _userService.GetUserById(userId);

            var editForm = new EditUserForm(user);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                var editedUser = editForm.EditedUser;
                await _userService.UpdateUser(userId, editedUser);
                MessageBox.Show("Usuario modificado con éxito", SuccessCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                var users = await _userService.GetUsers();
                FillUsersDataGrid(users);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al editar el usuario: {ex.Message}", ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void RegisterFeeBtn_Click(object sender, EventArgs e)
    {
        try
        {
            var registerForm = new RegisterFeeForm(_userService, _feeService, _paymentService,
                _cashPaymentService, _cardPaymentService);
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Cuota registrada con éxito", SuccessCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                var fees = await _feeService.GetFees();
                FillFeesDataGrid(fees);
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show("Error al registrar la cuota: " + exception.Message, ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void EditFeeBtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (feesGridView.CurrentRow == null) return;
            var feeId = (long)feesGridView.CurrentRow.Cells["Id"].Value;
            var fee = await _feeService.GetFeeById(feeId);

            var editForm = new EditFeeForm(_userService, _feeService, _paymentService, fee);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Cuota modificada con éxito", SuccessCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                var fees = await _feeService.GetFees();
                FillFeesDataGrid(fees);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al editar la cuota: {ex.Message}", ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void MyDataTabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (myDataTabControl.SelectedTab == tabPage3)
            {
                var user = SessionManager.CurrentUser!;
                nameTxt.Text = user.FirstName;
                lastNameTxt.Text = user.LastName;
                emailTxt.Text = user.Email;
                rolesTxt.Text = string.Join(", ", user.UserRoles.Select(r => r.GetRoleName()));

                var fees =
                    await _feeService.SearchFees(DateOnly.MinValue, DateOnly.MaxValue, user.Id);
                feesUserGridView.DataSource = fees.Select(f => new
                {
                    De = f.StartDate,
                    Hasta = f.EndDate,
                    Monto = f.Amount,
                    Estado = f.Payment?.Status
                }).ToList();

                var payments =
                    await _paymentService.SearchPayments(DateOnly.MinValue, DateOnly.MaxValue,
                        user.Id);
                paymentsUserGridView.DataSource = payments.Select(p => new
                {
                    Fecha = p.PaymentDate,
                    Monto = p.Amount,
                    Estado = p.Status
                }).ToList();
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show("Error al cargar tus datos: " + exception.Message, ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            switch (reportTypeComboBox.SelectedItem?.ToString())
            {
                case "Recaudación mensual":
                {
                    await HandleMonthReceipts();
                    break;
                }
                case "Alumnos con más deuda":
                {
                    await HandleUsersWithMostDebt();
                    break;
                }
                case "Pagos por método":
                {
                    await HandlePaymentsByMethod();
                    break;
                }
                case "Cuotas impagas":
                {
                    await HandleUnpaidFees();
                    break;
                }
                case "Historial de pagos de un alumno":
                {
                    await HandleUserPaymentHistory();
                    break;
                }
                case "Pagos por rango de fechas":
                {
                    await HandlePaymentsByDateRange();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al generar el reporte: {ex.Message}", ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task HandlePaymentsByMethod()
    {
        var fromDatePicker = reportParamsPanel.Controls.OfType<DateTimePicker>().First();
        var toDatePicker = reportParamsPanel.Controls.OfType<DateTimePicker>().Last();
        var methodComboBox = reportParamsPanel.Controls.OfType<ComboBox>()
            .FirstOrDefault(cb => cb.Items.Contains(Efectivo));

        var from = DateOnly.FromDateTime(fromDatePicker.Value.Date);
        var to = DateOnly.FromDateTime(toDatePicker.Value.Date);
        var method = methodComboBox?.SelectedItem?.ToString();

        var payments = await _paymentService.SearchPayments(from, to, 0);

        IEnumerable<object> filteredPayments;
        if (method == Efectivo)
        {
            filteredPayments = payments
                .Where(p => p.GetType().Name == "CashPayment")
                .Select(p => new
                {
                    Fecha = p.PaymentDate,
                    Monto = p.Amount,
                    Estado = p.Status,
                    Metodo = Efectivo
                });
        }
        else
        {
            filteredPayments = payments
                .Where(p => p.GetType().Name == "CardPayment")
                .Select(p => new
                {
                    Fecha = p.PaymentDate,
                    Monto = p.Amount,
                    Estado = p.Status,
                    Metodo = Tarjeta
                });
        }

        reportGridView.DataSource = filteredPayments.ToList();
        if (reportGridView.Columns[Fecha] != null)
            reportGridView.Columns[Fecha]!.HeaderText = Fecha;
        if (reportGridView.Columns[Monto] != null)
            reportGridView.Columns[Monto]!.HeaderText = Monto;
        if (reportGridView.Columns[Estado] != null)
            reportGridView.Columns[Estado]!.HeaderText = Estado;
        if (reportGridView.Columns[Metodo] != null)
            reportGridView.Columns[Metodo]!.HeaderText = "Método";
    }

    private async Task HandleUsersWithMostDebt()
    {
        var users = await _userService.GetUsers();

        var debts = users.Select(u =>
            {
                var deuda = u.Fees.Sum(f =>
                    f.Payment == null ? f.Amount : Math.Max(0, f.Amount - f.Payment.Amount)
                );
                return new
                {
                    Alumno = $"{u.FirstName} {u.LastName}",
                    Deuda = deuda
                };
            })
            .Where(d => d.Deuda > 0)
            .OrderByDescending(d => d.Deuda)
            .ToList();

        reportGridView.DataSource = debts;
        if (reportGridView.Columns[Alumno] != null)
            reportGridView.Columns[Alumno]!.HeaderText = Alumno;
        if (reportGridView.Columns["Deuda"] != null)
            reportGridView.Columns["Deuda"]!.HeaderText = "Deuda ($)";
    }

    private async Task HandleMonthReceipts()
    {
        var monthComboBox = reportParamsPanel.Controls.OfType<ComboBox>().First();
        var yearComboBox = reportParamsPanel.Controls.OfType<ComboBox>().Last();

        var month = monthComboBox.SelectedIndex + 1;
        var year = int.Parse(yearComboBox.SelectedItem?.ToString()!);

        var from = new DateOnly(year, month, 1);
        var to = new DateOnly(year, month, DateTime.DaysInMonth(year, month));

        var payments = await _paymentService.SearchPayments(from, to, 0);
        var total = payments.Sum(p => p.Amount);

        reportGridView.DataSource = new[]
        {
            new { Mes = from.ToString("MMMM"), Año = year, TotalRecaudado = total }
        };
        reportGridView.Columns["TotalRecaudado"]!.HeaderText = "Total Recaudado";
    }

    private async Task HandleUnpaidFees()
    {
        var users = await _userService.GetUsers();

        var unpaidFees = users
            .SelectMany(user =>
            {
                var fees = user.Fees.Where(f => f.Payment != null && f.Payment.Amount < f.Amount)
                    .ToList();
                return fees.Select(f => new
                {
                    Alumno = $"{user.FirstName} {user.LastName}",
                    Desde = f.StartDate,
                    Hasta = f.EndDate,
                    Monto = f.Amount,
                    Pagado = f.Payment?.Amount ?? 0,
                    Estado = f.Payment == null ? "Impaga" : "Parcial"
                }).ToList();
            })
            .ToList();

        reportGridView.DataSource = unpaidFees;
        if (reportGridView.Columns[Alumno] != null)
            reportGridView.Columns[Alumno]!.HeaderText = Alumno;
        if (reportGridView.Columns[Desde] != null)
            reportGridView.Columns[Desde]!.HeaderText = Desde;
        if (reportGridView.Columns[Hasta] != null)
            reportGridView.Columns[Hasta]!.HeaderText = Hasta;
        if (reportGridView.Columns[Monto] != null)
            reportGridView.Columns[Monto]!.HeaderText = Monto;
        if (reportGridView.Columns[Pagado] != null)
            reportGridView.Columns[Pagado]!.HeaderText = Pagado;
        if (reportGridView.Columns[Estado] != null)
            reportGridView.Columns[Estado]!.HeaderText = Estado;
    }

    private async Task HandleUserPaymentHistory()
    {
        var studentComboBox = reportParamsPanel.Controls.OfType<ComboBox>().FirstOrDefault();
        if (studentComboBox?.SelectedItem == null)
        {
            MessageBox.Show("Seleccione un alumno.", ErrorCaption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        dynamic selected = studentComboBox.SelectedItem;
        long userId = selected.Value;

        await _userService.GetUserById(userId);
        var pagos =
            await _paymentService.SearchPayments(DateOnly.MinValue, DateOnly.MaxValue, userId);

        var pagosList = pagos.Select(p => new
        {
            Fecha = p.PaymentDate,
            Monto = p.Amount,
            Estado = p.Status
        }).ToList();

        reportGridView.DataSource = pagosList;
        if (reportGridView.Columns[Fecha] != null)
            reportGridView.Columns[Fecha]!.HeaderText = Fecha;
        if (reportGridView.Columns[Monto] != null)
            reportGridView.Columns[Monto]!.HeaderText = Monto;
        if (reportGridView.Columns[Estado] != null)
            reportGridView.Columns[Estado]!.HeaderText = Estado;
    }

    private async Task HandlePaymentsByDateRange()
    {
        var fromDatePicker = reportParamsPanel.Controls.OfType<DateTimePicker>().First();
        var toDatePicker = reportParamsPanel.Controls.OfType<DateTimePicker>().Last();

        var from = DateOnly.FromDateTime(fromDatePicker.Value.Date);
        var to = DateOnly.FromDateTime(toDatePicker.Value.Date);

        var payments = await _paymentService.SearchPayments(from, to, 0);

        var pagosList = payments.Select(p => new
        {
            Fecha = p.PaymentDate,
            Monto = p.Amount,
            Estado = p.Status,
            Metodo = p.GetType().Name == "CashPayment" ? Efectivo : Tarjeta
        }).ToList();

        reportGridView.DataSource = pagosList;
        if (reportGridView.Columns[Fecha] != null)
            reportGridView.Columns[Fecha]!.HeaderText = Fecha;
        if (reportGridView.Columns[Monto] != null)
            reportGridView.Columns[Monto]!.HeaderText = Monto;
        if (reportGridView.Columns[Estado] != null)
            reportGridView.Columns[Estado]!.HeaderText = Estado;
        if (reportGridView.Columns[Metodo] != null)
            reportGridView.Columns[Metodo]!.HeaderText = "Método";
    }
}