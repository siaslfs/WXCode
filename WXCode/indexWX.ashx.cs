using System;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace WXCode
{
    /// <summary>
    /// indexWX 的摘要说明
    /// </summary>
    public class indexWX : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            WXHelper wx = new WXHelper();
            if (context.Request.HttpMethod.ToUpper().Equals("GET"))
            {
                //验证是否是微信服务器
                CheckServer();
            }
            else
            {
                //处理提交到服务器的数据 
                Stream stream = context.Request.InputStream;//获取http中传递的流数据
                //处理xml文本
                XmlDocument dom = new XmlDocument(); //设置变量
                dom.Load(stream);//加载xml文件
                XmlElement root = dom.DocumentElement;
                //文本信息 
                string ToUserName = root.SelectSingleNode("ToUserName").InnerText;
                string FromUserName = root.SelectSingleNode("FromUserName").InnerText;
                string MsgType = root.SelectSingleNode("MsgType").InnerText;

                if (MsgType.ToLower().Equals("text"))
                {
                    string Content = root.SelectSingleNode("Content").InnerText;
                    switch (Content)
                    {
                        case "1":
                            wx.TextMessage(ToUserName, FromUserName, DateTime.Now.ToString());
                            break;
                        case "2":
                            wx.PhotoMessage(ToUserName, FromUserName, "TIuN5lvlR8khHsm-qJNI4MYNB7mka5IvQsnVOwg1y27gXLKnCmBVNQdNhZoId-95");
                            break;
                        case "3":
                            wx.VoiceMessage(ToUserName, FromUserName, "http://jy.occupationedu.com/music/no.mp3");
                            break;
                        case "4":
                            wx.VideoMessage(ToUserName, FromUserName, "kqidyroafvwMteYrj5bx5e6ePEcDDabrBaZoeO5X_zP4-KA8zEyj2cgBOd3iyUul");
                            break;
                        case "5":
                            wx.PhotoTextMessage(ToUserName, FromUserName, "kqidyroafvwMteYrj5bx5e6ePEcDDabrBaZoeO5X_zP4-KA8zEyj2cgBOd3iyUul");
                            break;
                        default:
                            wx.TextMessage(ToUserName, FromUserName, string.Format(@"1--当前时间
                                                                                     2--美图
                                                                                     3--音乐
                                                                                     4--视频
                                                                                     5--图文并茂"));
                            break;
                    }
                }
                else if (MsgType.ToLower().Equals("location"))
                {
                    wx.TextMessage(ToUserName, FromUserName, "这是一个地址！");
                }
                else if (MsgType.ToLower().Equals("event"))
                {
                    wx.TextMessage(ToUserName, FromUserName, "淫人，欢迎订阅！");
                }

            }
        }
        public void CheckServer()
        {
            //signature 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
            //timestamp 时间戳
            //nonce 随机数
            //echostr 随机字符串
            HttpContext context = HttpContext.Current; //获取当前请求的上下文
            string token = "siaslfs";
            string signature = context.Request["signature"];
            string timestamp = context.Request["timestamp"];
            string nonce = context.Request["nonce"];
            string echostr = context.Request["echostr"];
            //1）将token、timestamp、nonce三个参数进行字典序排序
            //2）将三个参数字符串拼接成一个字符串进行sha1加密
            //3）开发者获得加密后的字符串可与signature对比，标识该请求来源于微信
            string[] temp = { token, timestamp, nonce };
            //字典排序
            Array.Sort(temp);
            //拼接字符串
            string tempStr = string.Join("", temp);
            //sha1加密
            string temppwd = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "SHA1");
            if (temppwd.ToLower().Equals(signature))
            { //判断是否是通过微信服务器请求
                context.Response.Write(echostr);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}