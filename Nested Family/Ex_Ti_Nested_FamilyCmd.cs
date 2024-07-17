using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region User-Defined Namespaces

using Ex_Ti_Nested_Family.Utility;
using Ex_Ti_Nested_Family.Constants;

#endregion

namespace Ex_Ti_Nested_Family.Command
{
    [Transaction(TransactionMode.Manual)]

    public class Ex_Ti_Nested_FamilyCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            Result result = Result.Succeeded;

            if (RevitUtils.IsLineParameterExists(doc))
            {
                result = NestedFamilyLineParameterFilling(doc);

                if (Enum.Equals(result, Result.Cancelled))
                {
                    message = StringConstants.Cancelled_PipeFittingAndAccessoryNotFound;
                }
                else
                {
                    TaskDialog.Show("Success", StringConstants.Success_Nested_Families_Line_Parameters_Filled_Sucessfully);
                }
            }
            else
            {
                message = StringConstants.Failed_Parameter_Not_Exists;

                result = Result.Failed;
            }

            return result;
        }

        public Result NestedFamilyLineParameterFilling(Document doc)
        {
            Result result = Result.Succeeded;

            List<FamilyInstance> pipeInstances = RevitUtils.GetPipeFittingAndAccessories(doc);

            if (pipeInstances.Count > 0)
            {
                using (Transaction fillParameter = new Transaction(doc, "Nested Family Family Filling"))
                {
                    fillParameter.Start();

                    List<FamilyInstance>.Enumerator instanceEnum = pipeInstances.GetEnumerator();

                    while (instanceEnum.MoveNext())
                    {
                        //Get Line Number
                        Parameter parentParam = instanceEnum.Current.LookupParameter(StringConstants.LineParameter);

                        string lineNumber = string.Empty;

                        if(parentParam!=null)
                        {
                            lineNumber = parentParam.AsString();
                        }

                        if(!string.IsNullOrEmpty(lineNumber))
                        {
                            //Get Nested Elements
                            List<Element> subElements = RevitUtils.GetNestedElement(doc, instanceEnum.Current);

                            if (subElements.Count > 0)
                            {
                                List<Element>.Enumerator nestedElementEnum = subElements.GetEnumerator();

                                while (nestedElementEnum.MoveNext())
                                {
                                    Parameter param = nestedElementEnum.Current.LookupParameter(StringConstants.LineParameter);

                                    if (param != null && !param.IsReadOnly)
                                    {
                                        param.Set(lineNumber);
                                    }
                                }
                            }
                        }
                    }

                    fillParameter.Commit();
                }

            }
            else
            {
                result = Result.Cancelled;
            }


            return result;

        }
    }
}
