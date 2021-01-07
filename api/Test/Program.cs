using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
// using System.Net.Mail;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.IO;
using System.Linq;

namespace Test
{
    

    /// <summary>
    /// 基于MailKit的邮件帮助类
    /// </summary>
    public static class EMailHelper
    {
        /// <summary>
        /// 邮件服务器Host
        /// </summary>
        public static string Host { get; set; }
        /// <summary>
        /// 邮件服务器Port
        /// </summary>
        public static int Port { get; set; }
        /// <summary>
        /// 邮件服务器是否是ssl
        /// </summary>
        public static bool UseSsl { get; set; }
        /// <summary>
        /// 发送邮件的账号友善名称
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        /// 发送邮件的账号地址
        /// </summary>
        public static string UserAddress { get; set; }
        /// <summary>
        /// 发现邮件所需的账号密码
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// 发送电子邮件，默认发送方为<see cref="UserAddress"/>
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="textFormat">内容主题模式，默认TextFormat.Text</param>
        /// <param name="attachments">附件</param>
        /// <param name="dispose">是否自动释放附件所用Stream</param>
        /// <returns></returns>
        public static async Task SendEMailAsync(string subject, string content, IEnumerable<MailboxAddress> toAddress, TextFormat textFormat = TextFormat.Text, IEnumerable<AttachmentInfo> attachments = null, bool dispose = true)
        {
            await SendEMailAsync(subject, content, new MailboxAddress[] { new MailboxAddress(UserName, UserAddress) }, toAddress, textFormat, attachments, dispose).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="fromAddress">发送方信息</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="textFormat">内容主题模式，默认TextFormat.Text</param>
        /// <param name="attachments">附件</param>
        /// <param name="dispose">是否自动释放附件所用Stream</param>
        /// <returns></returns>
        public static async Task SendEMailAsync(string subject, string content, MailboxAddress fromAddress, IEnumerable<MailboxAddress> toAddress, TextFormat textFormat = TextFormat.Text, IEnumerable<AttachmentInfo> attachments = null, bool dispose = true)
        {
            await SendEMailAsync(subject, content, new MailboxAddress[] { fromAddress }, toAddress, textFormat, attachments, dispose).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="fromAddress">发送方信息</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="textFormat">内容主题模式，默认TextFormat.Text</param>
        /// <param name="attachments">附件</param>
        /// <param name="dispose">是否自动释放附件所用Stream</param>
        /// <returns></returns>
        public static async Task SendEMailAsync(string subject, string content, IEnumerable<MailboxAddress> fromAddress, IEnumerable<MailboxAddress> toAddress, TextFormat textFormat = TextFormat.Text, IEnumerable<AttachmentInfo> attachments = null, bool dispose = true)
        {
            var message = new MimeMessage();
            message.From.AddRange(fromAddress);
            message.To.AddRange(toAddress);
            message.Subject = subject;
            var body = new TextPart(textFormat)
            {
                Text = content
            };
            MimeEntity entity = body;
            if (attachments != null)
            {
                var mult = new Multipart("mixed")
                {
                    body
                };
                foreach (var att in attachments)
                {
                    if (att.Stream != null)
                    {
                        var attachment = string.IsNullOrWhiteSpace(att.ContentType) ? new MimePart() : new MimePart(att.ContentType);
                        attachment.Content = new MimeContent(att.Stream);
                        attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);
                        attachment.ContentTransferEncoding = att.ContentTransferEncoding;
                        attachment.FileName = ConvertHeaderToBase64(att.FileName, Encoding.UTF8);//解决附件中文名问题
                        mult.Add(attachment);
                    }
                }
                entity = mult;
            }
            message.Body = entity;
            message.Date = DateTime.Now;
            using (var client = new SmtpClient())
            {
                //创建连接
                await client.ConnectAsync(Host, Port, UseSsl).ConfigureAwait(false);
                await client.AuthenticateAsync(UserAddress, Password).ConfigureAwait(false);
                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
                if (dispose && attachments != null)
                {
                    foreach (var att in attachments)
                    {
                        att.Dispose();
                    }
                }
            }
        }
        private static string ConvertToBase64(string inputStr, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(inputStr));
        }
        private static string ConvertHeaderToBase64(string inputStr, Encoding encoding)
        {
            var encode = !string.IsNullOrEmpty(inputStr) && inputStr.Any(c => c > 127);
            if (encode)
            {
                return "=?" + encoding.WebName + "?B?" + ConvertToBase64(inputStr, encoding) + "?=";
            }
            return inputStr;
        }
    }
    /// <summary>
    /// 附件信息
    /// </summary>
    public class AttachmentInfo : IDisposable
    {
        /// <summary>
        /// 附件类型，比如application/pdf
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件传输编码方式，默认ContentEncoding.Default
        /// </summary>
        public ContentEncoding ContentTransferEncoding { get; set; } = ContentEncoding.Default;
        /// <summary>
        /// 文件数组
        /// </summary>
        public byte[] Data { get; set; }
        private Stream stream;
        /// <summary>
        /// 文件数据流，获取数据时优先采用此部分
        /// </summary>
        public Stream Stream
        {
            get
            {
                if (this.stream == null && this.Data != null)
                {
                    stream = new MemoryStream(this.Data);
                }
                return this.stream;
            }
            set { this.stream = value; }
        }
        /// <summary>
        /// 释放Stream
        /// </summary>
        public void Dispose()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
            }
        }
    }
    class Program
    {
        /// <summary>
        /// 发送邮件,成功返回true,否则false
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="body">内容</param>
        /// <param name="title">标题</param>
        /// <param name="whichEmail">是否join</param>
        /// <param name="path">附件</param>
        /// <param name="Fname">姓名</param>
        /// <returns>结果</returns>
        //public static bool SentMailHXD(string to, string body, string title, string whichEmail, string path, string Fname)
        //{

        //    bool retrunBool = false;
        //    MailMessage mail = new MailMessage();
        //    SmtpClient smtp = new SmtpClient();
        //    string strFromEmail = "1092473937@qq.com";//你的邮箱
        //    string strEmailPassword = "fhsarxkreksrfeeg";//QQPOP3/SMTP服务码
        //    try
        //    {
        //        mail.From = new MailAddress("" + Fname + "<" + strFromEmail + ">");
        //        mail.To.Add(new MailAddress(to));
        //        mail.BodyEncoding = Encoding.UTF8;
        //        mail.IsBodyHtml = true;
        //        mail.SubjectEncoding = Encoding.UTF8;
        //        mail.Priority = MailPriority.Normal;
        //        mail.Body = body;
        //        mail.Subject = title;
        //        smtp.Host = "smtp.qq.com";
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new System.Net.NetworkCredential(strFromEmail, strEmailPassword);
        //        发送邮件
        //        smtp.SendAsync(mail, "dddddd");   //同步发送
        //        retrunBool = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        retrunBool = false;
        //    }
        //    smtp.SendAsync(mail, mail.To); //异步发送 （异步发送时页面上要加上Async="true" ）
        //    return retrunBool;
        //}
        

        static async void email()
        {
            EMailHelper.Port = 465;
            EMailHelper.Host = "smtp.qq.com";
            EMailHelper.Password = "fhsarxkreksrfeeg";
            EMailHelper.UserAddress = "1092473937@qq.com";
            EMailHelper.UseSsl = true;
            EMailHelper.UserName = "天王盖地虎";
            await EMailHelper.SendEMailAsync("芝麻开门", "嘀嘀嘀嘀嘀嘀", new List<MailboxAddress>() { new MailboxAddress("这是一个名字", "2023117567@qq.com") });
        }
        static int Trap(int[] height)
        {
            int length = height.Length;
            int[] left = new int[length];//保存从左往右遍历时，每一个下标位置当前的最高柱子高度
            int[] right = new int[length];//保存从右往左遍历时，每一个下标位置当前的最高柱子高度
            int leftMax = 0;
            int rightMax = 0;
            int sum = 0;

            //计算left和right数组
            for (int i = 0; i < length; i++)
            {
                if (height[i] > leftMax)
                {
                    leftMax = height[i];
                }
                left[i] = leftMax;
                if (height[length - 1 - i] > rightMax)
                {
                    rightMax = height[length - 1 - i];
                }
                right[length - 1 - i] = rightMax;
            }
            //遍历，只有当前柱子往左看、往右看的最高柱子都比当前柱子高，才能接住雨水
            for (int j = 0; j < length; j++)
            {
                if (height[j] < left[j] && height[j] < right[j])
                {
                    sum = sum + Math.Min(left[j], right[j]) - height[j];
                }
            }
            return sum;
        }
        static int rob(int[] nums)
        {
            int n = nums.Length;
            // 处理边界条件。
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return nums[0];
            }
            // 定义dp数组，按照状态转移方程递推。
            int[] dp = new int[n];
            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < n; i++)
            {
                dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
            }
            return dp[n - 1];
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(2000);
                email();
            }
           
            //demo5
            //demo6();
            //demo7();
            //var arr = Trap(new int[]{ 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 });
            //rob(new int[] { 0, 1, 0, 2, 1 });
            //var s = SentMailHXD("2263666854@qq.com", "内容哦", "通知", "true", "", "天王");
            //Thread.Sleep(5000);
            Console.ReadKey();
        }

        #region join的使用，线程等待
        static void demo1()
        {
            var th1 = new Thread(() => {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("ht11111111111线程");
                }
            });
            var th2 = new Thread(() => {
                th1.Start();
                th1.Join();
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("ht22222222222线程");
                }
            });
            th2.Start();
            th2.Join();
            Console.WriteLine("所以线程执行完毕");
            Console.ReadKey();
        }
        #endregion

        #region Abort的使用，线程终止
        // 目标线程可以通过处理该异常并调用Thread.ResetAbort方法来拒绝被终止。
        //因此并不推荐使用,Abort方法来关闭线程 。
        static void demo2()
        {
            var th = new Thread(() => Console.WriteLine("执行代码"));
            Thread.Sleep(4000);
            th.Abort();
            Console.WriteLine("线程被终止了");
        }
        #endregion

        #region 获取当前线程状态
        static void demo3()
        {
            var th = new Thread(() => Console.WriteLine("执行代码"));
            th.Start();
            Console.WriteLine("线程状态："+th.ThreadState.ToString());
            th.Join();
            Console.WriteLine("线程状态：" + th.ThreadState.ToString());
            Console.WriteLine("线程th完成");
            Console.WriteLine("线程状态：" + th.ThreadState.ToString());
        }
        #endregion

        #region 线程优先级
        static void demo4()
        {
            
            var th1 = new Thread(sum);
            var th2 = new Thread(sum);
            th1.Name = "tang";
            th2.Name = "zeng";
            th1.Priority = ThreadPriority.Lowest;
            th2.Priority = ThreadPriority.Highest;
            th1.Start();
            th2.Start();
            Thread.Sleep(TimeSpan.FromSeconds(3));
            flage = false;

        }
        #endregion

        static bool flage = true;
        static void sum()
        {
            int sum = 0;
            while (flage)
            {
                sum++;
            }
            Console.WriteLine($"当前线程名字：{Thread.CurrentThread.Name}--当前线程优先级：{Thread.CurrentThread.Priority}--当前的sum：{sum}");
        }

        #region 前台线程和后台线程
        static void demo5()
        {
            var sum = 0;
            var th1 = new Thread(() => { 
                    while(true)
                    Console.WriteLine(sum); 
            });
            var th2 = new Thread(() => {
                while (true)
                {
                    sum++;
                }
            });
            th1.IsBackground = false;
            th2.IsBackground = true;
            th1.Start();
            th2.Start();
        }
        #endregion

        #region 向线程中传递参数
        public static void demo6()
        {
            Thread th = new Thread(sum2);
            th.Start(10);

        }
        public static void sum2(object obj)
        {
            int count = (int)obj;
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("heloo");
            }
        }
        #endregion

        #region 线程锁的使用
        static void demo7() {
            int count = 0;
            object obj = new object();
            Task.Run(()=> {
                while (true)
                lock (obj)
                {
                        Monitor.Wait(obj);
                        count++;
                }
                
            });
            Task.Run(() => {
               
                //while (true)
                lock (obj)
                {
                    Monitor.Pulse(obj);
                    count--;
                }
            });
            Thread.Sleep(2000);
            Console.WriteLine(count);
        }
        #endregion
    }


}
