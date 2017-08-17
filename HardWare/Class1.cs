using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardWare
{
    public interface IDataChannel
    {
        void Send(byte[] b);
        byte[] Recive();
    }

    public interface IControl
    {
        void Init();
        void Dispose();
    }



}
