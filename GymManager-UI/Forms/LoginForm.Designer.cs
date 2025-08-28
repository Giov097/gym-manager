using System.ComponentModel;

namespace GymManager.Forms;

partial class LoginForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _txtUsername = new System.Windows.Forms.TextBox();
        _txtPassword = new System.Windows.Forms.TextBox();
        loginBtn = new System.Windows.Forms.Button();
        loginLabel = new System.Windows.Forms.Label();
        pwdLabel = new System.Windows.Forms.Label();
        emailLabel = new System.Windows.Forms.Label();
        btnShowPassword = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // _txtUsername
        // 
        _txtUsername.Location = new System.Drawing.Point(258, 243);
        _txtUsername.Name = "_txtUsername";
        _txtUsername.Size = new System.Drawing.Size(253, 23);
        _txtUsername.TabIndex = 0;
        // 
        // _txtPassword
        // 
        _txtPassword.Location = new System.Drawing.Point(258, 284);
        _txtPassword.Name = "_txtPassword";
        _txtPassword.Size = new System.Drawing.Size(253, 23);
        _txtPassword.TabIndex = 1;
        _txtPassword.UseSystemPasswordChar = true;
        // 
        // loginBtn
        // 
        loginBtn.Location = new System.Drawing.Point(258, 313);
        loginBtn.Name = "loginBtn";
        loginBtn.Size = new System.Drawing.Size(253, 23);
        loginBtn.TabIndex = 2;
        loginBtn.Text = "Iniciar sesión";
        loginBtn.UseVisualStyleBackColor = true;
        loginBtn.Click += btnLogin_Click;
        // 
        // loginLabel
        // 
        loginLabel.CausesValidation = false;
        loginLabel.Font = new System.Drawing.Font("Copperplate Gothic Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        loginLabel.Location = new System.Drawing.Point(258, 125);
        loginLabel.Name = "loginLabel";
        loginLabel.Size = new System.Drawing.Size(253, 101);
        loginLabel.TabIndex = 3;
        loginLabel.Text = "Gym Manager";
        loginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // pwdLabel
        // 
        pwdLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        pwdLabel.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
        pwdLabel.Location = new System.Drawing.Point(258, 269);
        pwdLabel.Name = "pwdLabel";
        pwdLabel.Size = new System.Drawing.Size(89, 12);
        pwdLabel.TabIndex = 4;
        pwdLabel.Text = "Password";
        // 
        // emailLabel
        // 
        emailLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        emailLabel.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
        emailLabel.Location = new System.Drawing.Point(258, 226);
        emailLabel.Name = "emailLabel";
        emailLabel.Size = new System.Drawing.Size(89, 12);
        emailLabel.TabIndex = 5;
        emailLabel.Text = "Email";
        // 
        // btnShowPassword
        // 
        btnShowPassword.Location = new System.Drawing.Point(488, 284);
        btnShowPassword.Name = "btnShowPassword";
        btnShowPassword.Size = new System.Drawing.Size(23, 23);
        btnShowPassword.TabIndex = 6;
        btnShowPassword.Text = "👁️";
        btnShowPassword.UseVisualStyleBackColor = true;
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(btnShowPassword);
        Controls.Add(emailLabel);
        Controls.Add(pwdLabel);
        Controls.Add(loginLabel);
        Controls.Add(loginBtn);
        Controls.Add(_txtPassword);
        Controls.Add(_txtUsername);
        Text = "Gym Manager - Inicio de sesión";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button btnShowPassword;

    private System.Windows.Forms.Label pwdLabel;

    private System.Windows.Forms.Label emailLabel;

    private System.Windows.Forms.Label loginLabel;

    private System.Windows.Forms.TextBox _txtPassword;
    private System.Windows.Forms.Button loginBtn;

    private System.Windows.Forms.TextBox _txtUsername;

    #endregion
}