namespace CenECommerce.Classes
{
    using System.IO;
    using System.Web;

    public class FilesHelpers
    {
        public static bool UploadPhoto(HttpPostedFileBase file, string folder, string nameImage)
        {
            if (file == null || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(nameImage))
            {
                return false;
            }

            try
            {
                string path = string.Empty;

                string pic = string.Empty;

                if (file != null)
                {

                    path =
                        Path.Combine(
                            HttpContext.
                            Current.
                            Server.
                            MapPath(
                                folder), nameImage);

                    file.SaveAs(path);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);

                        byte[] array = ms.GetBuffer();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}