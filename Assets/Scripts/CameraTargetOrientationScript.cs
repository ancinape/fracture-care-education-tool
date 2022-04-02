/***************************************************************/
/********** Simple target orientation camera script. ***********/
/*** You can change parameters, such as rotation/zoom speed. ***/
/***************************************************************/

using UnityEngine;
using System.Collections;

public class CameraTargetOrientationScript : MonoBehaviour
{
    [Header("Mouse input:", order = 0)]
    [Space(-10, order = 1)]
    [Header("- Hold and drag RMB to rotate", order = 2)]
    [Space(-10, order = 3)]
    [Header("- Use mouse wheel to zoom in/out", order = 4)]
    [Space(5, order = 5)]

    public bool enableRotation = true;

    [Header("Choose target")]
    public Transform target;

    //Camera fields
    private float _smoothness = 0.5f;
    private Vector3 _cameraOffset;

    //Mouse control fields
    [Space(2)]
    [Header("Mouse Controls")]
    public float rotationSpeedMouse = 5;
    public float zoomSpeedMouse = 10;

    private float _zoomAmountMouse = 0;
    private float _maxToClampMouse = 5;

    void Start()
    {
        _cameraOffset = transform.position - target.position;
        transform.LookAt(target);
    }

    void LateUpdate()
    {

        // Rotating camera with RMB dragging on PC.
        if (enableRotation && (Input.GetMouseButton(1)))
        {

            Quaternion camAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeedMouse, Vector3.up);

            Vector3 newPos = target.position + _cameraOffset;
            _cameraOffset = camAngle * _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, _smoothness);
            transform.LookAt(target);
        }

        else
        {
        // Translating camera on PC with mouse wheel.

            _zoomAmountMouse += Input.GetAxis("Mouse ScrollWheel");
            _zoomAmountMouse = Mathf.Clamp(_zoomAmountMouse, -_maxToClampMouse, _maxToClampMouse);

            var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), _maxToClampMouse - Mathf.Abs(_zoomAmountMouse));
            transform.Translate(0, 0, translate * zoomSpeedMouse * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));

            _cameraOffset = transform.position - target.position;

        }

    }



}