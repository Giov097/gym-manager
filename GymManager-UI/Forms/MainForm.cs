using GymManager_BE;
using GymManager_BLL;

namespace GymManager.Forms;

public partial class MainForm : Form
{
    private readonly IUserService _userService;
    private readonly IFeeService _feeService;
    private readonly IPaymentService _paymentService;
    private const string? SuccessCaption = "Éxito";
    private const string? ErrorCaption = "Error";

    public MainForm(IUserService userService, IFeeService feeService,
        IPaymentService paymentService)
    {
        _userService = userService;
        _feeService = feeService;
        _paymentService = paymentService;
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
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            var users = await _userService.GetUsers();
            FillUsersDataGrid(users);
            var fees = await _feeService.GetFees(DateOnly.MinValue, DateOnly.MaxValue, 0);
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
            ViewUser = "👁️",
            f.UserId
        }).ToList();

        // Personalizar columnas de cuotas
        feesGridView.Columns["Id"]!.HeaderText = "ID";
        feesGridView.Columns["Id"]!.Width = 50;
        feesGridView.Columns["StartDate"]!.HeaderText = "Desde";
        feesGridView.Columns["StartDate"]!.Width = 100;
        feesGridView.Columns["EndDate"]!.HeaderText = "Hasta";
        feesGridView.Columns["EndDate"]!.Width = 100;
        feesGridView.Columns["Amount"]!.HeaderText = "Monto";
        feesGridView.Columns["Amount"]!.Width = 80;
        feesGridView.Columns["ViewUser"]!.HeaderText = "Usuario";
        feesGridView.Columns["ViewUser"]!.Width = 80;
        feesGridView.Columns["UserId"]!.Visible = false;
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
                var userId = (long)feesGridView.Rows[e.RowIndex].Cells["UserId"].Value;
                var user = await _userService.GetUserById(userId);

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
                MessageBox.Show("Usuario modificado con éxito", SuccessCaption,
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
            var registerForm = new RegisterFeeForm(_userService, _feeService, _paymentService);
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Cuota registrada con éxito", SuccessCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                var fees = await _feeService.GetFees(DateOnly.MinValue, DateOnly.MaxValue, 0);
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
                var fees = await _feeService.GetFees(DateOnly.MinValue, DateOnly.MaxValue, 0);
                FillFeesDataGrid(fees);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al editar la cuota: {ex.Message}", ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}