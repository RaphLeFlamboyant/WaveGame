using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

 // Required in C#

public class GameInputManager : MonoBehaviour
{
    PlayerIndex player1Index;
    PlayerIndex player2Index;

    public GamePadState stateP1;
    GamePadState prevStateP1;

    GamePadState stateP2;
    GamePadState prevStateP2;

    IDictionary<string, bool> player1InputValues;
    IDictionary<string, bool> player2InputValues;

    // Use this for initialization
    void Start()
    {
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
        player2Index = (PlayerIndex)1;

        player1InputValues = new Dictionary<string, bool>()
        {
            { "A", false },
            { "B", false },
            { "X", false },
            { "Y", false },
            { "Left", false },
            { "Right", false }
        };

        player2InputValues = new Dictionary<string, bool>()
        {
            { "A", false },
            { "B", false },
            { "X", false },
            { "Y", false },
            { "Left", false },
            { "Right", false }
        };
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);

        prevStateP1 = stateP1;
        prevStateP2 = stateP2;

        stateP1 = GamePad.GetState(player1Index);
        stateP2 = GamePad.GetState(player2Index);

        if (stateP1.IsConnected)
        {
            /*
			player1InputValues ["Left"] = stateP1.ThumbSticks.Left.X < 0;
			player1InputValues ["Right"] = stateP1.ThumbSticks.Left.X > 0;
			player1InputValues ["A"] = GetPressValue (stateP1.Buttons.A);
			player1InputValues ["B"] = GetPressValue (stateP1.Buttons.B);
			player1InputValues ["X"] = GetPressValue (stateP1.Buttons.X);
			player1InputValues ["Y"] = GetPressValue (stateP1.Buttons.Y);
   */         
            // Ne gère PAS le changement de joueur P1 défenseur/attaquant P2 attaquant/défenseur
            player1InputValues["Left"] = (stateP1.ThumbSticks.Left.X < 0) || (Input.GetKey(KeyCode.LeftArrow));
            player1InputValues["Right"] = (stateP1.ThumbSticks.Left.X > 0) || (Input.GetKey(KeyCode.RightArrow));
            player1InputValues["A"] = GetPressValue(stateP1.Buttons.A) || (Input.GetKey(KeyCode.UpArrow)); // Jump
            player1InputValues["B"] = GetPressValue(stateP1.Buttons.B) || (Input.GetKey(KeyCode.DownArrow)); // Repair
            player1InputValues["X"] = GetPressValue(stateP1.Buttons.X);
            player1InputValues["Y"] = GetPressValue(stateP1.Buttons.Y);
        }
        else
        {
            player1InputValues["Left"] = Input.GetKey(KeyCode.LeftArrow);
            player1InputValues["Right"] = Input.GetKey(KeyCode.RightArrow);
            player1InputValues["A"] = Input.GetKey(KeyCode.UpArrow); // Jump
            player1InputValues["B"] = Input.GetKey(KeyCode.DownArrow); // Repair
        }

        if (stateP2.IsConnected)
        {
            /*
			player2InputValues["Left"] = stateP2.ThumbSticks.Left.X < 0;
			player2InputValues["Right"] = stateP2.ThumbSticks.Left.X > 0;
			player2InputValues["A"] = GetPressValue(stateP2.Buttons.A);
			player2InputValues["B"] = GetPressValue(stateP2.Buttons.B);
			player2InputValues["X"] = GetPressValue(stateP2.Buttons.X);
			player2InputValues["Y"] = GetPressValue(stateP2.Buttons.Y);
*/
            player2InputValues["Left"] = stateP2.ThumbSticks.Left.X < 0;
            player2InputValues["Right"] = stateP2.ThumbSticks.Left.X > 0;
            player2InputValues["A"] = GetPressValue(stateP2.Buttons.A) || (Input.GetKey(KeyCode.D)); // Ground
            player2InputValues["B"] = GetPressValue(stateP2.Buttons.B) || (Input.GetKey(KeyCode.F)); // Fire
            player2InputValues["X"] = GetPressValue(stateP2.Buttons.X) || (Input.GetKey(KeyCode.S)); // Water
            player2InputValues["Y"] = GetPressValue(stateP2.Buttons.Y) || (Input.GetKey(KeyCode.E)); // Lightning
        }
        else
        {
            player2InputValues["A"] = Input.GetKey(KeyCode.D); // Ground
            player2InputValues["B"] = Input.GetKey(KeyCode.F); // Fire
            player2InputValues["X"] = Input.GetKey(KeyCode.S); // Water
            player2InputValues["Y"] = Input.GetKey(KeyCode.E); // Lightning
        }
    }

    private bool GetPressValue(ButtonState btnState)
    {
        return btnState == ButtonState.Pressed ? true : false; 
    }

    public bool IsPressed(string buttonName, int player)
    {
        if (player == 1)
            return player1InputValues[buttonName];

        return player2InputValues[buttonName];
    }

    public void SetVibrating(int player, float left, float right)
    {
        GamePad.SetVibration(player == 1 ? player1Index : player2Index, left, right);
    }
}
