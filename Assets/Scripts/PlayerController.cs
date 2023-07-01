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
    [SerializeField] private GameObject _magicProjectilePrefab;
    [SerializeField] private Transform _baitSpawnTransform;
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
    [SerializeField] private GameObject _baitPrefab;


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
        _inputReader.SpawnBaitEvent += OnSpawnBait;
    }
    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.CameraMoveEvent -= OnLook;
        _inputReader.AimEvent -= OnAimInitiated;
        _inputReader.AimCanceledEvent -= OnAimCanceled;
        _inputReader.AttackEvent -= OnStartedAttack;
        _inputReader.SpawnBaitEvent -= OnSpawnBait;
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
            }
            else if(aimInput)
            {
                Vector3 aimInputDirection = _cinemachineCameraTarget.transform.forward;
                aimInputDirection.y = 0f;
                targetRotation = Quaternion.LookRotation(aimInputDirection);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(inputDir);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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
        if (aimInput)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if(Physics.Raycast(ray, out RaycastHit raycast, 200f, aimColliderLayerMask))
            {
                Vector3 aimDirection = (raycast.point - _baitSpawnTransform.position).normalized;
                Instantiate(_magicProjectilePrefab, _baitSpawnTransform.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            }
        }
    }
    private void SpawnBait()
    {
        Instantiate(_baitPrefab, _baitSpawnTransform.position, Quaternion.LookRotation(_baitSpawnTransform.forward, Vector3.up));
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
    private void OnSpawnBait() { SpawnBait(); }
    private void OnStartedAttack() { Shoot(); }
}
