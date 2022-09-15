using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollSnappingBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollSnapping, IPointerClickHandler
{
    private Rect panelDimensions; // 패널의 Rect 값
    private RectTransform _screenContainer; 
    private bool _isVertical;

    private int _screens = 1;

    private float _scrollStartPosition;
    private float _childSize;
    private float _childPos, _maskSize;
    private Vector2 _childAnchorPoint;
    private ScrollRect _baseScrollRect;
    private Vector3 _baseLerpTarget;
    private bool _lerp;
    private bool _pointerDown;
    private bool settled = true;
    private Vector3 _startPosition = new();

    [Tooltip("현재 활성화 되어 있는 페이지")] 
    private int _currentPage;
    private int _previousPage;
    private int _halfNoVisibleItems;
    private bool _isInfinite;
    private int _infiniteWindow;
    private float _infiniteOffset;
    private int _bottomItem, _topItem;
    private bool _startEventCalled;
    private bool _endEventCalled;
    private bool _suspendEvent;
    
    // 이벤트를 넣어줌.
    [Serializable]
    public class SelectionChangeStartEvent : UnityEvent { }
    [Serializable]
    public class SelectionPageChangedEvent : UnityEvent<int> { }
    [Serializable]
    public class SelectionChangeEndEvent : UnityEvent<int> { }
    
    // SerializeField

    [Tooltip("The screen / page to start the control on\n*Note, this is a 0 indexed array")]
    [SerializeField] private int m_startingScreen = 0;
    public int StartingScreen => m_startingScreen;
    
    [Tooltip("The distance between two pages based on page height, by default pages are next to each other")]
    [Range(0, 8)]
    [SerializeField] private float m_pageStep = 0;
    public float PageStep => m_pageStep;

    [Tooltip("The gameobject that contains toggles which suggest pagination. (optional)")]
    public GameObject Pagination;

    [Tooltip("Button to go to the previous page. (optional)")]
    public GameObject PrevButton;
    
    [Tooltip("Button to go to the next page. (optional)")]
    public GameObject NextButton;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void ChangePage(int page)
    {
        throw new System.NotImplementedException();
    }

    public void SetLerp(bool value)
    {
        throw new System.NotImplementedException();
    }

    public int CurrentPage()
    {
        throw new System.NotImplementedException();
    }

    public void StartScreenChange()
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
