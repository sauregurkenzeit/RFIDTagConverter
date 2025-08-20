using System;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace RFIDTagConverter
{
    public abstract class Format
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsInput { get; set; }
        public bool IsOutput { get; set; }

        protected Format(string name)
        {
            Name = name;
            InitializePropertiesFromSettings();
        }

        private void InitializePropertiesFromSettings()
        {
            var visibleFormats = Properties.Settings.Default.VisibleFormats.Split(',').ToList();
            IsVisible = visibleFormats.Contains(Name);
            IsInput = Name == Properties.Settings.Default.InputFormat;
            IsOutput = Name == Properties.Settings.Default.OutputFormat;
        }

        public abstract string ToHex(TextBox input);
        public abstract string FromHex(string hex);
    }
    public class HexFormat : Format
    {
        public HexFormat():base("Standard hexidecimal")
        {
        }

        public override string ToHex(TextBox input) => input.Text;
        public override string FromHex(string hex) => hex;
    }
    public class TIDFormat : Format
    {
        public TIDFormat():base("TID decimal")
        {
        }

        public override string ToHex(TextBox input) => int.Parse(input.Text).ToString("X6");
        public override string FromHex(string hex) => int.Parse(hex, NumberStyles.HexNumber).ToString("D8");
    }
    public class FacilityFormat : Format
    {
        public FacilityFormat():base("Card number with facility decimal")
        {
        }

        public override string ToHex(TextBox textBox)
        {
            string input = textBox.Text;
            if (!input.Contains(','))
            {
                if (input.Length == 5)
                    input = (string)Properties.Settings.Default["FacilityCode"] + ',' + input;
                else
                    input = input.PadLeft(8, '0').Insert(3, ",");
                textBox.Text = input;
            }

            string facility = int.Parse(input.Split(',')[0]).ToString("X");
            string code = int.Parse(input.Split(',')[1]).ToString("X");
            facility = string.Format("{0:00}", facility);
            code = string.Format("{0:0000}", code);
            return facility + code;
        }
        public override string FromHex(string hex)
        {
            string facility = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber).ToString("D3");
            string code = uint.Parse(hex.Substring(2, 4), NumberStyles.HexNumber).ToString("D5");
            return facility + ',' + code;
        }
    }
    public class OrionFormat : Format
    {
        public OrionFormat():base("Orion Pro with CRC")
        {
        }

        public override string ToHex(TextBox input) => input.Text.Substring(2, input.Text.Length - 4).TrimStart('0');
        public override string FromHex(string hex) => DallasCrc(hex) + hex.PadLeft(12, '0') + "01";
        private string DallasCrc(string input)
        {

            byte[] crc_array =
             {
                0x00, 0x5e, 0xbc, 0xe2, 0x61, 0x3f, 0xdd, 0x83,
                0xc2, 0x9c, 0x7e, 0x20, 0xa3, 0xfd, 0x1f, 0x41,
                0x9d, 0xc3, 0x21, 0x7f, 0xfc, 0xa2, 0x40, 0x1e,
                0x5f, 0x01, 0xe3, 0xbd, 0x3e, 0x60, 0x82, 0xdc,
                0x23, 0x7d, 0x9f, 0xc1, 0x42, 0x1c, 0xfe, 0xa0,
                0xe1, 0xbf, 0x5d, 0x03, 0x80, 0xde, 0x3c, 0x62,
                0xbe, 0xe0, 0x02, 0x5c, 0xdf, 0x81, 0x63, 0x3d,
                0x7c, 0x22, 0xc0, 0x9e, 0x1d, 0x43, 0xa1, 0xff,
                0x46, 0x18, 0xfa, 0xa4, 0x27, 0x79, 0x9b, 0xc5,
                0x84, 0xda, 0x38, 0x66, 0xe5, 0xbb, 0x59, 0x07,
                0xdb, 0x85, 0x67, 0x39, 0xba, 0xe4, 0x06, 0x58,
                0x19, 0x47, 0xa5, 0xfb, 0x78, 0x26, 0xc4, 0x9a,
                0x65, 0x3b, 0xd9, 0x87, 0x04, 0x5a, 0xb8, 0xe6,
                0xa7, 0xf9, 0x1b, 0x45, 0xc6, 0x98, 0x7a, 0x24,
                0xf8, 0xa6, 0x44, 0x1a, 0x99, 0xc7, 0x25, 0x7b,
                0x3a, 0x64, 0x86, 0xd8, 0x5b, 0x05, 0xe7, 0xb9,
                0x8c, 0xd2, 0x30, 0x6e, 0xed, 0xb3, 0x51, 0x0f,
                0x4e, 0x10, 0xf2, 0xac, 0x2f, 0x71, 0x93, 0xcd,
                0x11, 0x4f, 0xad, 0xf3, 0x70, 0x2e, 0xcc, 0x92,
                0xd3, 0x8d, 0x6f, 0x31, 0xb2, 0xec, 0x0e, 0x50,
                0xaf, 0xf1, 0x13, 0x4d, 0xce, 0x90, 0x72, 0x2c,
                0x6d, 0x33, 0xd1, 0x8f, 0x0c, 0x52, 0xb0, 0xee,
                0x32, 0x6c, 0x8e, 0xd0, 0x53, 0x0d, 0xef, 0xb1,
                0xf0, 0xae, 0x4c, 0x12, 0x91, 0xcf, 0x2d, 0x73,
                0xca, 0x94, 0x76, 0x28, 0xab, 0xf5, 0x17, 0x49,
                0x08, 0x56, 0xb4, 0xea, 0x69, 0x37, 0xd5, 0x8b,
                0x57, 0x09, 0xeb, 0xb5, 0x36, 0x68, 0x8a, 0xd4,
                0x95, 0xcb, 0x29, 0x77, 0xf4, 0xaa, 0x48, 0x16,
                0xe9, 0xb7, 0x55, 0x0b, 0x88, 0xd6, 0x34, 0x6a,
                0x2b, 0x75, 0x97, 0xc9, 0x4a, 0x14, 0xf6, 0xa8,
                0x74, 0x2a, 0xc8, 0x96, 0x15, 0x4b, 0xa9, 0xf7,
                0xb6, 0xe8, 0x0a, 0x54, 0xd7, 0x89, 0x6b, 0x35,
            };
            uint crc = 0;
            input = input.PadLeft(14, '0') + "01";
            for (int i = 7; i > 0; --i)
            {
                crc = crc_array[crc ^ byte.Parse(input.Substring(i * 2, 2), NumberStyles.HexNumber)];
            }
            return crc.ToString("X2");
        }
    }
    public class SmartecFormat : Format
    {
        public SmartecFormat():base("Smartec usb HID")
        {
        }

        public override string ToHex(TextBox input)
        {
            int facility = int.Parse((string)Properties.Settings.Default["FacilityCode"]);
            int k = FacilityMap.GetOffset(facility);
            int cardNumber = (int.Parse(input.Text) - k) & 0xFFFF;
            long tid = facility * 65536L + cardNumber;
            return tid.ToString("X");
        }
        public override string FromHex(string hex) => new String('?', 8);
    }

    public static class FacilityMap
    {
        // facility code -> K_facility
        public static Dictionary<int, int> Map { get; private set; } = new Dictionary<int, int>();

        public static void Load()
        {
            // load from settings (comma-separated facility=K pairs)
            string raw = Properties.Settings.Default["FacilityMap"] as string ?? "";
            Map.Clear();
            foreach (var entry in raw.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(entry)) continue;
                var parts = entry.Split('=');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int facility) &&
                    int.TryParse(parts[1], out int k))
                {
                    Map[facility] = k;
                }
            }
        }

        public static void Save()
        {
            var raw = string.Join(";", Map.Select(kv => $"{kv.Key}={kv.Value}"));
            Properties.Settings.Default["FacilityMap"] = raw;
            Properties.Settings.Default.Save();
        }

        public static int GetOffset(int facility)
        {
            if (Map.TryGetValue(facility, out int k))
                return k;
            return 0;
        }

        public static void AddNewFacility(int iValue, int facility, int card)
        {
            int k = (iValue - card) & 0xFFFF;  // compute offset
            Map[facility] = k;
            Save();
        }

    }

    public static class FormatManager
    {
        public static Dictionary<string, Format> AvailableFormats { get; } = InitializeFormats();

        private static Dictionary<string, Format> InitializeFormats()
        {
            var formats = new List<Format>
            {
                new HexFormat(),
                new TIDFormat(),
                new FacilityFormat(),
                new OrionFormat(),
                new SmartecFormat()
            };

            var formatDictionary = new Dictionary<string, Format>();

            foreach (var format in formats)
            {
                formatDictionary[format.Name] = format;
            }

            return formatDictionary;
        }
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
