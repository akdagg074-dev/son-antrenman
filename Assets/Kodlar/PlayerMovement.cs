using UnityEngine;
using UnityEngine.InputSystem;

// Karakteri WASD / yon tuslariyla hareket ettirir, Animator'a hizi gonderir.
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8.5f;          // yakinda top yokken (topa kosarken) hiz
    public float ballNearDistance = 2.5f;  // bu mesafede top varsa normal hiz
    public float turnSpeed = 720f;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 moveInput;
    private bool nearBall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        float h = 0f;
        float v = 0f;

        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) h -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) h += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) v -= 1f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) v += 1f;

        moveInput = new Vector3(h, 0f, v).normalized;

        // Speed > 0.1 ise Walk, degilse Idle oynar
        if (animator != null)
        {
            animator.SetFloat("Speed", moveInput.magnitude);
            // kosarken animasyon da hizlansin
            bool running = moveInput != Vector3.zero && !nearBall;
            animator.speed = running ? runSpeed / moveSpeed : 1f;
        }
    }

    void FixedUpdate()
    {
        // Yakinda top var mi? Varsa topu kontrol edebilmek icin normal hiz, yoksa kosu hizi
        nearBall = Physics.CheckSphere(transform.position, ballNearDistance, LayerMask.GetMask("Ball"));
        float speed = nearBall ? moveSpeed : runSpeed;

        // Fizikle hareket: karakter Rigidbody oldugu icin kureleri dogal sekilde iter
        Vector3 velocity = moveInput * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        }
    }
}
