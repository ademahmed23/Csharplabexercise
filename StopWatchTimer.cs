using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

public class StopWatchTimer
{
    private Timer timer;
    private Label timeLabel;
    private bool isRunning = false;
    private bool isPaused = false; 
    private TimeSpan currentTime;
    private TimeSpan initialTime; 

    private Button resumeButton;  

    public Form GetForm()
    {
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
            Font = new Font("Arial", 20, FontStyle.Bold)
        };
        form.Controls.Add(windowTitle);

        // Time Label
        timeLabel = new Label
        {
            Text = "00:00:00",
            Location = new Point(120, 100),
            AutoSize = true,
            Font = new Font("Arial", 86, FontStyle.Bold),
            BorderStyle = BorderStyle.FixedSingle
        };
        form.Controls.Add(timeLabel);

        // Buttons
        Button resetButton = new Button { Text = "RESET", Location = new Point(150, 300), Width = 100 };
        Button setButton = new Button { Text = "SET-TIMER", Location = new Point(270, 300), Width = 100 };
        Button startButton = new Button { Text = "START", Location = new Point(390, 300), Width = 100 };
        Button pauseButton = new Button { Text = "PAUSE", Location = new Point(510, 300), Width = 100 };
        resumeButton = new Button { Text = "RESUME", Location = new Point(630, 300), Width = 100, Enabled = false };

        // Add buttons to the form
        form.Controls.Add(resetButton);
        form.Controls.Add(setButton);
        form.Controls.Add(startButton);
        form.Controls.Add(pauseButton);
        form.Controls.Add(resumeButton);

        // Timer initialization
        timer = new Timer { Interval = 1000 };
        timer.Tick += Timer_Tick;

        // Button Event Handlers
        setButton.Click += (sender, e) => { MakeTimeEditable(); };
        startButton.Click += (sender, e) => { StartTimer(); };
        pauseButton.Click += (sender, e) => { PauseTimer(); };
        resumeButton.Click += (sender, e) => { ResumeTimer(); };
        resetButton.Click += (sender, e) => { ResetTimer(); };

        return form;
    }

    private void MakeTimeEditable()
    {
        TextBox textBox = new TextBox
        {
            Text = timeLabel.Text,
            Location = timeLabel.Location,
            Size = timeLabel.Size,
            Font = timeLabel.Font
        };

        timeLabel.Visible = false;
        Form parentForm = timeLabel.Parent as Form;
        parentForm.Controls.Add(textBox);
        textBox.SelectAll();
        textBox.Focus();

        textBox.KeyDown += (s, args) =>
        {
            if (args.KeyCode == Keys.Enter)
            {
                if (TimeSpan.TryParse(textBox.Text, out TimeSpan parsedTime))
                {
                    currentTime = parsedTime;
                    initialTime = parsedTime;
                    timeLabel.Text = parsedTime.ToString(@"hh\:mm\:ss");
                    timer.Stop(); // Stop the timer after setting time
                    isRunning = false;
                    isPaused = false;
                }
                else
                {
                    MessageBox.Show("Invalid Time Format. Use HH:MM:SS.");
                }
                textBox.Dispose();
                timeLabel.Visible = true;
            }
        };

        textBox.Leave += (s, args) =>
        {
            textBox.Dispose();
            timeLabel.Visible = true;
        };
    }

    private void StartTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            isPaused = false;
            resumeButton.Enabled = false;
            timer.Start();
        }
    }

    private void PauseTimer()
    {
        if (isRunning)
        {
            timer.Stop();
            isRunning = false;
            isPaused = true;
            resumeButton.Enabled = true;
        }
    }

    private void ResumeTimer()
    {
        if (isPaused)
        {
            timer.Start();
            isRunning = true;
            isPaused = false;
            resumeButton.Enabled = false;
        }
    }

    private void ResetTimer()
    {
        timer.Stop();
        isRunning = false;
        isPaused = false;
        currentTime = TimeSpan.Zero;
        timeLabel.Text = initialTime == TimeSpan.Zero ? "00:00:00" : initialTime.ToString(@"hh\:mm\:ss");
        resumeButton.Enabled = false;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (initialTime != TimeSpan.Zero)
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
        else
        {
            currentTime = currentTime.Add(TimeSpan.FromSeconds(1));
            timeLabel.Text = currentTime.ToString(@"hh\:mm\:ss");
        }
    }

    private void PlaySoundAlert()
    {
        try
        {
            SystemSounds.Beep.Play();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error playing sound: {ex.Message}");
        }
    }
}
