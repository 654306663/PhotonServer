using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Common
{
    [Serializable]
    public class PlayerData
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public string Username { get; set; }
    }
}
