# RFID Tag Converter

RFID Tag Converter is a simple utility to convert RFID tags to different formats. Access control systems store RFID tags in various formats, and this tool is designed to help convert those formats as needed. It is particularly useful for USB readers that simulate keyboard input.

## Features

- Convert RFID tags between different formats.
- Select input format, output format, and visible formats from the settings menu.
- Automatically copy the converted tag to the clipboard.
- Easy to add new custom formats by inheriting from the `Format` class and adding to the `FormatManager`.

## Installation

1. Clone the repository:

```sh
git clone https://github.com/yourusername/rfid-tag-converter.git
```

2. Open the project in Visual Studio.

3. Build the project to restore the NuGet packages.

## Usage

1. Run the application.

2. Go to the settings menu to select the desired input format, output format, and visible formats.

3. Enter the RFID tag into the input text box and press Enter. (Some readers add the Enter key at the end by default.)

4. The application will convert the tag to the selected output format and copy it to the clipboard.

## Adding New Formats

To add a new custom format, follow these steps:

1. Create a new class that inherits from the `Format` class.
2. Implement the required methods (`ToHex` and `FromHex`).
3. Add an instance of your new format class to the `FormatManager`.

### Example

```csharp
public class NewFormat : Format
{
    public NewFormat()
    {
        Name = "NewFormat";
        IsVisible = true;
        IsInput = true;
        IsOutput = true;
    }

    public override string ToHex(string input, TextBox textBox = null)
    {
        // Implement your conversion logic here
        return input; // Example implementation
    }

    public override string FromHex(string hex)
    {
        // Implement your conversion logic here
        return hex; // Example implementation
    }
}

// Add to FormatManager
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
            new SmartecFormat(),
            new NewFormat() // Add your new format here
        };

        var formatDictionary = new Dictionary<string, Format>();

        foreach (var format in formats)
        {
            formatDictionary[format.Name] = format;
        }

        return formatDictionary;
    }
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Thank you for using RFID Tag Converter! If you have any questions or feedback, please feel free to open an issue on GitHub.