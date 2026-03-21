using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch; 

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    
    private Rigidbody playerRigidbody;
    private Camera mainCamera;
    
    private Vector3 movementDirection;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
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
            
            movementDirection = worldPosition - transform.position;
            movementDirection.z = 0f;
            movementDirection.Normalize();

            //Debug.Log($"[Touch Test] Screen Pos: {screenPos} | World Pos: {worldPosition}");
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }
        
        playerRigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);
        playerRigidbody.linearVelocity = Vector3.ClampMagnitude(playerRigidbody.linearVelocity, maxVelocity);
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
