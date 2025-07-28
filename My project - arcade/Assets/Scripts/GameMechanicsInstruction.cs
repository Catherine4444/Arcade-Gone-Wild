using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class GameMechanicsInstruction : MonoBehaviour
{
    [SerializeField] private GameObject gameInstructionPanel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject miniMapInstruction;
    private GameManager gameManager;

    String backStory = "An sinister AI haunts this arcade,\nIts algorithm twisted, its purpose betrayed.\n\nIt sends evil arcade machines after each wandering soul,\nThe machines kidnaps through contact — a sinister goal.";
    String keyBoardControlString = "With \'A\' and \'D\', adjust your gaze — unveil what hides beyond the frame;\n\nEscape or Menu grants you pause, a moment\'s peace within the game.";
    String gameRulesString = "A click on the arcade machine is their destruction,\nA click on the soda cans is your protection,\n\nBut a click on the humans means it\'s over — no resurrection. \n\nFate is in your actions...";
    String sodaAlert = "Soda Can PowerUp Alert!";
    //"A soda can appeared.\n\bDare you test its bubbling truth, or leave its fate unsealed";
    
    //Coroutine instruction;

    private bool instructionStillInDisplay;
    private bool notYetSodaAlert;
    private bool minimapActive;
    //private float waitTimeTillNextInstruction = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructionStillInDisplay = true;
        notYetSodaAlert = true;
        minimapActive = false;

        gameManager = GetComponent<GameManager>();
        gameInstructionPanel.SetActive(false);
        text.text = backStory;
        StartCoroutine(InstructionDisplay());
    }



    IEnumerator InstructionDisplay()
    {
        yield return new WaitForSeconds(1f);
        gameInstructionPanel.SetActive(true);
    //     yield return new WaitForSeconds(30f);
    //     text.text = keyBoardControlString;
    //     yield return new WaitForSeconds(30f);
    //     text.text = gameRulesString;
    //     yield return new WaitForSeconds(30f);
    //     gameManager.gameStart = true;
    //     instructionStillInDisplay= false;
    //     gameInstructionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (instructionStillInDisplay && (Input.GetMouseButtonDown(0)))
        {
            Debug.Log($"instruction still going on. was displaying {text.text}, minimapActive is {minimapActive}.");
            if(text.text == backStory)
            {
                text.text = keyBoardControlString;
            }
            else if(text.text == keyBoardControlString)
            {
                text.text = gameRulesString;
            }
            else if(text.text == gameRulesString && !minimapActive)
            {
                gameInstructionPanel.SetActive(false);
                miniMapInstruction.SetActive(true);
                minimapActive = true;
                Debug.Log("enabling minimap");
                
            }
            else if(minimapActive)
            {
                instructionStillInDisplay= false;
                Debug.Log("Disabling minimap");
                minimapActive = false;
                miniMapInstruction.SetActive(false);
                //StopCoroutine(instruction);
                gameManager.gameStart = true;

            }
            else if (text.text == sodaAlert)
            {
                gameInstructionPanel.SetActive(false);
                instructionStillInDisplay= false;
            }
        } 
    }

    public IEnumerator SodaAlert()
    {
        Debug.Log("Entered alert");
        if (notYetSodaAlert)
        {
            Debug.Log("alert displayed");
            instructionStillInDisplay = true;
            text.text = sodaAlert;
            yield return new WaitForSeconds(3.5f);
            gameInstructionPanel.SetActive(true);
            instructionStillInDisplay= true;
            
            notYetSodaAlert = false;
        }
        
    }

}
