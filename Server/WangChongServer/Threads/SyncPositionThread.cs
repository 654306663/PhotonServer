using MyGameServer.Common;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyGameServer.Threads
{
    class SyncPositionThread
    {
        private Thread t;

        //启动线程的方法
        public void Run()
        {
            t = new Thread(UpdataPosition);//UpdataPosition表示线程要启动的方法
            t.IsBackground = true;//后台运行
            t.Start();//启动线程
        }

        private void UpdataPosition()
        {
            Thread.Sleep(5000);//开始的时候休息5秒开始同步
            while (true)//死循环
            {
                Thread.Sleep(100);//没隔0.1秒同步一次位置信息
                //进行同步
                SendPosition();

            }
        }

        //把所有客户端的位置信息发送到各个客户端
        //封装位置信息，封装到字典里,然后利用Xml序列化去发送
        private void SendPosition()
        {
            //装载PlayerData里面的信息
            List<PlayerData> playerDatraList = new List<PlayerData>();
            foreach (ClientPeer peer in MyGameServer.Instance.peerList)//遍历所有客户段
            {
                if (string.IsNullOrEmpty(peer.username) == false)//取得当前已经登陆的客户端
                {
                    PlayerData playerdata = new PlayerData();
                    playerdata.Username = peer.username;//设置playerdata里面的username
                    playerdata.x = peer.x;//设置playerdata里面的Position
                    playerdata.y = peer.y;
                    playerdata.z = peer.z;
                    playerDatraList.Add(playerdata);//把playerdata放入集合
                }
            }
            //进行Xml序列化成String
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
            serializer.Serialize(sw, playerDatraList);
            sw.Close();
            string playerDataListString = sw.ToString();


            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add(1, playerDataListString);//把所有的playerDataListString装载进字典里面
            //把Xml序列化的信息装在字典里发送给各个客户端
            foreach (ClientPeer peer in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    EventData ed = new EventData((byte)EventCode.SyncPosition);
                    ed.Parameters = data;
                    peer.SendEvent(ed, new SendParameters());

                }
            }

        }

        //关闭线程
        public void Stop()
        {
            t.Abort();//终止线程
        }
    }
}
