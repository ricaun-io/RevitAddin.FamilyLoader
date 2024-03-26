using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Tasks;
using System;

namespace RevitAddin.FamilyLoader.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private RibbonPanel ribbonPanel;
        private static RevitTaskService RevitTaskService;
        public static IRevitTask RevitTask => RevitTaskService;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("FamilyLoader");
            ribbonPanel.CreatePushButton<Commands.CommandView>("Load\rFamily")
                .SetLargeImage("Resources/Revit.ico");

            RevitTaskService = new RevitTaskService(application);
            RevitTaskService.Initialize();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();

            RevitTaskService.Dispose();

            return Result.Succeeded;
        }
    }

}