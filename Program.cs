
using System;
using System.Windows.Forms;

public class Program
{
    public static void Main(string[] args)
    {
        ApplicationsEntry applicationsEntry = new();

        // LoginWindow loginWindow = new();
        // Application.Run(loginWindow);

        applicationsEntry.Run();
    }
}
