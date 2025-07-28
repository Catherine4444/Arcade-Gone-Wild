using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameNotOver)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

        }
        
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // freeze game, can use for slomo
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freeze game, can use for slomo
        GameIsPaused = true;
    }

    //public void 
}
