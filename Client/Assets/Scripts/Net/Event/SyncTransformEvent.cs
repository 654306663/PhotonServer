using UnityEngine;
using ExitGames.Client.Photon;
using Tools;

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
            byte[] bytes = (byte[])DictTool.GetValue<byte, object>(eventData.Parameters, 1);

            ProtoData.SyncPositionEvtS2C syncPositionEvtS2C = BinSerializer.DeSerialize<ProtoData.SyncPositionEvtS2C>(bytes);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().OnSyncPositionEvent(syncPositionEvtS2C.dataList);
        }
    }
}
