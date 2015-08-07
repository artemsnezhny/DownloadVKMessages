using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DownloadVKMessages
{
    [Serializable]
    [XmlRoot("DDialog")]
    [DataContract]
    public class DMessage
    {
        [DataMember(Name = "Text")]
        public string Text { get; private set; }
        [DataMember(Name = "Date")]
        public DateTime Date { get; private set; }
        [DataMember(Name = "IsSended")]
        public bool IsSended { get; private set; }

        [DataMember(Name = "HaveForwardMessage")]
        public bool HaveForwardMessage { get; private set; }
        [DataMember(Name = "ForwardMessage")]
        public DMessage ForwardMessage { get; private set; }

        public DMessage()
        { }

        public DMessage(string text, DateTime date, bool isCurrentUser)
        {
            this.Text = text;
            this.Date = date;
            this.IsSended = isCurrentUser;
            this.HaveForwardMessage = false;
            this.ForwardMessage = null;
        }

        public DMessage(string text, DateTime date, bool isCurrentUser, DMessage forwardMessage)
            : this(text,date,isCurrentUser)
        {
            this.HaveForwardMessage = true;
            this.ForwardMessage = forwardMessage;
        }
    }
}
