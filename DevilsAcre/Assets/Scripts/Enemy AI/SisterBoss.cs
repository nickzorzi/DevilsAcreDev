using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SisterBoss : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float lineOfSight;
    public int health;
    public int scoreValueOnDeath;


    [HideInInspector] public bool canFireRed = false;
    [HideInInspector] public bool canFireBlue = false;
    [HideInInspector] public bool canFireYellow = false;
    [HideInInspector] public bool phase2Triggered = false;
    [HideInInspector] public bool phase3Triggered = false;
    [HideInInspector] public bool phase4Triggered = false;
    [HideInInspector] public bool phase5Triggered = false;
    [HideInInspector] public bool phase6Triggered = false;
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
    [SerializeField] private AudioClip phaseEffect;
    [SerializeField] private AudioClip phaseEffect2;

    [Space(10)]

    private Transform player;
    private bool hasEnteredLineOfSight = false;
    private bool transformationFinished = false;

    [Space(10)]
    [SerializeField] private GameObject DialogueDeath;
    [SerializeField] private GameObject SetMusic;
    [SerializeField] private GameObject VictoryZone;
    
    
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

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        SoundManager.Instance.PlaySound(phaseEffect);
        SoundManager.Instance.PlaySound(enemyDeathSoundEffect);
        CinemachineShake.Instance.ShakeCamera(3f, 1f); //Camera Shake
        displayHealthBar.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        SetMusic.SetActive(true);
        DialogueDeath.SetActive(true);
        // Score.scoreValue += scoreValueOnDeath;
        yield return new WaitForSeconds(1);
        VictoryZone.SetActive(true);
        yield return null;
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount; 

        bossHealthBar.UpdateHealthBar(health);

        if (health <= 0)
        {
            canFireBlue = false;
            canFireRed = false;
            canFireYellow = false;
            canFireLinePattern = false;
            canFireRedCross = false;
            canFireYellowCross = false;

            yellowCrossCount = 0;
            yellowCrossCoolDown = 0;

            StartCoroutine(Death());
        }

        if(health % 2 == 0 ) // fires every 2 damage
        {
            canFireBlue = true;
        }

        if (health <=10)
        {
            if (!phase6Triggered)
            {
                animator.SetTrigger("Phase6");
                SoundManager.Instance.PlaySound(phaseEffect2);
                phase6Triggered = true;
            }

            yellowCrossCount = 6;
            yellowCrossCoolDown = 2;
            canFireYellowCross = true;
        }
        else if (health <= 20)
        {
            if (!phase5Triggered)
            {
                animator.SetTrigger("Phase5");
                SoundManager.Instance.PlaySound(phaseEffect2);
                phase5Triggered = true;
            }
        }
        else if (health <= 30)
        {
            if (!phase4Triggered)
            {
                animator.SetTrigger("Phase4");
                SoundManager.Instance.PlaySound(phaseEffect2);
                phase4Triggered = true;
            }

            canFireRed = false;
            canFireYellow = true;
            canFireLinePattern = true;
        }
        else if(health <= 40)
        {
            if (!phase3Triggered)
            {
                animator.SetTrigger("Phase3");
                SoundManager.Instance.PlaySound(phaseEffect);
                phase3Triggered = true;
            }

            canFireYellowCross = false;
            canFireRedCross = true;
        }
        else if (health <= 50)
        {
            if (!phase2Triggered)
            {
                animator.SetTrigger("Phase2");
                SoundManager.Instance.PlaySound(phaseEffect);
                phase2Triggered = true;
            }

            canFireRed = true;
            canFireYellow = false;

            canFireLinePattern = false;
        }
        else if (health <= 60)
        {
            canFireYellowCross = true;
        }
        else if (health <= 70)
        {
            canFireRed = false;
            canFireYellow = true;
            canFireLinePattern = true;
        }
    }



}
