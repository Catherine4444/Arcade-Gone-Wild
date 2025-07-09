using UnityEngine;

public class ShieldReflectionPowerup : MonoBehaviour
{
    GameManager gameManager;
    public float reflectForce;
    [SerializeField] private AudioClip crashSounds;
    private AudioSource shieldAudio;
    private ParticleSystem shield;
    private ParticleSystemRenderer shieldRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reflectForce = 3000f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        shieldAudio = GetComponent<AudioSource>();
        shield = transform.parent.Find("Magic shield pink").GetComponent<ParticleSystem>();
        if (shield != null)
        {
            Debug.Log("shield GO found");
            shieldRenderer= shield.GetComponent<ParticleSystemRenderer>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.hasPowerUp)
        {
            ActivateShield();
        }
        else
        {
            DeactivateShield();
        }
    }

    public void ActivateShield()
    {
        //shieldRenderer.enabled = true;
        shield.gameObject.SetActive(true);
        shield.Play();
        //shield.Pause();
    }

    public void DeactivateShield()
    {
        shield.gameObject.SetActive(false);
        shield.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    private void OnTriggerEnter(Collider other) 
    {
        //gameManager.hasPowerUp && 
        //Debug.Log($"1. Object entered name: {other.name}");
        if( other.gameObject.CompareTag("Enemy") && gameManager.hasPowerUp)
        {
            //Debug.Log($"2. Object entered name: {other.name}");
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            if (enemyRb != null )
            {
                shieldAudio.PlayOneShot(crashSounds, 1f);
                //Debug.Log("Collided with "+other.gameObject.name+" with power up set to " + gameManager.hasPowerUp);

                if (other.TryGetComponent(out IKnockbackable knockbackable))
                {
                    //get knockback strength 
                    Vector3 force = (other.gameObject.transform.position - transform.position).normalized;
                    force = new Vector3(force.x * reflectForce, 0f, force.z * reflectForce);
                    //
                    knockbackable.GetKnockedBack(force);
                }
                

                //enemyRb.AddForce(force, ForceMode.VelocityChange);
                

            }

            gameManager.hasPowerUp = false;
            DeactivateShield();
            Debug.Log("Power up used");
        }
        
    }

    //private void HandleEnemyImpct
}
