using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] private float mouseSens;
    [SerializeField] private Transform cameraTransform;

    private float xRotation;
    private float yRotation;

    [SerializeField] private int anchorLayerMask = 6;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;

        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    public void SetAnchor(GameObject gameObject)
    {
        anchor = gameObject;
    }

    private void Update()
    {
        CheckForCursorClick();
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        if (!Input.GetKey(KeyCode.Mouse1)) { return; }

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

            SetAnchor(hit.transform.gameObject);
            Debug.Log(hit.transform.name);

            transform.position = anchor.transform.position;
        }
    }
}
