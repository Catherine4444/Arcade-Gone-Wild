using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 10;
    private float xRange = 8.7f;
    
    [SerializeField] GameManager gameManager;
    private AudioSource bgm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgm = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameManager.gameNotOver)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 moveAmount = Vector3.right*Time.deltaTime*horizontalInput*scrollSpeed;
            if(transform.position.x + moveAmount.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            else if(transform.position.x + moveAmount.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(moveAmount);
            }
        }
        else
        {
            bgm.Stop();
        }
    }
}




