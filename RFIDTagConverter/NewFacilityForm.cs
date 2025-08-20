using System;
using System.Windows.Forms;

namespace RFIDTagConverter
{
    public partial class NewFacilityForm : Form
    {
        public int Facility { get; private set; }
        public int IValue { get; private set; }
        public int Card { get; private set; }

        public NewFacilityForm(int facility)
        {
            InitializeComponent();
            textBoxIValue.Text = "Reader input";
            textBoxIValue.SelectAll();
            textBoxIValue.Focus();
            textBoxFacility.Text = facility.ToString("D3");
            textBoxCard.Text = "Enter card number...";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxIValue.Text, out int iVal) ||
                !int.TryParse(textBoxFacility.Text, out int fac) ||
                !int.TryParse(textBoxCard.Text, out int card))
            {
                MessageBox.Show("Please enter valid numbers.");
                return;
            }

            IValue = iVal;
            Facility = fac;
            Card = card;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}