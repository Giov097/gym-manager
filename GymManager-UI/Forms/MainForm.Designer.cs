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
        nameLbl = new System.Windows.Forms.Label();
        nameTxt = new System.Windows.Forms.TextBox();
        lastNameLbl = new System.Windows.Forms.Label();
        lastNameTxt = new System.Windows.Forms.TextBox();
        emailLbl = new System.Windows.Forms.Label();
        emailTxt = new System.Windows.Forms.TextBox();
        rolesLbl = new System.Windows.Forms.Label();
        rolesTxt = new System.Windows.Forms.TextBox();
        feesUserGridView = new System.Windows.Forms.DataGridView();
        paymentsUserGridView = new System.Windows.Forms.DataGridView();
        greetingLbl = new System.Windows.Forms.Label();
        greetingMenu = new System.Windows.Forms.ContextMenuStrip(components);
        logoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        tabPageReportes = new System.Windows.Forms.TabPage();
        reportTypeComboBox = new System.Windows.Forms.ComboBox();
        reportParamsPanel = new System.Windows.Forms.Panel();
        btnGenerateReport = new System.Windows.Forms.Button();
        reportGridView = new System.Windows.Forms.DataGridView();

        tabPageChart = new System.Windows.Forms.TabPage();
        chartComboBox = new System.Windows.Forms.ComboBox();
        chartGenerateBtn = new System.Windows.Forms.Button();
        reportChart = new System.Windows.Forms.DataVisualization.Charting.Chart();

        myDataTabControl.SuspendLayout();
        tabPage1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)usersGridView).BeginInit();
        tabPage2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)feesGridView).BeginInit();
        tabPage3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)feesUserGridView).BeginInit();
        ((System.ComponentModel.ISupportInitialize)paymentsUserGridView).BeginInit();
        greetingMenu.SuspendLayout();
        SuspendLayout();
        // 
        // myDataTabControl
        // 
        myDataTabControl.Controls.Add(tabPage1);
        myDataTabControl.Controls.Add(tabPage2);
        myDataTabControl.Controls.Add(tabPage3);
        myDataTabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        myDataTabControl.Location = new System.Drawing.Point(12, 13);
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
        tabPage3.Controls.Add(label2);
        tabPage3.Controls.Add(label1);
        tabPage3.Controls.Add(nameLbl);
        tabPage3.Controls.Add(nameTxt);
        tabPage3.Controls.Add(lastNameLbl);
        tabPage3.Controls.Add(lastNameTxt);
        tabPage3.Controls.Add(emailLbl);
        tabPage3.Controls.Add(emailTxt);
        tabPage3.Controls.Add(rolesLbl);
        tabPage3.Controls.Add(rolesTxt);
        tabPage3.Controls.Add(feesUserGridView);
        tabPage3.Controls.Add(paymentsUserGridView);
        tabPage3.Location = new System.Drawing.Point(4, 24);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new System.Windows.Forms.Padding(3);
        tabPage3.Size = new System.Drawing.Size(768, 398);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "Mis datos";
        tabPage3.UseVisualStyleBackColor = true;
        // 
        // nameLbl
        // 
        nameLbl.Location = new System.Drawing.Point(20, 20);
        nameLbl.Name = "nameLbl";
        nameLbl.Size = new System.Drawing.Size(62, 23);
        nameLbl.TabIndex = 0;
        nameLbl.Text = "Nombre:";
        // 
        // nameTxt
        // 
        nameTxt.Location = new System.Drawing.Point(260, 17);
        nameTxt.Name = "nameTxt";
        nameTxt.ReadOnly = true;
        nameTxt.Size = new System.Drawing.Size(200, 23);
        nameTxt.TabIndex = 1;
        // 
        // lastNameLbl
        // 
        lastNameLbl.Location = new System.Drawing.Point(20, 60);
        lastNameLbl.Name = "lastNameLbl";
        lastNameLbl.Size = new System.Drawing.Size(62, 23);
        lastNameLbl.TabIndex = 2;
        lastNameLbl.Text = "Apellido:";
        // 
        // lastNameTxt
        // 
        lastNameTxt.Location = new System.Drawing.Point(260, 57);
        lastNameTxt.Name = "lastNameTxt";
        lastNameTxt.ReadOnly = true;
        lastNameTxt.Size = new System.Drawing.Size(200, 23);
        lastNameTxt.TabIndex = 3;
        // 
        // emailLbl
        // 
        emailLbl.Location = new System.Drawing.Point(20, 100);
        emailLbl.Name = "emailLbl";
        emailLbl.Size = new System.Drawing.Size(62, 23);
        emailLbl.TabIndex = 4;
        emailLbl.Text = "Correo:";
        // 
        // emailTxt
        // 
        emailTxt.Location = new System.Drawing.Point(260, 97);
        emailTxt.Name = "emailTxt";
        emailTxt.ReadOnly = true;
        emailTxt.Size = new System.Drawing.Size(200, 23);
        emailTxt.TabIndex = 5;
        // 
        // rolesLbl
        // 
        rolesLbl.Location = new System.Drawing.Point(20, 140);
        rolesLbl.Name = "rolesLbl";
        rolesLbl.Size = new System.Drawing.Size(62, 23);
        rolesLbl.TabIndex = 6;
        rolesLbl.Text = "Roles:";
        // 
        // rolesTxt
        // 
        rolesTxt.Location = new System.Drawing.Point(260, 137);
        rolesTxt.Name = "rolesTxt";
        rolesTxt.ReadOnly = true;
        rolesTxt.Size = new System.Drawing.Size(200, 23);
        rolesTxt.TabIndex = 7;
        // 
        // feesUserGridView
        // 
        feesUserGridView.Location = new System.Drawing.Point(20, 210);
        feesUserGridView.Name = "feesUserGridView";
        feesUserGridView.ReadOnly = true;
        feesUserGridView.Size = new System.Drawing.Size(350, 150);
        feesUserGridView.TabIndex = 8;
        // 
        // paymentsUserGridView
        // 
        paymentsUserGridView.Location = new System.Drawing.Point(400, 210);
        paymentsUserGridView.Name = "paymentsUserGridView";
        paymentsUserGridView.ReadOnly = true;
        paymentsUserGridView.Size = new System.Drawing.Size(350, 150);
        paymentsUserGridView.TabIndex = 9;
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
        // label1
        // 
        label1.Location = new System.Drawing.Point(20, 184);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(100, 23);
        label1.TabIndex = 10;
        label1.Text = "Mis cuotas";
        // 
        // label2
        // 
        label2.Location = new System.Drawing.Point(400, 184);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(100, 23);
        label2.TabIndex = 11;
        label2.Text = "Mis pagos";
        tabPageReportes.Text = "Reportes";
        reportTypeComboBox.Location = new System.Drawing.Point(20, 20);
        reportTypeComboBox.Size = new System.Drawing.Size(250, 23);
        reportTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        reportTypeComboBox.Items.AddRange(new object[]
        {
            "Recaudación mensual",
            "Alumnos con más deuda",
            "Pagos por método",
            "Cuotas impagas",
            "Historial de pagos de un alumno",
            "Pagos por rango de fechas"
        });
        reportTypeComboBox.SelectedIndex = 0;

        reportParamsPanel.Location = new System.Drawing.Point(20, 60);
        reportParamsPanel.Size = new System.Drawing.Size(400, 60);

        btnGenerateReport.Text = "Generar";
        btnGenerateReport.Location = new System.Drawing.Point(440, 60);
        btnGenerateReport.Size = new System.Drawing.Size(100, 30);

        reportGridView.Location = new System.Drawing.Point(20, 130);
        reportGridView.Size = new System.Drawing.Size(700, 250);
        reportGridView.ReadOnly = true;

        tabPageReportes.Controls.Add(reportTypeComboBox);
        tabPageReportes.Controls.Add(reportParamsPanel);
        tabPageReportes.Controls.Add(btnGenerateReport);
        tabPageReportes.Controls.Add(reportGridView);
        myDataTabControl.Controls.Add(tabPageReportes);

        // 
        // tabPageChart (nuevo)
        // 
        tabPageChart.Location = new System.Drawing.Point(4, 24);
        tabPageChart.Name = "tabPageChart";
        tabPageChart.Padding = new System.Windows.Forms.Padding(3);
        tabPageChart.Size = new System.Drawing.Size(768, 398);
        tabPageChart.TabIndex = 4;
        tabPageChart.Text = "Reportes - Chart";
        tabPageChart.UseVisualStyleBackColor = true;

        chartComboBox.Location = new System.Drawing.Point(20, 20);
        chartComboBox.Size = new System.Drawing.Size(300, 23);
        chartComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        chartComboBox.Items.AddRange(new object[]
        {
            "Recaudación mensual",
            "Alumnos con más deuda"
        });
        chartComboBox.SelectedIndex = 0;

        chartGenerateBtn.Text = "Generar";
        chartGenerateBtn.Location = new System.Drawing.Point(340, 18);
        chartGenerateBtn.Size = new System.Drawing.Size(80, 26);

        reportChart.Location = new System.Drawing.Point(20, 60);
        reportChart.Size = new System.Drawing.Size(720, 320);
        reportChart.Name = "reportChart";

        tabPageChart.Controls.Add(chartComboBox);
        tabPageChart.Controls.Add(chartGenerateBtn);
        tabPageChart.Controls.Add(reportChart);
        myDataTabControl.Controls.Add(tabPageChart);

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
        tabPage3.ResumeLayout(false);
        tabPage3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)feesUserGridView).EndInit();
        ((System.ComponentModel.ISupportInitialize)paymentsUserGridView).EndInit();
        greetingMenu.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

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

     private System.Windows.Forms.Label nameLbl;
     private System.Windows.Forms.Label lastNameLbl;
     private System.Windows.Forms.Label emailLbl;
     private System.Windows.Forms.Label rolesLbl;
     private System.Windows.Forms.TextBox nameTxt;
     private System.Windows.Forms.TextBox lastNameTxt;
     private System.Windows.Forms.TextBox emailTxt;
     private System.Windows.Forms.TextBox rolesTxt;
     private System.Windows.Forms.DataGridView feesUserGridView;
     private System.Windows.Forms.DataGridView paymentsUserGridView;

    private System.Windows.Forms.TabControl myDataTabControl;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;

    private  System.Windows.Forms.TabPage tabPageReportes;
    private  System.Windows.Forms.ComboBox reportTypeComboBox;
    private System.Windows.Forms.Panel reportParamsPanel;
    private System.Windows.Forms.Button btnGenerateReport;
    private System.Windows.Forms.DataGridView reportGridView;

    private System.Windows.Forms.TabPage tabPageChart;
    private System.Windows.Forms.ComboBox chartComboBox;
    private System.Windows.Forms.Button chartGenerateBtn;
    private System.Windows.Forms.DataVisualization.Charting.Chart reportChart;

    private ComboBox chartMonthComboBox;
    private ComboBox chartYearComboBox;
    private ComboBox viewerMonthComboBox;
    private ComboBox viewerYearComboBox;

    
    #endregion
}

