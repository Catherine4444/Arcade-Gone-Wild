using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 10;
    private float xRange = 8.7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*Time.deltaTime*horizontalInput*scrollSpeed);

    }
}
