// GymManager-UI/Forms/EditUserForm.cs

using GymManager_BE;
using GymManager_SEC;

namespace GymManager.Forms;

public partial class EditUserForm : Form
{
    public User EditedUser { get; private set; }

    private readonly EncryptionUtils _encryptionUtils = new();

    public EditUserForm(User user)
    {
        InitializeComponent();
        EditedUser = user;
        txtEmail.Text = user.Email;
        txtFirstName.Text = user.FirstName;
        txtLastName.Text = user.LastName;
        foreach (var role in user.UserRoles)
        {
            var idx = clbRoles.Items.IndexOf(role.GetRoleName());
            if (idx >= 0) clbRoles.SetItemChecked(idx, true);
        }

        btnOk.Click += BtnOk_Click!;
        btnCancel.Click += (_, _) => Close();
        btnChangePassword.Click += BtnChangePassword_Click!;
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(txtFirstName.Text) ||
            string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
            if (addr.Address != txtEmail.Text)
                throw new FormatException();
        }
        catch
        {
            MessageBox.Show("El correo ingresado no tiene un formato válido.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (clbRoles.CheckedItems.Count == 0)
        {
            MessageBox.Show("Debe seleccionar al menos un rol.", "Error", MessageBoxButtons.OK,
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

        EditedUser.Email = txtEmail.Text;
        EditedUser.FirstName = txtFirstName.Text;
        EditedUser.LastName = txtLastName.Text;
        EditedUser.UserRoles = selectedRoles;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnChangePassword_Click(object sender, EventArgs e)
    {
        var changeForm = new ChangePasswordForm();
        if (changeForm.ShowDialog() == DialogResult.OK)
        {
            EditedUser.Password = _encryptionUtils.EncryptString(changeForm.NewPassword!);
        }
    }
}