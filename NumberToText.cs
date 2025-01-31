using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class NumberToText
{
    public Form GetForm()
    {
        // Create Form
        Form form = new Form
        {
            Text = "NUMBER TO TEXT CONVERSION",
            Width = 600,
            Height = 500
        };

        // Title Label
        Label windowTitle = new Label
        {
            Text = "NUMBER TEXT CONVERSION TECHNIQUE",
            Location = new System.Drawing.Point(120, 20),
            AutoSize = true
        };
        form.Controls.Add(windowTitle);

        // Input Label
        Label inputValue = new Label
        {
            Text = "INPUT NUMBER",
            Location = new System.Drawing.Point(20, 70),
            AutoSize = true
        };
        form.Controls.Add(inputValue);

        // Input TextBox
        TextBox inputValueBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 70),
            Width = 150
        };
        form.Controls.Add(inputValueBox);

        // Dropdown for selecting conversion type
        Label convertToLabel = new Label
        {
            Text = "SELECT LANGUAGE",
            Location = new System.Drawing.Point(300, 70),
            AutoSize = true
        };
        form.Controls.Add(convertToLabel);

        ComboBox conversionToDropdown = new ComboBox
        {
            Location = new System.Drawing.Point(430, 70),
            Width = 100,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        conversionToDropdown.Items.AddRange(new string[] { "Amharic", "English", "Afan Oromo", "Tigrinya" });
        conversionToDropdown.SelectedIndex = 0; // Default selection
        form.Controls.Add(conversionToDropdown);

        // Result Label
        Label resultLabel = new Label
        {
            Text = "RESULT",
            Location = new System.Drawing.Point(20, 130),
            AutoSize = true
        };
        form.Controls.Add(resultLabel);

        // Result TextBox
        Label resultBox = new Label
        {
            Text = "",
            Location = new System.Drawing.Point(120, 130),
            AutoSize = true
        };
        form.Controls.Add(resultBox);

        // Convert Button
        Button convertButton = new Button
        {
            Text = "CONVERT",
            Location = new System.Drawing.Point(250, 100),
            Width = 100
        };
        convertButton.Click += (sender, e) =>
        {
            string input = inputValueBox.Text;
            string toLanguage = conversionToDropdown.SelectedItem.ToString();

            try
            {
                string result = ConvertNumber(input, toLanguage);
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

    private string ConvertNumber(string input, string language)
    {
        if (!int.TryParse(input, out int number))
            throw new Exception("Invalid number entered.");

        string result;

        switch (language)
        {
            case "English":
                result = NumberToWords(number);
                break;
            case "Amharic":
                result = NumberToWordsAmharic(number);
                break;
            case "Afan Oromo":
                result = NumberToWordsAfanOromo(number);
                break;
            case "Tigrinya":
                result = NumberToWordsTigrigna(number);
                break;
            default:
                result = "Language not supported.";
                break;
        }

        return result;
    }

    private string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        var words = "";
        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                                  "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

            string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    private string NumberToWordsAmharic(int number)
    {
         if (number == 0)
            return " ዜሮ ";

        if (number < 0)
            return " ሲቀነስ " + NumberToWordsAmharic(Math.Abs(number));

        var words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWordsAmharic(number / 1000000) + " ሚሊዮን ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWordsAmharic(number / 1000) + " ሺህ ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWordsAmharic(number / 100) + " መቶ ";
            number %= 100;
        }

        if (number > 0)
        {
            string[] unitsMap = { "ዜሮ", "አንድ", "ሁለት", "ሶስት", "አራት", "አምስት", "ስድስት", "ሰባት", "ስምንት", "ዘጠኝ", "አስር", 
                                  "አስራ አንድ", "አስራ ሁለት", "አስራ ሶስት", "አስራ አራት", "አስራ አምስት", "አስራ ስድስት", "አስራ ሰባት", "አስራ ስምንት", "አስራ ዘጠኝ" };

            string[] tensMap = { "ዜሮ", "አስር", "ሀያ", "ሰላሳ", "አርባ", "ሀምሳ", "ስልሳ", "ሰባ", "ሰማንያ", "ዘጠና" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    private string NumberToWordsAfanOromo(int number)
    {
        if (number == 0)
            return "zeeroo";

        if (number < 0)
            return "nagativa " + NumberToWordsAfanOromo(Math.Abs(number));

        var words = "";

        if ((number / 1000000) > 0)
        {
            words += " miliyoona " + NumberToWordsAfanOromo(number / 1000000) + " fi ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += " kuma " + NumberToWordsAfanOromo(number / 1000)  + " fi ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += " dhibba " + NumberToWordsAfanOromo(number / 100)  + " fi ";
            number %= 100;
        }

        if (number > 0)
        {
            string[] unitsMap = { "zeeroo", "tokko", "lama", "sadii", "afur", "shan", "ja'a", "torba", "saddeet", "sagal", "kudhan", 
                                  "kudha tokko", "kudha lama", "kudha sadii", "kudha afur", "kudha shan", "kudha jaha", "kudha torba", "kudha saddeet", "kudha sagal" };

            string[] tensMap = { "zeeroo", "kudhan", "diigdami", "soddomi", "afurtami", "shantami", "jaatami", "torbaatami", "saddeettami", "sagaltami" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    private string NumberToWordsTigrigna(int number)
    {
        if (number == 0)
            return "ባዶ";

        if (number < 0)
            return "ኣጉዲልካ " + NumberToWordsTigrigna(Math.Abs(number));

        var words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWordsTigrigna(number / 1000000) + " ሚሊዮን ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWordsTigrigna(number / 1000) + " ሽሕ ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWordsTigrigna(number / 100) + " ሚእቲ ";
            number %= 100;
        }

        if (number > 0)
        {
            string[] unitsMap = { "ባዶ", "ሓደ", "ክልተ", "ሰለስተ", "ኣርባዕተ", "ሓሙሽተ", "ሽዱሽተ", "ሸውዓተ", "ሸሞንተ", "ትሸዓተ", "ዓሰርተ", 
                                  "ዓሰርተ ሓደ", "ዓሰርተ ክልተ", "ዓሰርተ ሰለስተ", "ዓሰርተ ኣርባዕተ", "ዓሰርተ ሓሙሽተ", "ዓሰርተ ሽድሽተ", "ዓሰርተ ሸውዓተ", "ዓሰርተ ሾሞንተ", "ዓሰርተ ትሽዓተ" };

            string[] tensMap = { "ባዶ", "ዓሰርተ", "ዒስራ", "ሰላሳ", "ኣርብዓ", "ሓምሳ", "ስድሳ", "ሰብዓ", "ሰማንያ", "ቴስዓ" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }
}
