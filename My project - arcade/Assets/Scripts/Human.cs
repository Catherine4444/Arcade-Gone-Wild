using UnityEngine;

public class Human : MonoBehaviour
{
    public static Vector3 spawnPos = new Vector3(0, 0.5f , 5.7f);
    Rigidbody humanRb;
    public float speed = 100;

    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawnPos;
        humanRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //GetComponentInParent<GameManager>();
        humanRb.AddForce(Vector3.back * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() 
    {
        gameManager.GameOver();
        //Destroy(gameObject);
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Humans);

    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Destroy(gameObject);
            //Destroy(other.gameObject);
            ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Humans);
            ObjectPoolManager.ReturnObjectToPool(other.gameObject);
            gameManager.numberOfHumansAlive--;
            if(gameManager.numberOfHumansAlive == 0)
            {
                gameManager.GameOver();
            }
        }
        
    }
}
