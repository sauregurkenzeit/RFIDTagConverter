using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RFIDTagConverter
{
    public partial class SettingsForm : Form
    {


        private TextBox FacilityTextBox = new TextBox
        {
            Text = (string)Properties.Settings.Default["FacilityCode"],
        };

        private TextBox FacilityOffsetTextBox = new TextBox
        {
           Enabled = false,
        };

        public SettingsForm()
        {
            InitializeComponent();
            PopulateFormatOptions();
        }

        private void PopulateFormatOptions()
        {
            var yPos = 25;
            string VisibleFormats = (string)Properties.Settings.Default["VisibleFormats"];
            string InputFormat = (string)Properties.Settings.Default["InputFormat"];
            string OutputFormat = (string)Properties.Settings.Default["OutputFormat"];
            foreach (var format in FormatManager.AvailableFormats.Values.ToList())
            {
                var checkBox = new CheckBox { Text = format.Name, Checked = format.IsVisible };
                checkBox.CheckedChanged += (sender, e) => format.IsVisible = checkBox.Checked;

                var radioButtonInput = new RadioButton { Checked = format.IsInput };
                radioButtonInput.CheckedChanged += (sender, e) => format.IsInput = radioButtonInput.Checked;

                var radioButtonOutput = new RadioButton { Checked = format.IsOutput };
                radioButtonOutput.CheckedChanged += (sender, e) => format.IsOutput = radioButtonOutput.Checked;

                checkBox.Location = new Point(15, yPos+12);
                checkBox.AutoSize = false;
                checkBox.Width = 100;
                checkBox.Height = 30;
                checkBox.CheckAlign = ContentAlignment.MiddleLeft;
                checkBox.TextAlign = ContentAlignment.MiddleLeft;

                radioButtonInput.Location = new Point(10, yPos);
                radioButtonOutput.Location = new Point(10, yPos);
                Controls.Add(checkBox);
                groupBoxInput.Controls.Add(radioButtonInput);
                groupBoxOutput.Controls.Add(radioButtonOutput);
                
                yPos += 50;
            }
            groupBoxInput.Height = yPos;
            groupBoxOutput.Height = yPos;

            yPos += 20;

            var FacilityCodeLabel = new Label
            {
                Text = "Default facility code",
                Location = new Point(10, yPos)
            };
            FacilityTextBox.Location = new Point(110, yPos);
            Controls.Add(FacilityCodeLabel);
            Controls.Add(FacilityTextBox);

            yPos += 25;

            var FacilityOffsetLabel = new Label
            {
                Text = "Facility offset",
                Location = new Point(10, yPos)
            };
            var FacilityCode = int.Parse(Properties.Settings.Default["FacilityCode"] as string);
            FacilityOffsetTextBox.Text = FacilityMap.GetOffset(FacilityCode).ToString("D3");
            FacilityOffsetTextBox.Location = new Point(110, yPos);
            Controls.Add(FacilityOffsetLabel);
            Controls.Add(FacilityOffsetTextBox);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            var formats = FormatManager.AvailableFormats.Values.ToList();
            var selectedInput = formats.FirstOrDefault(f => f.IsInput)?.Name;
            var selectedOutput = formats.FirstOrDefault(f => f.IsOutput)?.Name;
            var facility = int.Parse(FacilityTextBox.Text);

            if (!FacilityMap.Map.ContainsKey(facility))
            {
                using (var dlg = new NewFacilityForm(facility))
                {
                    dlg.TopMost = true;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        FacilityMap.AddNewFacility(dlg.IValue, dlg.Facility, dlg.Card);
                        MessageBox.Show($"New facility {dlg.Facility} added with offset {FacilityMap.GetOffset(facility):D3}");
                    }
                    else
                    {
                        return; // user canceled
                    }
                }
            }

            if (string.IsNullOrEmpty(selectedInput) || string.IsNullOrEmpty(selectedOutput))
            {
                MessageBox.Show("Please select both input and output formats.");
                return;
            }
            string VisibleFormats = string.Empty;
            foreach (var format in formats)
            {
                if (format.IsVisible)
                {
                    VisibleFormats += ',' + format.Name;
                }
            }

            Properties.Settings.Default["InputFormat"] = selectedInput;
            Properties.Settings.Default["OutputFormat"] = selectedOutput;
            Properties.Settings.Default["VisibleFormats"] = VisibleFormats;
            Properties.Settings.Default["FacilityCode"] = FacilityTextBox.Text;
            Properties.Settings.Default.Save();

            Close();
        }
    }
}
