using UnityEngine;

public class BaseOnClick : MonoBehaviour
{
    public InteractionEnum interactionType;
    [HideInInspector]
    public bool followMouse;
    public virtual void OnClickRight()
    {
        switch (interactionType)
        {
            case InteractionEnum.Click:
                followMouse = false;
                break;
            case InteractionEnum.Take:
                TakeUp();
                break;
        }
    }
    protected virtual void Awake()
    {
        gameObject.layer = 20;
        ClickManager.Instance.AddInteractionDict(name, this);
    }
    private void OnDestroy()
    {
        ClickManager.Instance.RemoveInteractionDict(name);
    }
    public virtual void OnClickWrong() { }
    public virtual void OnDoubleClick() { }
    /// <summary>
    /// 物体跟随鼠标
    /// </summary>
    protected void FollowMouse()
    {
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.8f);
        Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);
        transform.position = CurPosition;
    }
    /// <summary>
    /// 拿起一个物体
    /// </summary>
    private void TakeUp()
    {
        followMouse = true;
        ClickManager.Instance.FollowMouseObj = this;
        GetComponent<BoxCollider>().enabled = false;
    }
    /// <summary>
    /// 放下跟随鼠标的物体
    /// </summary>
    protected void DropDown(Vector3 point,Vector3 rotate)
    {
        if (!ClickManager.Instance.FollowMouseObj) return;
        ClickManager.Instance.FollowMouseObj.GetComponent<BoxCollider>().enabled = true;
        ClickManager.Instance.FollowMouseObj.followMouse = false;//跟随鼠标的物体放下
        ClickManager.Instance.FollowMouseObj.transform.localPosition = point;
        ClickManager.Instance.FollowMouseObj.transform.localEulerAngles = rotate;
    }
    private void FixedUpdate()
    {
        if (followMouse)
            FollowMouse();
    }
}