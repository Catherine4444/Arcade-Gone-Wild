using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IKnockbackable
{
    private GameObject[] humans;
    
    //public float speed;
    private Rigidbody enemyRb;
    private UnityEngine.AI.NavMeshAgent agent;
    private int index;
    private static float enemySpeed = 1.5f;
    private GameObject target;
    private AudioSource enemyAudio;

    private Coroutine MoveCoroutine;
    [Range(0.001f, 0.1f)] [SerializeField] private float StillThreshold = 0.1f;

    
    
    //private float poweupStrength = 3;
    GameManager gameManager;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
    }

    void OnEnable() 
    {
        agent.speed = Enemy.enemySpeed;
        humans = GameObject.FindGameObjectsWithTag("Human");
        index = Random.Range(0, humans.Length);
        target = humans[index];
        agent.enabled = true;
        agent.Warp(transform.position);
        MoveCoroutine = StartCoroutine(Chase());
    }

    public static void SetEnemySpeed(float s)
    {
        enemySpeed = s;
    }


    IEnumerator Chase()
    {
        //Debug.Log($"Chase coroutine started for {gameObject.name}");
        while (gameManager.gameNotOver)
        {
            if (agent.enabled == true) // didn't want to do this implicitly 
            {
                agent.SetDestination(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            }
            yield return new WaitForSeconds(0.125f);
        }

        enemyAudio.Stop();
    }

    public void GetKnockedBack(Vector3 force)
    {
        StopCoroutine(MoveCoroutine);
        MoveCoroutine = StartCoroutine(ApplyKnockBack(force));

    }

    IEnumerator ApplyKnockBack(Vector3 force)
    {

        yield return null; // wait 1 frame avoid carry over from other coroutines 
        agent.enabled = false;
        enemyRb.useGravity = true;
        enemyRb.isKinematic = false;
        enemyRb.AddForce(force);

        yield return new WaitForFixedUpdate(); //sometimes ai won't get knocked back bcs force hasn't apply to rb
        //yield return new WaitUntil(() => enemyRb.linearVelocity.magnitude < StillThreshold);
        yield return new WaitForSeconds(2f); // stunned for a sec

        //undo the rigid body stuff
        enemyRb.linearVelocity = Vector3.zero;
        enemyRb.angularVelocity = Vector3.zero;
        enemyRb.useGravity = false;
        enemyRb.isKinematic = true;
        agent.enabled = true;
        agent.Warp(transform.position);

        yield return null; // wait a frame 

        MoveCoroutine = StartCoroutine(Chase());

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
