using UnityEngine;

public class ShieldReflectionPowerup : MonoBehaviour
{
    GameManager gameManager;
    public float reflectForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reflectForce = 1000f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        //gameManager.hasPowerUp && 
        if( other.gameObject.CompareTag("Enemy") && gameManager.hasPowerUp)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();


            if (enemyRb != null )
            {
                Debug.Log("Collided with "+other.gameObject.name+" with power up set to " + gameManager.hasPowerUp);
                Vector3 awayFromShield = (other.gameObject.transform.position - transform.position);
                enemyRb.AddForce(awayFromShield * reflectForce, ForceMode.Impulse);
            }

            gameManager.hasPowerUp = false;
        }
        
    }
}
