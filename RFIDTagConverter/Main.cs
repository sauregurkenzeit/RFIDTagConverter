using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RFIDTagConverter
{
    public partial class MainWindow : Form
    {
        private TextBox textBoxInput;
        private TextBox textBoxOutput;
        private List<TextBox> textBoxFmt = new List<TextBox>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Controls.Clear();
            textBoxFmt.Clear();
            string VisibleFormats = (string)Properties.Settings.Default["VisibleFormats"];
            string InputFormat = (string)Properties.Settings.Default["InputFormat"];
            string OutputFormat = (string)Properties.Settings.Default["OutputFormat"];
            var yPos = 35;
            var vSpacer = 28;
            int i = 0;
            textBoxInput = new TextBox
            {
                Text = InputFormat,
                Location = new Point(10, yPos),
                Width = 300
            };
            textBoxInput.KeyDown += new KeyEventHandler(InputTextBoxTest_KeyDown);
            yPos += vSpacer;

            foreach (var format in VisibleFormats.Split(','))
            {
                if (format != InputFormat & format != OutputFormat & !String.IsNullOrEmpty(format))
                {
                    textBoxFmt.Add(new TextBox
                    {
                        Text = format,
                        Name = format,
                        Location = new Point(10, yPos),
                        ReadOnly = true,
                        Enabled = false,
                        Width = 300,
                    });
                    Controls.Add(textBoxFmt[i]);
                    i++;
                    yPos += vSpacer;
                }
            }

            textBoxOutput = new TextBox
            {
                Text = OutputFormat,
                Location = new Point(10, yPos),
                ReadOnly = true,
                Width = 300
            };

            Controls.Add(textBoxInput);
            Controls.Add(textBoxOutput);
            InitializeComponent();
            Size = new Size(260, yPos + 50);
            ActiveControl = textBoxInput;
        }

        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.ShowDialog();
                FormLoad(sender, e);
            }
        }

        private void InputTextBoxTest_KeyDown(object sender, KeyEventArgs e)
        {
            string inputFormat = (string)Properties.Settings.Default["InputFormat"];
            string outputFormat = (string)Properties.Settings.Default["OutputFormat"];

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string hexValue = FormatManager.AvailableFormats[inputFormat].ToHex(textBoxInput);
                    foreach (var txtBoxFmt in textBoxFmt)
                    {
                        txtBoxFmt.Text = FormatManager.AvailableFormats[txtBoxFmt.Name].FromHex(hexValue);
                    }
                    string output = FormatManager.AvailableFormats[outputFormat].FromHex(hexValue);
                    textBoxOutput.Text = output;
                    Clipboard.SetText(output);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Conversion failed: {ex.Message}");
                }
                textBoxInput.SelectAll();
                textBoxInput.Focus();
            }
        }

    }

}
