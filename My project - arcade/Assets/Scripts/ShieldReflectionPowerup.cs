using UnityEngine;

public class ShieldReflectionPowerup : MonoBehaviour
{
    GameManager gameManager;
    public float reflectForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reflectForce = 10f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                //Debug.Log("Collided with "+other.gameObject.name+" with power up set to " + gameManager.hasPowerUp);
                Vector3 awayFromShield = (other.gameObject.transform.position - transform.position).normalized;
                awayFromShield = new Vector3(awayFromShield.x * reflectForce, 0f, awayFromShield.z * reflectForce);
                enemyRb.AddForce(awayFromShield, ForceMode.VelocityChange);
            }

            gameManager.hasPowerUp = false;
            Debug.Log("Power up used");
        }
        
    }
}
