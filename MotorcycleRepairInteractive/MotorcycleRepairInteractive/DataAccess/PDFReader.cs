using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MRI.Core
{
    public static class PDFReader
    {
        public static List<ImagePointDataModel> GetIndexPoints(int startPageNumber)
        {
            List<ImagePointDataModel> IndexPoints = new List<ImagePointDataModel>();

            PdfDocument doc = new PdfDocument();

            //doc.LoadFromFile(string.Format(@"\\SearchablePdfImages\\PartItemsImage0008.pdf"));

            doc.LoadFromFile(@"D:\source\repos\GitHubTeamRepos\MotorcycleManual\MotorcycleRepairInteractive\MotorcycleRepairInteractive\SearchablePdfImages\PartItemsImage8.pdf");

            var otherresult = doc.Pages[0].FindAllText();
            //_ = doc.Pages[0].ExtractImages();
            foreach (var TextFind in doc.Pages[0].FindAllText().Finds)
            {
                ImagePointDataModel _ = new ImagePointDataModel(TextFind.SearchText,
                    TextFind.Position, TextFind.Size.Width, TextFind.Size.Height);

                IndexPoints.Add(new ImagePointDataModel(
                    TextFind.SearchText, TextFind.Position, TextFind.Size.Width, TextFind.Size.Height));

            }
            return IndexPoints;
        }
    }
}
