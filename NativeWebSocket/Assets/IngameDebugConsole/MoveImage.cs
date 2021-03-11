using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MoveImage : MonoBehaviour,IPointerDownHandler, IDragHandler,IEndDragHandler
{
    RectTransform rect;
    Vector3 offsetPos;
    bool canDrag=false;
    private void Awake()
    {
        rect = transform as RectTransform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //拖拽移动图片
         if(canDrag)SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rect.position = globalMousePos+offsetPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canDrag = true;
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            offsetPos =  rect.position -globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canDrag = false;
        offsetPos = Vector3.zero;
    }
}

