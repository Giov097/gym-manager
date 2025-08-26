using GymManager_BE;
using GymManager_BLL;

namespace GymManager.Forms;

public partial class MainForm : Form
{
    private readonly IUserService _userService;
    private readonly IFeeService _feeService;

    public MainForm(IUserService userService, IFeeService feeService)
    {
        _userService = userService;
        _feeService = feeService;
        InitializeComponent();
        greetingLbl.Enabled = true;
        greetingLbl.Text = $"¡Hola {SessionManager.CurrentUser!.FirstName}!";
        Load += MainForm_Load!;
        feesGridView.CellClick += FeesGridView_CellClick;
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        var users = await _userService.GetUsers();
        FillUsersDataGrid(users);

        var fees = await _feeService.GetFees(DateOnly.MinValue, DateOnly.MaxValue, 0);
        FillFeesDataGrid(fees);
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
        // Verifica que no sea el header y que sea la columna "Usuario"
        if (e.RowIndex >= 0 && feesGridView.Columns[e.ColumnIndex].Name == "ViewUser")
        {
            var userId = (long)feesGridView.Rows[e.RowIndex].Cells["UserId"].Value;
            var user = await _userService.GetUserById(userId);

            // Abre el formulario de usuario (debes crear UserDetailsForm)
            var userForm = new UserDetailsForm(user);
            userForm.ShowDialog();
        }
    }
}