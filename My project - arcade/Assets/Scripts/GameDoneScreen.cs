using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDoneScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasgroup;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI loseText;
    
    public enum ScreenType 
    {
        GameOver,
        GamePass
    }

    void Start() 
    {
        //canvasgroup = GetComponent<CanvasGroup>();
        canvasgroup.alpha = 0;
        nextLevelButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

    }

    public void SetUp(ScreenType screen)
    {
        canvasgroup.alpha = 1;
        //gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        switch (screen)
        {
            case ScreenType.GameOver:
        
                restartButton.gameObject.SetActive(true);
                loseText.gameObject.SetActive(true);
                nextLevelButton.gameObject.SetActive(false);
                winText.gameObject.SetActive(false);
                break;
        
            case ScreenType.GamePass:
                nextLevelButton.gameObject.SetActive(false);
                winText.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(false);
                loseText.gameObject.SetActive(false);
                break;
        }
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Start Page"); // load start scene 
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelButton()
    {
        Debug.Log("Next level button clicked");
    }

    
}
