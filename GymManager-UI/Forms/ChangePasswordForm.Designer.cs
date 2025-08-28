// GymManager-UI/Forms/ChangePasswordForm.Designer.cs
namespace GymManager.Forms
{
    partial class ChangePasswordForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblRepeatPassword;
        private System.Windows.Forms.TextBox txtRepeatPassword;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblPassword = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            lblRepeatPassword = new System.Windows.Forms.Label();
            txtRepeatPassword = new System.Windows.Forms.TextBox();
            btnOk = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            btnShowPassword = new System.Windows.Forms.Button();
            btnShowPasswordRepeat = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(30, 25);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(70, 15);
            lblPassword.TabIndex = 0;
            lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(140, 22);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(180, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblRepeatPassword
            // 
            lblRepeatPassword.AutoSize = true;
            lblRepeatPassword.Location = new System.Drawing.Point(30, 65);
            lblRepeatPassword.Name = "lblRepeatPassword";
            lblRepeatPassword.Size = new System.Drawing.Size(108, 15);
            lblRepeatPassword.TabIndex = 2;
            lblRepeatPassword.Text = "Repetir contraseña:";
            // 
            // txtRepeatPassword
            // 
            txtRepeatPassword.Location = new System.Drawing.Point(140, 62);
            txtRepeatPassword.Name = "txtRepeatPassword";
            txtRepeatPassword.Size = new System.Drawing.Size(180, 23);
            txtRepeatPassword.TabIndex = 3;
            txtRepeatPassword.UseSystemPasswordChar = true;
            // 
            // btnOk
            // 
            btnOk.Location = new System.Drawing.Point(140, 110);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(80, 27);
            btnOk.TabIndex = 4;
            btnOk.Text = "Aceptar";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(240, 110);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(80, 27);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnShowPassword
            // 
            btnShowPassword.Location = new System.Drawing.Point(288, 22);
            btnShowPassword.Name = "btnShowPassword";
            btnShowPassword.Size = new System.Drawing.Size(32, 23);
            btnShowPassword.TabIndex = 6;
            btnShowPassword.Text = "👁️";
            btnShowPassword.UseVisualStyleBackColor = true;
            // 
            // btnShowPasswordRepeat
            // 
            btnShowPasswordRepeat.Location = new System.Drawing.Point(288, 62);
            btnShowPasswordRepeat.Name = "btnShowPasswordRepeat";
            btnShowPasswordRepeat.Size = new System.Drawing.Size(32, 23);
            btnShowPasswordRepeat.TabIndex = 7;
            btnShowPasswordRepeat.Text = "👁️";
            btnShowPasswordRepeat.UseVisualStyleBackColor = true;
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(370, 160);
            Controls.Add(btnShowPasswordRepeat);
            Controls.Add(btnShowPassword);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(txtRepeatPassword);
            Controls.Add(lblRepeatPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Cambiar contraseña";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnShowPasswordRepeat;

        private System.Windows.Forms.Button btnShowPassword;
    }
}