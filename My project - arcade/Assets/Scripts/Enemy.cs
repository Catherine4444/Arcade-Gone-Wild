using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject[] humans;
    public float speed = 150;
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
        Destroy(gameObject);
    }
}
