using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    private Camera _camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //_camera.transform.LookAt(target);
        Vector3 desiredPosition = target.position - offset;
        Vector3 smoothedPosition = Vector3.Lerp(_camera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        _camera.transform.position = smoothedPosition;
    }
}
