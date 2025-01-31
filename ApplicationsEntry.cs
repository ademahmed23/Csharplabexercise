using System;
using System.Drawing;
using System.Windows.Forms;

public class ApplicationsEntry
{
    private Label entryLabel;

    public void Run()
    {
        // Create Form
        Form form = new Form
        {
            Text = "All Lab Exercises In One",
            Width = 1000,
            Height = 900,
            StartPosition = FormStartPosition.CenterScreen,
            BackColor = Color.WhiteSmoke
        };

        // Header Panel
        Panel headerPanel = new Panel
        {
            Size = new Size(form.Width, 100),
            BackColor = Color.Navy
        };
        form.Controls.Add(headerPanel);

        // Title Label
        Label windowTitle = new Label
        {
            Text = "All Lab Exercises In One",
            ForeColor = Color.White,
            Location = new Point(250, 30),
            AutoSize = true,
            Font = new Font("Arial", 28, FontStyle.Bold)
        };
        headerPanel.Controls.Add(windowTitle);

        // Time Label
        entryLabel = new Label
        {
            Text = "Main Window",
            Location = new Point(250, 130),
            AutoSize = true,
            Font = new Font("Arial", 48, FontStyle.Bold),
            ForeColor = Color.DarkBlue
        };
        form.Controls.Add(entryLabel);

        // Button Styles
        Font buttonFont = new Font("Arial", 12, FontStyle.Bold);
        Color buttonColor = Color.Black;
        Size buttonSize = new Size(280, 50);
        
        // Buttons Layout
        int xPos = 120, yPos = 250, spaceX = 300, spaceY = 80;

        Button[] buttons = new Button[9];
        string[] buttonTexts =
        {
            "String Concatenation", "Array Manipulation", "Encryption-decryption",
            "Number System conversion", "Num. To Text", "Stopwatch and Timer",
            "Calendar conversion", "ATM-Simulation", "EXPORT PDF"
        };

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = new Button
            {
                Text = buttonTexts[i],
                Size = buttonSize,
                Font = buttonFont,
                ForeColor = Color.White,
                BackColor = buttonColor,
                FlatStyle = FlatStyle.Flat
            };
            buttons[i].FlatAppearance.BorderSize = 0;
            form.Controls.Add(buttons[i]);
            
            buttons[i].Location = new Point(xPos, yPos);
            xPos += spaceX;
            if ((i + 1) % 2 == 0) { xPos = 120; yPos += spaceY; }
        }

        // Assign Events (Keeping Existing Logic)
        buttons[0].Click += (sender, e) => ShowForm(form, new StringConcatenation());
        buttons[1].Click += (sender, e) => ShowForm(form, new ArrayOperation());
        buttons[2].Click += (sender, e) => ShowForm(form, new EncryptionDecryption());
        buttons[3].Click += (sender, e) => ShowForm(form, new NumberSystemConversion());
        buttons[4].Click += (sender, e) => ShowForm(form, new NumberToText());
        buttons[5].Click += (sender, e) => ShowForm(form, new StopWatchTimer());
        buttons[6].Click += (sender, e) => ShowForm(form, new CalendarConverter());
        
        
        buttons[7].Click += (sender, e) =>
        {
        
            form.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            form.Show();
        };

        
        buttons[8].Click += (sender, e) => new PDFExporter().ExportToPDF("Lab Report", "This is a sample lab report content.");

        
        Application.Run(form);
    }

    private void ShowForm(Form parent, object instance)
    {
        parent.Hide();
        try
        {
            Form newForm = ((dynamic)instance).GetForm();
            newForm.ShowDialog();
        }
        finally
        {
            parent.Show();
        }
    }
}
