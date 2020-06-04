using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _minZoomDistance;
    [SerializeField] private float _maxZoomDistance;

    [SerializeField] private float _horizontalRotationSpeed;    // Speed for rotating camera left / right
    [SerializeField] private float _verticalRotationSpeed;      // Speed for rotating camera up / down

    [SerializeField] private Vector3 _headOffset;

    private Vector3 _cameraOffset;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        _cameraOffset = _camera.transform.position - _target.position;

        // Because the camera looks at the player's feet if target.position is used only, I added an offset for the players head. 
        _camera.transform.LookAt(_target.position + _headOffset);
    }

    private void Update()
    {
        Zoom();
        Rotate();
        Move();
    }

    private void Zoom()
    {
        // Added offset for players head. This lets the camera zoom on the head instead of player's feet.
        Vector3 cameraToTarget = _target.position + _headOffset - _camera.transform.position;

        // ScrollWheel UP
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (_cameraOffset.magnitude > _minZoomDistance)
            {
                // Direction of cameraToTarget is opposite of cameraOffset
                _cameraOffset += cameraToTarget.normalized;
            }
        }

        // ScrollWheel DOWN
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (_cameraOffset.magnitude < _maxZoomDistance)
            {
                // Direction of cameraToTarget is opposite of cameraOffset
                _cameraOffset -= cameraToTarget.normalized;
            }
        }
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // This changes the camera's position and rotates around world axis
            if (mouseX < -0.2 || mouseX > 0.2)
            {
                Vector3 rotateH = new Vector3(0, mouseX, 0) * _horizontalRotationSpeed * Time.deltaTime;
                _cameraOffset = Quaternion.Euler(rotateH) * _cameraOffset;
                _camera.transform.Rotate(rotateH, Space.World);
            }

            if (mouseY < -0.2 || mouseY > 0.2)
            {
                // This changes the camera's rotation relative to itself
                Vector3 rotateV = new Vector3(-mouseY, 0, 0);
                _camera.transform.Rotate(rotateV, _verticalRotationSpeed * Time.deltaTime);
            }
        }
    }

    private void Move()
    {
        _camera.transform.position = _target.position + _cameraOffset;
    }
}
