using System.Text.RegularExpressions;

namespace GymManager.Controls
{
    public class LoginControl : UserControl
    {
        private readonly TextBox _txtUsername;
        private readonly TextBox _txtPassword;
        private readonly Button _btnShowPassword;

        public event EventHandler? SubmitRequested;

        public string Username => _txtUsername.Text;
        public string Password => _txtPassword.Text;

        public LoginControl()
        {
            Size = new Size(360, 120);

            _txtUsername = new TextBox { Location = new Point(3, 3), Width = 320 };
            _txtPassword = new TextBox { Location = new Point(3, 33), Width = 240, UseSystemPasswordChar = true };

            _btnShowPassword = new Button { Text = "👁", Location = new Point(248, 31), Size = new Size(35, 24) };

            Controls.Add(_txtUsername);
            Controls.Add(_txtPassword);
            Controls.Add(_btnShowPassword);

            _btnShowPassword.MouseDown += (_, _) => _txtPassword.UseSystemPasswordChar = false;
            _btnShowPassword.MouseUp += (_, _) => _txtPassword.UseSystemPasswordChar = true;

            _txtUsername.KeyDown += TextBox_KeyDown;
            _txtPassword.KeyDown += TextBox_KeyDown;
        }

        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SubmitRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool TryGetValidatedCredentials(out string email, out string password)
        {
            email = _txtUsername.Text?.Trim() ?? string.Empty;
            password = _txtPassword.Text ?? string.Empty;

            if (!IsValidEmail(email))
            {
                MessageBox.Show("El usuario debe tener formato de email válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("La contraseña no puede estar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPassword.Focus();
                return false;
            }

            return true;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public void ConfigurePlacement(Point usernameFormLocation, Size usernameFormSize, Point passwordFormLocation, Size passwordFormSize)
        {
            Location = usernameFormLocation;

            _txtUsername.Location = Point.Empty;
            _txtUsername.Size = usernameFormSize;

            _txtPassword.Location = new Point(passwordFormLocation.X - usernameFormLocation.X, passwordFormLocation.Y - usernameFormLocation.Y);
            _txtPassword.Size = passwordFormSize;

            const int gap = 6;
            _btnShowPassword.Location = new Point(_txtPassword.Right + gap, _txtPassword.Top);
            _btnShowPassword.Size = new Size(28, _txtPassword.Height);

            var rightMost = Math.Max(_txtUsername.Right, _btnShowPassword.Right);
            var bottomMost = Math.Max(_txtPassword.Bottom, _txtUsername.Bottom);
            Size = new Size(rightMost + 3, bottomMost + 3);
        }
    }
}
