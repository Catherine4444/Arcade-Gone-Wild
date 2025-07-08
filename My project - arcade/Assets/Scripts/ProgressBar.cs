using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    //private ParticleSystem particleSys;
    private GameManager gameManager;

    public float fillSpeed = 0.0000000000000000000001f;
    private float targetProgress = 0;

    private void Awake() 
    {
        slider = gameObject.GetComponent<Slider>();
        //particleSys = GameObject.Find("ProgressBarParticles").GetComponent<ParticleSystem>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //IncrementProgress(0.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameNotOver && (slider.value < targetProgress))
        {
            slider.value += fillSpeed * Time.deltaTime;
            // if(!particleSys.isPlaying)
            // {
            //     particleSys.Play();
            // }
        }
        // else
        // {
        //     particleSys.Stop();
        // }
        
    }
    public void IncrementProgress(float newPrgress)
    {
        targetProgress = slider.value + newPrgress;

    }
}
