namespace GymManager.Controls;

using System;
using System.Linq;
using System.Windows.Forms;
using GymManager_BE;
using GymManager_SEC;

public partial class UserEditorControl : UserControl
{
    private readonly TextBox _txtEmail = new();
    private readonly TextBox _txtFirstName = new();
    private readonly TextBox _txtLastName = new();
    private readonly TextBox _txtPassword = new();
    private readonly TextBox _txtRepeatPassword = new();
    private readonly CheckedListBox _clbRoles = new();

    public UserEditorControl()
    {
        InitializeComponent();
        BuildLayout();
    }

    private void BuildLayout()
    {
        var lblEmail = new Label { Text = "Email", AutoSize = true, Top = 6, Left = 6 };
        _txtEmail.SetBounds(6, 26, 220, 23);

        var lblFirst = new Label { Text = "Nombre", AutoSize = true, Top = 56, Left = 6 };
        _txtFirstName.SetBounds(6, 76, 220, 23);

        var lblLast = new Label { Text = "Apellido", AutoSize = true, Top = 106, Left = 6 };
        _txtLastName.SetBounds(6, 126, 220, 23);

        var lblPwd = new Label { Text = "Contraseña", AutoSize = true, Top = 156, Left = 6 };
        _txtPassword.SetBounds(6, 176, 220, 23);
        _txtPassword.UseSystemPasswordChar = true;

        var lblPwdRepeat = new Label
            { Text = "Repetir contraseña", AutoSize = true, Top = 206, Left = 6 };
        _txtRepeatPassword.SetBounds(6, 226, 220, 23);
        _txtRepeatPassword.UseSystemPasswordChar = true;

        var lblRoles = new Label { Text = "Roles", AutoSize = true, Top = 256, Left = 6 };
        _clbRoles.SetBounds(6, 276, 220, 80);
        _clbRoles.Items.AddRange(["ADMINISTRADOR", "ENTRENADOR", "ALUMNO"]);

        Height = 370;
        Width = 240;
        Controls.AddRange([
            lblEmail, _txtEmail,
            lblFirst, _txtFirstName,
            lblLast, _txtLastName,
            lblPwd, _txtPassword,
            lblPwdRepeat, _txtRepeatPassword,
            lblRoles, _clbRoles
        ]);
    }

    public bool ValidateInputs(out string errorMessage)
    {
        errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(_txtEmail.Text) ||
            string.IsNullOrWhiteSpace(_txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(_txtLastName.Text) ||
            string.IsNullOrWhiteSpace(_txtPassword.Text) ||
            string.IsNullOrWhiteSpace(_txtRepeatPassword.Text))
        {
            errorMessage = "Todos los campos son obligatorios.";
            return false;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(_txtEmail.Text);
            if (addr.Address != _txtEmail.Text) throw new FormatException();
        }
        catch
        {
            errorMessage = "El correo ingresado no tiene un formato válido.";
            return false;
        }

        if (_txtPassword.Text != _txtRepeatPassword.Text)
        {
            errorMessage = "Las contraseñas no coinciden.";
            return false;
        }

        if (_clbRoles.CheckedItems.Count == 0)
        {
            errorMessage = "Debe seleccionar al menos un rol.";
            return false;
        }

        return true;
    }

    public User BuildUser(EncryptionUtils encryptionUtils)
    {
        var selectedRoles = _clbRoles.CheckedItems.Cast<string>().Select(r =>
            r switch
            {
                "ADMINISTRADOR" => UserRole.Admin,
                "ENTRENADOR" => UserRole.Trainer,
                _ => UserRole.Student
            }
        ).ToArray();

        return new User
        {
            Email = _txtEmail.Text,
            FirstName = _txtFirstName.Text,
            LastName = _txtLastName.Text,
            Password = encryptionUtils.EncryptString(_txtPassword.Text),
            UserRoles = selectedRoles
        };
    }
}