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
        animDoor.SetBool("isOpening", false);
        playButton.gameObject.SetActive(false);
        animDoor.SetBool("isOpening", true);
        StartCoroutine(WaitForAnim());
    }

    IEnumerator WaitForAnim()
    {
        Debug.Log("Enter Into anim");
        yield return new WaitForSeconds(1);
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        //Debug.Log("loadScene");
        SceneManager.LoadScene("Game Scene");
    }

}
