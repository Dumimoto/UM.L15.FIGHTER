using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    private Animator animator;
    private float rotationSpeed = 30f;
    private Vector3 inputVector;
    private Vector3 targetDirection;

    private float health;
    private static float damage;

    public float Player2Health { get { return health; } set { health = value; } }
    public float Player2Damage { get { return damage; } set { damage = value; } }

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        health = 500f;
        damage = 25f;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Player2Horizontal");
        float vertical = Input.GetAxisRaw("Player2Vertical");
        inputVector = new Vector3(horizontal, 0, vertical);

        animator.SetFloat("Input X", horizontal);
        animator.SetFloat("Input Y", vertical);

        if (vertical != 0 || horizontal != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (Input.GetButtonDown("Player2AttackBtn"))
        {
            animator.SetTrigger("Attack1Trigger");
            StartCoroutine(AnimationPause(1.2f));
        }
        UpdateMovement();

    }
    IEnumerator AnimationPause(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
    }
    private void RotatePlayer()
    {
        if (inputVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
        }
    }
    private void GetCameraRelativeMovement()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float horizontal = Input.GetAxisRaw("Player2Horizontal");
        float vertical = Input.GetAxisRaw("Player2Vertical");

        targetDirection = horizontal * right + -vertical * forward;
    }
    private void UpdateMovement()
    {
        RotatePlayer();
        GetCameraRelativeMovement();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player1Weapon")
        {
            Player2Health -= 1;
        }
    }

    void FootR(){}
    void FootL(){}
    void Hit(){}


}
