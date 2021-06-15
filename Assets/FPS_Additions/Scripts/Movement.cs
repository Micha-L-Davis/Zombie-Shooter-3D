using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Space]
    [Header("Import Cinemachine First to work!", order = 0)]
    [SerializeField]
    [Header("Place GO with Animator Here", order = 1)]
    private GameObject Animator;
    [SerializeField]
    [Header("Place scene main camera here", order = 2)]
    private Camera _cam;
    [SerializeField]
    [Header("How fast can the controller walk?", order = 3)]
    private float _walkSpeed; //how fast the character is walking
    [SerializeField]
    [Header("Place your Rig GO Here", order = 4)]
    private Rig _rig;
    [SerializeField]
    [Header("Place your Pistol Location Here", order = 5)]
    private GameObject _PistolLocation;
    [SerializeField]
    [Header("Place your Pistol Height GO Here", order = 6)]
    private GameObject _PistolHeight;
    [SerializeField]
    [Header("Place your weapon GO Here", order = 7)]
    private GameObject _weapon;
    [SerializeField]
    [Header("Place your Spine IK Here", order = 8)]
    private GameObject _spine;
    [Header("Are you running?", order = 9)]
    public bool _areAiming;
    public bool _areDead;

    private Animator _animator;
    private CharacterController _controller; //reference variable to the character controller component
    private Vector3 _currentRotation;
    private bool _lookWhereAiming;
    [SerializeField]
    private Image _crosshair;
    


    // Start is called before the first frame update
    void Start()
    {
        _animator = Animator.GetComponent<Animator>();
        _controller = GetComponent<CharacterController>(); //assign the reference variable to the component
        _rig.weight = 0;
        _weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        float h = Input.GetAxis("Horizontal"); //horizontal inputs (a, d, leftarrow, rightarrow)
        float v = Input.GetAxis("Vertical"); //veritical inputs (w, s, uparrow, downarrow)

        Move(h, v);

        Vector3 direction = new Vector3(h, 0, v); //direction to move
        direction.Normalize();

        Vector3 velocity = direction * _walkSpeed; //velocity is the direction and speed we travel

        if (direction.magnitude > 0 && _lookWhereAiming == false)
        {
            _currentRotation =  transform.eulerAngles;           
            _currentRotation.y = _cam.transform.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_currentRotation), Time.deltaTime);
        }
        
        if (_lookWhereAiming == true)
        {
            _currentRotation = transform.eulerAngles;
            _currentRotation.y = _cam.transform.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_currentRotation), Time.deltaTime * 8.0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _areAiming = !_areAiming;
            Aiming();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetBool("Running", true);
            _walkSpeed = 4f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _animator.SetBool("Running", false);
            _walkSpeed = 1.9f;
        }

        if (_areDead == true)
        {
            _animator.SetBool("Aim", false);
            _lookWhereAiming = false;
            _rig.weight = 0;
            _weapon.SetActive(false);
            _animator.SetBool("Death", true);
        }

        velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime); //move the controller x meters per second

 
        //Move arm up and down based on camera angle
        float _cameraAngleForArm = _cam.transform.eulerAngles.x;
        _cameraAngleForArm = ((_cameraAngleForArm > 180) ? _cameraAngleForArm - 360 : _cameraAngleForArm);//angle from camera
        _spine.transform.eulerAngles = new Vector3(6 + _cameraAngleForArm, _spine.transform.eulerAngles.y, _spine.transform.eulerAngles.z);//move spine with camera aim
    }

    private void Move(float h, float v)
    {
        _animator.SetFloat("VelX", h, 1f, Time.smoothDeltaTime * 10f);
        _animator.SetFloat("VelY", v, 1f, Time.smoothDeltaTime * 10f);
    }

   
    private void Aiming()
    {
        if (_areAiming == true)
        {
            _animator.SetBool("Aim", true);
            _lookWhereAiming = true;
            _rig.weight = 1;
            _weapon.SetActive(true);

        }

        else if (_areAiming == false)
        {
            _animator.SetBool("Aim", false);
            _lookWhereAiming = false;
            _rig.weight = 0;
            _weapon.SetActive(false);
        }
    }
}
