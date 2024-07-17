using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;


namespace PipeSorting
{
    [Transaction(TransactionMode.Manual)]
    public class RevitMain : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ///Invoke View
            PipeSortingView view = new PipeSortingView(commandData);
            view.ShowDialog();

            return Result.Succeeded;
        }
    }
}
