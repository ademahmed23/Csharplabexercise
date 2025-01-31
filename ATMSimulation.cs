using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

public class LoginWindow : Form
{
    private TextBox idTextBox;
    private Button loginButton;

    // MySQL connection string (update with your credentials)
    private string connectionString = "server=localhost;uid=root;pwd=;database=atm_db"; // Change your password

    public LoginWindow()
    {
        // Configure the form
        this.Text = "ATM - Login";
        this.Width = 400;
        this.Height = 200;

        // Add a label with a colorful design
        Label idLabel = new Label
        {
            Text = "Enter your ID:",
            Location = new Point(50, 50),
            AutoSize = true,
            ForeColor = Color.Blue // Set label text color to blue
        };
        this.Controls.Add(idLabel);

        // Add a textbox for ID input
        idTextBox = new TextBox
        {
            Location = new Point(150, 50),
            Width = 150
        };
        this.Controls.Add(idTextBox);

        // Add a button to proceed with a colorful design
        loginButton = new Button
        {
            Text = "Login",
            Location = new Point(150, 100),
            Width = 80,
            BackColor = Color.LightGreen // Set button background color
        };
        loginButton.Click += LoginButton_Click;
        this.Controls.Add(loginButton);
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
        string userID = idTextBox.Text;
        string userDetails = GetUserDetails(userID);

        if (userDetails != null)
        {
            this.Hide(); // Hide the login window
            WelcomeWindow welcomeWindow = new WelcomeWindow(userID, userDetails);
            welcomeWindow.ShowDialog(); // Show the welcome window
            this.Close(); // Close the login window after welcome window is closed
        }
        else
        {
            MessageBox.Show("Invalid ID. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string GetUserDetails(string userID)
    {
        string userDetails = null;

        try
        {
            // Connect to MySQL database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Prepare SQL query
                string query = "SELECT details FROM users WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userID);

                // Execute the query and retrieve user details
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    userDetails = result.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that may have occurred
            MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return userDetails;
    }
}

public class WelcomeWindow : Form
{
    public WelcomeWindow(string userID, string userDetails)
    {
        // Configure the form
        this.Text = "Welcome";
        this.Width = 400;
        this.Height = 300;

        // Add a label with a colorful design
        Label welcomeLabel = new Label
        {
            Text = $"Welcome, {userID}!\nDetails: {userDetails}",
            Location = new Point(50, 20),
            AutoSize = true,
            Font = new Font("Arial", 12, FontStyle.Regular),
            ForeColor = Color.Green // Set label text color to green
        };
        this.Controls.Add(welcomeLabel);

        // Add buttons for options with colorful styles
        string[] options = { "Withdraw", "Balance Inquiry", "Transfer", "Deposit", "Change PIN" };
        int xOffset = 50; // Starting x-coordinate for buttons
        int yOffset = 80; // Starting y-coordinate for buttons
        int buttonWidth = 120;
        int buttonHeight = 40;

        for (int i = 0; i < options.Length; i++)
        {
            int index = i;
            Button optionButton = new Button
            {
                Text = options[i],
                Width = buttonWidth,
                Height = buttonHeight,
                Location = new Point(xOffset + (i % 2) * (buttonWidth + 20), yOffset + (i / 2) * (buttonHeight + 10)),
                BackColor = Color.LightSkyBlue // Set button background color
            };

            // Add event handler for each button
            optionButton.Click += (sender, e) => NavigateToOptionWindow(userID, options[index]);
            this.Controls.Add(optionButton);
        }
    }

    private void NavigateToOptionWindow(string userID, string option)
    {
        Form optionWindow;

        // Select the appropriate window based on the option
        switch (option)
        {
            case "Withdraw":
                optionWindow = new WithdrawWindow(userID);
                break;
            case "Balance Inquiry":
                optionWindow = new BalanceInquiryWindow(userID);
                break;
            case "Transfer":
                optionWindow = new TransferWindow(userID);
                break;
            case "Deposit":
                optionWindow = new DepositWindow(userID);
                break;
            case "Change PIN":
                optionWindow = new ChangePinWindow(userID);
                break;
            default:
                throw new ArgumentException("Invalid option");
        }

        this.Hide(); // Hide the Welcome Window
        optionWindow.ShowDialog(); // Show the selected Option Window
        this.Show(); // Show the Welcome Window again after Option Window is closed
    }
}

// Base class for option windows
public class OptionWindowBase : Form
{
    public OptionWindowBase(string title, string userID)
    {
        this.Text = title;
        this.Width = 400;
        this.Height = 200;

        Label optionLabel = new Label
        {
            Text = $"You selected: {title}\nUser ID: {userID}",
            Location = new Point(50, 50),
            AutoSize = true,
            Font = new Font("Arial", 12, FontStyle.Regular),
            ForeColor = Color.Purple // Set label text color to purple
        };
        this.Controls.Add(optionLabel);

        Button backButton = new Button
        {
            Text = "Back",
            Location = new Point(150, 100),
            Width = 100,
            BackColor = Color.Orange // Set button background color
        };
        backButton.Click += (sender, e) => this.Close();
        this.Controls.Add(backButton);
    }
}

// Specific option windows
public class WithdrawWindow : OptionWindowBase
{
    public WithdrawWindow(string userID) : base("Withdraw", userID) { }
}

public class BalanceInquiryWindow : OptionWindowBase
{
    public BalanceInquiryWindow(string userID) : base("Balance Inquiry", userID) { }
}

public class TransferWindow : OptionWindowBase
{
    public TransferWindow(string userID) : base("Transfer", userID) { }
}

public class DepositWindow : OptionWindowBase
{
    public DepositWindow(string userID) : base("Deposit", userID) { }
}

public class ChangePinWindow : OptionWindowBase
{
    public ChangePinWindow(string userID) : base("Change PIN", userID) { }
}
