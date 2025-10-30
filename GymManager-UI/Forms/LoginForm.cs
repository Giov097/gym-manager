using GymManager_BE;
using GymManager_BLL;
using GymManager_BLL.Impl;
using GymManager_SEC;
using GymManager.Controls;

namespace GymManager.Forms;

public partial class LoginForm : Form
{
    private readonly IUserService _userService;

    private readonly EncryptionUtils _encryptionUtils = new();

    private readonly LoginControl _loginControl;

    public LoginForm()
    {
        InitializeComponent();

        _loginControl = new LoginControl();
        Controls.Add(_loginControl);

        _loginControl.ConfigurePlacement(_txtUsername.Location, _txtUsername.Size,
            _txtPassword.Location, _txtPassword.Size);

        _loginControl.SubmitRequested += (_, _) => loginBtn.PerformClick();

        _txtUsername.Visible = false;
        _txtPassword.Visible = false;
        btnShowPassword.Visible = false;

        _userService = new UserService();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (!_loginControl.TryGetValidatedCredentials(out var email, out var passwordPlain))
            {
                return;
            }

            var userToValidate = new User
            {
                Email = email,
                Password = _encryptionUtils.EncryptString(passwordPlain)
            };
            var user =
                await _userService.Login(userToValidate);
            MessageBox.Show(Lang.LoginSuccess, Lang.Login,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            SessionManager.CurrentUser = user;
            var mainForm = new MainForm(new UserService(), new XmlUserService(), new FeeService(),
                new XmlFeeService(),
                new PaymentService(), new CashPaymentService(), new CardPaymentService(),
                new XmlPaymentService(), new XmlCashPaymentService(), new XmlCardPaymentService());
            mainForm.FormClosed += (_, _) => Close();
            mainForm.Show();
            Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show(Lang.LoginFailed + ex.Message, Lang.Login,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnLogin_Click(sender, EventArgs.Empty);
            e.SuppressKeyPress = true;
        }
    }
}