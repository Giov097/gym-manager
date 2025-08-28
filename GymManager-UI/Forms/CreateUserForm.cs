using GymManager_BE;

namespace GymManager.Forms;

public partial class CreateUserForm : Form
{
    private const string? ErrorCaption = "Error";
    public User? CreatedUser { get; private set; }

    public CreateUserForm()
    {
        InitializeComponent();
        btnOk.Click += BtnOk_Click!;
        btnCancel.Click += (_, _) => Close();
        btnShowPassword.MouseDown += (_, _) => txtPassword.UseSystemPasswordChar = false;
        btnShowPasswordRepeat.MouseDown +=
            (_, _) => txtRepeatPassword.UseSystemPasswordChar = false;
        btnShowPassword.MouseUp += (_, _) => txtPassword.UseSystemPasswordChar = true;
        btnShowPasswordRepeat.MouseUp += (_, _) => txtRepeatPassword.UseSystemPasswordChar = true;
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text) ||
            string.IsNullOrWhiteSpace(txtRepeatPassword.Text))
        {
            MessageBox.Show("Todos los campos son obligatorios.", ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Validar formato de email
        try
        {
            var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
            if (addr.Address != txtEmail.Text)
                throw new FormatException();
        }
        catch
        {
            MessageBox.Show("El correo ingresado no tiene un formato válido.", ErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (txtPassword.Text != txtRepeatPassword.Text)
        {
            MessageBox.Show("Las contraseñas no coinciden.", ErrorCaption, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        if (clbRoles.CheckedItems.Count == 0)
        {
            MessageBox.Show("Debe seleccionar al menos un rol.", ErrorCaption, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        var selectedRoles = clbRoles.CheckedItems.Cast<string>().Select(r =>
            r switch
            {
                "ADMINISTRADOR" => UserRole.Admin,
                "ENTRENADOR" => UserRole.Trainer,
                _ => UserRole.Student
            }
        ).ToArray();

        CreatedUser = new User
        {
            Email = txtEmail.Text,
            FirstName = txtFirstName.Text,
            LastName = txtLastName.Text,
            Password = txtPassword.Text,
            UserRoles = selectedRoles
        };
        DialogResult = DialogResult.OK;
        Close();
    }
}