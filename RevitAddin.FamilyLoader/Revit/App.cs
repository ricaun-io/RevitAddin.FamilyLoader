using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.FamilyLoader.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("FamilyLoader");
            ribbonPanel.CreatePushButton<Commands.Command>("Load\rFamily")
                .SetLargeImage("Resources/Revit.ico");
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

}