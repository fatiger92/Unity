using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCamera : MonoBehaviour
{   
    Camera Camera;
    Bounds bounds;
    Vector2 deltaV;
    
    //float currentTime;
    bool touched;
    
    Vector2 _prevPosition;
    Transform _transform;

    Vector2 _topPos;
    Vector2 _bottomPos;
    
    void Initialize()
    {
        Camera = Camera.main;
    }

    public void MapSizeCheck(Mine[] mines)
    {
        SetMapsBounds(mines);
    }

    void SetMapsBounds(Mine[] mines)
    {
        if (mines.Length <= 0)
            return;

        var spriteBoundX = 0f;
        var spriteBoundY = 0f;
        var modifiedBoundY = 0f;
        var cnt = 0;
        
        spriteBoundX = mines[0].MapBoundX;
        spriteBoundY = mines[0].MapBoundY;
        
        Debug.Log($"spriteBoundX :: {spriteBoundX} // spriteBoundY :: {spriteBoundY}");
        
        for (var i = 0; i < mines.Length; i++)
        {
            if (mines[i].gameObject.activeSelf)
            {
                ++cnt;
            }
        }

        modifiedBoundY = mines.Length != cnt
            ? spriteBoundY * cnt + mines[cnt - 1].ExtendMapboundsY
            : spriteBoundY * cnt;

        bounds.center = modifiedBoundY == spriteBoundY ? 
            bounds.center = Vector3.zero : 
            bounds.center = new Vector2(0f, spriteBoundY - modifiedBoundY);

        bounds.extents = new Vector2(spriteBoundX, modifiedBoundY);

        _topPos = new Vector2(transform.position.x, bounds.max.y - Camera.orthographicSize);
        
        
        _bottomPos = new Vector2(transform.position.x,Camera.orthographicSize + bounds.min.y);
        
        Debug.Log($"bounds.min.x :: {bounds.min.x}, bounds.max.x :: {bounds.max.x}");
        Debug.Log($"bounds.min.y :: {bounds.min.y}, bounds.max.y :: {bounds.max.y}");
    }
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            deltaV = Vector2.zero;
            _prevPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 curMousePos = Input.mousePosition;
            
            MoveCamera(_prevPosition, curMousePos);
            deltaV = _prevPosition - curMousePos;
            
            _prevPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 속도가 필요하다면..
        }
        else
        {
            Debug.Log("else");
            deltaV -= deltaV * Time.deltaTime * 10;
            transform.Translate(deltaV.x / 30, deltaV.y / 30, 0);
        }
    }

    void MoveCamera(Vector2 prevPos, Vector2 curPos)
    {
        if (EventSystem.current.IsPointerOverGameObject(-1))
            return;
        
        SetPosition(
            transform.localPosition +
            (Camera.ScreenToWorldPoint(prevPos) - Camera.ScreenToWorldPoint(curPos)));
    }
    
    void SetPosition(Vector2 position)
    {
        var validatedPosition = ApplyBounds(position);
        _transform = transform;
        _transform.position = new Vector3(validatedPosition.x, validatedPosition.y, _transform.position.z);
    }

    Vector2 ApplyBounds(Vector2 position)
    {
        var cameraHeight = Camera.orthographicSize * 2f;
        
        // (float)Screen.width / Screen.height = 비율
        var cameraWidth = ((float)Screen.width / Screen.height) * cameraHeight; 
        
        Debug.Log($"Screen.width :: {Screen.width}, Screen.height :: {Screen.height}, cameraHeight :: {cameraHeight} = cameraWidth:: {cameraWidth}");
        Debug.Log($"Screen.width / Screen.height :: {(float)Screen.width / Screen.height}");
        
        // 양 옆 바운드 넘어 갔을 시 고정
        position.x = Mathf.Max(position.x, bounds.min.x + cameraWidth / 2f);
        position.x = Mathf.Min(position.x, bounds.max.x - cameraWidth / 2f);
        
        // 아래 위 바운드 넘어 갔을 시 고정
        position.y = Mathf.Max(position.y, bounds.min.y + cameraHeight / 2f);
        position.y = Mathf.Min(position.y, bounds.max.y - cameraHeight / 2f);
        
        return position;
    }

    public void MoveTopCam()
    {
        // 두트윈으로 해결
        transform.position = _topPos;
        Debug.Log(_topPos);
    }

    public void MoveBottomCam()
    {
        transform.Translate(_bottomPos);
    }

    // Unity Method;
    
    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        HandleMouseInput();
    }
    
    void LateUpdate()
    {
        SetPosition(transform.position);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
