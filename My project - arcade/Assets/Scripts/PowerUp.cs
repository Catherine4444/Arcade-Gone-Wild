using UnityEngine;
using System.Collections;


public class PowerUp : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        StartCoroutine(TimeOutPowerUp());

        //StartCoroutine(WaitToRelease());
    }

    // IEnumerator WaitToRelease()
    // {
    //     yield return new WaitForSeconds(10f);
    //     ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Powerups);
    // }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeOutPowerUp()
    {   
        yield return new WaitForSeconds(10);
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Powerups);
    }



    private void OnMouseDown() 
    {
        //Destroy(gameObject);
        gameObject.GetComponent<Renderer>().enabled = false; // instant "disappear"
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.Powerups);
        gameObject.GetComponent<Renderer>().enabled = true;
        gameManager.Print("got powerup");
        
        gameManager.PowerUpAquired();
    }
}

