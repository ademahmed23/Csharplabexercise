using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media; // Required for playing sounds

public class StopWatch
{
    private Timer timer;
    private Label timeLabel;
    private bool isRunning = false;
    private TimeSpan currentTime;

    public void Run()
    {
        // Create Form
        Form form = new Form
        {
            Text = "STOPWATCH AND TIMER",
            Width = 800,
            Height = 500
        };

        // Title Label
        Label windowTitle = new Label
        {
            Text = "STOPWATCH AND TIMER",
            Location = new Point(80, 20),
            AutoSize = true,
            Font = new Font("Arial", 36, FontStyle.Bold)
        };
        form.Controls.Add(windowTitle);

        // Time Label
        timeLabel = new Label
        {
            Text = "00:00:00",
            Location = new Point(120, 110),
            AutoSize = true,
            Font = new Font("Arial", 86, FontStyle.Bold),
            BorderStyle = BorderStyle.FixedSingle
        };
        form.Controls.Add(timeLabel);

        // Buttons
        Button resetButton = new Button
        {
            Text = "RESET",
            Location = new Point(130, 300),
            Width = 100
        };
        Button setButton = new Button
        {
            Text = "SET-TIMER",
            Location = new Point(250, 300),
            Width = 100
        };
        Button startButton = new Button
        {
            Text = "START",
            Location = new Point(370, 300),
            Width = 100
        };
        Button pauseButton = new Button
        {
            Text = "PAUSE",
            Location = new Point(490, 300),
            Width = 100
        };

        // Add Buttons to the form
        form.Controls.Add(resetButton);
        form.Controls.Add(setButton);
        form.Controls.Add(startButton);
        form.Controls.Add(pauseButton);

        // Timer initialization
        timer = new Timer();
        timer.Interval = 1000; // 1 second
        timer.Tick += Timer_Tick;

        // Button Functions
        setButton.Click += (sender, e) => 
        {
            timeLabel.BorderStyle = BorderStyle.Fixed3D; // Highlight the label
            timeLabel.Click += MakeTimeEditable;
            timeLabel.Focus();
        };

        startButton.Click += (sender, e) => 
        {
            if (!isRunning)
            {
                isRunning = true;
                StartTimer();
            }
        };

        pauseButton.Click += (sender, e) => 
        {
            if (isRunning)
            {
                PauseTimer();
            }
        };

        resetButton.Click += (sender, e) => 
        {
            ResetTimer();
        };

        // Run the application
        Application.Run(form);
    }

    // Function to make the time label editable
    private void MakeTimeEditable(object sender, EventArgs e)
    {
        TextBox textBox = new TextBox
        {
            Text = timeLabel.Text,
            Location = timeLabel.Location,
            Size = timeLabel.Size,
            Font = timeLabel.Font
        };

        timeLabel.Visible = false; // Hide the label
        Form parentForm = timeLabel.Parent as Form;
        parentForm.Controls.Add(textBox);
        textBox.SelectAll();
        textBox.Focus();

        textBox.KeyDown += (s, args) => 
        {
            if (args.KeyCode == Keys.Enter)
            {
                string newTime = textBox.Text;
                if (TimeSpan.TryParse(newTime, out TimeSpan parsedTime))
                {
                    currentTime = parsedTime;
                    timeLabel.Text = parsedTime.ToString(@"hh\:mm\:ss");
                    textBox.Dispose();
                    timeLabel.Visible = true;
                }
                else
                {
                    MessageBox.Show("Invalid Time Format. Use HH:MM:SS.");
                }
            }
        };

        textBox.Leave += (s, args) => 
        {
            textBox.Dispose();
            timeLabel.Visible = true;
        };
    }

    // Start the timer
    private void StartTimer()
    {
        timer.Start();
    }

    // Pause the timer
    private void PauseTimer()
    {
        timer.Stop();
        isRunning = false;
    }

    // Reset the timer
    private void ResetTimer()
    {
        timer.Stop();
        isRunning = false;
        currentTime = TimeSpan.Zero;
        timeLabel.Text = "00:00:00";
    }

    // Timer tick event to update the time
    private void Timer_Tick(object sender, EventArgs e)
    {
        if (currentTime.TotalSeconds > 0)
        {
            currentTime = currentTime.Subtract(TimeSpan.FromSeconds(1));
            timeLabel.Text = currentTime.ToString(@"hh\:mm\:ss");
        }
        else
        {
            timer.Stop();
            isRunning = false;
            PlaySoundAlert();
            MessageBox.Show("Time's up!");
        }
    }

    // Play an alert sound when the time is up
    private void PlaySoundAlert()
    {
        try
        {
            // Option 1: Play a simple system beep (uncomment the line below)
            SystemSounds.Beep.Play();

            // Option 2: Use a custom WAV file for the alert (place the path to your WAV file below)
            string soundFilePath = "alert.wav"; // Update this path with the correct path to your WAV file
            if (System.IO.File.Exists(soundFilePath))
            {
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.Play();
            }
            else
            {
                // If custom sound file is not found, play a simple beep
                SystemSounds.Beep.Play();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error playing sound: {ex.Message}");
        }
    }
}
