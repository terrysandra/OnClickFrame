using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public enum ClickState
{
    单击,
    双击
}

//[RequireComponent(typeof(AudioSource))]
public class CheckOnClick : MonoBehaviour
{
    int layer = 1 << 20;
    float delay = 0.2f;
    float currentTime;
    bool doubleClick = false;
    public ClickState  clickState;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//如果物体是不可以拿起的，执行点击方法
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (clickState == ClickState.双击)
                CheckMouse();
            else
                OnMouseClick();
        }

    }

    void CheckMouse()
    {
        if (Time.realtimeSinceStartup - currentTime < delay)
            OnMouseDoubleClick();
        else
            StartCoroutine(Click());
        currentTime = Time.realtimeSinceStartup;

    }

    private IEnumerator Click()
    {
        yield return new WaitForSeconds(delay);
        if (Time.realtimeSinceStartup - currentTime > delay)
            OnMouseClick();
    }

    private void OnMouseClick()
    {
        RaycastHit hit;
        Ray ra = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ra, out hit, 10, layer))
        {
            if (ClickManager.Instance.CheckNameList(hit.transform.name))//检查碰到的物体 在当前列表中是否存在
            {
                OnClickRight(hit.transform.name);
            }
            else OnClickWrong(hit.transform.name);
        }
    }

    private void OnMouseDoubleClick()
    {
        RaycastHit hit;
        Ray ra = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ra, out hit, 10, layer))
        {
            OnDoubleClick(hit.transform.name);
        }
    }
    /// <summary>
    /// 操作正确执行的方法
    /// </summary>
    /// <param name="hit">被点击物体的名字</param>
    private void OnClickRight(string objName)
    {
        ClickManager.Instance.dicInteraction[objName].OnClickRight();
    }
    /// <summary>
    /// 操作错误执行的方法
    /// </summary>
    /// <param name="hit">被点击物体的名字</param>
    private void OnClickWrong(string objName)
    {
        ClickManager.Instance.dicInteraction[objName].OnClickWrong();
    }
    /// <summary>
    /// 双击执行的方法
    /// </summary>
    /// <param name="hit">被点击物体的名字</param>
    private void OnDoubleClick(string objName)
    {
        ClickManager.Instance.dicInteraction[objName].OnDoubleClick();
    }
}