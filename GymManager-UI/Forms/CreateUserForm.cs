using GymManager_BE;
using GymManager_SEC;
using GymManager.Controls;

namespace GymManager.Forms;

public partial class CreateUserForm : Form
{
    public User? CreatedUser { get; private set; }

    private readonly EncryptionUtils _encryptionUtils = new();

    #region Constants

    private const string? ErrorCaption = "Error";

    #endregion

    public CreateUserForm()
    {
        InitializeComponent();


        btnOk.Click += BtnOk_Click!;
        btnCancel.Click += (_, _) => Close();

        btnShowPassword.MouseDown += (_, _) => _editor.SetPasswordFieldVisibility(true);
        btnShowPassword.MouseUp += (_, _) => _editor.SetPasswordFieldVisibility(false);
        btnShowPasswordRepeat.MouseDown += (_, _) => _editor.SetRepeatPasswordFieldVisibility(true);
        btnShowPasswordRepeat.MouseUp += (_, _) => _editor.SetRepeatPasswordFieldVisibility(false);
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