using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rb;

    float x = 0.0f;
    float y = 0.0f;

    private float originalX;
    private float originalY;
    private Vector3 originalPos;
    public Transform CameraOrigin;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        originalPos = transform.position;
        originalX = angles.y;
        originalY = angles.x;

        rb = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            //Debug.Log("(" + x + "," + y + ")");
            //Debug.Log("CamOrigin X Diff: " + (CameraOrigin.eulerAngles.y - x));
            if (Input.GetMouseButton(0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            }

            x = AdjustAngle(x);

            //Rotate back behind player if they start moving forward
            if (Input.GetAxis("Vertical") != 0)
            {
                x = Mathf.LerpAngle(x, CameraOrigin.eulerAngles.y, Time.deltaTime * xSpeed * distance * 0.006f);
                y = Mathf.LerpAngle(y, CameraOrigin.eulerAngles.x, Time.deltaTime * ySpeed * 0.006f);
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public static float AdjustAngle(float angle)
    {
        if (angle < 0)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return angle;
    }
}
