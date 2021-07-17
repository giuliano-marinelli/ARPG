using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator animator;
    private PlayerMovement movement;
    private AudioManager audioManager;

    private float attackTime = 0f;
    public float attackDelay = 2f;

    private float aimTime = 0f;
    private bool aiming = false;
    public float aimDelay = 1f;

    [SerializeField]
    public Transform projectile;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !movement.falling && !movement.landing)
        {
            if (Time.time >= attackTime)
            {
                aiming = true;
                aimTime = Time.time + aimDelay;
                movement.SetSpeed(0.5f);
                animator.SetBool("Aim", true);
                audioManager.Play("BowString");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (aiming && Time.time >= aimTime && !movement.falling && !movement.landing)
            {
                Attack();
                attackTime = Time.time + attackDelay;
            }
            aiming = false;
            movement.SetSpeed(1f);
            animator.SetBool("Aim", false);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Shoot");
    }

    void Shoot()
    {
        audioManager.Play("BowShoot");
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
