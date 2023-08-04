using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
//using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    private CharacterController _characterController;
    private Vector2 _inputPlayerVector;
    private Vector2 _inputCameraVector;

    [Header("Attacking")]
    public ProjectileStandard _magicProjectilePrefab;
    [SerializeField] private Transform _baitSpawnTransform;

    [Tooltip("Angle for the cone in which the bullets will be shot randomly (0 means no spread at all)")]
    public float BulletSpreadAngle = 0f;
    private bool aimInput;

    [Header("Cinemachine")]
    [SerializeField] private GameObject _cinemachine;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _aimCamera;
    [SerializeField] private GameObject _cinemachineCameraTarget;
    [SerializeField] private float cameraSpeed = 10;
    [SerializeField] private float topClamp = 70.0f;
    [SerializeField] private float bottomClamp = -30.0f;
    [SerializeField] public bool invertY = false;
    [SerializeField] public bool invertX = false;
    [SerializeField] private bool lockCameraPosition = false;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    private Vector2 screenCenter;

    [SerializeField] private LayerMask aimColliderLayerMask;

    [Header("Temporary Bait Stuff")]
    [SerializeField] private GameObject _baitPrefabOne;
    [SerializeField] private GameObject _baitPrefabTwo;
    [SerializeField] private GameObject _baitPrefabThree;
    [SerializeField] private GameObject _baitPrefabFour;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.CameraMoveEvent += OnLook;
        _inputReader.AimEvent += OnAimInitiated;
        _inputReader.AimCanceledEvent += OnAimCanceled;
        _inputReader.AttackEvent += OnStartedAttack;
        _inputReader.SpawnBaitEventOne += OnSpawnBaitOne;
        _inputReader.SpawnBaitEventTwo += OnSpawnBaitTwo;
        _inputReader.SpawnBaitEventThree += OnSpawnBaitThree;
        _inputReader.SpawnBaitEventFour += OnSpawnBaitFour;
    }
    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.CameraMoveEvent -= OnLook;
        _inputReader.AimEvent -= OnAimInitiated;
        _inputReader.AimCanceledEvent -= OnAimCanceled;
        _inputReader.AttackEvent -= OnStartedAttack;
        _inputReader.SpawnBaitEventOne -= OnSpawnBaitOne;
        _inputReader.SpawnBaitEventTwo -= OnSpawnBaitTwo;
        _inputReader.SpawnBaitEventThree -= OnSpawnBaitThree;
        _inputReader.SpawnBaitEventFour -= OnSpawnBaitFour;
    }
    // Start is called before the first frame update
    void Start()
    {
        _inputReader.EnableGameplayInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveCamera();
        PlayerMovement();
        CameraMovement();
    }
    private void PlayerMovement()
    {
        Vector3 inputDir;

        // camera rotating player
        if (_cinemachine)
        {
            inputDir = _cinemachine.transform.forward * _inputPlayerVector.y + _cinemachine.transform.right * _inputPlayerVector.x;
            inputDir.y = 0f;

            Quaternion targetRotation;

            if (inputDir != Vector3.zero && !aimInput)
            {
                targetRotation = Quaternion.LookRotation(inputDir);
                // this line gets repeated in the else if below and ive tried refactoring it to be out of the if 
                // statement but it keeps giving the Vector3.zero vector error
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else if (aimInput)
            {
                Vector3 aimInputDirection = _cinemachineCameraTarget.transform.forward;
                aimInputDirection.y = 0f;
                targetRotation = Quaternion.LookRotation(aimInputDirection);
                // this here yeah idk fuckin sue me its the only line that repeats itself in the entire script
                // really dont gotta look in to it like idk why im still typing but i just fell like youre judging me for the 
                // first comment so this is me making it extra clear that im in fact not insecure about the repitition
                // you know what from now on im going to duplicate code more often just out of spite as my final fuck you
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            //No Camera transform exists in the scene, so the input is just used absolute in world-space
            Debug.LogWarning("No gameplay camera in the scene. Movement orientation will not be correct.");
            inputDir = new Vector3(_inputPlayerVector.x, 0f, _inputPlayerVector.y);
        }

        //Fix to avoid getting a Vector3.zero vector, which would result in the player turning to x:0, z:0
        if (_inputPlayerVector.sqrMagnitude == 0f)
            inputDir = transform.forward * (inputDir.magnitude + .01f);

        // Calculate movement
        Vector3 movement = inputDir * (movementSpeed * (_characterController.isGrounded ? 1f : 0.5f));

        movement += Physics.gravity;
        movement *= Time.deltaTime;

        _characterController.Move(movement);
    }
    private void CameraMovement()
    {
        // if there is an input and camera position is not fixed
        if (_inputCameraVector.sqrMagnitude >= _threshold && !lockCameraPosition)
        {
            _cinemachineTargetYaw += _inputCameraVector.x * (invertX ? -1f : 1f) * cameraSpeed * Time.deltaTime;
            _cinemachineTargetPitch += _inputCameraVector.y * (invertY ? 1f : -1f) * cameraSpeed * Time.deltaTime;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

        // Cinemachine will follow this target
        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }
    private void Shoot()
    {
        if (!aimInput)
        {
            return; // No need to continue if there is no aim input
        }

        RaycastHit hit;
        Vector3 direction;

        if (Physics.Raycast(_cinemachine.transform.position, _cinemachine.transform.forward, out hit, Mathf.Infinity))
        {
            direction = hit.point - _baitSpawnTransform.position; // Calculate the direction based on the hit point
        }
        else
        {
            direction = (_cinemachine.transform.position + _cinemachine.transform.forward * 20f) - _baitSpawnTransform.position; // Use a point far ahead if no hit point is found
        }

        ProjectileStandard newProjectile = Instantiate(_magicProjectilePrefab, _baitSpawnTransform.position, Quaternion.LookRotation(direction));
        newProjectile.Shoot(this);
    }
    private void SpawnBaitOne()
    {
        Instantiate(_baitPrefabOne, _baitSpawnTransform.position, Quaternion.LookRotation(_baitSpawnTransform.forward, Vector3.up));
    }
    private void SpawnBaitTwo()
    {
        Instantiate(_baitPrefabTwo, _baitSpawnTransform.position, Quaternion.LookRotation(_baitSpawnTransform.forward, Vector3.up));
    }
    private void SpawnBaitThree()
    {
        Instantiate(_baitPrefabThree, _baitSpawnTransform.position, Quaternion.LookRotation(_baitSpawnTransform.forward, Vector3.up));
    }
    private void SpawnBaitFour()
    {
        Instantiate(_baitPrefabFour, _baitSpawnTransform.position, Quaternion.LookRotation(_baitSpawnTransform.forward, Vector3.up));
    }
    private void SetActiveCamera()
    {
        _aimCamera.SetActive(aimInput);
        _mainCamera.SetActive(!aimInput);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    //---- EVENT LISTENERS ----
    private void OnMove(Vector2 movement) { _inputPlayerVector = movement; }
    private void OnLook(Vector2 movement) { _inputCameraVector = movement; }
    private void OnAimInitiated() { aimInput = true; }
    private void OnAimCanceled() { aimInput = false; }
    private void OnSpawnBaitOne() { SpawnBaitOne(); }
    private void OnSpawnBaitTwo() { SpawnBaitTwo(); }
    private void OnSpawnBaitThree() { SpawnBaitThree(); }
    private void OnSpawnBaitFour() { SpawnBaitFour(); }
    private void OnStartedAttack() { Shoot(); }
}
