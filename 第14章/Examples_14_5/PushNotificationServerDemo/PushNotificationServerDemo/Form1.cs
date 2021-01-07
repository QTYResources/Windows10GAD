using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PushNotificationServerDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // 发送通知的按钮
        private void button1_Click(object sender, EventArgs e)
        {
            sendNotificationType(textBox2.Text, textBox1.Text);
        }
        /// <summary>
        /// 发送推送通知
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="notifyUrl">推送的通道</param>
        void sendNotificationType(string message, string notifyUrl)
        {
            // 程序包安全标识符(SID)
            string sid = "ms-app://s-1-15-2-4017463433-3104818020-3212661602-2100054673-1509338986-1481803562-2878777805";
            // 客户端密钥
            string secret = "dmKrqkwpNF1Bd1L0RDTW1AWkxoTlwsqu";// "bs08Acs1RG7jB7pkGVMh8EmGKCG3pH+3";
            OAuthHelper oAuth = new OAuthHelper();
            // 获取访问的令牌
            OAuthToken token = oAuth.GetAccessToken(secret, sid);
            try
            {
                // 创建Http对象
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(notifyUrl);
                // toast, tile, badge 为 text/xml; raw 为 application/octet-stream
                myRequest.ContentType = "text/xml";
                // 设置 access-token
                myRequest.Headers.Add("Authorization", String.Format("Bearer {0}", token.AccessToken));
                string message2 = "test";
                if (radioButton1.Checked)
                {
                    message2 = message;
                    // 推送raw消息
                    myRequest.Headers.Add("X-WNS-Type", "wns/raw");
                    // 注意raw 消息为 application/octet-stream
                    myRequest.ContentType = "application/octet-stream";
                }
                else if (radioButton2.Checked)
                {
                    message2 = NotifyTile(message);
                    // 推送tile消息
                    myRequest.Headers.Add("X-WNS-Type", "wns/tile");
                }
                else if (radioButton3.Checked)
                {
                    message2 = NotifyToast(message);
                    // 推送toast消息
                    myRequest.Headers.Add("X-WNS-Type", "wns/toast");
                }
                else if (radioButton4.Checked)
                {
                    message2 = NotifyBadge(message);
                    // 推送badge消息
                    myRequest.Headers.Add("X-WNS-Type", "wns/badge");
                }
                else
                {
                    // 默认的消息
                    myRequest.Headers.Add("X-WNS-Type", "wns/raw");
                    myRequest.ContentType = "application/octet-stream";
                }
                byte[] buffer = Encoding.UTF8.GetBytes(message2);
                myRequest.ContentLength = buffer.Length;
                myRequest.Method = "POST";
                using (Stream stream = myRequest.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    /*
                     * 响应代码说明
                     *     200 - OK，WNS 已接收到通知
                     *     400 - 错误的请求
                     *     401 - 未授权，token 可能无效
                     *     403 - 已禁止，manifest 中的 identity 可能不对
                     *     404 - 未找到
                     *     405 - 方法不允许
                     *     406 - 无法接受
                     *     410 - 不存在，信道不存在或过期
                     *     413 - 请求实体太大，限制为 5000 字节
                     *     500 - 内部服务器错误
                     *     503 - 服务不可用
                     */
                    label4.Text = webResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                label4.Text = "异常" + ex.Message;
            }
        }
        // 封装Toast消息格式
        public string NotifyToast(string message)
        {
            string toastmessage =
                    @"<toast>
                         <visual>
                            <binding template=""ToastText02"">
                                <text id=""1"">" + message + @"</text>
                            </binding>
                        </visual>
                     </toast>";
            return toastmessage;
        }
        // 封装Tile消息格式
        public string NotifyTile(string message)
        {
            string tilemessage =
                @"<tile>
                      <visual>
                          <binding template=""TileWideText03"">
                               <text id=""1"">" + message + @"</text>
                          </binding>  
                      </visual>
                 </tile>";

            return tilemessage;
        }
        // 封装Badge消息格式
        public string NotifyBadge(string badge)
        {
            string badgemessage = (@"<badge value=""" + badge + @""" />");
            return badgemessage;
        }
    }
}
