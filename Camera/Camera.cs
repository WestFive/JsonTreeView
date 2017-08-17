using NetSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Camera
{
    public class CamerController
    {

        private int m_nLoginID;     //Device Login ID
        private bool m_bInit;       //Whether the Library is Initialized successfully
        private int m_nChannelNum;  //Device Channel Number
        private bool m_bJSON;       //
        private NET_SNAP_ATTR_EN m_stuSnapAttr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private NETDEV_SNAP_CFG[] m_stuSnapCfg;
        private fDisConnect m_disConnect;
        private fSnapRev m_SnapRecv;
        private event fSnapRev snaprecv;
        public CamerController()
        {
            m_nLoginID = 0;
            m_bInit = false;
            m_stuSnapCfg = new NETDEV_SNAP_CFG[32];
            for (int i = 0; i < 32; ++i)
            {
                m_stuSnapCfg[i].struSnapEnc = new NET_VIDEOENC_OPT[32];
            }
            m_stuSnapAttr = new NET_SNAP_ATTR_EN();
            m_stuSnapAttr.stuSnap = new NET_QUERY_SNAP_INFO[16];

        }



        public string InitSDK()
        {
            m_disConnect = new fDisConnect(DisConnectEvent);
            m_SnapRecv = new fSnapRev(SnapRev);
            m_bInit = NETClient.NETInit(m_disConnect, IntPtr.Zero);
            if (!m_bInit)
            {
                //MessageBox.Show("初始化失败");
                return ("初始化失败");
            }
            else
            {
                NETClient.NETSetSnapRevCallBack(m_SnapRecv, 0);
            }
            return "初始化成功";

        }

        private void SnapRev(int lLoginID, IntPtr pBuf, uint RevLen, uint EncodeType, uint CmdSerial, uint dwUser)
        {
            byte[] buf = new byte[RevLen];
            Marshal.Copy(pBuf, buf, 0, (int)RevLen);

            string strName = string.Format("{0:n0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmssfff"));

            using (System.IO.FileStream fs = System.IO.File.Create(strName))
            {
                fs.Write(buf, 0, (int)RevLen);
                fs.Close();

                //pictureBox2.ImageLocation = fs.Name;
            }
            return;
        }

        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// string [0]status [1] result
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="passwod"></param>
        /// <returns></returns>
        public List<string> Login(string ip, string port, string username, string passwod)
        {
            List<string> list = new List<string>();

            if (!m_bInit)
            {
                list.Add("未初始化或者初始化失败");
                return list;
            }

            NET_DEVICEINFO deviceInfo = new NET_DEVICEINFO();
            int error = 0;
            m_nLoginID = NETClient.NETLogin(ip, (ushort)Convert.ToInt32(port), username, passwod, out deviceInfo, out error);

            if (m_nLoginID > 0)
            {
                m_nChannelNum = deviceInfo.byChanNum;

                Int32 dwRetLen = 0;
                IntPtr pDevEnable = new IntPtr();
                pDevEnable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NET_DEV_ENABLE_INFO)));
                bool bRet = NETClient.NETQuerySystemInfo(m_nLoginID, NET_SYS_ABILITY.ABILITY_DEVALL_INFO, pDevEnable,
                                                Marshal.SizeOf(typeof(NET_DEV_ENABLE_INFO)), ref dwRetLen, 1000);
                NET_DEV_ENABLE_INFO devEnable = new NET_DEV_ENABLE_INFO();
                devEnable = (NET_DEV_ENABLE_INFO)Marshal.PtrToStructure(pDevEnable, typeof(NET_DEV_ENABLE_INFO));
                m_bJSON = devEnable.IsFucEnable[(Int32)NET_FUN_SUPPORT.EN_JSON_CONFIG] > 0 ? true : false;

                if (m_bJSON == false)
                {
                    int nRetLen = 0;
                    IntPtr pStuSnapAttr = new IntPtr();
                    pStuSnapAttr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NET_SNAP_ATTR_EN)));
                    bool nRet = NETClient.NETQueryDevState(m_nLoginID, (int)NETClient.NET_DEVSTATE_SNAP
                                                       , pStuSnapAttr, Marshal.SizeOf(typeof(NET_SNAP_ATTR_EN)), ref nRetLen, 1000);
                    if (nRet == false || nRetLen != Marshal.SizeOf(typeof(NET_SNAP_ATTR_EN)))
                    {
                        //MessageBox.Show("查询设备状态失败");
                        list.Add("查询设备状态失败");
                        return list;
                    }
                    else
                    {
                        m_stuSnapAttr = (NET_SNAP_ATTR_EN)Marshal.PtrToStructure(pStuSnapAttr, typeof(NET_SNAP_ATTR_EN));
                    }
                }

                IntPtr pSnapCfg = new IntPtr();
                pSnapCfg = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NETDEV_SNAP_CFG)) * 32);
                UInt32 dwRetConfig = 0;
                bRet = NETClient.NETGetDevConfig(m_nLoginID, CONFIG_COMMAND.NET_DEV_SNAP_CFG, -1, pSnapCfg, (UInt32)Marshal.SizeOf(typeof(NETDEV_SNAP_CFG)) * 32, ref dwRetConfig, 1000);

                for (int i = 0; i < 32; ++i)
                {
                    m_stuSnapCfg[i] = (NETDEV_SNAP_CFG)Marshal.PtrToStructure((IntPtr)((UInt32)pSnapCfg + i * Marshal.SizeOf(typeof(NETDEV_SNAP_CFG)))
                                                               , typeof(NETDEV_SNAP_CFG));
                }


            }
            list.Add("登陆成功");
            return list;

        }
        public string GetImage()
        {
            SNAP_PARAMS snapparams = new SNAP_PARAMS();
            snapparams.Channel = 0;
            snapparams.mode = 0;
            snapparams.CmdSerial = 1;
            bool bRet = NETClient.NETSnapPicture(m_nLoginID, snapparams);
            if (bRet)
            {
                return "抓拍成功";
            }
            else
            {
                return "抓拍失败";
            }
        }



        public string ConvertTo(string number)
        {
            byte dwStep = Convert.ToByte(number);
            PTZ_CONTROL ptz_control = PTZ_CONTROL.PTZ_POINT_MOVE_CONTROL;
            return PtzControlEx(ptz_control, dwStep, false);
        }

        private string PtzControlEx(PTZ_CONTROL ptz_control, byte dwStep, bool stop)
        {
            if (0 != m_nLoginID)
            {
                bool bRet = NETClient.NETPTZControl(m_nLoginID, 0, ptz_control, dwStep, stop);
                if (bRet)
                {
                    return "预置位操作成功";
                }
                else
                {
                    return "预置位操作失败";
                }
            }
            return "NULL";
        }
    }
}
