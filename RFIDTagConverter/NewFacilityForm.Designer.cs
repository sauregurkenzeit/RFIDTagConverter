using System;
using System.Windows.Forms;
namespace RFIDTagConverter
{
    partial class NewFacilityForm: Form
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox textBoxIValue;
        private TextBox textBoxFacility;
        private TextBox textBoxCard;
        private Label labelIValue;
        private Label labelFacility;
        private Label labelCard;
        private Button buttonOk;
        private Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxIValue = new System.Windows.Forms.TextBox();
            this.textBoxFacility = new System.Windows.Forms.TextBox();
            this.textBoxCard = new System.Windows.Forms.TextBox();
            this.labelIValue = new System.Windows.Forms.Label();
            this.labelFacility = new System.Windows.Forms.Label();
            this.labelCard = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxIValue
            // 
            this.textBoxIValue.Location = new System.Drawing.Point(80, 12);
            this.textBoxIValue.Name = "textBoxIValue";
            this.textBoxIValue.Size = new System.Drawing.Size(100, 22);
            this.textBoxIValue.TabIndex = 0;
            // 
            // textBoxFacility
            // 
            this.textBoxFacility.Location = new System.Drawing.Point(80, 42);
            this.textBoxFacility.Name = "textBoxFacility";
            this.textBoxFacility.Size = new System.Drawing.Size(100, 22);
            this.textBoxFacility.TabIndex = 1;
            // 
            // textBoxCard
            // 
            this.textBoxCard.Location = new System.Drawing.Point(80, 72);
            this.textBoxCard.Name = "textBoxCard";
            this.textBoxCard.Size = new System.Drawing.Size(100, 22);
            this.textBoxCard.TabIndex = 2;
            // 
            // labelIValue
            // 
            this.labelIValue.AutoSize = true;
            this.labelIValue.Location = new System.Drawing.Point(12, 15);
            this.labelIValue.Name = "labelIValue";
            this.labelIValue.Size = new System.Drawing.Size(52, 16);
            this.labelIValue.TabIndex = 3;
            this.labelIValue.Text = "Input (i):";
            // 
            // labelFacility
            // 
            this.labelFacility.AutoSize = true;
            this.labelFacility.Location = new System.Drawing.Point(12, 45);
            this.labelFacility.Name = "labelFacility";
            this.labelFacility.Size = new System.Drawing.Size(52, 16);
            this.labelFacility.TabIndex = 4;
            this.labelFacility.Text = "Facility:";
            // 
            // labelCard
            // 
            this.labelCard.AutoSize = true;
            this.labelCard.Location = new System.Drawing.Point(12, 75);
            this.labelCard.Name = "labelCard";
            this.labelCard.Size = new System.Drawing.Size(39, 16);
            this.labelCard.TabIndex = 5;
            this.labelCard.Text = "Card:";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(15, 110);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(100, 110);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // NewFacilityForm
            // 
            this.ClientSize = new System.Drawing.Size(220, 150);
            this.Controls.Add(this.textBoxIValue);
            this.Controls.Add(this.textBoxFacility);
            this.Controls.Add(this.textBoxCard);
            this.Controls.Add(this.labelIValue);
            this.Controls.Add(this.labelFacility);
            this.Controls.Add(this.labelCard);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Name = "NewFacilityForm";
            this.Text = "New Facility";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}