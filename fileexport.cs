using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using System.Windows.Forms;

public class PDFExporter
{
    public void ExportToPDF(string title, string content)
    {
        try
        {
            // Set the file path on the desktop
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{title}.pdf");

            // Create a PdfWriter and PdfDocument
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    // Use Helvetica Bold font for title
                    PdfFont boldFont = PdfFontFactory.CreateFont("Helvetica-Bold");

                    // Add Title with Bold Font
                    document.Add(new Paragraph(title)
                        .SetFont(boldFont) // Apply bold font
                        .SetFontSize(24)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                    // Add Content with normal font
                    document.Add(new Paragraph(content)
                        .SetFontSize(12)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT));

                    document.Close();
                }
            }

            // Show success message
            MessageBox.Show($"PDF exported successfully to: {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            // Show error message
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
