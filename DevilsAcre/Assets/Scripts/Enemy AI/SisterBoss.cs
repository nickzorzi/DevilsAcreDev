using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SisterBoss : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float lineOfSight;
    public int health;
    public GameObject deathEffect;
    public int scoreValueOnDeath;


    [HideInInspector] public bool canFireRed = false;
    [HideInInspector] public bool canFireBlue = false;
    [HideInInspector] public bool canFireYellow = false;
    [HideInInspector] public bool phase2Triggered = false;
    [HideInInspector] public bool phase3Triggered = false;
    private bool canFireYellowCross = false;
    private bool canFireRedCross = false;
    private bool canFireLinePattern = false;
    
    [Space(10)]
    [Header("Yellow Cross Stats")]
    [SerializeField] private GameObject yellowCross;
    [SerializeField] private int yellowCrossCount = 3;
    [SerializeField] private float yellowCrossCoolDown = 2.5f;
    private bool canYellowCross = true;

    [Space(10)]
    [Header("Red Cross Stats")]
    [SerializeField] private GameObject redCross;
    [SerializeField] private int redCrossCount = 3;
    [SerializeField] private float redCrossCoolDown = 2.5f;
    private bool canRedCross = true;
    [Space(10)]

    [Header("Attach Objects")]
    [SerializeField] private Animator animator;
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private GameObject displayHealthBar;
    [SerializeField] private HitFlash hitFlash;
    [SerializeField] private SpecialPatterns specialPatterns;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip enemyHitSoundEffect;
    [SerializeField] private AudioClip enemyDeathSoundEffect;
    [SerializeField] private AudioClip transformEffect;
    [SerializeField] private AudioClip phase2Effect;
    [SerializeField] private AudioClip phase3Effect;


    private Transform player;
    private bool hasEnteredLineOfSight = false;
    private bool transformationFinished = false;
    
    
    // private bool test = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bossHealthBar.UpdateHealthBar(health);

        displayHealthBar.SetActive(false);

        canFireBlue = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            if (!hasEnteredLineOfSight)
            {
                SoundManager.Instance.PlaySound(transformEffect);
                animator.SetTrigger("Entry");
                StartCoroutine(FinishedAnimation());
                hasEnteredLineOfSight = true;
                
            }
        }
        if (hasEnteredLineOfSight)
        {
            if (canFireYellowCross && canYellowCross)
            {
                canYellowCross = false;
                StartCoroutine(fireCrosses(yellowCross,yellowCrossCount,yellowCrossCoolDown,false));
            }
            if (canFireRedCross && canRedCross)
            {
                canRedCross = false;
                StartCoroutine(fireCrosses(redCross,redCrossCount,redCrossCoolDown,true));
            }            
            if(canFireLinePattern)
            {
                if(health <= 15)
                {
                    specialPatterns.intiatePattern(SpecialPatterns.patterns.line, 3, 2);
                }
                else
                {
                    specialPatterns.intiatePattern(SpecialPatterns.patterns.line, 3, 3);
                }
            }
        }
    }

    private IEnumerator fireCrosses(GameObject prefab,int crossCount,float crossCoolDown,bool crossType)
    {
        

        for(int i = 0; i < crossCount; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject temp = Instantiate(prefab);
            temp.transform.position = player.transform.position;
        }

        yield return new WaitForSeconds(crossCoolDown);
        
        if(!crossType) { canYellowCross = true; }
        else { canRedCross = true; }
    }

    private IEnumerator FinishedAnimation()
    {
        animator.SetTrigger("FinishedTransformation");
        yield return new WaitForSeconds(2);
        transformationFinished = true;
        displayHealthBar.SetActive(true);
        canFireRed = true;
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (transformationFinished)
        {
            if (other.tag == "Projectile" || other.tag == "MolotovSpread" || other.tag == "PlayerAxe" || other.tag == "MolotovP")
            {
                Debug.Log("Collision with Projectile detected!");
                Debug.Log("Collided with: " + other.gameObject.name);

                //TakeDamage(other.GetComponent<Projectile>().damage);
                TakeDamage(Projectile.damage);

                hitFlash.Flash();



                SoundManager.Instance.PlaySound(enemyHitSoundEffect);
            } 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lineOfSight);
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount; 

        bossHealthBar.UpdateHealthBar(health);

        if (health <= 0)
        {
            SoundManager.Instance.PlaySound(enemyDeathSoundEffect);

            Instantiate(deathEffect, transform.position, Quaternion.identity);

            CinemachineShake.Instance.ShakeCamera(3f, 1f); //Camera Shake

            Destroy(gameObject);

            //SCORE VALUE SYSTEM
            Score.scoreValue += scoreValueOnDeath;
        }

        if(health % 2 == 0 ) // fires every 2 damage
        {
            canFireBlue = true;
        }

        if (health <=5)
        {
            yellowCrossCount = 6;
            yellowCrossCoolDown = 2;
            canFireYellowCross = true;
        }
        else if (health <= 15)
        {
            canFireRed = false;
            canFireYellow = true;
            canFireLinePattern = true;
            
            if (!phase3Triggered)
            {
                animator.SetTrigger("Phase3");
                SoundManager.Instance.PlaySound(phase3Effect);
                phase3Triggered = true;
            }
        }
        else if(health <= 20)
        {
            canFireYellowCross = false;
            canFireRedCross = true;
        }
        else if (health <= 25)
        {
            canFireRed = true;
            canFireYellow = false;

            canFireLinePattern = false;

            if (!phase2Triggered)
            {
                animator.SetTrigger("Phase2");
                SoundManager.Instance.PlaySound(phase2Effect);
                phase2Triggered = true;
            }
        }
        else if (health <= 30)
        {
            canFireYellowCross = true;
        }
        else if (health <= 35)
        {
            canFireRed = false;
            canFireYellow = true;
            canFireLinePattern = true;
        }
    }



}
