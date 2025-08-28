namespace GymManager.Forms;

public partial class ChangePasswordForm : Form
{
    public string? NewPassword { get; private set; }

    public ChangePasswordForm()
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
        if (string.IsNullOrWhiteSpace(txtPassword.Text) ||
            string.IsNullOrWhiteSpace(txtRepeatPassword.Text))
        {
            MessageBox.Show("Debe completar ambos campos.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        if (txtPassword.Text != txtRepeatPassword.Text)
        {
            MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        NewPassword = txtPassword.Text;
        DialogResult = DialogResult.OK;
        Close();
    }
}