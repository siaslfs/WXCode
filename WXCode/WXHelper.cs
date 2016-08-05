using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXCode
{
    public class WXHelper
    {
        HttpContext context = HttpContext.Current;
        /// <summary>
        /// 获取格林威治时间
        /// </summary>
        /// <returns></returns>
        public int GetTime()
        {
            DateTime date = new DateTime(1970, 1, 1, 8, 1, 0, 0);
            return (int)(DateTime.Now - date).TotalSeconds;
        }
        public void TextMessage(string toUserName, string fromUserName, string content)
        {
            string reply_msg = string.Format(@"<xml>
                                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                                <CreateTime>{2}</CreateTime>
                                                <MsgType><![CDATA[text]]></MsgType>
                                                <Content><![CDATA[{3}]]></Content>
                                                </xml>", fromUserName, toUserName, GetTime(), content);
            context.Response.Write(reply_msg);
        }
        public void PhotoMessage(string toUserName, string fromUserName, string content)
        {
            string reply_msg = string.Format(@"<xml>
                                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                                <CreateTime>{2}</CreateTime>
                                                <MsgType><![CDATA[image]]></MsgType>
                                                <Image>
                                                    <MediaId><![CDATA[{3}]]></MediaId>
                                                </Image>
                                                </xml>", fromUserName, toUserName, GetTime(), content);
            context.Response.Write(reply_msg);
        }
        public void VoiceMessage(string toUserName, string fromUserName, string content)
        {
            string reply_msg = string.Format(@"<xml>
                                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                                <CreateTime>{2}</CreateTime>
                                                <MsgType><![CDATA[music]]></MsgType>
                                                <Music>
                                                    <Title><![CDATA[{3}]]></Title>
                                                    <Description><![CDATA[{4}]]></Description>
                                                    <MusicUrl><![CDATA[{5}]]></MusicUrl>
                                                    <HQMusicUrl><![CDATA[{5}]]></HQMusicUrl>
                                                </Music>
                                                </xml>", fromUserName, toUserName, GetTime(), "音乐", "爱之歌", content);
            context.Response.Write(reply_msg);
        }
        public void VideoMessage(string toUserName, string fromUserName, string content)
        {
            string reply_msg = string.Format(@"<xml>
                                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                                <CreateTime>{2}</CreateTime>
                                                <MsgType><![CDATA[video]]></MsgType>
                                                <Video>
                                                <MediaId><![CDATA[{3}]]></MediaId>
                                                <Title><![CDATA[{4}]]></Title>
                                                <Description><![CDATA[{5}]]></Description>
                                                </Video> 
                                                </xml>", fromUserName, toUserName, GetTime(), content, "音乐", "爱之歌");
            context.Response.Write(reply_msg);
        }
        public void PhotoTextMessage(string toUserName, string fromUserName, string content)
        {
            string reply_msg = string.Format(@"<xml>
                                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                                <CreateTime>{2}</CreateTime>
                                                <MsgType><![CDATA[news]]></MsgType>
                                                <ArticleCount>1</ArticleCount>
                                                <Articles>
                                                <item>
                                                <Title><![CDATA[{3}]]></Title> 
                                                <Description><![CDATA[{4}]]></Description>
                                                <PicUrl><![CDATA[{5}]]></PicUrl>
                                                <Url><![CDATA[{6}]]></Url>
                                                </item>
                                                </Articles>
                                                </xml>", fromUserName, toUserName, GetTime(), "励志家族", "相亲相爱一家人，所有励志人的家", "http://jy.occupationedu.com/photo/1.jpg", "http://www.baidu.com");
            context.Response.Write(reply_msg);
        }

    }
}