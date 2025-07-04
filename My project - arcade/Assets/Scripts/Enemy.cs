using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject[] humans;
    public float speed;
    private Rigidbody enemyRb;
    
    private float poweupStrength = 30;
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 5;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        humans = GameObject.FindGameObjectsWithTag("Human");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameNotOver)
        {
            Vector3 towardsHuman = (humans[0].transform.position - transform.position).normalized;
            enemyRb.AddForce(towardsHuman * speed, ForceMode.Impulse);
        }   
    }


    private void OnMouseDown() 
    {
        //Destroy(gameObject);
        gameObject.GetComponent<Renderer>().enabled = false; // instant "disappear"
        ObjectPoolManager.ReturnObjectToPool(gameObject);
        gameManager.enemiesActive--;
        gameObject.GetComponent<Renderer>().enabled = true;
        //Debug.Log($"Enenies Active {gameManager.enemiesActive}");
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Human"))
        {
            if( gameManager.hasPowerUp)
            {
                Vector3 awayFromHuman = ( transform.position - other.gameObject.transform.position );

                Rigidbody HumanRb = other.GetComponent<Rigidbody>();
                HumanRb.isKinematic = true; // So it's not affected by physics
                //Debug.Log("Collided with "+other.gameObject.name+" with power up set to " + hasPowerUp);
                enemyRb.AddForce(awayFromHuman*poweupStrength, ForceMode.Impulse);
                HumanRb.isKinematic = false;
                gameManager.hasPowerUp = false;
            }
            else
            {
                ObjectPoolManager.ReturnObjectToPool(other.gameObject, ObjectPoolManager.PoolType.Humans);
                ObjectPoolManager.ReturnObjectToPool(gameObject);
                gameManager.numberOfHumansAlive--;
                if(gameManager.numberOfHumansAlive == 0)
                {
                    gameManager.GameOver();
                }

            }
            
        }
        else if(other.gameObject.CompareTag("Power Up")) 
        {
            ObjectPoolManager.ReturnObjectToPool(other.gameObject, ObjectPoolManager.PoolType.Powerups);
            
        }
        
    }
}
