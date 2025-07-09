using UnityEngine;

public class ShieldReflectionPowerup : MonoBehaviour
{
    GameManager gameManager;
    public float reflectForce;
    [SerializeField] private AudioClip crashSounds;
    private AudioSource shieldAudio;
    private ParticleSystem shield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reflectForce = 15f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        shieldAudio = GetComponent<AudioSource>();
        shield = transform.parent.Find("Magic shield pink").GetComponent<ParticleSystem>();
        if (shield != null)
        {
            Debug.Log("shield GO found");
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
        shield.Play();
    }

    public void DeactivateShield()
    {
        shield.Stop();
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

                //get knockback strength 
                Vector3 force = (other.gameObject.transform.position - transform.position).normalized;
                force = new Vector3(awayFromShield.x * reflectForce, 0f, awayFromShield.z * reflectForce);
                //
                //knockbackable.GetKnockedBack(force);

                enemyRb.AddForce(force, ForceMode.VelocityChange);
                

            }

            gameManager.hasPowerUp = false;
            DeactivateShield();
            Debug.Log("Power up used");
        }
        
    }

    //private void HandleEnemyImpct
}
