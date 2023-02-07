using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    //jump
    //public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 currentVelocity;
    bool isGrounded;
    //float normalHeight = 0f;
    float sprintSpeed = 0f;


    private void Start()
    {
        sprintSpeed = speed * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // check om vi er på grund
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && currentVelocity.y < 0)
        {
            currentVelocity.y = -2f;
        }

        // movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        ////jump
        //if(Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    currentVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        //}

        // Gravity
        currentVelocity.y += gravity * Time.deltaTime;
        controller.Move(currentVelocity * Time.deltaTime);

        //// crouch
        //if(Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    controller.height = 0f;
        //    speed = 9f;
        //}

        //if (Input.GetKeyUp(KeyCode.LeftControl))
        //{
        //    controller.height = normalHeight;
        //    speed = 12f;
        //}

        // sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12f;
        }
    }
}
