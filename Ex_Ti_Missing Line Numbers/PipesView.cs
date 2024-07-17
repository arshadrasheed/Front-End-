using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex_Ti_Missing_Line_Numbers
{
    public partial class PipesView : Form
    {
        List<Autodesk.Revit.DB.Element> _pipeIds = new List<Autodesk.Revit.DB.Element>();
        UIDocument _uiDoc = null;

        public PipesView(List<Autodesk.Revit.DB.Element> ids, UIDocument uiDoc)
        {
            _pipeIds = ids;
            _uiDoc = uiDoc;
            InitializeComponent();

            lst_ListView.Columns.Add("Element Id");
            lst_ListView.Columns.Add("Element Type");
            lst_ListView.Columns.Add("Element Service");
             lst_ListView.GridLines = true;
            foreach (Autodesk.Revit.DB.Element element in _pipeIds)
            {
                lst_ListView.Items.Add(element.Id.IntegerValue.ToString());
            }
        }

        private void lst_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lst_ListView.FocusedItem;

            _uiDoc.Selection.SetElementIds(new List<Autodesk.Revit.DB.ElementId> { new Autodesk.Revit.DB.ElementId(Convert.ToInt32(item.Text))});
            _uiDoc.ShowElements(_uiDoc.Document.GetElement(new Autodesk.Revit.DB.ElementId(Convert.ToInt32(item.Text))));
        }
    }
}
