using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject[] humans;
    public float speed;
    private Rigidbody enemyRb;
    private UnityEngine.AI.NavMeshAgent agent;
    private int index;
    private GameObject target;
    private AudioSource enemyAudio;

    
    
    //private float poweupStrength = 3;
    GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 0.2f;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        humans = GameObject.FindGameObjectsWithTag("Human");
        index = Random.Range(0, humans.Length);
        target = humans[index];
        
    }

    void Chase()
    {
        
        agent.SetDestination(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(gameManager.gameNotOver)
        {
            // Vector3 towardsHuman = (humans[0].transform.position - transform.position).normalized;
            // enemyRb.AddForce(towardsHuman * speed, ForceMode.VelocityChange);
            Vector3 targetPos = new Vector3((-target.transform.position.z) , transform.position.y , target.transform.position.x);
            transform.LookAt(targetPos);
            Chase();
        }
        else
        {
            enemyAudio.Stop();
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
