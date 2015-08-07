using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;

namespace DownloadVKMessages
{
    public class VK
    {
        public VkApi Api { get; private set; }

        public Settings scope { get; private set; }

        public VK()
        {
            Api = new VkApi();
            scope = Settings.Messages;
        }

        public void Authorize(int appID, string email, string pass)
        {
            Api.Authorize(appID, email, pass, scope);
        }

        public void AuthorizeWithCaptcha(int appID, string email, string pass, long? captchasid, string captchakey)
        {
            Api.Authorize(appID, email, pass, scope, captchasid, captchakey);
        }

        public DDialog GetFullHistoryByUserId(long userId)
        {
            var user = Api.Users.Get(userId);
            DDialog dDialog = new DDialog(userId, user.FirstName + " " + user.LastName);

            int totalCount = 0;
            int offset = 0;
            int downloadCount = 30;

            IReadOnlyCollection<VkNet.Model.Message> dialog = null;
            bool ok = false;
            while (!ok)
            {
                try
                {
                    dialog = this.Api.Messages.GetHistory(userId, false, out totalCount,0,10);
                    ok = true;
                }
                catch (Exception e)
                {
                    ok = false;
                    Console.WriteLine("Error in download dialog " + dDialog.WithUsername + "offset: " + offset);
                }
            }
            bool isSended;
            foreach (var msg in dialog)
            {
                isSended = (msg.Type == VkNet.Enums.MessageType.Sended) ? true : false;
                dDialog.Add(new DMessage(msg.Body, (DateTime)msg.Date, isSended));
            }

            for (long i = dDialog.DMessages.Count; i < totalCount; i += downloadCount)
            {
                //It isn't good
                offset = (int)i;
                ok = false;
                while (!ok)
                {
                    try
                    {
                        dialog = this.Api.Messages.GetHistory(userId, false, out totalCount, offset, downloadCount);
                        ok = true;
                    }
                    catch (Exception e)
                    {
                        ok = false;
                        Console.WriteLine("Error in download dialog " + dDialog.WithUsername + "offset: " + offset);
                    }
                }
                foreach (var msg in dialog)
                {
                    isSended = (msg.Type == VkNet.Enums.MessageType.Sended) ? true : false;
                    dDialog.Add(new DMessage(msg.Body, (DateTime)msg.Date, isSended));
                }
            }

            return dDialog;
        }
    }
}
