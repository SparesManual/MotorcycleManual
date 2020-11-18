using MRI.Core;
using Spire.Pdf;
using Spire.Pdf.Exporting;
using Spire.Pdf.General.Find;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MRI.AssignCoord
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Text { get; private set; } = "this is set";
        public ObservableCollection<BookDataModel> Books { get; private set; } =
            new ObservableCollection<BookDataModel>();

        public ObservableCollection<PageDataModel> Pages { get; private set; } =
            new ObservableCollection<PageDataModel>();

        public ObservableCollection<PageItemDataModel> PageItems { get; private set; } =
            new ObservableCollection<PageItemDataModel>();

        public ObservableCollection<PartDataModel> Parts { get; private set; } =
            new ObservableCollection<PartDataModel>();

        public List<ImagePointDataModel> IndexPoints { get; private set; } =
            new List<ImagePointDataModel>();


        public Image Image { get; private set; }

        public BitmapImage BitmapImage { get; set; }

        public ICommand Button1Command { get; set; }

        public MainWindowViewModel()
        {
            CreateObservableCollectionsFromSql();

            var bitmap = new BaseImage("").BitmapImage;

            Image = new BaseImage("").Image;

            IndexPoints = PDFReader.GetIndexPoints(8);

            Button1Command = new RelayCommand(Button1Method);
        }

        public void Button1Method()
        {
            //var t = "";
        }

        private void CreateObservableCollectionsFromSql()
        {
            var BooksTable = SqlStaticHelpers.ReadTableData("Books");
            var PagesTable = SqlStaticHelpers.ReadTableData("Pages");
            var PageItemsTable = SqlStaticHelpers.ReadTableData("PageItems");
            var PartsTable = SqlStaticHelpers.ReadTableData("Parts");

            Books = new ObservableCollection<BookDataModel>(
                new BookListDataModel(BooksTable).Items);

            Pages = new ObservableCollection<PageDataModel>(
                new PageListDataModel(PagesTable).Items);


            PageItems = new ObservableCollection<PageItemDataModel>(
                new PageItemListDataModel(PageItemsTable).Items);

            Parts = new ObservableCollection<PartDataModel>(
                new PartListDataModel(PartsTable).Items);



        }

    }
}


