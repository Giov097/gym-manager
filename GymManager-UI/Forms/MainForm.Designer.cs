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
        components = new System.ComponentModel.Container();
        myDataTabControl = new System.Windows.Forms.TabControl();
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
        greetingMenu = new System.Windows.Forms.ContextMenuStrip(components);
        logoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        myDataTabControl.SuspendLayout();
        tabPage1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
        tabPage2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)feesGridView).BeginInit();
        greetingMenu.SuspendLayout();
        SuspendLayout();
        //
        // tabControl1
        //
        myDataTabControl.Controls.Add(tabPage1);
        myDataTabControl.Controls.Add(tabPage2);
        myDataTabControl.Controls.Add(tabPage3);
        myDataTabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        myDataTabControl.Location = new System.Drawing.Point(12, 12);
        myDataTabControl.Name = "myDataTabControl";
        myDataTabControl.SelectedIndex = 0;
        myDataTabControl.Size = new System.Drawing.Size(776, 426);
        myDataTabControl.TabIndex = 0;
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
        deleteUsrBtn.Cursor = System.Windows.Forms.Cursors.Hand;
        deleteUsrBtn.Location = new System.Drawing.Point(375, 369);
        deleteUsrBtn.Name = "deleteUsrBtn";
        deleteUsrBtn.Size = new System.Drawing.Size(126, 23);
        deleteUsrBtn.TabIndex = 3;
        deleteUsrBtn.Text = "Borrar usuario";
        deleteUsrBtn.UseVisualStyleBackColor = true;
        //
        // editUserBtn
        //
        editUserBtn.Cursor = System.Windows.Forms.Cursors.Hand;
        editUserBtn.Location = new System.Drawing.Point(507, 369);
        editUserBtn.Name = "editUserBtn";
        editUserBtn.Size = new System.Drawing.Size(126, 23);
        editUserBtn.TabIndex = 2;
        editUserBtn.Text = "Editar usuario";
        editUserBtn.UseVisualStyleBackColor = true;
        //
        // newUserBtn
        //
        newUserBtn.Cursor = System.Windows.Forms.Cursors.Hand;
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
        editFeeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
        editFeeBtn.Location = new System.Drawing.Point(504, 369);
        editFeeBtn.Name = "editFeeBtn";
        editFeeBtn.Size = new System.Drawing.Size(126, 23);
        editFeeBtn.TabIndex = 7;
        editFeeBtn.Text = "Editar cuota";
        editFeeBtn.UseVisualStyleBackColor = true;
        //
        // registerFeeBtn
        //
        registerFeeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
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
        greetingLbl.Cursor = System.Windows.Forms.Cursors.Hand;
        greetingLbl.Location = new System.Drawing.Point(681, 7);
        greetingLbl.Name = "greetingLbl";
        greetingLbl.Size = new System.Drawing.Size(100, 23);
        greetingLbl.TabIndex = 1;
        greetingLbl.Text = "GREETING";
        greetingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        //
        // greetingMenu
        //
        greetingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { logoutMenuItem, exitMenuItem });
        greetingMenu.Name = "greetingMenu";
        greetingMenu.Size = new System.Drawing.Size(143, 48);
        //
        // logoutMenuItem
        //
        logoutMenuItem.Name = "logoutMenuItem";
        logoutMenuItem.Size = new System.Drawing.Size(142, 22);
        logoutMenuItem.Text = "Cerrar sesión";
        //
        // exitMenuItem
        //
        exitMenuItem.Name = "exitMenuItem";
        exitMenuItem.Size = new System.Drawing.Size(142, 22);
        exitMenuItem.Text = "Salir";
        //
        // MainForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(greetingLbl);
        Controls.Add(myDataTabControl);
        Location = new System.Drawing.Point(15, 15);
        Text = "Gym Manager";
        myDataTabControl.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)usersGridView).EndInit();
        tabPage2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)feesGridView).EndInit();
        greetingMenu.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label greetingLbl;
    private System.Windows.Forms.ContextMenuStrip greetingMenu;
    private System.Windows.Forms.ToolStripMenuItem logoutMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitMenuItem;

    private System.Windows.Forms.Button newUserBtn;
    private System.Windows.Forms.Button editUserBtn;
    private System.Windows.Forms.Button deleteUsrBtn;
    private System.Windows.Forms.DataGridView usersGridView;
    private System.Windows.Forms.DataGridView feesGridView;
    private System.Windows.Forms.Button editFeeBtn;
    private System.Windows.Forms.Button registerFeeBtn;

    private System.Windows.Forms.TabControl myDataTabControl;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;

    #endregion
}