using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _rotationSpeed = 180.0f;

    private CharacterController _characterController;
    private Transform _cameraTransform;
    private Transform _playerTransform;

    private float _pitch = 0.0f;
    private float _yaw = 0.0f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        _playerTransform = transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        RotatePlayer();
        RotateCamera();
        UnlockCursor();
    }

    private void Move()
    {
        Vector3 movement = (_playerTransform.forward * Vertical()) + (_playerTransform.right * Horizontal());
        _characterController.SimpleMove(movement * _movementSpeed);
    }

    private void RotatePlayer()
    {
        float rotation = Horizontal() * _rotationSpeed * Time.deltaTime;
        _playerTransform.Rotate(0, rotation, 0);
    }

    private void RotateCamera()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        float mouseVertical = -Input.GetAxis("Mouse Y");

        _yaw += mouseHorizontal * _rotationSpeed * Time.deltaTime;
        _pitch += mouseVertical * _rotationSpeed * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90.0f, 90.0f);

        _playerTransform.rotation = Quaternion.Euler(0.0f, _yaw, 0.0f);
        _cameraTransform.rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);
    }

    private void UnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private float Horizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    private float Vertical()
    {
        return Input.GetAxis("Vertical");
    }
}