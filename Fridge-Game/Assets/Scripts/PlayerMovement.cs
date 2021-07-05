using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;


    Vector3 startPostion = new Vector3(1, 0.71f, 1);
    Vector3 endPosition = new Vector3(1, 1f, 1);
    [SerializeField] Transform orientation;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject body;

    [Header("Player Movement Settings")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Player Bind Keys")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode crouch = KeyCode.LeftControl;

    [Header("Player Jump Force")]
    [SerializeField] float jumpForce = 5f;

    float movementMultiplier = 10f;
    float groundDrag = 6f;
    float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Direction")]
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    bool nowCrouch = false;
    float groundDistance = 0.4f;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    IEnumerator SlowScaleDown(float startPosition, float endPosition)
    {
        for (float i = startPosition; i < endPosition; i += .1f)
        {
            body.transform.localScale = new Vector3(1, i, 1);
            camera.transform.localPosition = new Vector3(0, i, -0.023f);
            yield return new WaitForSeconds(.03f);
        }
    }

    IEnumerator SlowScaleUp(float startPosition, float endPosition)
    {
        for (float i = startPosition; i > endPosition; i -= .1f)
        {
            body.transform.localScale = new Vector3(1, i, 1);
            camera.transform.localPosition = new Vector3(0, i, -0.023f);
            yield return new WaitForSeconds(.03f);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight /2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        MyInput();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(crouch))
        {
            if (!nowCrouch)
            {
                DownCrouch();
                nowCrouch = true;
            }
            else if (nowCrouch)
            {
                UpCrouch();
                nowCrouch = false;
            }
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void DownCrouch()
    {
        StartCoroutine(SlowScaleUp(1.47f, 0.75f));
        
        //body.transform.localScale = new Vector3(1, 0.71f, 1);
    }

    void UpCrouch()
    {
        StartCoroutine(SlowScaleDown(0.75f, 1.47f));
        //body.transform.localScale = Vector3.Lerp(startPostion, endPosition, 1000);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}
