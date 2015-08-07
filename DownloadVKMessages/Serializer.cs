using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DownloadVKMessages
{
    public static class Serializer
    {
        private const string directory = "Dialogs";

        public static void SaveDialog(DDialog dialog)
        {
            DataContractSerializer formatter = new DataContractSerializer(typeof(DDialog));
            var settings = new XmlWriterSettings { Indent = true };
            using (XmlWriter xw = XmlWriter.Create(directory + "\\" + dialog.WithUsername + "-" + dialog.DMessages.Count + ".xml",settings))
            {
                formatter.WriteObject(xw, dialog);
            }
        }
    }
}
