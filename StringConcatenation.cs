using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

public class StringConcatenation
{
    public Form GetForm()
    {
        Form form = new Form
        {
            Text = "STRING CONCATENATION AND SPLIT",
            Width = 600,
            Height = 500
        };

        Label firstNameL = new Label
        {
            Text = "Your Name:",
            Location = new System.Drawing.Point(20, 20),
            AutoSize = true
        };
        form.Controls.Add(firstNameL);

        TextBox youNameBox = new TextBox
        {
            Location = new System.Drawing.Point(100, 20),
            Width = 200
        };
        form.Controls.Add(youNameBox);

        Label fatherNameL = new Label
        {
            Text = "Father Name:",
            Location = new System.Drawing.Point(20, 50),
            AutoSize = true
        };
        form.Controls.Add(fatherNameL);

        TextBox fatherNameBox = new TextBox
        {
            Location = new System.Drawing.Point(100, 50),
            Width = 200
        };
        form.Controls.Add(fatherNameBox);

        Label gFatherNameL = new Label
        {
            Text = "GFather Name:",
            Location = new System.Drawing.Point(20, 80),
            AutoSize = true
        };
        form.Controls.Add(gFatherNameL);

        TextBox gFatherNameBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 80),
            Width = 200
        };
        form.Controls.Add(gFatherNameBox);

        // Add a button
        Button button = new Button
        {
            Text = "Concatinate",
            Location = new System.Drawing.Point(50, 110)
        };

        Label fullName = new Label
        {
            Text = "Full Name:",
            Location = new System.Drawing.Point(20, 140),
            AutoSize = true
        };
        form.Controls.Add(fullName);

        TextBox fullNameBox = new TextBox
        {
            Location = new System.Drawing.Point(82, 140),
            Width = 200
        };
        form.Controls.Add(fullNameBox);

        Button split = new Button
        {
            Text = "Split",
            Location = new System.Drawing.Point(290, 140)
        };

        // Add a label for output
        Label outputLabel = new Label
        {
            Location = new System.Drawing.Point(20, 180),
            AutoSize = true
        };
        form.Controls.Add(outputLabel);

        // Button click event
        button.Click += (sender, e) =>
        {
            string firstName = youNameBox.Text;
            string lastName = fatherNameBox.Text;
            string gFatherName = gFatherNameBox.Text;

            // Regular expression to match only alphabetic characters
            string pattern = @"^[a-zA-Z]+$";
            if (!Regex.IsMatch(firstName, pattern) || !Regex.IsMatch(lastName, pattern) || !Regex.IsMatch(gFatherName, pattern))
            {
                MessageBox.Show("Please enter valid names (only letters).");
                return;
            }

            fullNameBox.Text = $"{firstName} {lastName} {gFatherName}";
            youNameBox.Clear();
            fatherNameBox.Clear();
            gFatherNameBox.Clear();
        };

        split.Click += (sender, e) =>
        {
            string fullName = fullNameBox.Text;
            string[] fullNames = fullName.Split(' ');
            if (fullNames.Length >= 3)
            {
                youNameBox.Text = fullNames[0]; 
                fatherNameBox.Text = fullNames[1]; 
                gFatherNameBox.Text = fullNames[2];
            }
            else
            {
                MessageBox.Show("Please enter a full name with three parts (First Name, Father's Name, Grandfather's Name).");
            }
            fullNameBox.Clear();
        };

        form.Controls.Add(button);
        form.Controls.Add(split);

        // Show the form
        return form;
    }
}
