using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB.Plumbing;

#region User-Defined Namespaces

using Ex_Ti_Missing_Line_Numbers;
using System.Xml.Linq;
using Autodesk.Revit.UI.Selection;
using static Autodesk.Revit.DB.SpecTypeId;
using Autodesk.Revit.DB.Fabrication;

#endregion

#region User-Defined Namespaces

using Ex_Ti_Missing_Line_Number.TransferParameterBetweenConnectors;


#endregion
/// <summary>
/// Filter Pipes
/// Check the Line parameter == Empty, Take Element List
/// Hightlight
/// Notepad Write 
/// Feed
/// </summary>
namespace Ex_Ti_Missing_Line_Number.Command
{
    [Transaction(TransactionMode.Manual)]
    public class Ex_Ti_Missing_Line_NumberCmd : IExternalCommand
    {
#pragma warning disable CS0649 // Field 'Cmd.elementId' is never assigned to, and will always have its default value null
        private ElementId elementId;
        private ElementId elemendtype;
        private ElementId elemstartService;
#pragma warning restore CS0649 // Field 'Cmd.elementId' is never assigned to, and will always have its default value null

        public object instanceEnum { get; private set; }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            const string LineParameter = "ex_T_Line Number";
            Result result = Result.Succeeded;
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            ///Apply Filter (Filter all Pipes)
            List<BuiltInCategory> category = new List<BuiltInCategory>();
            category.Add(BuiltInCategory.OST_PipeCurves);
            category.Add(BuiltInCategory.OST_PipeFitting);
            category.Add(BuiltInCategory.OST_PipeAccessory);
            ElementMulticategoryFilter multiCategory = new ElementMulticategoryFilter(category);
            FilteredElementCollector pipeCollector = new FilteredElementCollector(doc);
            List<Element> pipeElements = pipeCollector.WhereElementIsNotElementType().WherePasses(multiCategory)
                 .ToElements().ToList();
            if (pipeElements.Count > 0)
            {
                ///Check the Pipe has Parameter and Value
                List<Element> pipeWithEmptyValue = new List<Element>();

                foreach (Element pipe in pipeElements)
                {
                    Parameter parameter = pipe.LookupParameter(LineParameter);

                    if (parameter != null)
                    {
                        //Check Parameter has value or not

                        if (string.IsNullOrEmpty(parameter.AsString()))
                        {
                            pipeWithEmptyValue.Add(pipe);

                        }
                    }
                   
                    else
                    {
                        TaskDialog.Show("Error", $"Add the mentioned shared parameter {LineParameter}");
                        return Result.Failed;
                    }
                }

                if (pipeWithEmptyValue.Count > 0)
                {
                    PipesView view = new PipesView(pipeWithEmptyValue, uiDoc);
                    view.Show();
                }
                else
                {
                    TaskDialog.Show("info", $"No Empty Paameters found");
                    return Result.Succeeded;
                }
            }
            else
            {
                TaskDialog.Show("Error", "No Pipes Found!!!");
                return Result.Cancelled;
            }



            return result;
        }

       

        }
    }

 