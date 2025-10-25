using GymManager_BE;
using GymManager_SEC;
using GymManager.Controls;

namespace GymManager.Forms;

public partial class CreateUserForm : Form
{
    public User? CreatedUser { get; private set; }

    private readonly EncryptionUtils _encryptionUtils = new();
    private readonly UserEditorControl _editor = new();

    #region Constants

    private const string? ErrorCaption = "Error";

    #endregion

    public CreateUserForm()
    {
        InitializeComponent();
        _editor.SetBounds(10, 10, _editor.Width, _editor.Height);
        Controls.Add(_editor);
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
        if (!_editor.ValidateInputs(out var error))
        {
            MessageBox.Show(error, ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        CreatedUser = _editor.BuildUser(_encryptionUtils);
        DialogResult = DialogResult.OK;
        Close();
    }
}