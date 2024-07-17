using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Windows.Media.Imaging;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using System.Diagnostics;



namespace RevitFamilyFileProcessing
{
    [Transaction(TransactionMode.Manual)]
    public class Apps : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            TaskDialog.Show("Information", "Revit is closing");
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string TabName = "Ex_DTI";
            string PanelName = "Digital Tool Installation";
            string addinpath = Assembly.GetExecutingAssembly().Location;
            string addinDirPath = Path.GetDirectoryName(addinpath);

            ///Dll Location
            string missingLineNumber = typeof(Ex_Ti_Missing_Line_Number.Command.Ex_Ti_Missing_Line_NumberCmd).Assembly.Location;
            string nestedFamily = typeof(Ex_Ti_Nested_Family.Command.Ex_Ti_Nested_FamilyCmd).Assembly.Location;
            string pipeSorting = typeof(PipeSorting.RevitMain).Assembly.Location;

            string iconpath = @"Resources\Importcsv.ico";
            string iconcompletepath = Path.Combine(addinDirPath, iconpath);
            application.CreateRibbonTab(TabName);
            RibbonPanel panel = application.CreateRibbonPanel(TabName, PanelName);

            PushButtonData PushButtonData = new PushButtonData("btn_01", "Import Metadata", addinpath, typeof(FamilyFileProcessor).FullName);
            PushButton pushbutton = panel.AddItem(PushButtonData) as PushButton;
            pushbutton.LargeImage = new BitmapImage(new Uri(iconcompletepath));
            pushbutton.ToolTip = "Import Metadata to Folder of Familes";

            string iconpath1 = @"Resources\NestedFamily.ico";
            string iconcompletepath1 = Path.Combine(addinDirPath, iconpath1);
            PushButtonData PushButtonData1 = new PushButtonData("btn_02", "Nested Family", nestedFamily, typeof(Ex_Ti_Nested_Family.Command.Ex_Ti_Nested_FamilyCmd).FullName);
            PushButton pushbutton1 = panel.AddItem(PushButtonData1) as PushButton;
            pushbutton1.LargeImage = new BitmapImage(new Uri(iconcompletepath1));
            pushbutton1.ToolTip = "Import Line Numbers to Nested Family";

            string iconpath2 = @"Resources\MissingLineNumber.ico";
            string iconcompletepath2 = Path.Combine(addinDirPath, iconpath2);
            PushButtonData PushButtonData2 = new PushButtonData("btn_03", "Missing Line Number", missingLineNumber, typeof(Ex_Ti_Missing_Line_Number.Command.Ex_Ti_Missing_Line_NumberCmd).FullName);
            PushButton pushbutton2 = panel.AddItem(PushButtonData2) as PushButton;
            pushbutton2.LargeImage = new BitmapImage(new Uri(iconcompletepath2));
            pushbutton2.ToolTip = "Find Missing Line Number";

            string iconpath3 = @"Resources\Elbow.ico";
            string iconcompletepath3 = Path.Combine(addinDirPath, iconpath3);
            PushButtonData PushButtonData3 = new PushButtonData("btn_04", "QA-QC Angle Check", pipeSorting, typeof(PipeSorting.RevitMain).FullName);
            PushButton pushbutton3 = panel.AddItem(PushButtonData3) as PushButton;
            pushbutton3.LargeImage = new BitmapImage(new Uri(iconcompletepath3));
            pushbutton3.ToolTip = "Find Elbow Angle";

            return Result.Succeeded;
        }
    }
}

