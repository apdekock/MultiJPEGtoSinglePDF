using System;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace combineJPEGToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //Create a new document
                Document Doc = new Document(PageSize.A4, 0, 0, 0, 0);
                //Store the document on the desktop
                string PDFOutput = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Output" + DateTime.Now.Millisecond + ".pdf");
                PdfWriter writer = PdfWriter.GetInstance(Doc, new FileStream(PDFOutput, FileMode.Create, FileAccess.Write, FileShare.Read));

                //Open the PDF for writing
                Doc.Open();

                string Folder = args[0];
                Console.WriteLine(Folder);
                foreach (string F in Directory.GetFiles(Folder, "*.jpg").OrderBy(y => y))
                {
                    Console.WriteLine(F);
                    //Insert a page
                    Doc.NewPage();
                    //Add image
                    var element = new Jpeg(new Uri(new FileInfo(F).FullName));
                    element.ScaleToFit(600, 1200);
                    Doc.Add(element);
                }

                //Close the PDF
                Doc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
