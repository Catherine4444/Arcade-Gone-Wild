using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class StartPageSceneManager : MonoBehaviour
{
    Animator animDoor;
    public Button playButton;
    FadeInOut fade;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fade = GameObject.Find("Image").GetComponent<FadeInOut>();
        animDoor = GameObject.Find("wall-door-rotate").GetComponent<Animator>();
        
    }


    public void PlayButton()
    {
        playButton.gameObject.SetActive(false);
        animDoor.SetBool("isOpening", true);
        StartCoroutine(WaitForAnim());
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(1);
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        //Debug.Log("loadScene");
        SceneManager.LoadScene("Game Scene");
    }

}
