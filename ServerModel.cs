using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager
{
    public class Server
    {
        public string Name { get; set; }
        public string Map { get; set; }
        public string ImageURL { get; set; }
        public bool InLobby { get; set; }
        public string IP { get; set; }
        public ushort Port { get; set; }
    }
}
