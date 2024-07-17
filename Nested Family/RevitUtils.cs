using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Revit API Namespace Declaration

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

#region User-Defined Namespace Declaration

using Ex_Ti_Nested_Family.Constants;

#endregion


namespace Ex_Ti_Nested_Family.Utility
{
    public class RevitUtils
    {
        /// <summary>
        /// Filter Pipe Category and Pipe Fittings from the Project
        /// </summary>
        /// <param name="doc"> Active Documment </param>
        /// <returns> Pipe Elements </returns>
        public static List<FamilyInstance> GetPipeFittingAndAccessories(Document doc)
        {
            //Internal Variables
            List<FamilyInstance> pipeInstances = new List<FamilyInstance>();

            List<Element> pipeElements = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance))
                .WherePasses(GetPipeCategory())
                .ToElements()
                .ToList();

            if (pipeElements.Count > 0)
            {
                pipeInstances = pipeElements
                    .Select(x => x as FamilyInstance)
                    .Where(x => x.SuperComponent==null)
                    .ToList();
            }

            return pipeInstances;

        }

        public static ElementMulticategoryFilter GetPipeCategory()
        {
            List<BuiltInCategory> categories = new List<BuiltInCategory>()
            {
                BuiltInCategory.OST_PipeAccessory,
                BuiltInCategory.OST_PipeFitting
            };

            return new ElementMulticategoryFilter(categories);
        }

        public static List<Element> GetNestedElement(Document doc, FamilyInstance familyInstance)
        {
            //Internal Variable
            List<Element> subElements = new List<Element>();

            List<ElementId> subElementsId = familyInstance.GetSubComponentIds().ToList();

            if (subElementsId.Count > 0)
            {
                subElements = subElementsId.Select(x => doc.GetElement(x)).ToList();
            }

            return subElements;
        }

        public static bool IsLineParameterExists(Document doc)
        {
            bool result = false;

            DefinitionBindingMapIterator bindingMapIterator = doc.ParameterBindings.ForwardIterator();

            while(bindingMapIterator.MoveNext())
            {
                InstanceBinding instanceBinding = bindingMapIterator.Current as InstanceBinding;

                if (bindingMapIterator.Key.Name.Equals(StringConstants.LineParameter) &&
                    (instanceBinding).Categories.Contains(Category.GetCategory(doc,BuiltInCategory.OST_PipeFitting)) &&
                    (instanceBinding).Categories.Contains(Category.GetCategory(doc,BuiltInCategory.OST_PipeAccessory))) 
                {
                    result= true;
                    break;
                }
            }

            return result;
        }
    }
}
