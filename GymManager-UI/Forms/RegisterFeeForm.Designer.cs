using System.ComponentModel;

namespace GymManager.Forms;

partial class RegisterFeeForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.ComboBox userCombo;
    private System.Windows.Forms.DateTimePicker startDatePicker;
    private System.Windows.Forms.DateTimePicker endDatePicker;
    private System.Windows.Forms.TextBox amountTxt;
    private System.Windows.Forms.CheckBox paymentCheck;
    private System.Windows.Forms.ComboBox paymentTypeCombo;
    private System.Windows.Forms.DateTimePicker paymentDatePicker;
    private System.Windows.Forms.TextBox paymentAmountTxt;
    private System.Windows.Forms.Panel cashPanel;
    private System.Windows.Forms.TextBox receiptTxt;
    private System.Windows.Forms.Panel cardPanel;
    private System.Windows.Forms.TextBox brandTxt;
    private System.Windows.Forms.TextBox lastFourTxt;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;

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
        userCombo = new System.Windows.Forms.ComboBox();
        startDatePicker = new System.Windows.Forms.DateTimePicker();
        endDatePicker = new System.Windows.Forms.DateTimePicker();
        amountTxt = new System.Windows.Forms.TextBox();
        paymentCheck = new System.Windows.Forms.CheckBox();
        paymentTypeCombo = new System.Windows.Forms.ComboBox();
        paymentDatePicker = new System.Windows.Forms.DateTimePicker();
        paymentAmountTxt = new System.Windows.Forms.TextBox();
        cashPanel = new System.Windows.Forms.Panel();
        receiptTxt = new System.Windows.Forms.TextBox();
        cardPanel = new System.Windows.Forms.Panel();
        brandTxt = new System.Windows.Forms.TextBox();
        lastFourTxt = new System.Windows.Forms.TextBox();
        btnOk = new System.Windows.Forms.Button();
        btnCancel = new System.Windows.Forms.Button();
        cashPanel.SuspendLayout();
        cardPanel.SuspendLayout();
        SuspendLayout();
        // 
        // userCombo
        // 
        userCombo.Location = new System.Drawing.Point(20, 20);
        userCombo.Name = "userCombo";
        userCombo.Size = new System.Drawing.Size(200, 23);
        userCombo.TabIndex = 0;
        // 
        // startDatePicker
        // 
        startDatePicker.CustomFormat = "";
        startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        startDatePicker.Location = new System.Drawing.Point(20, 60);
        startDatePicker.Name = "startDatePicker";
        startDatePicker.Size = new System.Drawing.Size(200, 23);
        startDatePicker.TabIndex = 1;
        // 
        // endDatePicker
        // 
        endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        endDatePicker.Location = new System.Drawing.Point(20, 100);
        endDatePicker.Name = "endDatePicker";
        endDatePicker.Size = new System.Drawing.Size(200, 23);
        endDatePicker.TabIndex = 2;
        // 
        // amountTxt
        // 
        amountTxt.Location = new System.Drawing.Point(20, 140);
        amountTxt.Name = "amountTxt";
        amountTxt.PlaceholderText = "Monto";
        amountTxt.Size = new System.Drawing.Size(200, 23);
        amountTxt.TabIndex = 3;
        // 
        // paymentCheck
        // 
        paymentCheck.Location = new System.Drawing.Point(20, 180);
        paymentCheck.Name = "paymentCheck";
        paymentCheck.Size = new System.Drawing.Size(200, 24);
        paymentCheck.TabIndex = 4;
        paymentCheck.Text = "Registrar pago";
        // 
        // paymentTypeCombo
        // 
        paymentTypeCombo.Items.AddRange(new object[] { "Efectivo", "Tarjeta" });
        paymentTypeCombo.Location = new System.Drawing.Point(20, 220);
        paymentTypeCombo.Name = "paymentTypeCombo";
        paymentTypeCombo.Size = new System.Drawing.Size(200, 23);
        paymentTypeCombo.TabIndex = 5;
        // 
        // paymentDatePicker
        // 
        paymentDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        paymentDatePicker.Location = new System.Drawing.Point(20, 260);
        paymentDatePicker.Name = "paymentDatePicker";
        paymentDatePicker.Size = new System.Drawing.Size(200, 23);
        paymentDatePicker.TabIndex = 6;
        // 
        // paymentAmountTxt
        // 
        paymentAmountTxt.Location = new System.Drawing.Point(20, 300);
        paymentAmountTxt.Name = "paymentAmountTxt";
        paymentAmountTxt.PlaceholderText = "Monto pago";
        paymentAmountTxt.Size = new System.Drawing.Size(200, 23);
        paymentAmountTxt.TabIndex = 7;
        // 
        // cashPanel
        // 
        cashPanel.Controls.Add(receiptTxt);
        cashPanel.Location = new System.Drawing.Point(240, 220);
        cashPanel.Name = "cashPanel";
        cashPanel.Size = new System.Drawing.Size(200, 60);
        cashPanel.TabIndex = 8;
        // 
        // receiptTxt
        // 
        receiptTxt.Location = new System.Drawing.Point(0, 0);
        receiptTxt.Name = "receiptTxt";
        receiptTxt.PlaceholderText = "N° Recibo";
        receiptTxt.Size = new System.Drawing.Size(200, 23);
        receiptTxt.TabIndex = 0;
        // 
        // cardPanel
        // 
        cardPanel.Controls.Add(brandTxt);
        cardPanel.Controls.Add(lastFourTxt);
        cardPanel.Location = new System.Drawing.Point(240, 300);
        cardPanel.Name = "cardPanel";
        cardPanel.Size = new System.Drawing.Size(200, 60);
        cardPanel.TabIndex = 9;
        cardPanel.Visible = false;
        // 
        // brandTxt
        // 
        brandTxt.Location = new System.Drawing.Point(0, 0);
        brandTxt.Name = "brandTxt";
        brandTxt.PlaceholderText = "Marca tarjeta";
        brandTxt.Size = new System.Drawing.Size(200, 23);
        brandTxt.TabIndex = 0;
        // 
        // lastFourTxt
        // 
        lastFourTxt.Location = new System.Drawing.Point(0, 30);
        lastFourTxt.Name = "lastFourTxt";
        lastFourTxt.PlaceholderText = "Últimos 4 dígitos";
        lastFourTxt.Size = new System.Drawing.Size(200, 23);
        lastFourTxt.TabIndex = 1;
        // 
        // btnOk
        // 
        btnOk.Location = new System.Drawing.Point(20, 380);
        btnOk.Name = "btnOk";
        btnOk.Size = new System.Drawing.Size(100, 30);
        btnOk.TabIndex = 10;
        btnOk.Text = "Registrar";
        // 
        // btnCancel
        // 
        btnCancel.Location = new System.Drawing.Point(130, 380);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(100, 30);
        btnCancel.TabIndex = 11;
        btnCancel.Text = "Cancelar";
        // 
        // RegisterFeeForm
        // 
        ClientSize = new System.Drawing.Size(470, 430);
        Controls.Add(userCombo);
        Controls.Add(startDatePicker);
        Controls.Add(endDatePicker);
        Controls.Add(amountTxt);
        Controls.Add(paymentCheck);
        Controls.Add(paymentTypeCombo);
        Controls.Add(paymentDatePicker);
        Controls.Add(paymentAmountTxt);
        Controls.Add(cashPanel);
        Controls.Add(cardPanel);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        Text = "Registrar cuota";
        cashPanel.ResumeLayout(false);
        cashPanel.PerformLayout();
        cardPanel.ResumeLayout(false);
        cardPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}