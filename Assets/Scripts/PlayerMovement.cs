using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float fallingThreshold = -4f;
    public float landDelay = 1f;

    public bool falling = false;
    public bool landing = false;

    private float fullSpeed;
    private float landTime = 0f;

    private Rigidbody _rigidbody;
    private Animator animator;

    private Vector3 lookPos;

    private Transform _camera;
    private Vector3 cameraForward;
    private Vector3 move;
    private Vector3 movement;
    private Vector3 moveInput;

    private float forwardAmount;
    private float turnAmount;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _camera = Camera.main.transform;
        fullSpeed = speed;
    }

    private void Update()
    {
        CheckFalling();
        if (!falling)
        {
            Look();
        }
    }

    private void FixedUpdate()
    {
        CheckFalling();
        if (!falling)
        {
            Move();
        }
    }
    private void Look()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0;
        movement.z = Input.GetAxisRaw("Vertical");

        if (_camera != null)
        {
            cameraForward = Vector3.Scale(_camera.up, new Vector3(1, 0, 1)).normalized;
            move = movement.z * cameraForward + movement.x * _camera.right;
        }
        else
        {
            move = movement.z * Vector3.forward + movement.x * Vector3.right;
        }

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        moveInput = move;

        ConvertMoveInput();

        UpdateAnimator();

        _rigidbody.MovePosition(_rigidbody.position + movement.normalized * speed * Time.fixedDeltaTime);
        //_rigidbody.AddForce(movement * speed / Time.deltaTime);
    }

    private void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);

        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Horizontal", turnAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void CheckFalling()
    {
        if (landing && Time.time >= landTime)
        {
            landing = false;
            SetSpeed(1f);
        }

        falling = _rigidbody.velocity.y < fallingThreshold;
        animator.SetBool("Falling", falling);
    }

    private void Land()
    {
        landTime = Time.time + landDelay;
        landing = true;
        SetSpeed(0f);
    }

    public void SetSpeed(float percentage)
    {
        speed = fullSpeed * percentage;
    }
}
