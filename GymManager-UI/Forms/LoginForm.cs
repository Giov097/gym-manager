using GymManager_BLL.Impl;

namespace GymManager.Forms;

using System;
using System.Windows.Forms;
using GymManager_BLL;

public partial class LoginForm : Form
{
    private readonly IUserService _userService;

    public LoginForm()
    {
        InitializeComponent();
        _txtUsername.KeyDown += TextBox_KeyDown;
        _txtPassword.KeyDown += TextBox_KeyDown;
        _userService = new UserService();
    }


    private async void btnLogin_Click(object sender, EventArgs e)
    {
        var username = _txtUsername.Text;
        var password = _txtPassword.Text;

        try
        {
            var user = await _userService.Login(username, password);
            MessageBox.Show("¡Login exitoso!");
            SessionManager.CurrentUser = user;
            var mainForm = new MainForm(new UserService(), new FeeService());
            mainForm.FormClosed += (_, _) => Close();
            mainForm.Show();
            Hide();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al iniciar sesión: " + ex.Message);
        }
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnLogin_Click(sender, EventArgs.Empty);
            e.SuppressKeyPress = true; // Evita el beep
        }
    }
}