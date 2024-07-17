using Revit =Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PipeSorting
{
    public partial class PipeSortingView : Form
    {
        private UIDocument _uiDoc = null;

        public PipeSortingView(ExternalCommandData commandData)
        {
            InitializeComponent();
            _uiDoc = commandData.Application.ActiveUIDocument;
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            ///Need to Update List
            string lineNumber = tb_LineNumberText.Text;

            if (!string.IsNullOrEmpty(lineNumber))
            {
                Dictionary<Revit.ElementId, string> fittingIds = new Revit.FilteredElementCollector(_uiDoc.Document)
                  .OfClass(typeof(Revit.FamilyInstance))
                  .OfCategory(Revit.BuiltInCategory.OST_PipeFitting)
                  .Where(X => X.LookupParameter("Angle") != null)
                  .Where(x => x.LookupParameter("ex_T_Line Number").AsString() == lineNumber)
                  .ToDictionary(x => x.Id, X => X.LookupParameter("Angle").AsValueString());


                lst_ElementIds.Clear();

                foreach (KeyValuePair<Revit.ElementId, string> id in fittingIds)
                {
                    lst_ElementIds.Items.Add($"{id.Key.IntegerValue}-{id.Value}");
                }
            }
            else
            {
                MessageBox.Show(
                    "Fill Line Number and Sort",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }

        }

        private void btn_findAll_Click(object sender, EventArgs e)
        {
            Dictionary<Revit.ElementId, string> fittingIds = new Revit.FilteredElementCollector(_uiDoc.Document)
                      .OfClass(typeof(Revit.FamilyInstance))
                      .OfCategory(Revit.BuiltInCategory.OST_PipeFitting)
                      .Where(X => X.LookupParameter("Angle") != null)
                      .ToDictionary(x => x.Id, X => X.LookupParameter("Angle").AsValueString());

            lst_ElementIds.Clear();

            foreach (KeyValuePair<Revit.ElementId, string> id in fittingIds)
            {
                lst_ElementIds.Items.Add($"{id.Key.IntegerValue}-{id.Value}");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                btn_findAll.Enabled = true;
                btn_Sort.Enabled = false;
                tb_LineNumberText.Enabled = false;
            }
            else
            {
                btn_findAll.Enabled = false;
                btn_Sort.Enabled = true;
                tb_LineNumberText.Enabled = true;
            }

        }

        private void lst_ElementIds_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lst_ElementIds.SelectedItems)
            {
                string id = item.Text.Split('-')[0];
                Revit.ElementId eId = new Revit.ElementId(Convert.ToInt32(id));
                _uiDoc.ShowElements(new List<Revit.ElementId> { eId });
                _uiDoc.Selection.SetElementIds(new List<Revit.ElementId> { eId });
            }
            
        }

        private void tb_LineNumberText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
