using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Items")]
    [SerializeField] private int maxGuuNumber;
    [SerializeField] private GameObject guuPrefab;

    [Header("Grounding")]
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Aiming")]
    [SerializeField] private GameObject aimArrow;
    [SerializeField] private float arrowDistance = 1f;

    [SerializeField] private StageInfo currentStageInfo;
    private bool onJumping = false;

    private float horizontal;
    private bool isGrounded;
    private bool guuUsing = false;
    private GameObject guutemp;
    private Vector2 direction;
    public void Move(InputAction.CallbackContext context)
    {
        if(guuUsing) return;
        horizontal = context.ReadValue<Vector2>().x;
        if(horizontal != 0 && !onJumping)
        {
            AnimationManager.instance.OnWalk();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(guuUsing) return;
        Debug.Log("Jumping");
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            AnimationManager.instance.OnJump();
            onJumping = true;
        }
    }
    void Start()
    {
        if (currentStageInfo != null)
        {
            currentStageInfo.setGuuNumber();
        }
    }
    void FixedUpdate()
    {
        
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }
    public void SetCurrentStageInfo(StageInfo stageInfo)
    {
        currentStageInfo = stageInfo;
    }
    public StageInfo GetCurrentStageInfo()
    {
        return currentStageInfo;
    }
    
   

    private bool IsGrounded()
    {
        return isGrounded;
    }
    public void GuuUse()
    {
        if (guuUsing)
        {
            return;
        }
        Debug.Log("Guu Use");
        aimArrow.SetActive(true);
        guuUsing = true;
        guutemp = Instantiate(guuPrefab, transform.position+Vector3.up * 3f, Quaternion.identity);
        StartCoroutine(GuuUseCoroutine());

    }
    IEnumerator GuuUseCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        yield return new WaitUntil (() => Mouse.current.leftButton.wasReleasedThisFrame);
        if(guutemp != null)
        {
            
        guutemp.GetComponent<GuuAct>().GuuUse(15f, direction);
        }
        
        aimArrow.SetActive(false);
        guuUsing = false;
        guutemp = null;
    }
    void Update()
    {
        isGrounded = groundCheck.IsTouchingLayers(groundLayer);
        if (isGrounded)
        {
            AnimationManager.instance.OnStopJumping();
            onJumping = false;
        }
        

        if (GameManager.instance.GetCurrentGameState() != GameState.Playing)
        {
            aimArrow.SetActive(false);
            return;
        }
        //fix arrow base location later
        if (guuUsing)
        {
            if (Mouse.current == null) return;
            // mouse
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 mouseScreen = new Vector3(mousePosition.x,mousePosition.y,-Camera.main.transform.position.z);

            Vector2 worldMousePosition =Camera.main.ScreenToWorldPoint(mouseScreen);
            direction = (worldMousePosition - (Vector2)transform.position).normalized;

            aimArrow.transform.position = transform.position + (Vector3)(direction * arrowDistance);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            aimArrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Guu"))
        {
            GuuObject guu = collision.gameObject.GetComponent<GuuObject>();

            if (guu != null)
            {
                guu.UseGuu();
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed * 2f);
            }
            
        }
        if (collision.gameObject.CompareTag("GuuChunk"))
        {
            if (currentStageInfo != null)
            {
                currentStageInfo.RefillGuu();
            }
        }
    }

}
