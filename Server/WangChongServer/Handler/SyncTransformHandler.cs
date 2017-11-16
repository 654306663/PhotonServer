using MyGameServer.Tools;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            float x = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, 1);
            float y = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, 2);
            float z = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, 3);

            peer.x = x;
            peer.y = y;
            peer.z = z;
            MyGameServer.log.Info(x + "--" + y + "--" + z);//输出测试
        }
    }
}
