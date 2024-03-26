using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddin.FamilyLoader.Services;
using System;
using System.Linq;

namespace RevitAddin.FamilyLoader.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;
            Selection selection = uidoc.Selection;

            string fileName = "Family/Family.rfa";

            var familyLoadService = new FamilyLoadService();

            var family = familyLoadService.LoadOrSelectFamily(document, fileName);
            var familySymbols = familyLoadService.GetFamilySymbols(family);

            try
            {
                uidoc.PromptForFamilyInstancePlacement(familySymbols.First());
            }
            catch { }

            return Result.Succeeded;
        }
    }

}
