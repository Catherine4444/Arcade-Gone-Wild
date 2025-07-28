using UnityEngine;
using System.Collections;


public class PowerUp : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        StartCoroutine(TimeOutPowerUp());
    }

    IEnumerator TimeOutPowerUp()
    {   
        yield return new WaitForSeconds(10);
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Powerups);
        //Debug.Log("powerup expired");
    }
    
    private void OnMouseDown() 
    {
        if(gameManager.gameNotOver)
        {
            gameObject.GetComponent<Renderer>().enabled = false; // instant "disappear"
            ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Powerups);
            gameObject.GetComponent<Renderer>().enabled = true;
            //gameManager.Print("got powerup");
            
            gameManager.PowerUpAquired();
        }
        
    }
}

