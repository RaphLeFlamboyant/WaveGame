using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class GameOver : MonoBehaviour
{
    public Sprite player1Won;
    public Sprite player2Won;
    public SpriteRenderer winScreen;

    // Gestion de la condition de victoire
    private int winnerPlayer;

    // Image waiting for end
    //private int maxWaitingForEnd = 25;
    //private int waitingForEnd = 0;

    // Gestion manette
    PlayerIndex player1Index;
    public GamePadState stateP1;
    GamePadState prevStateP1;
    IDictionary<string, bool> player1InputValues;

    // Use this for initialization
    void Start()
    {
        // Gestion écran
        // On cache l'écran de victoire
        winScreen.enabled = false;	

        winnerPlayer = GameStaticData.PlayerWinner;

        if (winnerPlayer == 1)
        {
            // Joueur 1 = bleu
            winScreen.sprite = player1Won;
        }
        else
        {
            // Joueur 2 = rouge
            winScreen.sprite = player2Won;
        }

        winScreen.enabled = true;


        // Gestion manette
        PlayerIndex playerIndex;
        bool playerIndexSet;

        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                playerIndex = testPlayerIndex;
                playerIndexSet = true;
            }
        }

        player1Index = (PlayerIndex)0;

        player1InputValues = new Dictionary<string, bool> ()
        {
            {"A", false},
            {"B", false},
            {"X", false},
            {"Y", false},
            {"Start", false}
        };
    }
	
    // Update is called once per frame
    void Update()
    {
        // Récupération état manette
        prevStateP1 = stateP1;

        stateP1 = GamePad.GetState (player1Index);

        if (stateP1.IsConnected)
        {
            player1InputValues ["A"] = GetPressValue (stateP1.Buttons.A);
            player1InputValues ["B"] = GetPressValue (stateP1.Buttons.B);
            player1InputValues ["X"] = GetPressValue (stateP1.Buttons.X);
            player1InputValues ["Y"] = GetPressValue (stateP1.Buttons.Y);
            player1InputValues ["Start"] = GetPressValue (stateP1.Buttons.Start);
        }

        // Start ou A
        if (player1InputValues["Start"] || player1InputValues["A"] || player1InputValues["B"]
            || player1InputValues["X"] || player1InputValues["Y"]
            || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)
            || Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
    }

    private bool GetPressValue(ButtonState btnState)
    {
        return btnState == ButtonState.Pressed ? true : false; 
    }
}
