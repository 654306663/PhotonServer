using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerPrefab;

    private Vector3 lastPosition = Vector3.zero;
    private float moveOffset = 0.1f;

    //存储所有实例化出来的Player
    private Dictionary<string, GameObject> playerDic = new Dictionary<string, GameObject>();

    void Start()
    {
        //设置本地的Player的颜色设置成绿色
        GetComponent<Renderer>().material.color = Color.green;

        SyncPlayerRequest.Instance.SendSyncPlayerRequest();

        //参数一 方法名，参数二 从等多久后开始执行这个方法  参数三 同步的时间速率。这里一秒同步十次
        InvokeRepeating("SyncPosition", 3, 0.03f);//重复调用某个方法  
    }
    void Update()
    {
        //只有本地的Player，可以控制移动     
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 4);
    }

    //位置信息时时更新
    void SyncPosition()
    {
        //如果玩家的位置当前玩家的位置和上玩家上一个的位置距离大于0.1，就表示玩家移动了，就需要他位置的同步
        if (Vector3.Distance(transform.position, lastPosition) > moveOffset)
        {
            lastPosition = transform.position;
            Net.SyncTransformRequest.Instance.SendSyncPositionRequest(transform.position);//调用位置信息同步的请求
        }
    }

    //实例化其他客户端的角色
    public void OnSyncPlayerResponse(List<string> usernameList)
    {
        //创建其他客户端的角色
        foreach (string username in usernameList)
        {
            OnNewPlayerEvent(username);
        }
    }

    public void OnNewPlayerEvent(string username)
    {
        GameObject go = GameObject.Instantiate(playerPrefab);
        playerDic.Add(username, go);//利用集合保存所有的其他客户端
    }

    public void OnSyncPositionEvent(List<ProtoData.SyncPositionEvtS2C.PositionData> positionDataList)
    {
        foreach (ProtoData.SyncPositionEvtS2C.PositionData pd in positionDataList)//遍历所有的数据
        {
            GameObject go = DictTool.GetValue<string, GameObject>(playerDic, pd.username);//根据传递过来的Username去找到所对应的实例化出来的Player

            //如果查找到了相应的角色，就把相应的位置信息赋值给这个角色的position
            if (go != null)
            {
                go.transform.position = new Vector3() { x = pd.x, y = pd.y, z = pd.z };
            }
        }
    }
}