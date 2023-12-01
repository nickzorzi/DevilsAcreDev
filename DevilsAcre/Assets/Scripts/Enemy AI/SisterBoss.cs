using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterBoss : MonoBehaviour
{
    public float lineOfSight;
    private Transform player;
    public int health;
    public GameObject deathEffect;

    private bool hasEnteredLineOfSight = false;
    private bool transformationFinished = false;
    public bool canFireRed = false;
    public bool canFireBlue = false;
    public bool canFireYellow = false;
    // public bool canRedCross = false;
    // public bool canYellowCross = false;

    public int scoreValueOnDeath;

    public Animator animator;

    public GameObject waveSB;
    public GameObject waveGB;

    public BossHealthBar bossHealthBar;
    public GameObject displayHealthBar;

    [SerializeField] private HitFlash hitFlash;

    [SerializeField] private AudioClip enemyHitSoundEffect;
    [SerializeField] private AudioClip enemyDeathSoundEffect;

    [SerializeField] private AudioClip transformEffect;
    [SerializeField] private AudioClip phase2Effect;
    [SerializeField] private AudioClip phase3Effect;

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

        //Waves Enabler
        if (hasEnteredLineOfSight == true)
        {
            waveSB.SetActive(true);
            waveGB.SetActive(true);
        }
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
        if (!transformationFinished)
        {
            
        }
        if (transformationFinished)
        {
            if (other.tag == "Projectile" || other.tag == "MolotovSpread" && canFireRed == true)
            {
                Debug.Log("Collision with Projectile detected!");
                Debug.Log("Collided with: " + other.gameObject.name);

                //TakeDamage(other.GetComponent<Projectile>().damage);
                TakeDamage(Projectile.damage);

                hitFlash.Flash();

                canFireBlue = true;

                SoundManager.Instance.PlaySound(enemyHitSoundEffect);
            } 
            else if ((other.tag == "Projectile" || other.tag == "MolotovSpread" && canFireYellow == true))
            {
                Debug.Log("Collision with Projectile detected!");
                Debug.Log("Collided with: " + other.gameObject.name);

                //TakeDamage(other.GetComponent<Projectile>().damage);
                TakeDamage(Projectile.damage);

                hitFlash.Flash();

                canFireBlue = true;

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

        if (health <= 35)
        {
            canFireRed = false;
            canFireYellow = true;
        }
        else if (health <= 25)
        {
            canFireRed = true;
            canFireYellow = false;
            animator.SetTrigger("Phase2");

            SoundManager.Instance.PlaySound(phase2Effect);
            // canRedCross = true;
        }
        else if (health <= 15)
        {
            canFireRed = false;
            canFireYellow = true;
            animator.SetTrigger("Phase3");

            SoundManager.Instance.PlaySound(phase3Effect);
            // canYellowCross = true;
        }
        else if (health <=5)
        {
            canFireRed = true;
            canFireYellow = true;
        }
    }
}
