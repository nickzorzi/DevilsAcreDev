using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform weapon;
    public float offset;

    public Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 movePosition;

    public Transform shotPoint;
    public GameObject projectile;

    public float timeBetweenShots;
    private float nextShotTime;

    public int maxHealth = 5;
    public int currentHealth;

    public int damage = 1;

    public HealthBar healthBar;

    public Animator animator;

    public static event Action OnPlayerDeath;
    public static event Action OnLevelUp;
    public static event Action OnVictory;

    public static bool allowLevelMenu = true;
    public static bool allowVictoryMenu = true;

    private SpriteRenderer render;
    public SpriteRenderer sprite;

    [SerializeField] private AudioSource gunshotSoundEffect;
    [SerializeField] private AudioSource playerHitSoundEffect;
    [SerializeField] private AudioSource playerWalkSoundEffect;
    [SerializeField] private AudioSource playerFastWalkSoundEffect;
    [SerializeField] private AudioSource playerSlowWalkSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;

    [Header("Dash Settings")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    public bool isDashing;
    public bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {
        EnablePlayerMovement();
        
        nextShotTime = 0f; // Initialize nextShotTime

        currentHealth = maxHealth;
        healthBar.SetMaxHealth((maxHealth));

        canDash = false;

        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDashing)
        {
            return;
        }
        
        animator.SetBool("isDashing", false);
        
        // Player Movement
        ProcessInputs();

        // Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        // transform.position += playerInput.normalized * speed * Time.deltaTime;

        // float speedValue = playerInput.magnitude;
        float speedValue = moveDirection.magnitude;

        //WALK SOUND EFFECT CHECKING AND PLAYING
        if(speed == 5)
        {
            if(moveDirection.magnitude > 0)
            {
                playerWalkSoundEffect.enabled = true;
            }
            else
            {
                playerWalkSoundEffect.enabled = false;
            }
        }
        else if (speed > 5)
        {
            if(moveDirection.magnitude > 0)
            {
                playerFastWalkSoundEffect.enabled = true;
            }
            else
            {
                playerFastWalkSoundEffect.enabled = false;
            }
        }
        else
        {
            if(moveDirection.magnitude > 0)
            {
                playerSlowWalkSoundEffect.enabled = true;
            }
            else
            {
                playerSlowWalkSoundEffect.enabled = false;
            }
        }

        // Weapon Rotation
        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > nextShotTime)
            {
                gunshotSoundEffect.Play();

                nextShotTime = Time.time + timeBetweenShots;
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
            }
        }

        //  Animator
        // animator.SetFloat("Speed", Mathf.Abs(playerInput));
        animator.SetFloat("Speed", Mathf.Abs(speedValue));

        //Level Up Checker
        if (Score.scoreValue >= 150 && allowLevelMenu == true) //150 default
        {
            // Invoke the OnLevelUp event
            OnLevelUp?.Invoke();
            allowLevelMenu = false;
        }

        if (Score.scoreValue >= 1090 && allowVictoryMenu == true) //1090 default
        {
            // Invoke the Victory event
            OnVictory?.Invoke();
            allowVictoryMenu = false;
        }

        //DASH CHECKING
        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        
        Move();

        render.flipX = sprite.flipY;
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("isDashing", true);
        dashSoundEffect.Play();
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Axe" || other.tag == "Bottle")
        {
            if (isDashing == false)
            {
                playerHitSoundEffect.Play();
                TakeDamage(damage);
            }
            else if (isDashing == true)
            {
                //invulnerable
            }
        }
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            deathSoundEffect.Play();
            currentHealth = 0;
            Debug.Log("You're Dead!");
            OnPlayerDeath?.Invoke();
        }
    }

    private void DisablePlayerMovement()
    {
        Time.timeScale = 0f;
    }

    private void EnablePlayerMovement()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        Debug.Log("Enable");
        OnPlayerDeath += DisablePlayerMovement;
        OnLevelUp += DisablePlayerMovement;
        OnVictory += DisablePlayerMovement;
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        OnPlayerDeath -= DisablePlayerMovement;
        OnLevelUp -= DisablePlayerMovement;
        OnVictory -= DisablePlayerMovement;
    }
}

