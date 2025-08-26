using System.ComponentModel;

namespace GymManager.Forms;

partial class MainForm
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
        tabControl1 = new System.Windows.Forms.TabControl();
        tabPage1 = new System.Windows.Forms.TabPage();
        usersGridView = new System.Windows.Forms.DataGridView();
        deleteUsrBtn = new System.Windows.Forms.Button();
        editUserBtn = new System.Windows.Forms.Button();
        newUserBtn = new System.Windows.Forms.Button();
        tabPage2 = new System.Windows.Forms.TabPage();
        editFeeBtn = new System.Windows.Forms.Button();
        registerFeeBtn = new System.Windows.Forms.Button();
        feesGridView = new System.Windows.Forms.DataGridView();
        tabPage3 = new System.Windows.Forms.TabPage();
        greetingLbl = new System.Windows.Forms.Label();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
        tabPage2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)feesGridView).BeginInit();
        SuspendLayout();
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Controls.Add(tabPage3);
        tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        tabControl1.Location = new System.Drawing.Point(12, 12);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new System.Drawing.Size(776, 426);
        tabControl1.TabIndex = 0;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(usersGridView);
        tabPage1.Controls.Add(deleteUsrBtn);
        tabPage1.Controls.Add(editUserBtn);
        tabPage1.Controls.Add(newUserBtn);
        tabPage1.Location = new System.Drawing.Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new System.Windows.Forms.Padding(3);
        tabPage1.Size = new System.Drawing.Size(768, 398);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Usuarios";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // usersGridView
        // 
        usersGridView.Location = new System.Drawing.Point(0, 0);
        usersGridView.Name = "usersGridView";
        usersGridView.Size = new System.Drawing.Size(768, 363);
        usersGridView.TabIndex = 4;
        // 
        // deleteUsrBtn
        // 
        deleteUsrBtn.Location = new System.Drawing.Point(375, 369);
        deleteUsrBtn.Name = "deleteUsrBtn";
        deleteUsrBtn.Size = new System.Drawing.Size(126, 23);
        deleteUsrBtn.TabIndex = 3;
        deleteUsrBtn.Text = "Borrar usuario";
        deleteUsrBtn.UseVisualStyleBackColor = true;
        // 
        // editUserBtn
        // 
        editUserBtn.Location = new System.Drawing.Point(507, 369);
        editUserBtn.Name = "editUserBtn";
        editUserBtn.Size = new System.Drawing.Size(126, 23);
        editUserBtn.TabIndex = 2;
        editUserBtn.Text = "Editar usuario";
        editUserBtn.UseVisualStyleBackColor = true;
        // 
        // newUserBtn
        // 
        newUserBtn.Location = new System.Drawing.Point(639, 369);
        newUserBtn.Name = "newUserBtn";
        newUserBtn.Size = new System.Drawing.Size(126, 23);
        newUserBtn.TabIndex = 1;
        newUserBtn.Text = "Nuevo usuario";
        newUserBtn.UseVisualStyleBackColor = true;
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(editFeeBtn);
        tabPage2.Controls.Add(registerFeeBtn);
        tabPage2.Controls.Add(feesGridView);
        tabPage2.Location = new System.Drawing.Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new System.Windows.Forms.Padding(3);
        tabPage2.Size = new System.Drawing.Size(768, 398);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Cuotas";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // editFeeBtn
        // 
        editFeeBtn.Location = new System.Drawing.Point(504, 369);
        editFeeBtn.Name = "editFeeBtn";
        editFeeBtn.Size = new System.Drawing.Size(126, 23);
        editFeeBtn.TabIndex = 7;
        editFeeBtn.Text = "Editar cuota";
        editFeeBtn.UseVisualStyleBackColor = true;
        // 
        // registerFeeBtn
        // 
        registerFeeBtn.Location = new System.Drawing.Point(636, 369);
        registerFeeBtn.Name = "registerFeeBtn";
        registerFeeBtn.Size = new System.Drawing.Size(126, 23);
        registerFeeBtn.TabIndex = 6;
        registerFeeBtn.Text = "Registrar cuota";
        registerFeeBtn.UseVisualStyleBackColor = true;
        // 
        // feesGridView
        // 
        feesGridView.Location = new System.Drawing.Point(0, 0);
        feesGridView.Name = "feesGridView";
        feesGridView.Size = new System.Drawing.Size(768, 363);
        feesGridView.TabIndex = 5;
        // 
        // tabPage3
        // 
        tabPage3.Location = new System.Drawing.Point(4, 24);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new System.Windows.Forms.Padding(3);
        tabPage3.Size = new System.Drawing.Size(768, 398);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "Mis datos";
        tabPage3.UseVisualStyleBackColor = true;
        // 
        // greetingLbl
        // 
        greetingLbl.Enabled = false;
        greetingLbl.Location = new System.Drawing.Point(681, 7);
        greetingLbl.Name = "greetingLbl";
        greetingLbl.Size = new System.Drawing.Size(100, 23);
        greetingLbl.TabIndex = 1;
        greetingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(greetingLbl);
        Controls.Add(tabControl1);
        Text = "Gym Manager";
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
        tabPage2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)feesGridView).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label greetingLbl;

    private System.Windows.Forms.Button newUserBtn;
    private System.Windows.Forms.Button editUserBtn;
    private System.Windows.Forms.Button deleteUsrBtn;
    private System.Windows.Forms.DataGridView usersGridView;
    private System.Windows.Forms.DataGridView feesGridView;
    private System.Windows.Forms.Button editFeeBtn;
    private System.Windows.Forms.Button registerFeeBtn;

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;

    #endregion
}