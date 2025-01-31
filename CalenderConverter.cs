using System;
using System.Windows.Forms;

public class CalendarConverter
{
    public Form GetForm()
    {
        string[] amharicMonths = { "መስከረም/Meskerem", "ጥቅምት/Tikimt", "ኅዳር/Hidar", "ታህሣሥ/Tahsas", "ጥር/Tir",
                                   "የካቲት/Yekatit", "መጋቢት/Megabit", "ሚያዚያ/Miazia", "ግንቦት/Genbot", "ሰኔ/Sene",
                                   "ሐምሌ/Hamle", "ነሐሴ/Nehase", "ጳጕሜ/Puagume" };

        string[] gregorianMonths = { "January", "February", "March", "April", "May", "June",
                                     "July", "August", "September", "October", "November", "December" };

        Form form = new Form
        {
            Text = "Ethiopian to Gregorian Calendar Converter",
            Width = 600,
            Height = 400
        };

        Label titleLabel = new Label
        {
            Text = "Ethiopian to Gregorian Calendar Converter",
            AutoSize = true,
            Location = new System.Drawing.Point(150, 20)
        };
        form.Controls.Add(titleLabel);

        ComboBox calendarTypeDropdown = new ComboBox
        {
            Location = new System.Drawing.Point(50, 50),
            Width = 200,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        calendarTypeDropdown.Items.AddRange(new string[] { "Ethiopian", "Gregorian" });
        calendarTypeDropdown.SelectedIndex = 0;
        form.Controls.Add(calendarTypeDropdown);

        Label monthLabel = new Label
        {
            Text = "Month:",
            Location = new System.Drawing.Point(50, 100),
            AutoSize = true
        };
        form.Controls.Add(monthLabel);

        ComboBox monthDropdown = new ComboBox
        {
            Location = new System.Drawing.Point(110, 95),
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        monthDropdown.Items.AddRange(amharicMonths);
        monthDropdown.SelectedIndex = 0;
        form.Controls.Add(monthDropdown);

        Label dayLabel = new Label
        {
            Text = "Day:",
            Location = new System.Drawing.Point(270, 100),
            AutoSize = true
        };
        form.Controls.Add(dayLabel);

        TextBox dayInput = new TextBox
        {
            Location = new System.Drawing.Point(310, 95),
            Width = 50
        };
        form.Controls.Add(dayInput);

        Label yearLabel = new Label
        {
            Text = "Year:",
            Location = new System.Drawing.Point(370, 100),
            AutoSize = true
        };
        form.Controls.Add(yearLabel);

        TextBox yearInput = new TextBox
        {
            Location = new System.Drawing.Point(420, 95),
            Width = 70
        };
        form.Controls.Add(yearInput);

        calendarTypeDropdown.SelectedIndexChanged += (sender, e) =>
        {
            monthDropdown.Items.Clear();
            if (calendarTypeDropdown.SelectedItem.ToString() == "Ethiopian")
            {
                monthDropdown.Items.AddRange(amharicMonths);
            }
            else
            {
                monthDropdown.Items.AddRange(gregorianMonths);
            }
            monthDropdown.SelectedIndex = 0;
        };

        Label resultOutput = new Label
        {
            Location = new System.Drawing.Point(180, 180),
            AutoSize = true
        };
        form.Controls.Add(resultOutput);

        Button convertButton = new Button
        {
            Text = "Convert",
            Location = new System.Drawing.Point(170, 230),
            Width = 100
        };
        convertButton.Click += (sender, e) =>
        {
            try
            {
                int day = int.Parse(dayInput.Text);
                int year = int.Parse(yearInput.Text);
                string selectedMonth = monthDropdown.SelectedItem.ToString();
                string calendarType = calendarTypeDropdown.SelectedItem.ToString();

                string convertedDate = ConvertDate(calendarType, selectedMonth, day, year);
                resultOutput.Text = convertedDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        form.Controls.Add(convertButton);

        return form;
    }

    private string ConvertDate(string calendarType, string selectedMonth, int day, int year)
    {
        string[] amharicMonths = { "መስከረም/Meskerem", "ጥቅምት/Tikimt", "ኅዳር/Hidar", "ታህሣሥ/Tahsas", "ጥር/Tir",
                               "የካቲት/Yekatit", "መጋቢት/Megabit", "ሚያዚያ/Miazia", "ግንቦት/Genbot", "ሰኔ/Sene",
                               "ሐምሌ/Hamle", "ነሐሴ/Nehase", "ጳጕሜ/Puagume" };

        string[] gregorianMonths = { "September", "October", "November", "December", "January", "February",
                                 "March", "April", "May", "June", "July", "August" };

        if (calendarType == "Ethiopian")
        {
            // Ethiopian to Gregorian Conversion
            int ethiopianMonthIndex = Array.IndexOf(amharicMonths, selectedMonth);
            if (ethiopianMonthIndex == -1) throw new Exception("Invalid Ethiopian month.");

            int gregorianMonthIndex = ethiopianMonthIndex - 1;
            if (gregorianMonthIndex < 0) gregorianMonthIndex += 12;

            int adjustedYear = year + ((ethiopianMonthIndex < 4) ? 8 : 7); // Add 7 or 8 years
            int adjustedDay = day; // Day remains the same for most cases

            // Adjust for leap year
            if (IsEthiopianLeapYear(year) && ethiopianMonthIndex == 12 && day == 6) adjustedDay += 1;

            string gregorianMonth = gregorianMonths[gregorianMonthIndex];
            return $"{gregorianMonth} {adjustedDay}, {adjustedYear}";
        }
        else
        {
            // Gregorian to Ethiopian Conversion
            int gregorianMonthIndex = Array.IndexOf(gregorianMonths, selectedMonth);
            if (gregorianMonthIndex == -1) throw new Exception("Invalid Gregorian month.");

            int ethiopianMonthIndex = (gregorianMonthIndex + 1) % 12;
            int adjustedYear = year - ((gregorianMonthIndex >= 4) ? 8 : 7); // Subtract 7 or 8 years
            int adjustedDay = day; // Day remains the same for most cases

            // Adjust for leap year
            if (IsGregorianLeapYear(year) && gregorianMonthIndex == 1 && day == 29) adjustedDay -= 1;

            string ethiopianMonth = amharicMonths[ethiopianMonthIndex];
            return $"{ethiopianMonth} {adjustedDay}, {adjustedYear}";
        }
    }

    // Helper methods to check leap years
    private bool IsEthiopianLeapYear(int year)
    {
        return (year + 1) % 4 == 0;
    }

    private bool IsGregorianLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

}
