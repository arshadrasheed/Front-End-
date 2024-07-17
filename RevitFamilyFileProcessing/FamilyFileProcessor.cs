using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using System.IO;

namespace RevitFamilyFileProcessing
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class FamilyFileProcessor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;

            // Folder path containing .rfa files
            string folderPath = "\\global.alltfs.com\\Projects\\MWGRP-APAC\\SGP\\1106123_5D_TI\\S13_BIM-VDC\\17-Hook-Up\\176-CAD\\Working Folders\\Handover Documents Vignesh\\Vignesh Hanover Documents\\2.Revit\\3)Revit Families\\Metric\\Pipe Fittings\\Gas\\ST05";

            // Get all .rfa files from the folder
            string[] rfaFiles = Directory.GetFiles(folderPath, "*.rfa");

            // Iterate through each .rfa file
            foreach (string rfaFilePath in rfaFiles)
            {
                // Open the .rfa file
                Document doc = app.OpenDocumentFile(rfaFilePath);
                // Start a new transaction for each family file
                //using (Transaction transaction = new Transaction(doc, "Import Lookup Table"))
                //{
                //    transaction.Start();

                // Get the name of the .rfa file
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(rfaFilePath);

                // Find the corresponding lookup table file in the folder
                string lookupTableFilePath = Path.Combine(folderPath, $"{fileNameWithoutExtension}_item code.csv");

                // Check if the lookup table file exists
                if (!File.Exists(lookupTableFilePath))
                {
                    TaskDialog.Show("Error", $"Lookup table file '{lookupTableFilePath}' not found for {Path.GetFileName(rfaFilePath)}");
                    doc.Close(false);
                    continue;
                }

                // Import the lookup table


                try
                {
                    using (Transaction trans = new Transaction(doc, "Import Lookup Table"))
                    {
                        trans.Start();


                        //transaction.Start();

                        FamilySizeTableManager familySizeTable = FamilySizeTableManager.GetFamilySizeTableManager(doc, doc.OwnerFamily.Id);
                        familySizeTable.ImportSizeTable(doc, lookupTableFilePath, new FamilySizeTableErrorInfo());
                        trans.Commit();
                    }

                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", $"Failed to import lookup table into {Path.GetFileName(rfaFilePath)}: {ex.Message}");
                }

                // Save and close the .rfa file
                doc.Save();
                //doc.Close(false);
                //}
                //SaveAsOptions saveAsOptions = new SaveAsOptions();
                //saveAsOptions.OverwriteExistingFile = true;
                //doc.SaveAs(rfaFilePath, saveAsOptions);

                // Close the family file
                //doc.Close(false);

            }

            //}
            return Result.Succeeded;

        }
    }
}


