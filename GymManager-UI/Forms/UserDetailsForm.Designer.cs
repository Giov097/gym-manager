using System.ComponentModel;

namespace GymManager.Forms;

partial class UserDetailsForm
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
        txtId = new System.Windows.Forms.TextBox();
        txtFirstName = new System.Windows.Forms.TextBox();
        txtLastName = new System.Windows.Forms.TextBox();
        txtEmail = new System.Windows.Forms.TextBox();
        lstRoles = new System.Windows.Forms.ListBox();
        idLbl = new System.Windows.Forms.Label();
        firstNameLbl = new System.Windows.Forms.Label();
        lastNameLbl = new System.Windows.Forms.Label();
        emailLbl = new System.Windows.Forms.Label();
        rolesLbl = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // txtId
        // 
        txtId.Location = new System.Drawing.Point(21, 28);
        txtId.Name = "txtId";
        txtId.ReadOnly = true;
        txtId.Size = new System.Drawing.Size(209, 23);
        txtId.TabIndex = 0;
        // 
        // txtFirstName
        // 
        txtFirstName.Location = new System.Drawing.Point(21, 70);
        txtFirstName.Name = "txtFirstName";
        txtFirstName.ReadOnly = true;
        txtFirstName.Size = new System.Drawing.Size(209, 23);
        txtFirstName.TabIndex = 1;
        // 
        // txtLastName
        // 
        txtLastName.Location = new System.Drawing.Point(21, 113);
        txtLastName.Name = "txtLastName";
        txtLastName.ReadOnly = true;
        txtLastName.Size = new System.Drawing.Size(209, 23);
        txtLastName.TabIndex = 2;
        // 
        // txtEmail
        // 
        txtEmail.Location = new System.Drawing.Point(21, 161);
        txtEmail.Name = "txtEmail";
        txtEmail.ReadOnly = true;
        txtEmail.Size = new System.Drawing.Size(209, 23);
        txtEmail.TabIndex = 3;
        // 
        // lstRoles
        // 
        lstRoles.FormattingEnabled = true;
        lstRoles.ItemHeight = 15;
        lstRoles.Location = new System.Drawing.Point(21, 210);
        lstRoles.Name = "lstRoles";
        lstRoles.Size = new System.Drawing.Size(209, 94);
        lstRoles.TabIndex = 4;
        // 
        // idLbl
        // 
        idLbl.Location = new System.Drawing.Point(21, 8);
        idLbl.Name = "idLbl";
        idLbl.Size = new System.Drawing.Size(100, 23);
        idLbl.TabIndex = 5;
        idLbl.Text = "ID";
        // 
        // firstNameLbl
        // 
        firstNameLbl.Location = new System.Drawing.Point(21, 54);
        firstNameLbl.Name = "firstNameLbl";
        firstNameLbl.Size = new System.Drawing.Size(100, 23);
        firstNameLbl.TabIndex = 6;
        firstNameLbl.Text = Lang.FirstName;
        // 
        // lastNameLbl
        // 
        lastNameLbl.Location = new System.Drawing.Point(21, 94);
        lastNameLbl.Name = "lastNameLbl";
        lastNameLbl.Size = new System.Drawing.Size(100, 23);
        lastNameLbl.TabIndex = 7;
        lastNameLbl.Text = Lang.LastName;
        // 
        // emailLbl
        // 
        emailLbl.Location = new System.Drawing.Point(21, 139);
        emailLbl.Name = "emailLbl";
        emailLbl.Size = new System.Drawing.Size(100, 23);
        emailLbl.TabIndex = 8;
        emailLbl.Text = Lang.Email;
        // 
        // rolesLbl
        // 
        rolesLbl.Location = new System.Drawing.Point(21, 187);
        rolesLbl.Name = "rolesLbl";
        rolesLbl.Size = new System.Drawing.Size(100, 23);
        rolesLbl.TabIndex = 9;
        rolesLbl.Text = "Roles";
        // 
        // UserDetailsForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        ClientSize = new System.Drawing.Size(256, 334);
        Controls.Add(rolesLbl);
        Controls.Add(emailLbl);
        Controls.Add(lastNameLbl);
        Controls.Add(firstNameLbl);
        Controls.Add(idLbl);
        Controls.Add(lstRoles);
        Controls.Add(txtEmail);
        Controls.Add(txtLastName);
        Controls.Add(txtFirstName);
        Controls.Add(txtId);
        Location = new System.Drawing.Point(15, 15);
        Text = Lang.Details;
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label idLbl;
    private System.Windows.Forms.Label firstNameLbl;
    private System.Windows.Forms.Label lastNameLbl;
    private System.Windows.Forms.Label emailLbl;
    private System.Windows.Forms.Label rolesLbl;

    private System.Windows.Forms.TextBox txtEmail;
    private System.Windows.Forms.ListBox lstRoles;

    private System.Windows.Forms.TextBox txtLastName;

    private System.Windows.Forms.TextBox txtFirstName;
    private System.Windows.Forms.TextBox txtId;


    #endregion
}