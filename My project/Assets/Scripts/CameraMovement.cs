using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] private GameObject defaultAnchor;
    [SerializeField] private Transform cameraTransform;

    [Range(1, 5)]
    [SerializeField] private float mouseSens;

    [Tooltip("Multiplicative"), Range(1, 6)]
    [SerializeField] private float zoomSpeed = 0.5f;

    private float defaultFov;

    [Tooltip("Try not too make it too high"), Range(60, 105)]
    [SerializeField] private float maxFov = 15.0f;

    [Range(30, 60)]
    [SerializeField] private float minFov = 15.0f;

    private float xRotation;
    private float yRotation;

    [SerializeField] private int anchorLayerMask = 6;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        defaultFov = camera.fieldOfView;
        transform.position = defaultAnchor.transform.position;
    }

    public void SetAnchor(GameObject gameObject)
    {
        anchor = gameObject;
    }

    private void Update()
    {
        CheckForCursorClick();
        HandleCameraRotation();
        ZoomHandling();
    }

    private void ZoomHandling()
    {
        //Not ideal, but should be good enough.
        if (camera.fieldOfView < maxFov && Input.mouseScrollDelta.y < 0 || camera.fieldOfView > minFov && Input.mouseScrollDelta.y > 0)
        {
            camera.fieldOfView += -Input.mouseScrollDelta.y * zoomSpeed;
        }
    }

    private void HandleCameraRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (!Input.GetMouseButton(1)) { return; }

        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.SetPositionAndRotation(anchor.transform.position, Quaternion.Euler(xRotation, yRotation, 0));
    }

    private void CheckForCursorClick()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }

        var Ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(Ray, out RaycastHit hit, 100.0f))
        {
            if (hit.transform.gameObject.layer != anchorLayerMask) { return; }

            if (hit.transform.gameObject == anchor)
            {
                SetAnchor(defaultAnchor);
            }
            else
            {
                SetAnchor(hit.transform.gameObject);
            }

            transform.position = anchor.transform.position;
        }
    }
}
