using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HubClient
{
    public class HubClient : IDisposable
    {
        public string HubAddress { get; set; }
        public string HubName { get; set; }
        public Dictionary<string, string> HubConnectctParames { get; set; }
        public IHubProxy hub { get; set; }
        public HubConnection connection { get; set; }
        #region HubReciveMessageCallBack
        public delegate void ReciveMessage(string str);
        /// <summary>
        /// 接收消息的回调
        /// </summary>
        public event ReciveMessage reciveMessage;
        #endregion
        #region HubStatusCallBack
        /// <summary>
        /// 接收状态的回调：例如连接成功
        /// </summary>
        /// <param name="str"></param>
        public delegate void ReciveStatus(string str);
        public event ReciveStatus reciveStatus;
        #endregion
        #region HubErrorCallBack
        /// <summary>
        /// 接收hub错误。
        /// </summary>
        public delegate void HubError(string str);
        public event HubError reciveHubError;
        #endregion
        #region DeviceCheckCallBack

        public delegate void DeviceCheckDelagate(string str);
        public event DeviceCheckDelagate DeviceCheck;
        #endregion
        /// <summary>
        /// CreateHub
        /// </summary>
        /// <param name="HubAddress">地址</param>
        /// <param name="HubMethod">名称</param>
        /// <param name="HubParams">参数例如：传入一个Dictionary dic = {{"Name","xiamenxxxxx"},{"Type","client"}}</param>
        public HubClient(string HubAddress, string HubMethod, Dictionary<string, string> HubParams = null)
        {
            this.HubAddress = HubAddress;
            this.HubName = HubMethod;
            this.HubConnectctParames = HubParams;
            ReConnecting();
        }
        /// <summary>
        /// HubInit
        /// </summary>
        /// <returns></returns>
        public void HubInit()
        {
            try
            {
                connection = new HubConnection(HubAddress, HubConnectctParames);
                hub = connection.CreateHubProxy(HubName);
                connection.Start().Wait();
                connection.StateChanged += Connection_StateChanged;
                hub.On("reciveMessage", data =>
                {
                    reciveMessage?.Invoke(data);
                });
                hub.Invoke("Ping");

            }
            catch (Exception ex)
            {
                reciveHubError?.Invoke(ex.ToString());
                reciveStatus?.Invoke("连接失败/与消息服务断开连接");
            }

        }
        public bool flag = false;
        private void Connection_StateChanged(StateChange obj)
        {

            if (obj.OldState == ConnectionState.Reconnecting && obj.NewState == ConnectionState.Disconnected)
            {
                flag = false;
            }
            else if (obj.OldState == ConnectionState.Connected && obj.NewState == ConnectionState.Disconnected)
            {
                flag = true;
                //connection.Dispose();
                //HubInit();
                reciveStatus?.Invoke("连接成功");
            }
            else if (obj.NewState == ConnectionState.Connecting)
            {
                reciveStatus?.Invoke("loading");
            }

            #region StateChange

            //switch (obj.OldState)
            //{
            //    case ConnectionState.Connected:
            //        switch (obj.NewState)
            //        {
            //            case ConnectionState.Reconnecting:
            //                flag = false;
            //                connection.Dispose();
            //                HubInit();
            //                reConnectedTrigger?.Invoke("重连机制触发，上次状态为重连");                 
            //                break;
            //            case ConnectionState.Disconnected:
            //                connection.Dispose();
            //                HubInit();
            //                reConnectedTrigger?.Invoke("重连机制触发，上次状态为重连");
            //                flag = true;
            //                break;
            //        }
            //        break;
            //    case ConnectionState.Reconnecting:
            //        switch (obj.NewState)
            //        {
            //            case ConnectionState.Disconnected:
            //                if (obj.OldState == ConnectionState.Connected && obj.NewState == ConnectionState.Disconnected)
            //                {
            //                    break;
            //                }
            //                else
            //                {

            //                    while (!(obj.OldState == ConnectionState.Connected && obj.NewState == ConnectionState.Disconnected))
            //                    {
            //                        if (flag == false)
            //                        {

            //                            Thread.Sleep(3000);
            //                            connection.Dispose();
            //                            HubInit();
            //                        }
            //                        else
            //                        {
            //                            break;
            //                        }
            //                    }

            //                    break;
            //                }
            //        }
            //        //connection.Dispose();
            //        ////hub = null;
            //        //HubInit();
            //        //reConnectedTrigger?.Invoke("重连机制触发，上次状态为重连");
            //        break;
            //    default:
            //        break;

            //} 
            #endregion

        }
        private void ReConnecting()
        {

            Task th = new Task(ReConnected);

            th.Start();



        }
        private void ReConnected()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (!flag)
                {

                    hub = null;
                    connection.Dispose();
                    HubInit();
                }
            }
        }
        /// <summary>
        /// SimulatorLaneSendMes
        /// </summary>
        public void Change(string lanecode, string jsonstr)
        {
            if (hub != null && connection.State == ConnectionState.Connected)
            {
                hub.Invoke("Change", lanecode, jsonstr);
                reciveStatus?.Invoke(connection.ConnectionId + ":" + lanecode + "::" + jsonstr);
                reciveStatus?.Invoke("已推送修改");
            }

        }
        public void SendMessage(string lanecode, string jsonstr)
        {
            if (hub != null && connection.State == ConnectionState.Connected)
            {
                hub.Invoke("SendMessage", lanecode, jsonstr);
            }
        }
        /// <summary>
        /// CheckConnectionStatus
        /// </summary>
        public void Ping()
        {
            if (hub != null && connection.State == ConnectionState.Connected)
            {
                hub.Invoke("Ping");
            }
        }
        /// <summary>
        /// DeleteQueueByLaneCode.
        /// </summary>
        /// <param name="lane_code">车道Code</param>
        public void DeleteQueueByLaneCode(string lane_code)
        {
            if (hub != null && connection.State == ConnectionState.Connected)
            {
                hub.Invoke("DeleteQueue", lane_code);
            }

        }
        public void Dispose()
        {
            try
            {
                connection.Stop();
            }
            catch
            {
                connection.Dispose();
                hub = null;
            }
            finally
            {
                connection.Dispose();
            }

        }
        public void TryPing(string ip, string name)
        {
            Ping ping = new Ping();
            ping.PingCompleted += Ping_PingCompleted;
            ping.SendAsync(ip, "硬件名" + name);
        }
        private void Ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            var reply = e.Reply;
            var name = e.UserState;
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add(name.ToString(), reply.Status.ToString());
            DeviceCheck?.Invoke(DicToJsonStr(result));
        }
        public void TryPingDeviceGroup(Dictionary<string, string> deviceGroup)
        {
            foreach (var item in deviceGroup)
            {
                TryPing(item.Key, item.Value);

            }
        }
        /// <summary>
        /// JsonEncodeTo Dictionary
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Dictionary<string, string> JsonStringToDic(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        }
        /// <summary>
        /// Dictionary DecodeToJson
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public string DicToJsonStr(IEnumerable<KeyValuePair<string, string>> dic)
        {
            return JsonConvert.SerializeObject(dic);
        }


         public static string ConvertJsonString(string str)
        {
            //格式化json字符串  
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }

    
}
