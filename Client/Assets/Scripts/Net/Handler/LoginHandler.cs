using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System;
using Tools;

namespace Net
{
    public class LoginHandler : HandlerBase
    {

        /// <summary>
        /// 注册监听事件
        /// </summary>
        public override void AddListener()
        {
            HandlerMediat.AddListener(OperationCode.Login, OnLoginReceived);
            HandlerMediat.AddListener(OperationCode.Register, OnRegisterReceived);
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public override void RemoveListener()
        {
            HandlerMediat.RemoveAllListener(OperationCode.Login);
            HandlerMediat.RemoveAllListener(OperationCode.Register);
        }

        /// <summary>
        /// 收到登录消息
        /// </summary>
        void OnLoginReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            if (returnCode == ReturnCode.Success)
            {
                Debug.LogError("用户名和密码验证成功");

                PhotonEngine.username = DictTool.GetValue<byte, object>(response.Parameters, 1) as string;
                //验证成功，跳转到下一个场景
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);

            }
            else if (returnCode == ReturnCode.Failed)
            {
                Debug.LogError("用户名或密码错误");
            }
        }

        /// <summary>
        /// 收到注册消息
        /// </summary>
        void OnRegisterReceived(OperationResponse response)
        {
            ReturnCode returnCode = (ReturnCode)response.ReturnCode;
            if (returnCode == ReturnCode.Success)
            {
                Debug.LogError("注册成功，请返回登陆");

            }
            else if (returnCode == ReturnCode.Failed)
            {
                Debug.LogError("所用的用户名已被注册，请更改用户名");
            }
        }
    }
}
