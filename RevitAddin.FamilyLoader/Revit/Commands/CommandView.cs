using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.FamilyLoader.ViewModels;

namespace RevitAddin.FamilyLoader.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandView : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            new FamilyLoaderViewModel(App.RevitTask).Show();

            return Result.Succeeded;
        }
    }

}
