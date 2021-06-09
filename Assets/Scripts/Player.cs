using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Controls.IPlayerActions
{
    CharacterController _controller;
    [Header("Controller Settings")]
    [SerializeField]
    float _speed = 5.0f;
    [SerializeField]
    float _jumpHeight = 15.0f;
    [SerializeField]
    float _gravity = 1.0f;
    Vector3 _direction, _velocity;
    [Header("Camera Settings")]
    [SerializeField]
    float _hLookSpeed = 20f;
    [SerializeField]
    float _vLookSpeed = 20f;
    float _yVelocity;
    Transform _camera;
    float _mouseX;
    float _mouseY;
    [Header("Weapon Settings")]
    [SerializeField]
    float _fireCooldown;
    float _canfire = -1;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
            throw new UnityException("Character Controller is NULL");
        _camera = Camera.main.transform;
        if (_camera == null)
            throw new UnityException("Main Camera is NULL");

        //lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CalculateMovement();
        CalculateLookMovement();
        Debug.DrawRay(_camera.position, _camera.TransformDirection(Vector3.forward) * 1000, Color.green);
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded == true)
            _velocity = _direction * _speed;
        else
            _yVelocity -= _gravity;

        _velocity.y = _yVelocity;
        Vector3 localVelocity = transform.TransformDirection(_velocity);
        _controller.Move(localVelocity * Time.deltaTime);
    }

    void CalculateLookMovement()
    {
        float hLook = _hLookSpeed * _mouseX * Time.deltaTime;
        float vLook = _vLookSpeed * _mouseY * Time.deltaTime;

        Vector3 cameraRotation = _camera.rotation.eulerAngles;
        Vector3 playerRotation = transform.rotation.eulerAngles;

        cameraRotation.x -= vLook;
        playerRotation.y += hLook;

        _camera.rotation = Quaternion.Euler(Mathf.Clamp(cameraRotation.x, 0, 25), cameraRotation.y, cameraRotation.z);
        transform.rotation = Quaternion.Euler(playerRotation);


    }

    public void OnFire(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Time.time > _canfire && Physics.Raycast(_camera.position, _camera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            _canfire = Time.time + _fireCooldown;
            Debug.Log("Hit " + hit.transform.name);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _direction = new Vector3(input.x, 0, input.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true && _controller.isGrounded == true)
            _yVelocity = _jumpHeight;
    }

    public void OnLookX(InputAction.CallbackContext context)
    {
        _mouseX = context.ReadValue<float>();
    }

    public void OnLookY(InputAction.CallbackContext context)
    {
        _mouseY = context.ReadValue<float>();
    }

    public void OnDisableLook(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
