using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody _rigid;

    [Header("무브")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    private Vector2 curMovementInput;

    [Header("플레이어 시점")]
    [SerializeField] private float minXLock;
    [SerializeField] private float maxXLock;
    private float camCurXRotate;
    [SerializeField] private float lookSensitivity; // 마우스 감도
    private Vector2 mouseDelta;
    public Transform contain_Camera;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        CamLook();
    }
    // Update is called once per frame
    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigid.velocity.y;

        _rigid.velocity = dir;
    }
    void CamLook()
    {
        camCurXRotate = mouseDelta.y * lookSensitivity;
        camCurXRotate = Mathf.Clamp(camCurXRotate, minXLock, maxXLock);
        contain_Camera.localEulerAngles += new Vector3(-camCurXRotate, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _rigid.AddForce(Vector2.up, ForceMode.Impulse);
        }
    }
}
