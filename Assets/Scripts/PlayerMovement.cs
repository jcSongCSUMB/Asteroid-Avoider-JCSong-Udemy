using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch; 

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];
            
            Vector2 screenPos = touch.screenPosition;
            
            if (float.IsInfinity(screenPos.x) || float.IsInfinity(screenPos.y)) return;
            
            Vector3 screenPosWithDepth = new Vector3(screenPos.x, screenPos.y, 10f);
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosWithDepth);
            
            Debug.Log($"[Touch Test] Screen Pos: {screenPos} | World Pos: {worldPosition}");
        }
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
