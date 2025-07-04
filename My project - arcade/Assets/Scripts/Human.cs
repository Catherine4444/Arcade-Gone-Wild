using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
    public static Vector3 spawnPos = new Vector3(0, 0.5f , 5.7f);
    Rigidbody humanRb;
    public float speed = 1;

    GameManager gameManager;

    //GameObject enemy;
    UnityEngine.AI.NavMeshAgent agent;


    // random walk aka patrol coz easier to follow video 
    Vector3 destPoint;
    bool walkPointSet; // if there is already a destination point 
    [SerializeField] float range; // how far allowed to walk 


    [SerializeField] LayerMask groundLayer, playerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawnPos;
        destPoint = new Vector3(0, 0.5f, 3);
        walkPointSet = true;
        //humanRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = 1.5f;
        agent.SetDestination(destPoint);

        
        //GetComponentInParent<GameManager>();
        //transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Patrol());
    }

    private void OnMouseDown() 
    {
        gameManager.GameOver();
        //Destroy(gameObject);
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Humans);

    }

    IEnumerator Patrol()
    {
        
        if (!walkPointSet)
        {
            // agent.isStopped = true;                // Stop movement
            // yield return new WaitForSeconds(Random.Range(0f, 10f));
            // agent.isStopped = false;               // Resume movement
            SearchForDest();
        }
        if (walkPointSet)
        {
            agent.SetDestination(destPoint);
        }
        if(Vector3.Distance(transform.position, destPoint) < 1) 
        {
            walkPointSet = false;
            yield return new WaitForSeconds(Random.Range(0f, 10f));
            
        }
        
        

    }

    void SearchForDest()
    {
        float z = Random.Range(-range,range);
        float x = Random.Range(-range,range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        // check if dest in nav mesh
        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

}
