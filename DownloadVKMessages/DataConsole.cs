using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;

namespace DownloadVKMessages
{
    public class DataConsole
    {
        public int appID { get; set; }
        public string email { get; set; }
        public string pass { get; set; }

        public void EnterAuthorizeData()
        {
            Console.Write("appID: ");
            appID = int.Parse(Console.ReadLine());
            Console.Write("email: ");
            email = Console.ReadLine();
            Console.Write("pass: ");
            pass = Console.ReadLine();
        }

        public string GetCaptcha(string captcha_img)
        {
            Console.WriteLine();
            Console.WriteLine("Captcha link: " + captcha_img);
            Console.Write("Enter captcha: ");
            string enterCaptcha = Console.ReadLine();

            return enterCaptcha;
        }

        public void ShowDialog(DDialog dialog)
        {
            Console.WriteLine("Dialog with: " + dialog.WithUsername);
            foreach (var msg in dialog.GetDMessages())
            {
                Console.WriteLine("===================");
                Console.WriteLine("Text: " + msg.Text);
                Console.WriteLine("Date: " + msg.Date);
                Console.WriteLine("My: " + msg.IsSended);
            }
        }
    }
}
