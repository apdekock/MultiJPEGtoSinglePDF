using System;
using System.IO;
using System.Linq;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

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
                var orderedFiles = new SortedDictionary<int, string>();
                foreach (var item in Directory.GetFiles(Folder, "*.jpg"))
                {
                    orderedFiles.Add(Convert.ToInt32(string.Join("", item.ToCharArray().Where(Char.IsDigit))), item);
                }
                foreach (string fileName in orderedFiles.Values)
                {
                    Console.WriteLine(fileName);
                    //Insert a page
                    Doc.NewPage();
                    //Add image
                    var element = new Jpeg(new Uri(new FileInfo(fileName).FullName));
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
