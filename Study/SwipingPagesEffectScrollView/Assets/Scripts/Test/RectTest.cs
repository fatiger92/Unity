using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTest : MonoBehaviour
{
    public RectTransform RectTr;
    
    [ContextMenu("What is rect / xMin, xMax, yMin, yMax From Pivot")]
    public void PrintRect1()
    {
        Debug.Log($"Pivot.x :: {RectTr.pivot.x} / Pivot.y :: {RectTr.pivot.y}");
        Debug.Log($"Rect.xMin :: {RectTr.rect.xMin} / Rect.xMax :: {RectTr.rect.xMax} / Rect.yMin :: {RectTr.rect.yMin} / Rect.yMax :: {RectTr.rect.yMax}");
    }

    [ContextMenu("What is rect / rect.x , rect.y coordinate")]
    public void PrintRect2()
    {
        Debug.Log($"Rect.x :: {RectTr.rect.x}/ Rext.y :: {RectTr.rect.y}");
    }

    [ContextMenu("UI is Transform == RectTransform??")]
    public void PrintTransformAndRectTransform()
    {
        Debug.Log($"Transform.x :: {transform.position.x}, Transform.y :: {transform.position.y}");
        Debug.Log($"RectTransform.x :: {RectTr.position.x} / RectTransform.y :: {RectTr.position.y}");
    }

    [ContextMenu("RectTransform Rect width, height??")]
    public void PrintRectWidthHeightSize()
    {
        Debug.Log(
            $"RectTransform.rect.width :: {RectTr.rect.width} / RectTransform.rect.height :: {RectTr.rect.height}");
        Debug.Log($"RectTransform.rect.size :: {RectTr.rect.size}");
    }

    [ContextMenu("PrintRectTransformAnchoredPosition x, y coordinate")]
    public void PrintRectTransformAnchoredPosition()
    {
        Debug.Log(
            $"RectTransform..anchoredPosition.x :: {RectTr.anchoredPosition.x} / RectTransform.anchoredPosition.y :: {RectTr.anchoredPosition.y}");
    }
}