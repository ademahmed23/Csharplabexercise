using System;
using System.Windows.Forms;

public class NumberSystemConversion
{
    public Form GetForm()
    {
        // Create Form
        Form form = new Form
        {
            Text = "NUMBER SYSTEM CONVERSION",
            Width = 600,
            Height = 500
        };
        form.BackColor = System.Drawing.Color.LightSkyBlue; // Set form background color

        // Title Label
        Label windowTitle = new Label
        {
            Text = "NUMBER SYSTEM CONVERSION TECHNIQUE",
            Location = new System.Drawing.Point(120, 20),
            AutoSize = true,
            Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.MidnightBlue // Title color
        };
        form.Controls.Add(windowTitle);

        // Input Label
        Label inputValue = new Label
        {
            Text = "INPUT VALUE",
            Location = new System.Drawing.Point(20, 70),
            AutoSize = true,
            Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.DarkCyan // Label color
        };
        form.Controls.Add(inputValue);

        // Input TextBox
        TextBox inputValueBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 70),
            Width = 200,
            BackColor = System.Drawing.Color.LightYellow, // Background color of textbox
            ForeColor = System.Drawing.Color.DarkSlateGray // Text color
        };
        form.Controls.Add(inputValueBox);

        // Dropdown for selecting conversion type (from)
        Label convertFromLabel = new Label
        {
            Text = "CONVERT FROM",
            Location = new System.Drawing.Point(20, 110),
            AutoSize = true,
            Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.DarkCyan
        };
        form.Controls.Add(convertFromLabel);

        ComboBox conversionFromDropdown = new ComboBox
        {
            Location = new System.Drawing.Point(120, 110),
            Width = 100,
            DropDownStyle = ComboBoxStyle.DropDownList,
            BackColor = System.Drawing.Color.LightGoldenrodYellow, // Dropdown background
            ForeColor = System.Drawing.Color.DarkSlateGray
        };
        conversionFromDropdown.Items.AddRange(new string[] { "Decimal", "Hexadecimal", "Octal", "Binary" });
        conversionFromDropdown.SelectedIndex = 0; // Default selection
        form.Controls.Add(conversionFromDropdown);

        // Dropdown for selecting conversion type (to)
        Label convertToLabel = new Label
        {
            Text = "CONVERT TO",
            Location = new System.Drawing.Point(240, 110),
            AutoSize = true,
            Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.DarkCyan
        };
        form.Controls.Add(convertToLabel);

        ComboBox conversionTypeDropdown = new ComboBox
        {
            Location = new System.Drawing.Point(320, 110),
            Width = 100,
            DropDownStyle = ComboBoxStyle.DropDownList,
            BackColor = System.Drawing.Color.LightGoldenrodYellow,
            ForeColor = System.Drawing.Color.DarkSlateGray
        };
        conversionTypeDropdown.Items.AddRange(new string[] { "Decimal", "Hexadecimal", "Octal", "Binary" });
        conversionTypeDropdown.SelectedIndex = 0; // Default selection
        form.Controls.Add(conversionTypeDropdown);

        // Result Label
        Label resultLabel = new Label
        {
            Text = "RESULT",
            Location = new System.Drawing.Point(20, 180),
            AutoSize = true,
            Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.DarkCyan
        };
        form.Controls.Add(resultLabel);

        // Result TextBox
        TextBox resultBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 180),
            Width = 200,
            BackColor = System.Drawing.Color.LightYellow,
            ForeColor = System.Drawing.Color.DarkSlateGray
        };
        form.Controls.Add(resultBox);

        // Convert Button
        Button convertButton = new Button
        {
            Text = "CONVERT",
            Location = new System.Drawing.Point(200, 150),
            BackColor = System.Drawing.Color.Orange, // Button background color
            ForeColor = System.Drawing.Color.White, // Button text color
            Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
            FlatStyle = FlatStyle.Flat
        };
        convertButton.Click += (sender, e) =>
        {
            string input = inputValueBox.Text;
            string fromSystem = conversionFromDropdown.SelectedItem.ToString();
            string toSystem = conversionTypeDropdown.SelectedItem.ToString();

            try
            {
                string result = ConvertNumber(input, fromSystem, toSystem);
                resultBox.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        form.Controls.Add(convertButton);

        // Run the application
        return form;
    }

    private string ConvertNumber(string input, string from, string to)
    {
        int number = from switch
        {
            "Decimal" => int.Parse(input),
            "Hexadecimal" => Convert.ToInt32(input, 16),
            "Octal" => Convert.ToInt32(input, 8),
            "Binary" => Convert.ToInt32(input, 2),
            _ => throw new ArgumentException("Invalid from-system type selected.")
        };

        string result = to switch
        {
            "Decimal" => number.ToString(),
            "Hexadecimal" => Convert.ToString(number, 16).ToUpper(),
            "Octal" => Convert.ToString(number, 8),
            "Binary" => Convert.ToString(number, 2),
            _ => throw new ArgumentException("Invalid to-system type selected.")
        };

        return result;
    }
}
