using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CatMania.Infrastructure
{
    public class Storage
    {
        public static IsolatedStorageFileStream StoreFile(Canvas canvas, string fileName)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists(fileName))
                {
                    store.DeleteFile(fileName);
                }

                var storeFile = store.CreateFile(fileName);
                var uri = new Uri(fileName, UriKind.Relative);
                var wb = new WriteableBitmap(canvas, new TranslateTransform());
                wb.SaveJpeg(storeFile, wb.PixelWidth, wb.PixelHeight, 0, 100);
                storeFile.Close();

                storeFile = store.OpenFile(fileName, FileMode.Open, FileAccess.Read);
                return storeFile;
            }
        } 
    }
}