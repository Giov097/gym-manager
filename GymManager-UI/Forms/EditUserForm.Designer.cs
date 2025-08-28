namespace GymManager.Forms
{
    partial class EditUserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChangePassword;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblRoles = new System.Windows.Forms.Label();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 20);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(38, 15);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(120, 17);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 23);
            this.txtEmail.TabIndex = 1;
            //
            // lblFirstName
            //
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(30, 55);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(54, 15);
            this.lblFirstName.TabIndex = 2;
            this.lblFirstName.Text = "Nombre:";
            //
            // txtFirstName
            //
            this.txtFirstName.Location = new System.Drawing.Point(120, 52);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 23);
            this.txtFirstName.TabIndex = 3;
            //
            // lblLastName
            //
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(30, 90);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(54, 15);
            this.lblLastName.TabIndex = 4;
            this.lblLastName.Text = "Apellido:";
            //
            // txtLastName
            //
            this.txtLastName.Location = new System.Drawing.Point(120, 87);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(200, 23);
            this.txtLastName.TabIndex = 5;
            //
            // lblRoles
            //
            this.lblRoles.AutoSize = true;
            this.lblRoles.Location = new System.Drawing.Point(30, 125);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(37, 15);
            this.lblRoles.TabIndex = 6;
            this.lblRoles.Text = "Roles:";
            //
            // clbRoles
            //
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Items.AddRange(new object[] {
                "ADMINISTRADOR",
                "ENTRENADOR",
                "ALUMNO"
            });
            this.clbRoles.Location = new System.Drawing.Point(120, 125);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(200, 58);
            this.clbRoles.TabIndex = 7;
            //
            // btnChangePassword
            //
            this.btnChangePassword.Location = new System.Drawing.Point(120, 200);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(200, 27);
            this.btnChangePassword.TabIndex = 8;
            this.btnChangePassword.Text = "Cambiar contraseña";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            //
            // btnOk
            //
            this.btnOk.Location = new System.Drawing.Point(120, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 27);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Aceptar";
            this.btnOk.UseVisualStyleBackColor = true;
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(230, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 27);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // EditUserForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 290);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.clbRoles);
            this.Controls.Add(this.lblRoles);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar Usuario";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}