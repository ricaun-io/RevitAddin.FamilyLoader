using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.FamilyLoader.Models;
using RevitAddin.FamilyLoader.Services;
using RevitAddin.FamilyLoader.Views;
using ricaun.Revit.Mvvm;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddin.FamilyLoader.ViewModels
{
    public class FamilyLoaderViewModel : ObservableObject
    {
        private readonly IRevitTask revitTask;
        #region Public Properties
        public ObservableCollection<FamilyLoaderModel> Models { get; } = new();
        public FamilyLoaderModel SelectedModel { get; set; }
        public IAsyncRelayCommand Command => new AsyncRelayCommand(CommandLoadAndPlace, CanLoadAndPlace);

        #endregion

        #region Constructor
        public FamilyLoaderViewModel(IRevitTask revitTask)
        {
            this.revitTask = revitTask;
        }
        #endregion

        #region View / Window
        public string Title { get; set; } = "FamilyLoader";
        public object Icon { get; set; } = "Resources/Revit.ico".GetBitmapSource();
        public FamilyLoaderView Window { get; private set; }
        public void Show()
        {
            if (Window is null)
            {
                Window = new FamilyLoaderView();
                Window.DataContext = this;
                Window.SetAutodeskOwner();
                Window.Closed += (s, e) => { Window = null; };
                Window.Loaded += Window_Loaded;
            }
            Window?.Show();
            Window?.Activate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Models.Add(new FamilyLoaderModel() { Name = "Family", Symbol = "0610 x 0160mm", Path = "Family/Family.rfa", Image = "Family/Family-0.png" });
            Models.Add(new FamilyLoaderModel() { Name = "Family", Symbol = "0610 x 0915mm", Path = "Family/Family.rfa", Image = "Family/Family-1.png" });
            Models.Add(new FamilyLoaderModel() { Name = "Family", Symbol = "0762 x 0762mm", Path = "Family/Family.rfa", Image = "Family/Family-2.png" });
            //Models.Add(new FamilyLoaderModel() { Name = "Family", Symbol = "Error", Path = "Family/Family2.rfa", Image = "Resources/Revit.ico" });

            SelectedModel = Models.FirstOrDefault();
        }
        #endregion

        #region Private Methods
        private async Task CommandLoadAndPlace()
        {
            try
            {
                Window.Hide();
                await revitTask.Run((uiapp) =>
                {

                    UIDocument uidoc = uiapp.ActiveUIDocument;
                    Document document = uidoc.Document;
                    View view = uidoc.ActiveView;

                    string fileName = SelectedModel.Path;

                    var familyLoadService = new FamilyLoadService();

                    var family = familyLoadService.LoadOrSelectFamily(document, fileName);
                    var familySymbols = familyLoadService.GetFamilySymbols(family);

                    try
                    {
                        var familySymbol = familySymbols.FirstOrDefault(e => e.Name == SelectedModel.Symbol);
                        uidoc.PromptForFamilyInstancePlacement(familySymbol);
                    }
                    catch { }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Window.Show();
            }
        }
        private bool CanLoadAndPlace()
        {
            return SelectedModel != null;
        }
        #endregion
    }
}