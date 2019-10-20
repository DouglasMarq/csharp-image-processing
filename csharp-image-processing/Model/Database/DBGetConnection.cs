using SQLite;
using System.IO;

namespace csharp_image_processing.Model.Database
{
    public class DBGetConnection
    {
        public static SQLiteConnection GetConnection()
        {
            //folder do banco
            return new SQLiteConnection(Path.Combine(Directory.CreateDirectory(Path.Combine(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ImageProcessing"), "Database")).FullName, "sqlite.db3"));
        }
    }
}
