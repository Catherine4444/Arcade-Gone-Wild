using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject[] humans;
    public float speed = 110;
    private Rigidbody enemyRb;
    
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            enemyRb.AddForce(towardsHuman * speed);
        }   
    }


    private void OnMouseDown() 
    {
        //Destroy(gameObject);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
        gameManager.enemiesActive--;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Human"))
        {
            //Destroy(gameObject);
            //Destroy(other.gameObject);
            ObjectPoolManager.ReturnObjectToPool(other.gameObject, ObjectPoolManager.PoolType.Humans);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            gameManager.numberOfHumansAlive--;
            if(gameManager.numberOfHumansAlive == 0)
            {
                gameManager.GameOver();
            }
        }
        else if(other.gameObject.CompareTag("Power Up")) 
        {
            ObjectPoolManager.ReturnObjectToPool(other.gameObject, ObjectPoolManager.PoolType.Powerups);
            
        }
        
    }
}
