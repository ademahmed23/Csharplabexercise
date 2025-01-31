using System;
using System.Windows.Forms;

public class EncryptionDecryption
{
    public Form GetForm()
    {
        Form form = new Form
        {
            Text = "ENCRYPTION AND DECRYPTION",
            Width = 600,
            Height = 500
        };

        Label windowTitle = new Label
        {
            Text = "CAESAR CIPHER ENCRYPTION TECHNIQUE",
            Location = new System.Drawing.Point(120, 20),
            AutoSize = true
        };
        form.Controls.Add(windowTitle);

        Label plainText = new Label
        {
            Text = "PLAIN TEXT",
            Location = new System.Drawing.Point(20, 70),
            AutoSize = true
        };
        form.Controls.Add(plainText);

        TextBox plainTextBox = new TextBox
        {
            Location = new System.Drawing.Point(100, 70),
            Width = 200
        };
        form.Controls.Add(plainTextBox);

        Label encryptionKey = new Label
        {
            Text = "ENCRYPTION/DECRYPTION KEY:",
            Location = new System.Drawing.Point(20, 110),
            AutoSize = true
        };
        form.Controls.Add(encryptionKey);

        TextBox encryptionBox = new TextBox
        {
            Location = new System.Drawing.Point(200, 110),
            Width = 50
        };
        form.Controls.Add(encryptionBox);

        Label cipherText = new Label
        {
            Text = "CIPHER TEXT",
            Location = new System.Drawing.Point(20, 150),
            AutoSize = true
        };
        form.Controls.Add(cipherText);

        TextBox cipherBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 150),
            Width = 200
        };
        form.Controls.Add(cipherBox);

        Button ecrypt = new Button
        {
            Text = "ENCRYPT",
            Location = new System.Drawing.Point(320, 70)
        };

        Button decrypt = new Button
        {
            Text = "DECRYPT",
            Location = new System.Drawing.Point(340, 150)
        };

        ecrypt.Click += (sender, e) =>
        {
            try
            {
                string plainText = plainTextBox.Text;
                int key = int.Parse(encryptionBox.Text);
                string encryptedText = Encrypt(plainText, key);
                cipherBox.Text = encryptedText;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input. Please ensure the key is a valid number.");
            }
        };

        decrypt.Click += (sender, e) =>
        {
            try
            {
                string cipherText = cipherBox.Text;
                int key = int.Parse(encryptionBox.Text);
                string decryptedText = Decrypt(cipherText, key);
                plainTextBox.Text = decryptedText;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input. Please ensure the key is a valid number.");
            }
        };

        form.Controls.Add(ecrypt);
        form.Controls.Add(decrypt);

        return form;
    }

    private string Encrypt(string input, int shift)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];
            if (char.IsLetter(letter))
            {
                char offset = char.IsUpper(letter) ? 'A' : 'a';
                letter = (char)((letter + shift - offset) % 26 + offset);
            }
            buffer[i] = letter;
        }
        return new string(buffer);
    }

    private string Decrypt(string input, int shift)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];
            if (char.IsLetter(letter))
            {
                char offset = char.IsUpper(letter) ? 'A' : 'a';
                letter = (char)((letter - shift - offset + 26) % 26 + offset);
            }
            buffer[i] = letter;
        }
        return new string(buffer);
    }
}
