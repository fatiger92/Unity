using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCamera : MonoBehaviour
{
    Vector2 _prevPosition;
    Transform _transform;

    public Camera Camera;
    public Bounds Bounds;
    
    Vector2 deltaV;
    float currentTime;
    bool touched;

    public GameObject LevelsMap;
    public GameObject[] Maps;
    public SpriteRenderer[] MapsRenderers;
    
    public void Initialize()
    {
        var cnt = LevelsMap.transform.childCount;
        
        Maps = new GameObject[cnt];

        for (int i = 0; i < Maps.Length; i++)
        {
            Maps[i] = LevelsMap.transform.GetChild(i).gameObject;
        }

        MapSizeCheck();
    }
    
    public void MapSizeCheck()
    {
        if (MapsRenderers.Length > 0)
            MapsRenderers = null;
        
        var cnt = LevelsMap.transform.childCount;
        
        MapsRenderers = new SpriteRenderer[cnt];
        MapsRenderers = LevelsMap.transform.GetComponentsInChildren<SpriteRenderer>();

        SetMapsBounds();
    }

    public void SetMapsBounds()
    {
        if (MapsRenderers.Length <= 0)
            return;
        
        var spriteBoundX = 0f;
        var spriteBoundY = 0f;
        var modifiedBoundY = 0f;

        spriteBoundX = MapsRenderers[0].sprite.bounds.extents.x;
        spriteBoundY = MapsRenderers[0].sprite.bounds.extents.y;

        modifiedBoundY = spriteBoundY * MapsRenderers.Length;

        Bounds.center = modifiedBoundY == spriteBoundY ? 
            Bounds.center = Vector3.zero : 
            Bounds.center = new Vector2(0f, spriteBoundY - modifiedBoundY);
        
        Bounds.extents = new Vector2(spriteBoundX, modifiedBoundY);
    }

    void Start()
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
        Gizmos.DrawWireCube(Bounds.center, Bounds.size);
    }
    
    public void HandleMouseInput()
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
        var cameraWidth = (Screen.width * 1f / Screen.height) * cameraHeight;
        
        //Debug.Log($"Screen.width :: {Screen.width}, Screen.height :: {Screen.height}, cameraHeight :: {cameraHeight} = cameraWidth:: {cameraWidth}");
        
        position.x = Mathf.Max(position.x, Bounds.min.x + cameraWidth / 2f);
        position.x = Mathf.Min(position.x, Bounds.max.x - cameraWidth / 2f);
        
        position.y = Mathf.Max(position.y, Bounds.min.y + cameraHeight / 2f);
        position.y = Mathf.Min(position.y, Bounds.max.y - cameraHeight / 2f);
        return position;
    }
}
