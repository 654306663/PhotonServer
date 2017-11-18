using MyGameServer.Tools;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    class SyncTransformHandler : IHandlerBase
    {
        public void AddListener()
        {
            HandlerMediat.AddListener(OperationCode.SyncPosition, OnSyncPositionReceived);
        }

        public void RemoveListener()
        {
            HandlerMediat.RemoveListener(OperationCode.SyncPosition, OnSyncPositionReceived);
        }

        //获取客户端位置请求的处理的代码
        public void OnSyncPositionReceived(ClientPeer peer, OperationRequest operationRequest, SendParameters sendParameters)
        {

            //接收位置并保持起来
            byte[] bytes = (byte[])DictTool.GetValue<byte, object>(operationRequest.Parameters, 1);
            ProtoData.SyncPositionC2S syncPositionC2S = BinSerializer.DeSerialize<ProtoData.SyncPositionC2S>(bytes);

            peer.x = syncPositionC2S.x;
            peer.y = syncPositionC2S.y;
            peer.z = syncPositionC2S.z;
        }
    }
}
