using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public CharacterController controller;
    public Transform cam;

    
    [SerializeField] private float speed = 8f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float gravity = -9.81f;


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 movement = new Vector3(horizontal, 0f, vertical);


        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        movement = Vector3.ClampMagnitude(moveDirection, speed);


        movement.y = gravity;

        movement *= Time.deltaTime * speed;

        if (direction.magnitude < 0.1f)
        {
            movement = Vector3.zero;
            if (animator != null)
            {
                animator.SetBool("isRunning", false);
            }

        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isRunning", true);
            }

        }

        controller.Move(movement);
    }
}
