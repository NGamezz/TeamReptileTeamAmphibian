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

    //Might be usefull later, not sure yet.
    private float defaultFov;

    [Tooltip("Try not too make it too high"), Range(60, 120)]
    [SerializeField] private float maxFov = 15.0f;

    [Tooltip("At which point the room will be deselected when zooming out."), Range(70, 120)]
    [SerializeField] private float minFovToDeselectRoom = 70.0f;

    [Range(30, 60)]
    [SerializeField] private float minFov = 15.0f;

    [Tooltip("The fov it will use when you select a room"), Range(30, 60)]
    [SerializeField] private float roomSelectedFov = 40.0f;

    private float xRotation;
    private float yRotation;

    [SerializeField] private int anchorLayerMask = 6;

    private Camera camera;
    private IRoom currentRoom;

    private void Start()
    {
        camera = Camera.main;
        defaultFov = camera.fieldOfView;

        SetAnchor(defaultAnchor);
    }

    private void SetAnchor(GameObject gameObject, GameObject gameObject2 = null, bool whichOne = true)
    {
        anchor = whichOne ? gameObject : gameObject2;
        transform.position = anchor.transform.position;
    }

    private void Update()
    {
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

        if (camera.fieldOfView >= minFovToDeselectRoom)
        {
            SetAnchor(defaultAnchor);

            if (currentRoom == null) { return; }
            currentRoom.OnRoomDeselection();
            currentRoom = null;
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
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        transform.SetPositionAndRotation(anchor.transform.position, Quaternion.Euler(xRotation, yRotation, 0));
    }

    private void CheckForRoom(RaycastHit hit, bool isAlreadySelected)
    {
        if (hit.transform.TryGetComponent(out IRoom room))
        {
            if (isAlreadySelected)
            {
                room.OnRoomDeselection();
            }
            else
            {
                room.OnRoomSelection();
            }
            currentRoom = isAlreadySelected ? null : room;
        }
        else if (currentRoom != null)
        {
            currentRoom.OnRoomDeselection();
            currentRoom = null;
        }
    }

    public void CheckForCursorClick()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }

        var Ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(Ray, out RaycastHit hit, 100.0f))
        {
            if (hit.transform.gameObject.layer != anchorLayerMask) { return; }

            CheckForRoom(hit, hit.transform.gameObject == anchor);

            camera.fieldOfView = roomSelectedFov;
            SetAnchor(defaultAnchor, hit.transform.gameObject, hit.transform.gameObject == anchor);
        }
    }
}
