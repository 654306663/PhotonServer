using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Net
{
    public class SyncTransformRequest : Singleton<SyncTransformRequest>
    {

        //发起位置信息请求
        public void SendSyncPositionRequest(Vector3 pos)
        {
            ProtoData.SyncPositionC2S syncPositionC2S = new ProtoData.SyncPositionC2S();
            syncPositionC2S.x = pos.x;
            syncPositionC2S.y = pos.y;
            syncPositionC2S.z = pos.z;
            byte[] bytes = BinSerializer.Serialize(syncPositionC2S);

            //把位置信息x,y,z传递给服务器端
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add(1, bytes);

            PhotonEngine.Peer.OpCustom((byte)OperationCode.SyncPosition, data, true);//把Player位置传递给服务器
        }
    }
}

