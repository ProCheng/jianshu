using System;
using System.Text.RegularExpressions;
using MailKit;
using MimeKit;

namespace Common
{
    public class EmailService
    {
       public static bool SendEmail(string code,string email)
        {
            string mailTo = email;
            // 设置消息对象
            var message = new MimeMessage();
            string mailFromAccount = "1092473937@qq.com";
            // 开通的smtp服务会发给你
            string mailPassword = "iwutgyefyglijhje";
            string mailFrom = "1092473937@qq.com";

            //添加from and to的消息地址列表和添加的地址
            message.From.Add(new MailboxAddress("简书官方", mailFrom));
            message.To.Add(new MailboxAddress("xx", mailTo));   //前边的名字随便写，
            //设置消息主题
            message.Subject = "欢迎注册简书!";
            //生成6位的验证码
            string content = $"<b>验证码</b>：<span style='color:red'>{code}</span>（仅五分钟有效）";

            //创建消息的文本
            message.Body = new TextPart("Html")
            {
                Text = @content
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.qq.com", 587, false); //前边是smtp的服务器，端口号
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(mailFromAccount, mailPassword);//认证发送者
                    client.Send(message);//发消息
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
            return true;
        }
    }
}
