using MRI.Core;
using Spire.Pdf;
using Spire.Pdf.Exporting;
using Spire.Pdf.General.Find;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MRI.AssignCoord
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string Text { get; private set; } = "this is set";

        public DataSet BookTable { get; private set; } = new DataSet();

        public DataSet PageTable { get; private set; } = new DataSet();

        private DataSet pageItemsDS = new DataSet();
        public DataSet PageItemsDS
        {
            get
            {
                return pageItemsDS;
            }

            set
            {
                pageItemsDS = value;
                PageItemsDV = pageItemsDS.Tables["PageItems"].AsDataView();
            }
        }      

        public DataTable DataTableDT { get; private set; } = new DataTable();

        public DataView PageItemsDV { get; set; } = new DataView();

        //public DataView PageItemsDV { get; set; }

        public ObservableCollection<PageItemDataModel> PageItems { get; private set; }

        public List<ImagePointDataModel> IndexPoints { get; private set; }

        private DataRow selectedItem;
        public DataRow SelectedItem
        {
            get
            {
                return selectedItem;

            }
            set
            {
                selectedItem = value;
            }
        }

        private DataRow selectedIndex;
        public DataRow SelectedIndex
        {
            get
            {
                return selectedIndex;

            }
            set
            {
                selectedIndex = value;
            }
        }

        public MainWindowViewModel()
        {
            LoadTablesFromSql();

            IndexPoints = PDFReader.GetIndexPoints(8);

            
        }

        private void LoadTablesFromSql()
        {
            //BookTable = SqlStaticHelpers.ReadTableData("Books");
            //PageTable = SqlStaticHelpers.ReadTableData("Pages");
            DataTableDT = SqlStaticHelpers.ReadTableData("Pages");
            //PartTable = SqlStaticHelpers.ReadTableData("Parts");

            //PageItemsDS = PageItemTable.AsDataView();

            IList<string> PageLines = 
                DataTableDT.AsEnumerable().Select(item =>
                string.Format("{0}, {1}", item["PageHeader"], item["FigureDescription"])).ToList();

            foreach (var p in DataTableDT.AsEnumerable())
            {
                //PageDataModel page = new PageDataModel(
                //    )
                var q = p.ItemArray;
            }
        }


    }
}


