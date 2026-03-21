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
    [SerializeField] private float rotationSpeed;
    
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
        ProcessInput();

        KeepPlayerOnScreen();

        RotateToFaceVelocity();
    }

    void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }
        
        playerRigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);
        playerRigidbody.linearVelocity = Vector3.ClampMagnitude(playerRigidbody.linearVelocity, maxVelocity);
    }

    private void ProcessInput()
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

    private void KeepPlayerOnScreen()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x > 1f)
        {
            viewportPosition.x = 0f + 0.1f;
        }
        else if (viewportPosition.x < 0f)
        {
            viewportPosition.x = 1f - 0.1f;
        }

        if (viewportPosition.y > 1f)
        {
            viewportPosition.y = 0f + 0.1f;
        }
        else if (viewportPosition.y < 0f)
        {
            viewportPosition.y = 1f - 0.1f;
        }

        Vector3 wrappedWorldPosition = mainCamera.ViewportToWorldPoint(
            new Vector3(viewportPosition.x, viewportPosition.y, viewportPosition.z)
        );

        transform.position = new Vector3(
            wrappedWorldPosition.x,
            wrappedWorldPosition.y,
            transform.position.z
        );
    }

    private void RotateToFaceVelocity()
    {
        if (playerRigidbody.linearVelocity == Vector3.zero) { return; }

        Quaternion targetRotation = Quaternion.LookRotation(playerRigidbody.linearVelocity, Vector3.back);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
