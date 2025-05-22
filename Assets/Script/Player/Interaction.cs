using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float checkRate = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxCheckDistance;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private GameObject curlnteractGameObject;
    [SerializeField] private Interactable curinteractable;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Debug.DrawRay(new Vector3(Screen.width / 2, Screen.height / 2), Vector3.forward * maxCheckDistance, Color.blue);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curlnteractGameObject)
                {
                    curlnteractGameObject = hit.collider.gameObject;
                    curinteractable = hit.collider.GetComponent<Interactable>();
                    SetPromptText();
                }
            }
            else
            {
                curlnteractGameObject = null;
                curinteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    public void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curinteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curinteractable != null)
        {
            curinteractable.Oninteract();
            curlnteractGameObject = null;
            curinteractable = null;
            //curlnteractGameObject.SetActive(false);
        }
    }
}
