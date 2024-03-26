
using UnityEngine;

public class lookAround : MonoBehaviour
{
    public Transform playerBody;
    [Range(10f, 500f)] public float mouseSensitivity = 100f;

    [Header("HeadBob")]
    public bool headBob = true;
    [Range(0.001f, 0.1f)] public float defaultAmount = 0.002f;
    [Range(1f, 30f)] public float defaultPower = 10f;
    [Range(10f, 100f)] public float defaultSmooth = 10f;
    [Range(0.001f, 0.1f)] public float sprintAmount = 0.002f;
    [Range(1f, 30f)] public float sprintPower = 10f;
    [Range(10f, 100f)] public float sprintSmooth = 10f;
    Vector3 startPos;



    float amount;
    float power;
    float smooth;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startPos = transform.localPosition;
    }

    void Update()
    {
        HandleMouseLook();

        if (headBob == true)
        {
            HandleHeadBob();
            StopHeadBob();
        }
        else { return; }

    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 45f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleHeadBob()
    {
        float input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (input > 0)
        {
            Vector3 bobOffset = HeadBob();
            transform.localPosition += bobOffset;
        }
    }

    Vector3 HeadBob()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            amount = sprintAmount;
            power = sprintPower;
            smooth = sprintSmooth;
        }
        else
        {
            amount = defaultAmount;
            power = defaultPower;
            smooth = defaultSmooth;
        }

        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * power) * amount * 1.4f;
        pos.x += Mathf.Cos(Time.time * power / 2f) * amount * 1.6f;
        return pos * smooth * Time.deltaTime;
    }

    void StopHeadBob()
    {
        if (transform.localPosition == startPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 1 * Time.deltaTime);
    }
}
