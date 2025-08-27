// GymManager-UI/Forms/CreateUserForm.Designer.cs
namespace GymManager.Forms;

partial class CreateUserForm
{
    private System.ComponentModel.IContainer components = null;
    protected System.Windows.Forms.Label lblEmail;
    private System.Windows.Forms.TextBox txtEmail;
    protected System.Windows.Forms.Label lblFirstName;
    private System.Windows.Forms.TextBox txtFirstName;
    protected System.Windows.Forms.Label lblLastName;
    private System.Windows.Forms.TextBox txtLastName;
    protected System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.CheckedListBox clbRoles;
    private System.Windows.Forms.Label lblRepeatPassword;
    private System.Windows.Forms.TextBox txtRepeatPassword;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblEmail = new System.Windows.Forms.Label();
        txtEmail = new System.Windows.Forms.TextBox();
        lblFirstName = new System.Windows.Forms.Label();
        txtFirstName = new System.Windows.Forms.TextBox();
        lblLastName = new System.Windows.Forms.Label();
        txtLastName = new System.Windows.Forms.TextBox();
        lblPassword = new System.Windows.Forms.Label();
        txtPassword = new System.Windows.Forms.TextBox();
        btnOk = new System.Windows.Forms.Button();
        btnCancel = new System.Windows.Forms.Button();
        clbRoles = new System.Windows.Forms.CheckedListBox();
        lblRepeatPassword = new System.Windows.Forms.Label();
        txtRepeatPassword = new System.Windows.Forms.TextBox();
        SuspendLayout();
        //
        // lblEmail
        //
        lblEmail.AutoSize = true;
        lblEmail.Location = new System.Drawing.Point(20, 20);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new System.Drawing.Size(46, 15);
        lblEmail.TabIndex = 0;
        lblEmail.Text = "Correo:";
        //
        // txtEmail
        //
        txtEmail.Location = new System.Drawing.Point(144, 20);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new System.Drawing.Size(180, 23);
        txtEmail.TabIndex = 1;
        //
        // lblFirstName
        //
        lblFirstName.AutoSize = true;
        lblFirstName.Location = new System.Drawing.Point(20, 60);
        lblFirstName.Name = "lblFirstName";
        lblFirstName.Size = new System.Drawing.Size(54, 15);
        lblFirstName.TabIndex = 2;
        lblFirstName.Text = "Nombre:";
        //
        // txtFirstName
        //
        txtFirstName.Location = new System.Drawing.Point(144, 60);
        txtFirstName.Name = "txtFirstName";
        txtFirstName.Size = new System.Drawing.Size(180, 23);
        txtFirstName.TabIndex = 3;
        //
        // lblLastName
        //
        lblLastName.AutoSize = true;
        lblLastName.Location = new System.Drawing.Point(20, 100);
        lblLastName.Name = "lblLastName";
        lblLastName.Size = new System.Drawing.Size(54, 15);
        lblLastName.TabIndex = 4;
        lblLastName.Text = "Apellido:";
        //
        // txtLastName
        //
        txtLastName.Location = new System.Drawing.Point(144, 100);
        txtLastName.Name = "txtLastName";
        txtLastName.Size = new System.Drawing.Size(180, 23);
        txtLastName.TabIndex = 5;
        //
        // lblPassword
        //
        lblPassword.AutoSize = true;
        lblPassword.Location = new System.Drawing.Point(20, 140);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new System.Drawing.Size(70, 15);
        lblPassword.TabIndex = 6;
        lblPassword.Text = "Contraseña:";
        //
        // txtPassword
        //
        txtPassword.Location = new System.Drawing.Point(144, 140);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new System.Drawing.Size(180, 23);
        txtPassword.TabIndex = 7;
        //
        // btnOk
        //
        btnOk.Location = new System.Drawing.Point(144, 286);
        btnOk.Name = "btnOk";
        btnOk.Size = new System.Drawing.Size(80, 30);
        btnOk.TabIndex = 8;
        btnOk.Text = "Crear";
        btnOk.UseVisualStyleBackColor = true;
        //
        // btnCancel
        //
        btnCancel.Location = new System.Drawing.Point(244, 286);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(80, 30);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancelar";
        btnCancel.UseVisualStyleBackColor = true;
        //
        // clbRoles
        //
        clbRoles.Items.AddRange(new object[] { "ADMINISTRADOR", "ENTRENADOR", "ALUMNO" });
        clbRoles.Location = new System.Drawing.Point(144, 208);
        clbRoles.Name = "clbRoles";
        clbRoles.Size = new System.Drawing.Size(180, 58);
        clbRoles.TabIndex = 10;
        //
        // lblRepeatPassword
        //
        lblRepeatPassword.AutoSize = true;
        lblRepeatPassword.Location = new System.Drawing.Point(20, 175);
        lblRepeatPassword.Name = "lblRepeatPassword";
        lblRepeatPassword.Size = new System.Drawing.Size(108, 15);
        lblRepeatPassword.TabIndex = 11;
        lblRepeatPassword.Text = "Repetir contraseña:";
        //
        // txtRepeatPassword
        //
        txtRepeatPassword.Location = new System.Drawing.Point(144, 175);
        txtRepeatPassword.Name = "txtRepeatPassword";
        txtRepeatPassword.PasswordChar = '*';
        txtRepeatPassword.Size = new System.Drawing.Size(180, 23);
        txtRepeatPassword.TabIndex = 12;
        //
        // CreateUserForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(350, 356);
        Controls.Add(clbRoles);
        Controls.Add(lblRepeatPassword);
        Controls.Add(txtRepeatPassword);
        Controls.Add(lblEmail);
        Controls.Add(txtEmail);
        Controls.Add(lblFirstName);
        Controls.Add(txtFirstName);
        Controls.Add(lblLastName);
        Controls.Add(txtLastName);
        Controls.Add(lblPassword);
        Controls.Add(txtPassword);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        Text = "Alta de Usuario";
        ResumeLayout(false);
        PerformLayout();
    }
}