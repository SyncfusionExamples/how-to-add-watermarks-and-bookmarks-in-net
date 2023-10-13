using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using Syncfusion.Drawing;

namespace Watermark_Bookmarks {
    internal class Program {
        static void Main(string[] args) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
            //Text_Watermark_PDF();
            //Image_Watermark_PDF();
            //Remove_Watermark_PDF();
            //Add_Bookmarks_PDF();
            //Insert_Bookmarks();
            //Modify_Bookmarks();
            Remove_Bookmarks();
        }
        /// <summary>
        /// Add a text watermark to a PDF document
        /// </summary>
        static void Text_Watermark_PDF() {
            //Get stream from an existing PDF document. 
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Input.pdf"), FileMode.Open, FileAccess.Read);
            //Load the PDF document. 
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Load the page. 
            PdfPageBase loadedPage = loadedDocument.Pages[0];
            //Create graphics for PDF page. 
            PdfGraphics graphics = loadedPage.Graphics;
            //Set the font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            //Watermark text.
            PdfGraphicsState state = graphics.Save();
            //Set transparency and rotate transform to page graphics. 
            graphics.SetTransparency(0.25f);
            graphics.RotateTransform(-40);
            //Draw watermark text in the PDF document. 
            graphics.DrawString("Imported using Essential PDF", font, PdfPens.Red, PdfBrushes.Red, new PointF(-150, 450));

            //Create file stream.
            using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                //Save the PDF document to file stream.
                loadedDocument.Save(outputFileStream);
            }
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Add an image watermark
        /// </summary>
        static void Image_Watermark_PDF() {
            //Get stream from an existing PDF document. 
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Input.pdf"), FileMode.Open, FileAccess.Read);
            //Load the PDF document. 
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Load the page. 
            PdfPageBase loadedPage = loadedDocument.Pages[0];
            //Create graphics for PDF page. 
            PdfGraphics graphics = loadedPage.Graphics;
            //Load the image file as stream.
            FileStream imageStream = new FileStream(Path.GetFullPath("../../../Image.png"), FileMode.Open, FileAccess.Read);
            //Load the image file.
            PdfImage image = new PdfBitmap(imageStream);
            //Create state for page graphics. 
            PdfGraphicsState state = graphics.Save();
            //Set transparency for page graphics. 
            graphics.SetTransparency(0.25f);
            //Draw image watermark in PDF page. 
            graphics.DrawImage(image, new PointF(0, 0), loadedPage.Graphics.ClientSize);

            //Create file stream.
            using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                //Save the PDF document to file stream.
                loadedDocument.Save(outputFileStream);
            }
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Remove a watermark
        /// </summary>
        static void Remove_Watermark_PDF() {
            //Get stream from an existing PDF document. 
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Data.pdf"), FileMode.Open, FileAccess.Read);
            //Load the PDF document. 
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            for (int i = 0; i < loadedDocument.Pages.Count; i++) {
                //Get the page.  
                PdfLoadedPage lpage = loadedDocument.Pages[i] as PdfLoadedPage;
                //Gets the annotation collection. 
                PdfLoadedAnnotationCollection loadedAnnotationCollection = lpage.Annotations;
                for (int j = 0; j < loadedAnnotationCollection.Count; j++) {
                    //Gets the annotation. 
                    PdfLoadedAnnotation annotation = lpage.Annotations[j] as PdfLoadedAnnotation;
                    if (annotation != null && annotation is PdfLoadedWatermarkAnnotation) {
                        //Removes the first annotation. 
                        loadedAnnotationCollection.RemoveAt(j);
                    }
                }
            }

            //Create file stream.
            using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                //Save the PDF document to file stream.
                loadedDocument.Save(outputFileStream);
            }
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Add bookmarks to a PDF document
        /// </summary>
        static void Add_Bookmarks_PDF() {
            //Load an existing PDF document.
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Input1.pdf"), FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);

            //Creates parent bookmark.
            PdfBookmark bookmark = loadedDocument.Bookmarks.Add("Chapter 1");
            //Sets the destination page.
            bookmark.Destination = new PdfDestination(loadedDocument.Pages[1]);
            //Sets the text style and color for parent bookmark.
            bookmark.TextStyle = PdfTextStyle.Bold;
            bookmark.Color = Color.Red;
            //Sets the destination location for parent bookmark. 
            bookmark.Destination.Location = new PointF(20, 20);

            //Adds the child bookmark.
            PdfBookmark childBookmark = bookmark.Insert(0, "Section 1");
            //Sets the destination location for child bookmark. 
            childBookmark.Destination = new PdfDestination(loadedDocument.Pages[1]);
            childBookmark.Destination.Location = new PointF(0, 200);

            //Create file stream.
            using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                //Save the PDF document to file stream.
                loadedDocument.Save(outputFileStream);
            }
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Insert bookmarks in an existing PDF document
        /// </summary>
        static void Insert_Bookmarks() {
            //Load the PDF document.
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Data1.pdf"), FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);

            //Inserts a new bookmark in the existing bookmark collection.
            PdfBookmark bookmark = loadedDocument.Bookmarks.Insert(0, "Title Page");
            //Sets the destination page and location.
            bookmark.Destination = new PdfDestination(loadedDocument.Pages[0]);
            bookmark.Destination.Location = new PointF(0, 0);
            //Sets the text style and color.
            bookmark.TextStyle = PdfTextStyle.Bold;
            bookmark.Color = Color.Green;

            //Create file stream.
            using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                //Save the PDF document to file stream.
                loadedDocument.Save(outputFileStream);
            }
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Modify bookmarks
        /// </summary>
        static void Modify_Bookmarks() {
            //Load the PDF document.
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Data1.pdf"), FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);

            //Gets all the bookmarks.
            PdfBookmarkBase bookmarks = loadedDocument.Bookmarks;
            //Gets the first bookmark and changes the properties of the bookmark.
            PdfLoadedBookmark bookmark = bookmarks[0] as PdfLoadedBookmark;
            bookmark.Destination = new PdfDestination(loadedDocument.Pages[2]);
            bookmark.Color = Color.Green;
            bookmark.TextStyle = PdfTextStyle.Bold;
            bookmark.Title = "Chapter2";

            //Create file stream.
            using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                //Save the PDF document to file stream.
                loadedDocument.Save(outputFileStream);
            }
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Remove bookmarks
        /// </summary>
        static void Remove_Bookmarks() {
            //Load the PDF document.
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Input2.pdf"), FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);

            //Gets all the bookmarks.
            PdfBookmarkBase bookmarks = loadedDocument.Bookmarks;
            //Remove parent bookmark by index.
            bookmarks.RemoveAt(1);
            //Remove child bookmark by bookmark name. 
            PdfLoadedBookmark parentBookmark = bookmarks[0] as PdfLoadedBookmark;
            parentBookmark.Remove("Section 4");

            //Create file stream.
            using (FileStream outputFileStream = new FileStream("Output.pdf", FileMode.Create, FileAccess.ReadWrite)) {
                //Save the PDF document to file stream.
                loadedDocument.Save(outputFileStream);
            }
            //Close the document.
            loadedDocument.Close(true);
        }
    }
}