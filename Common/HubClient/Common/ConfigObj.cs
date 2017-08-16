using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HubClient.Common
{
   public class ConfigObj
    {
        [CategoryAttribute("消息服务")]
        [DisplayName("消息服务名")]
        [Description("将连接的消息服务的名称")]
        public string hub_name { get; set; }
        [CategoryAttribute("消息服务")]
        [DisplayName("消息服务地址")]
        [Description("消息服务的地址")]
        public string hub_address { get; set; }
        [Category("参数设置")]
        [DisplayName("本服务器ID")]
        [Description("与消息服务交互时的主键")]
        public string server_code { get; set; }
        [Category("参数设置")]
        [DisplayName("本服务器名称")]
        [Description("与消息服务交互时的名称")]
        public string server_name { get; set; }

        [Category("模拟设置")]
        [DisplayName("步骤间隔时间(单位/毫秒）")]
        [Description("执行每个操作的间隔时间，默认为5秒")]
        public int time { get; set; }

        [Category("坐标点集合")]
        [DisplayName("设置坐标点的相关参数")]
        [Description("设置坐标点相关参数")]
        [TypeConverter(typeof(StringConverter))]
        public PointObject[] points { get; set; }


        public static bool WriteToLocal(ConfigObj obj)
        {
            try
            {
                if (obj != null)
                {
                    File.WriteAllText(Application.StartupPath + "/Config.json", JsonConvert.SerializeObject(obj));

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static ConfigObj ReadFromLocal()
        {
            try
            {
                return JsonConvert.DeserializeObject<ConfigObj>(File.ReadAllText(Application.StartupPath + "/Config.json"));
            }
            catch
            {
                return null;
            }
        }

      
    }

    public class PointObject
    {
        [Category("I坐标")]
        [DisplayName("设置坐标点的相关参数")]
        [Description("设置坐标点相关参数")]
        public string spreader_outstretch { get; set; }
        [Category("J坐标")]
        [DisplayName("设置坐标点的相关参数")]
        [Description("设置坐标点相关参数")]
        public string spreader_height { get; set; }

    }
}
