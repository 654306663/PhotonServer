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
    public class SyncPlayerEvent : EventBase
    {
        public override void AddListener()
        {
            EventMediat.AddListener(EventCode.NewPlayer, OnSyncPlayerReceived);
        }

        public override void RemoveListener()
        {
            EventMediat.RemoveListener(EventCode.NewPlayer, OnSyncPlayerReceived);
        }

        void OnSyncPlayerReceived(EventData eventData)
        {
            string username = (string)DictTool.GetValue<byte, object>(eventData.Parameters, 1);

            Debug.Log(username);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().OnNewPlayerEvent(username);
        }
    }

}
