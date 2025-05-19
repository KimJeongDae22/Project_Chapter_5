using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody _rigid;

    [Header("무브")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    private Vector2 curMovementInput;
    [SerializeField] private LayerMask groundLayerMask;
 
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
        Debug.DrawRay(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down * 0.1f, Color.blue);
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
        camCurXRotate += mouseDelta.y * lookSensitivity;
        camCurXRotate = Mathf.Clamp(camCurXRotate, minXLock, maxXLock);
        contain_Camera.localEulerAngles = new Vector3(-camCurXRotate, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                Debug.Log("땅 인식 완료");
                return true;
            }

        }
        Debug.Log("땅 인식 실패");
        return false;
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
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigid.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            Debug.Log("점프");
        }
    }
}
