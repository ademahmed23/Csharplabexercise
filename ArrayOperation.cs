using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class ArrayOperation
{
    public Form GetForm()
    {
        Form form = new Form
        {
            Text = "Array Manipulation",
            Width = 600,
            Height = 500,
            BackColor = System.Drawing.Color.Pink // Background color of the form
        };

        // Label for input instructions
        Label integerLabel = new Label
        {
            Text = "Please Insert Num That Separeted by (,)! ",
            Location = new System.Drawing.Point(120, 20),
            AutoSize = true,
            ForeColor = System.Drawing.Color.DarkRed, // Text color
            Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
        };
        form.Controls.Add(integerLabel);

        // Label for array
        Label arrayLabel = new Label
        {
            Text = "INTEGER ARRAY",
            Location = new System.Drawing.Point(20, 50),
            AutoSize = true,
            ForeColor = System.Drawing.Color.DarkGreen, // Text color
            Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic)
        };
        form.Controls.Add(arrayLabel);

        // TextBox to input the array of numbers
        TextBox arrayBox = new TextBox
        {
            Location = new System.Drawing.Point(160, 50),
            Width = 200,
            BackColor = System.Drawing.Color.WhiteSmoke,
            ForeColor = System.Drawing.Color.Black
        };
        form.Controls.Add(arrayBox);

        // GroupBoxes for the results (min, max, avg, sum, count)
        GroupBox minGroup = CreateGroupBox("Minimum", 20, 80, System.Drawing.Color.LightCoral);
        GroupBox maxGroup = CreateGroupBox("Maximum", 120, 80, System.Drawing.Color.LightGreen);
        GroupBox avgGroup = CreateGroupBox("Average", 220, 80, System.Drawing.Color.LightYellow);
        GroupBox sumGroup = CreateGroupBox("Sum", 320, 80, System.Drawing.Color.LightSkyBlue);
        GroupBox countGroup = CreateGroupBox("Count", 420, 80, System.Drawing.Color.LightGoldenrodYellow);

        // Labels inside each GroupBox to display results
        Label minLabel = new Label { Location = new System.Drawing.Point(10, 20), AutoSize = true, ForeColor = System.Drawing.Color.White };
        Label maxLabel = new Label { Location = new System.Drawing.Point(10, 20), AutoSize = true, ForeColor = System.Drawing.Color.White };
        Label avgLabel = new Label { Location = new System.Drawing.Point(10, 20), AutoSize = true, ForeColor = System.Drawing.Color.White };
        Label sumLabel = new Label { Location = new System.Drawing.Point(10, 20), AutoSize = true, ForeColor = System.Drawing.Color.White };
        Label countLabel = new Label { Location = new System.Drawing.Point(10, 20), AutoSize = true, ForeColor = System.Drawing.Color.White };

        minGroup.Controls.Add(minLabel);
        maxGroup.Controls.Add(maxLabel);
        avgGroup.Controls.Add(avgLabel);
        sumGroup.Controls.Add(sumLabel);
        countGroup.Controls.Add(countLabel);

        // Add GroupBoxes to the form
        form.Controls.Add(minGroup);
        form.Controls.Add(maxGroup);
        form.Controls.Add(avgGroup);
        form.Controls.Add(sumGroup);
        form.Controls.Add(countGroup);

        // Buttons to calculate and export the description
        Button button = new Button
        {
            Text = "DO ALL",
            Location = new System.Drawing.Point(220, 210),
            BackColor = System.Drawing.Color.MediumPurple,
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
        };
        Button exportButton = new Button
        {
            Text = "EXPORT DESCRIPTION",
            Location = new System.Drawing.Point(320, 210),
            BackColor = System.Drawing.Color.Coral,
            ForeColor = System.Drawing.Color.White,
            Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
        };

        // Labels for reverse and sorted arrays
        Label reverseArray = new Label { Text = "REVERSE ARRAY", Location = new System.Drawing.Point(20, 240), AutoSize = true, ForeColor = System.Drawing.Color.MediumPurple, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Underline) };
        TextBox reverseBox = new TextBox { Location = new System.Drawing.Point(160, 240), Width = 200, BackColor = System.Drawing.Color.WhiteSmoke };
        Label sortedArray = new Label { Text = "SORTED ARRAY", Location = new System.Drawing.Point(20, 290), AutoSize = true, ForeColor = System.Drawing.Color.MediumPurple, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Underline) };
        TextBox sortedBox = new TextBox { Location = new System.Drawing.Point(160, 290), Width = 200, BackColor = System.Drawing.Color.WhiteSmoke };

        form.Controls.Add(reverseArray);
        form.Controls.Add(reverseBox);
        form.Controls.Add(sortedArray);
        form.Controls.Add(sortedBox);

        // Button click event to process the array and display results
        button.Click += (sender, e) =>
        {
            string arrayInput = arrayBox.Text;
            string[] arrayValues = arrayInput.Split(',');
            try
            {
                var numericValues = arrayValues.Select(v => double.Parse(v.Trim())).ToArray();

                // Perform operations
                double minValue = numericValues.Min();
                double maxValue = numericValues.Max();
                double averageValue = numericValues.Average();
                double sumValue = numericValues.Sum();
                double countValue = numericValues.Length;

                // Update results in GroupBoxes
                minLabel.Text = minValue.ToString();
                maxLabel.Text = maxValue.ToString();
                avgLabel.Text = averageValue.ToString();
                sumLabel.Text = sumValue.ToString();
                countLabel.Text = countValue.ToString();

                reverseBox.Text = string.Join(", ", numericValues.Reverse());
                sortedBox.Text = string.Join(", ", numericValues.OrderBy(x => x));
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values separated by commas.");
            }
        };

        // Button click event to export the description as a PDF
        exportButton.Click += (sender, e) =>
        {
            ExportDescription();
            MessageBox.Show("Description exported to Desktop as program_description.pdf");
        };

        form.Controls.Add(button);
        form.Controls.Add(exportButton);

        return form;
    }

    // Helper method to create a GroupBox for each result category with a background color
    private GroupBox CreateGroupBox(string text, int x, int y, System.Drawing.Color bgColor)
    {
        return new GroupBox
        {
            Text = text,
            Location = new System.Drawing.Point(x, y),
            Width = 80,
            Height = 60,
            BackColor = bgColor,
            ForeColor = System.Drawing.Color.White
        };
    }

    // Method to export the program description as a PDF
    private void ExportDescription()
    {
        string description = "This C# program, 'Array Operation', is a Windows Forms application that allows users to perform basic operations on a list of numbers entered as comma-separated values. " +
                             "\n\nObjectives:\n" +
                             "1. User Input Handling: Users enter a list of numbers in a text box.\n" +
                             "2. Mathematical Operations: Find Min, Max, Average, Sum, and Count.\n" +
                             "3. Sorting and Reversing: Displays the sorted and reversed version of the numbers.\n" +
                             "4. User-Friendly Interface: Labels and text boxes display results clearly.\n" +
                             "5. Error Handling: Ensures users enter only valid numeric values.\n";

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "program_description.pdf");

        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();
        document.Add(new Paragraph(description));
        document.Close();
    }
}
