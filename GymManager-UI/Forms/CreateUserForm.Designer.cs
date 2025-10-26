// GymManager-UI/Forms/CreateUserForm.Designer.cs
namespace GymManager.Forms;

partial class CreateUserForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnShowPassword;
    private System.Windows.Forms.Button btnShowPasswordRepeat;
    private GymManager.Controls.UserEditorControl _editor;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btnOk = new System.Windows.Forms.Button();
        btnCancel = new System.Windows.Forms.Button();
        btnShowPassword = new System.Windows.Forms.Button();
        btnShowPasswordRepeat = new System.Windows.Forms.Button();
        _editor = new GymManager.Controls.UserEditorControl();
        SuspendLayout();
        // 
        // btnOk
        // 
        btnOk.Location = new System.Drawing.Point(144, 400);
        btnOk.Name = "btnOk";
        btnOk.Size = new System.Drawing.Size(80, 30);
        btnOk.TabIndex = 8;
        btnOk.Text = "Crear";
        btnOk.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.Location = new System.Drawing.Point(244, 400);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(80, 30);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancelar";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnShowPassword
        //
        btnShowPassword.Location = new System.Drawing.Point(262, 196);
        btnShowPassword.Name = "btnShowPassword";
        btnShowPassword.Size = new System.Drawing.Size(29, 23);
        btnShowPassword.TabIndex = 13;
        btnShowPassword.Text = "👁️";
        btnShowPassword.UseVisualStyleBackColor = true;
        //
        // btnShowPasswordRepeat
        //
        btnShowPasswordRepeat.Location = new System.Drawing.Point(262, 246);
        btnShowPasswordRepeat.Name = "btnShowPasswordRepeat";
        btnShowPasswordRepeat.Size = new System.Drawing.Size(29, 23);
        btnShowPasswordRepeat.TabIndex = 14;
        btnShowPasswordRepeat.Text = "👁️";
        btnShowPasswordRepeat.UseVisualStyleBackColor = true;
        //
        // _editor
        // 
        _editor.Location = new System.Drawing.Point(12, 20);
        _editor.Name = "_editor";
        _editor.Size = new System.Drawing.Size(240, 370);
        _editor.TabIndex = 0;
        // 
        // CreateUserForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(350, 460);
        Controls.Add(_editor);
        Controls.Add(btnShowPassword);
        Controls.Add(btnShowPasswordRepeat);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        Text = "Alta de Usuario";
        ResumeLayout(false);
        PerformLayout();
    }
}