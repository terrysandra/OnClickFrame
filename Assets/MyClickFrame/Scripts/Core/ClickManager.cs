using System.Collections.Generic;
using UnityEngine;
public enum InteractionEnum
{
    Click,
    Take
}

public class ClickManager
{
    private ClickManager() { }
    private static ClickManager instance;
    public static ClickManager Instance
    {
        get
        {
            if (instance == null) instance = new ClickManager();
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    #region 交互物体的操作
    /// <summary>
    /// 跟随鼠标的物体
    /// </summary>
    private BaseOnClick followMouseObj;
    public BaseOnClick FollowMouseObj
    {
        get { return followMouseObj; }
        set { followMouseObj = value; }
    }

    /// <summary>
    /// 交互物体的字典
    /// </summary>
    public Dictionary<string, BaseOnClick> dicInteraction = new Dictionary<string, BaseOnClick>();

    /// <summary>
    /// 当前交互的物体
    /// </summary>
    private List<string> currentNameList = new List<string>();

    /// <summary>
    /// 设置一个交互是否开启
    /// </summary>
    /// <param name="_name">需要交互的物体名字</param>
    /// <param name="value">不需要交互的物体名字</param>
    public void SetNameList(string _name, bool value)
    {
        if (value)
        {
            if (currentNameList.Contains(_name))
            {
                Debug.LogError("重复错误，无法添加:" + _name);
                return;
            }
            currentNameList.Add(_name);
        }
        else
        {
            if (!currentNameList.Contains(_name))
            {
                Debug.LogError("没有字符，无法删除:" + _name);
                return;
            }
            currentNameList.Remove(_name);
        }
    }

    /// <summary>
    /// 设置关闭一个交互，并且设置开启一个交互
    /// </summary>
    /// <param name="closeName">不需要交互的物体名字</param>
    /// <param name="openName">需要交互的物体名字</param>
    public void SetNameList(string closeName, string openName)
    {
        if (!currentNameList.Contains(closeName))
        {
            Debug.LogError("没有字符，无法删除:" + closeName);
            return;
        }
        currentNameList.Remove(closeName);
        if (currentNameList.Contains(openName))
        {
            Debug.LogError("重复错误，无法添加:" + openName);
            return;
        }
        currentNameList.Add(openName);
    }

    /// <summary>
    /// 设置一些物体的交互名字
    /// </summary>
    /// <param name="names"></param>
    public void SetNameList(List<string> names)
    {
        foreach (var item in names)
        {
            if (currentNameList.Contains(item))
            {
                Debug.LogError("重复错误，无法添加:" + item);
                return;
            }
            currentNameList.Add(item);
        }
    }

    /// <summary>
    /// 检查当前操作是否有这个交互物体
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool CheckNameList(string value)
    {
        return currentNameList.Contains(value);
    }

    /// <summary>
    /// 检查当前交互物体的个的个数是否为0，如果为0，说明都点击玩了
    /// </summary>
    /// <returns></returns>
    public bool CheckNameListCount()
    {
        if (currentNameList.Count == 0) return true;
        else return false;
    }

    /// <summary>
    /// 向交互字典里面增加
    /// </summary>
    /// <param name="key"></param>
    /// <param name="vaule"></param>
    public void AddInteractionDict(string key, BaseOnClick vaule)
    {
        if (dicInteraction.ContainsKey(key)) { Debug.LogError("交互字典重复，名字是：" + key); return; }
        dicInteraction.Add(key, vaule);
    }

    /// <summary>
    /// 从交互字典里面移除
    /// </summary>
    /// <param name="key"></param>
    public void RemoveInteractionDict(string key)
    {
        if (!dicInteraction.ContainsKey(key)) { Debug.LogError("交互字典没有这个Key，名字是：" + key); return; }
        dicInteraction.Remove(key);
    }

    #endregion

}
