using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Plumbing;




namespace Ex_Ti_Missing_Line_Number.TransferParameterBetweenConnectors
{
    [Transaction(TransactionMode.Manual)]
    public class TransferParameterBetweenConnectors : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string parameterName = "ex_T_Line Number";

            Document doc = commandData.Application.ActiveUIDocument.Document;

            ElementMulticlassFilter elementMulticlass = new ElementMulticlassFilter(new List<Type> { typeof(Pipe), typeof(FamilyInstance) });
            ElementMulticategoryFilter multicategoryFilter = new ElementMulticategoryFilter(new List<BuiltInCategory> { BuiltInCategory.OST_PipeCurves,
                BuiltInCategory.OST_PipeFitting ,BuiltInCategory.OST_PipeAccessory});

            string nonFilledPipeId = string.Empty;

            ///Filter all Pipes
            List<Element> pipingElements = new FilteredElementCollector(doc)
                .WherePasses(elementMulticlass)
                .WherePasses(multicategoryFilter)
                .ToList();

            List<Element> parameterNotFilledElements = new List<Element>();

            foreach (Element e in pipingElements)
            {
                Parameter param = e.LookupParameter(parameterName);

                if (string.IsNullOrEmpty(param.AsString()))
                {
                    parameterNotFilledElements.Add(e);
                }
            }

            Transaction fillParameter = new Transaction(doc, "Fill Parameter");
            fillParameter.Start();

            foreach (Element pipElement in parameterNotFilledElements)
            {
                ///Get Parameter
                Parameter param = pipElement.LookupParameter(parameterName);

                ConnectorManager connectorManager = null;

                if (pipElement is Pipe)
                {
                    Pipe p = pipElement as Pipe;
                    connectorManager = p.ConnectorManager;

                }
                else if (pipElement is FamilyInstance)
                {
                    FamilyInstance inst = pipElement as FamilyInstance;
                    connectorManager = inst.MEPModel.ConnectorManager;
                }

                ConnectorSet connectorSet = connectorManager.Connectors;

                Connector firstConnector = null;

                foreach (Connector connector in connectorSet)
                {
                    firstConnector = connector;
                    break;
                }

                ConnectorSet connectedConnectors = firstConnector.AllRefs; ///Get only Connected Connector

                foreach (Connector connector in connectedConnectors)
                {
                    Element connectedElement = connector.Owner;

                    if(connectedElement is PipingSystem)
                    {
                        continue;
                    }

                    if (connectedElement.Id.IntegerValue != pipElement.Id.IntegerValue)
                    {
                        string connectedElementParameterValue = connectedElement.LookupParameter(parameterName).AsString();

                        if (!string.IsNullOrEmpty(connectedElementParameterValue))
                        {
                            param.Set(connectedElementParameterValue);
                        }
                        else
                        {
                            nonFilledPipeId += $"{pipElement.Id.IntegerValue} \n";
                        }
                    }
                }
            }

            fillParameter.Commit();

            if (fillParameter.IsValidObject && fillParameter.HasStarted() && fillParameter.HasEnded())
            {
                fillParameter.Dispose();
            }

            if (!string.IsNullOrEmpty(nonFilledPipeId))
            {
                TaskDialog.Show("Information", nonFilledPipeId);
            }

            return Result.Succeeded;
        }
    }
}
