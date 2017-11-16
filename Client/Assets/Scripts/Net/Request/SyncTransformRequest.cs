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
            //把位置信息x,y,z传递给服务器端
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add(1, pos.x);
            data.Add(2, pos.y);
            data.Add(3, pos.z);

            PhotonEngine.Peer.OpCustom((byte)OperationCode.SyncPosition, data, true);//把Player位置传递给服务器
        }
    }
}

