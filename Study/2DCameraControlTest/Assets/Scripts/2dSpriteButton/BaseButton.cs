using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseButton : MonoBehaviour
{
    [SerializeField] UnityEvent unityEvent = new UnityEvent();
    Vector3 _localScale = Vector3.zero;

    public int eventCount = 0;
    public virtual void Awake()
    {
        UIinitial();
    }

    public virtual void UIinitial()
    {
        _localScale = transform.localScale;
    }

    public virtual void OnMouseEnter()
    {
        //Debug.Log($" {gameObject.name} :: OnMouseEnter");
        transform.localScale = _localScale + _localScale / 10f;
    }
    
    public virtual void OnMouseDown()
    {
        //Debug.Log($" {gameObject.name} :: OnMouseDown");
        transform.localScale = _localScale - _localScale / 10f;
    }
    
    public virtual void OnMouseUp()
    {
        //Debug.Log($" {gameObject.name} :: OnMouseUp");
        transform.localScale = _localScale + _localScale / 10f;
    }

    public virtual void OnMouseExit()
    {
        //Debug.Log($" {gameObject.name} :: OnMouseExit");
        transform.localScale = _localScale;
    }
    
    public virtual void OnMouseUpAsButton()
    {
        //Debug.Log($" {gameObject.name} :: Click Event");
        unityEvent?.Invoke();
    }

    public void AttachClickEvent(UnityAction action)
    {
        ++eventCount;
        unityEvent.AddListener(action);
    }

    public void DetachClickEvent(UnityAction action)
    {
        --eventCount;
        unityEvent.RemoveListener(action);
    }

    [ContextMenu("Manual Counting Event Count")]
    public void DebugCode()
    {
        Debug.Log($"unityEvent is Null? :: {unityEvent == null} :: {eventCount}");
    }
}
