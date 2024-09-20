using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    [SerializeField] public float activeMoveSpeed = 1f;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float dashMoveSpeed = 2f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float hitRecoveryDuration = 0.5f;
    [SerializeField] private GameObject spritePlayer;
    //[SerializeField] private GameObject spriteHands1;
    [SerializeField] private GameObject canvasDeath;

    private bool switchOrientation;
    public static event Action OnPlayerDamaged;

    //public Animator playerAnimator;
    public bool isDashing;

    [SerializeField] private Vector2 movement;
    [SerializeField] InputAction rollAction;
    private PlayerControls playerControls;

    private Rigidbody2D rb;

    //Player pos stuff
    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    //Player pos stuff
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Update()
    {
        if (/*!inDialogue() && */GameManager.instance.playerCanMove)
        {
            if (isDashing)
            {
                return;
            }

            PlayerInput();
            if (rollAction.WasPerformedThisFrame() && movement != Vector2.zero)
            {
                StartCoroutine(Roll());
            }
            if (GameManager.instance.playerIsHit)
            {
                //StartCoroutine(HitRecovery());
            }
            ChangeOrientation(movement.x);
        }
        if (GameManager.instance.playerHealth <= 0 || GameManager.instance.playerDied)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (/*!inDialogue() && */GameManager.instance.playerCanMove)
        {
            if (isDashing)
            {
                return;
            }
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        rollAction = playerControls.Actions.Roll;
    }
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (activeMoveSpeed * Time.fixedDeltaTime));
        if (movement != Vector2.zero)
        {
           // playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
           // playerAnimator.SetBool("IsRunning", false);
        }
    }

    private IEnumerator Roll()
    {

        GameManager.instance.playerCanAction = false;
        //playerAnimator.SetBool("IsRolling", true);

        isDashing = true;
        GameManager.instance.playerInvincibility = true;
        rb.velocity = new Vector2(movement.x * dashMoveSpeed, movement.y * dashMoveSpeed);

        System.Random random = new System.Random();
        int randomInt = random.Next(0, 2);
        switch (randomInt)
        {
            case 0:
               // AudioManager.instance.PlaySfx3("PlayerRoll1");
                break;
            case 1:
                //AudioManager.instance.PlaySfx3("PlayerRoll2");
                break;
        }
        yield return new WaitForSeconds(dashDuration);
        GameManager.instance.playerInvincibility = false;
        GameManager.instance.playerCanAction = true;
        isDashing = false;
        //playerAnimator.SetBool("IsRolling", false);

    }
    //private IEnumerator HitRecovery()
    //{
    //    System.Random random = new System.Random();
    //    int randomInt = random.Next(0, 2);
    //    print(randomInt);
    //    playerAnimator.SetBool("IsInvincible", true);
    //    GameManager.instance.playerIsHit = false;
    //    GameManager.instance.playerInvincibility = true;
    //    GameManager.instance.playerHealth = GameManager.instance.playerHealth - GameManager.instance.playerDamageReceived;
    //    CameraShake.Instance.shakeCamera(5f, .2f);
    //    switch (randomInt)
    //    {
    //        case 0:
    //            AudioManager.instance.PlayFootSteps("PlayerHit1");
    //            break;
    //        case 1:
    //            AudioManager.instance.PlayFootSteps("PlayerHit2");
    //            break;
    //    }

    //    OnPlayerDamaged?.Invoke();
    //    yield return new WaitForSeconds(hitRecoveryDuration);
    //    GameManager.instance.playerInvincibility = false;
    //    playerAnimator.SetBool("IsInvincible", false);
    //}
    void ChangeOrientation(float inputMovement)
    {
        if ((switchOrientation == true && inputMovement > 0) || (switchOrientation == false && inputMovement < 0))
        {
            switchOrientation = !switchOrientation;
            spritePlayer.transform.localScale = new Vector2(-spritePlayer.transform.localScale.x, spritePlayer.transform.localScale.y);
            //spriteHands1.transform.localScale = new Vector2(-spriteHands1.transform.localScale.x, spriteHands1.transform.localScale.y);
        }
    }
    private void Die()
    {
        GameManager.instance.playerCanMove = false;
        GameManager.instance.playerCanAction = false;
        spritePlayer.SetActive(false);
        spritePlayer.SetActive(false);
        canvasDeath.SetActive(true);
        GameManager.instance.playerDied = true;
        //AudioManager.instance.musicSource.Stop();
        //AudioManager.instance.PlayFootSteps("PlayerDeath");
        //AudioManager.instance.PlayFootSteps("PlayerDeathJingle");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }

}
