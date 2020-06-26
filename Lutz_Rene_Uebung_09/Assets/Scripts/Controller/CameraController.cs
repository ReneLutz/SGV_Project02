using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Controller _player;

    [SerializeField] private float _minZoomDistance;
    [SerializeField] private float _maxZoomDistance;

    [SerializeField] private float _horizontalRotationSpeed;    // Speed for rotating camera left / right
    [SerializeField] private float _verticalRotationSpeed;      // Speed for rotating camera up / down

    [SerializeField] private float _cameraThresholdX;
    [SerializeField] private float _cameraThresholdY;

    [SerializeField] private Vector3 _headOffset;

    private Vector3 _cameraOffset;
    private Transform _target;
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
        _target = _player.transform;

        _cameraOffset = _camera.transform.position - _target.position;

        // Because the camera looks at the player's feet if target.position is used only, I added an offset for the players head. 
        _camera.transform.LookAt(_target.position + _headOffset);
    }

    private void Update()
    {
        Zoom();
        Rotate();
        Move();

        FindNextPlayerPosition();
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
            if (mouseX < -_cameraThresholdX || mouseX > _cameraThresholdX)
            {
                Vector3 rotateH = new Vector3(0, mouseX, 0) * _horizontalRotationSpeed * Time.deltaTime;
                _cameraOffset = Quaternion.Euler(rotateH) * _cameraOffset;
                _camera.transform.Rotate(rotateH, Space.World);
            }

            if (mouseY < -_cameraThresholdY || mouseY > _cameraThresholdY)
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

    private void FindNextPlayerPosition()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Damagable damagable = hit.transform.GetComponent<Damagable>();

                if (damagable != null)
                {
                    _player.Attack(damagable);
                }
                else
                {
                    _player.Move(hit.point);
                }
            }
        }
    }
}
