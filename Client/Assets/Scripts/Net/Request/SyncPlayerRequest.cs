using UnityEngine;
using System.Collections;
using Net;

public class SyncPlayerRequest : Singleton<SyncPlayerRequest>
{
    /// <summary>
    /// 发送玩家信息
    /// </summary>
    public void SendSyncPlayerRequest()
    {
        PhotonEngine.Peer.OpCustom((byte)OperationCode.SyncPlayer, null, true);
    }
}
