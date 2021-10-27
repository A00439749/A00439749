using System;
using System.IO;
using System.Collections;

namespace Assignment1
{

    public class DirWalker
    {
        public ArrayList Walk(String path)
        {

            var fileList = new ArrayList();

            foreach (string file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string extn = Path.GetExtension(file);
                if (extn == ".csv")
                {
                    fileList.Add(file);
                }
            }

            return fileList;
        }


    }
}
