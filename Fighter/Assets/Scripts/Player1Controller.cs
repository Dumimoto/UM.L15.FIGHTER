using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{
    private Animator animator;
    private float rotationSpeed = 30f;
    private Vector3 inputVector;
    private Vector3 targetDirection;

    private float health;
    private static float damage;

    public float PlayerHealth {get { return health;} set {health = value;}}
    public float PlayerDamage {get { return damage;} set {damage = value;}}

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
        float horizontal = Input.GetAxisRaw("Player1Horizontal");
        float vertical = Input.GetAxisRaw("Player1Vertical");
        inputVector = new Vector3(horizontal, 0, vertical);

        if (vertical != 0 || horizontal != 0)
        {
            animator.SetBool("Moving", true);
        }
        else{
            animator.SetBool("Moving", false);
        }

        if (Input.GetButtonDown("Player1AttackBtn"))
        {
            animator.SetTrigger("Attack1Trigger");
            StartCoroutine(AnimationPause(1.2f));
        }

    }
    IEnumerator AnimationPause(float pauseTime){
        yield return new WaitForSeconds(pauseTime);
    }
    private void RotatePlayer(){
        if (inputVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
        }
    }
}
