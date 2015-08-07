using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization;

namespace DownloadVKMessages
{
    [Serializable]
    [DataContract]
    public class DDialog
    {
        [DataMember(Name = "WithUsername")]
        public string WithUsername { get; private set; }
        [DataMember(Name = "UserId")]
        public long UserId { get; private set; }
        [DataMember(Name = "DMessages")]
        [XmlArray("DMessages"), XmlArrayItem(typeof(DMessage), ElementName = "DMessage")]
        public List<DMessage> DMessages {get; private set;}

        public DDialog()
        {
            DMessages = new List<DMessage>();
        }

        public DDialog(long userId, string dialogWithUsername)
        {
            DMessages = new List<DMessage>();
            this.UserId = userId;
            this.WithUsername = dialogWithUsername;
        }

        public IEnumerable<DMessage> GetDMessages()
        {
            return DMessages;
        }

        public void Add(DMessage dMessage)
        {
            this.DMessages.Add(dMessage);
        }

        public void Add(IEnumerable<DMessage> dMessages)
        {
            foreach (var msg in dMessages)
            {
                Add(msg);
            }
        }
    }
}
