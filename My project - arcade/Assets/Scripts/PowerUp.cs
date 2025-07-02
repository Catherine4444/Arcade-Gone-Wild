using UnityEngine;

public class PowerUp : MonoBehaviour
{
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() 
    {
        //Destroy(gameObject);
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Powerups);
        gameManager.PowerUpAquired();
    }
}

