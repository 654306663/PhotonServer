using UnityEngine;
using System.Collections;
using System;
using ExitGames.Client.Photon;
using Tools;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Net
{
    public class SyncPlayerHandler : HandlerBase
    {

        public override void AddListener()
        {
            HandlerMediat.AddListener(OperationCode.SyncPlayer, OnSyncPlayerReceived);
        }

        public override void RemoveListener()
        {
            HandlerMediat.RemoveListener(OperationCode.SyncPlayer, OnSyncPlayerReceived);
        }


        void OnSyncPlayerReceived(OperationResponse response)
        {
            string usernameListString = (string)DictTool.GetValue<byte, object>(response.Parameters, 1);

            Debug.Log(usernameListString);

            //通过xml反序列化接收服务器传输过来的List数据
            using (StringReader reader = new StringReader(usernameListString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
                List<string> usernameList = (List<string>)serializer.Deserialize(reader);//表示读取字符串

                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().OnSyncPlayerResponse(usernameList);
            }
        }
    }
}
