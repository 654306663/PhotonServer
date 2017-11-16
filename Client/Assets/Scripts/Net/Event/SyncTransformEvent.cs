using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Tools;
using System.IO;
using System.Xml.Serialization;

namespace Net
{
    public class SyncTransformEvent : EventBase
    {
        public override void AddListener()
        {
            EventMediat.AddListener(EventCode.SyncPosition, OnSyncPositionReceived);
        }

        public override void RemoveListener()
        {
            EventMediat.RemoveListener(EventCode.SyncPosition, OnSyncPositionReceived);
        }

        void OnSyncPositionReceived(EventData eventData)
        {
            string playerDataListString = (string)DictTool.GetValue<byte, object>(eventData.Parameters, 1);

            //进行反序列化接收数据
            using (StringReader reader = new StringReader(playerDataListString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
                List<PlayerData> playerDataList = (List<PlayerData>)serializer.Deserialize(reader);

                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().OnSyncPositionEvent(playerDataList);
            }
        }
    }

}
