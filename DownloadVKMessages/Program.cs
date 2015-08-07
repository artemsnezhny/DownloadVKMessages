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
    class Program
    {
        static void Main(string[] args)
        {
            DataConsole dataConsole = new DataConsole();
            VK vk = new VK();
            dataConsole.EnterAuthorizeData();
            byte authCount = 0;
            bool exit = false;
            while (!exit && authCount < 3)
            {
                try
                {
                    vk.Authorize(dataConsole.appID, dataConsole.email, dataConsole.pass);
                    exit = true;
                }
                catch (CaptchaNeededException ex)
                {
                    try
                    {
                        vk.AuthorizeWithCaptcha(dataConsole.appID, dataConsole.email, dataConsole.pass,
                            ex.Sid, dataConsole.GetCaptcha(ex.Img.AbsoluteUri));
                        exit = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid captcha!");
                        authCount++;
                    }
                }
            }
            if (authCount < 3)
            {
                //Цикл по всем диалогам. Если чат, а не беседа, то загурузить историю и сохранить
                List<long> usersId = new List<long>();

                int dialogsCount;
                int unreadCount;
                int offset = 0;
                int downloadDialogs = 10;

                var dialogs = vk.Api.Messages.GetDialogs(downloadDialogs, offset, out dialogsCount, out unreadCount);
                foreach (var d in dialogs)
                {
                    if (d.ChatId == null)
                        usersId.Add((long)d.UserId);
                }

                for (long i = usersId.Count; i < dialogsCount; i += downloadDialogs)
                {
                    offset = (int)i;
                    dialogs = vk.Api.Messages.GetDialogs(downloadDialogs, offset, out dialogsCount, out unreadCount);
                    foreach (var d in dialogs)
                    {
                        if (d.ChatId == null)
                            usersId.Add((long)d.UserId);
                    }
                }

                for (int i = 0; i < usersId.Count; i++)
                {
                    long dialogId = usersId[i];
                    DDialog dialog = vk.GetFullHistoryByUserId(dialogId);
                    Console.WriteLine("Downloaded dialog with " + dialog.WithUsername + " Messages: " + dialog.DMessages.Count);
                    //dataConsole.ShowDialog(vk.GetFullHistoryByUserId(dialogId));
                    Serializer.SaveDialog(dialog);
                }
            }
            else
            {
                Console.WriteLine("Sorry... Goodbye ;(");
            }

            Console.ReadLine();
        }
    }
}
