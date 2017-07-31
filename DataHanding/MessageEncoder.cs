using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataHanding
{
    public static class MessageEncoder
    {
        public enum RecipientType
        {
            ALL,
            P2P
        }
        /// <summary>
        /// LaneEncode
        /// </summary>
        /// <param name="JsonStr">paramstring</param>
        /// <param name="recitype">p2porall</param>
        /// <param name="recipient_name">AllorSb</param>
        /// <returns></returns>
        public static string EncodingLaneMessage(dynamic Lane, string lane_code, string lane_name, RecipientType recitype, string recipient_name = "ALL")
        {
            //母版
            dynamic result = Lane;
            //解析
            result.message_type = "lane";
            result.sender_code = lane_code;
            result.sender_name = lane_name;
            result.recipient_code = recitype;
            result.recipient_name = recipient_name;
            result.send_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            result.message_content.lane = Lane.message_content.lane;
            return JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// Lane_Decode
        /// </summary>
        /// <param name="JsonStr">paramsstring</param>
        /// <returns></returns>
        public static string DecodingLaneMessage(string JsonStr)
        {
            dynamic BeDecodeObj = JsonConvert.DeserializeObject<dynamic>(JsonStr);
            return JsonConvert.SerializeObject(BeDecodeObj.message_content);
        }
        public enum QueueAction
        {
            create,
            update,
            delete
        }
        /// <summary>
        /// EncodeQueue
        /// </summary>
        /// <param name="JsonStr"></param>
        /// <param name="action"></param>
        /// <param name="recipient_name"></param>
        /// <returns></returns>
        public static string EncodingQueueMessage(dynamic Queue, string lanecode, string lanename, QueueAction action, string recipient_name = "ALL")
        {
            //母版
            dynamic result = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Application.StartupPath + "/conf/queue.json"));
            //赋值
            dynamic BeEncodeObj = Queue;
            //解析
            result.message_type = "queue";
            result.sender_code = lanecode;
            result.sender_name = lanename;
            result.message_content.lane_code = lanecode;
            BeEncodeObj.lane_code = lanecode;
            BeEncodeObj.lane_name = lanename;
            result.recipient_code = recipient_name;
            result.recipient_name = recipient_name;
            result.send_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            switch (action)
            {
                case QueueAction.create:
                    result.message_content.queue_code = new Guid();
                    BeEncodeObj.quque_code = result.message_content.queue_code;
                    result.message_content.action = "create";
                    break;
                case QueueAction.update:
                    result.message_content.action = "update";
                    break;
                case QueueAction.delete:
                    result.message_content.action = "delete";
                    break;
            }

            BeEncodeObj.update_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            result.message_content.queue = BeEncodeObj;
            return JsonConvert.SerializeObject(result);

        }
        /// <summary>
        /// DecodeQueue
        /// </summary>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        public static string DecodingQueueMessage(string JsonStr)
        {
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject<dynamic>(JsonStr).message_content.queue);
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
