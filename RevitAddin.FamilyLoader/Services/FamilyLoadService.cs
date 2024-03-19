using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RevitAddin.FamilyLoader.Services
{
    public class FamilyLoadService
    {
        private string GetDirectory()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public Family SelectFamilyByName(Document document, string name)
        {
            return new FilteredElementCollector(document)
                .OfClass(typeof(Family))
                .OfType<Family>()
                .FirstOrDefault(e => e.Name == name);
        }

        public Family LoadOrSelectFamily(Document document, string fileName)
        {
            var filePath = fileName;

            if (!File.Exists(filePath))
            {
                var directory = GetDirectory();
                filePath = Path.Combine(directory, fileName);
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            var name = Path.GetFileNameWithoutExtension(filePath);
            var family = SelectFamilyByName(document, name);

            if (family is null)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Load Family");
                    if (document.LoadFamily(filePath, out family))
                    {

                    }
                    transaction.Commit();
                }
            }

            return family;
        }

        public IEnumerable<FamilySymbol> GetFamilySymbols(Family family)
        {
            var document = family.Document;
            var familySymbols = family.GetFamilySymbolIds()
                .Select(document.GetElement)
                .OfType<FamilySymbol>()
                .ToList();

            return familySymbols;
        }
    }
}
