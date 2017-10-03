using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

// Required in C#

public class MenuManagement : MonoBehaviour
{
    public Sprite Cursor;

    public SpriteRenderer Box_Jouer;
    public SpriteRenderer Box_Credits;
    public SpriteRenderer Box_Quitter;

    public AudioClip Deplacement;
    public AudioClip Selection;

    private int position = 0;

    // Pour savoir si on est en mode menu ou crédits
    private bool modeMenu = true;
    public SpriteRenderer Credit;

    // On saute quelques frames pour les inputs... c'est trop sensible !
    private int MaxInputSkipFrame = 7;
    private int InputSkipFrame = 0;

    float timeLeft = 0.1f;
    float MaxtimeLeft = 0.1f;

    PlayerIndex player1Index;
    public GamePadState stateP1;
    GamePadState prevStateP1;
    IDictionary<string, bool> player1InputValues;

    // Use this for initialization
    void Start()
    {
        Box_Jouer.sprite = Cursor;
        Box_Credits.sprite = Cursor;
        Box_Quitter.sprite = Cursor;

        // On cache les 3 emplacements pour le moment
        Box_Jouer.enabled = false;
        Box_Credits.enabled = false;
        Box_Quitter.enabled = false;

        Credit.enabled = false;

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

        player1InputValues = new Dictionary<string, bool>()
        {
            { "A", false },
            { "StickUp", false },
            { "StickDown", false },
            { "PadUp", false },
            { "PadDown", false },
            { "Start", false }
        };
    }

    void Update()
    {
        // Récupération état manette
        prevStateP1 = stateP1;

        stateP1 = GamePad.GetState(player1Index);

        if (stateP1.IsConnected)
        {
            player1InputValues["StickUp"] = stateP1.ThumbSticks.Left.Y > 0;
            player1InputValues["StickDown"] = stateP1.ThumbSticks.Left.Y < 0;
            player1InputValues["PadUp"] = GetPressValue(stateP1.DPad.Up);
            player1InputValues["PadDown"] = GetPressValue(stateP1.DPad.Down);
            player1InputValues["A"] = GetPressValue(stateP1.Buttons.A);
            player1InputValues["Start"] = GetPressValue(stateP1.Buttons.Start);
        }
            
        if (modeMenu == true)
        {

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                // On gère le menu en temps normal
                var effect = GetComponent<AudioSource>();

                // Gestion de Haut, Bas, START, et A
                // On "limite" la sensibilité/le temps de décalage
                //if (InputSkipFrame == MaxInputSkipFrame)
                //{
                // Haut
                if (player1InputValues["StickUp"] || player1InputValues["PadUp"]
                    || Input.GetKey(KeyCode.UpArrow))
                {
                    if (position == 0)
                        position = 0; // position = 2; => on boucle
                    else
                    {                 
                        effect.clip = Deplacement;
                        effect.Play();
                        position--;
                    }
                }
                // Bas
                if (player1InputValues["StickDown"] || player1InputValues["PadDown"]
                    || Input.GetKey(KeyCode.DownArrow))
                {
                    if (position == 2)
                        position = 2; // position = 0; => on boucle
                    else
                    {
                        effect.clip = Deplacement;
                        effect.Play();
                        position++;
                    }
                }

                // Start ou A
                if (player1InputValues["Start"] || player1InputValues["A"]
                    || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
                {
                    effect.clip = Selection;
                    effect.Play();
                    manage_position(position);
                }

                player1InputValues["StickUp"] = false;
                player1InputValues["StickDown"] = false;
                player1InputValues["PadUp"] = false;
                player1InputValues["PadDown"] = false;
                player1InputValues["A"] = false;
                player1InputValues["Start"] = false;

                //InputSkipFrame = -1;
                //}
                //InputSkipFrame++;

                // Affichage
                switch (position)
                {
                // Jouer
                    case 0:
                        Box_Jouer.enabled = true;
                        Box_Credits.enabled = false;
                        Box_Quitter.enabled = false;
                        break;

                // Credits
                    case 1:
                        Box_Jouer.enabled = false;
                        Box_Credits.enabled = true;
                        Box_Quitter.enabled = false;
                        break;

                // Quitter
                    case 2:
                        Box_Jouer.enabled = false;
                        Box_Credits.enabled = false;
                        Box_Quitter.enabled = true;
                        break;
           
                    default :
                        Box_Jouer.enabled = true;
                        Box_Credits.enabled = false;
                        Box_Quitter.enabled = false;
                        break;
                }

                //Fin du timeLeft
                timeLeft = MaxtimeLeft;
            }

            // Fin du modeMenu == true
        }

        if (modeMenu == false)
        {

            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {

                // PAS MENU, MAIS CREDIT
                //var credits = GetComponent<SpriteRenderer>();

                Credit.enabled = true;

                if (player1InputValues["Start"] || player1InputValues["A"]
                    || Input.GetKey(KeyCode.Escape))
                {
                    modeMenu = true;
                    Credit.enabled = false;
                }

                player1InputValues["StickUp"] = false;
                player1InputValues["StickDown"] = false;
                player1InputValues["PadUp"] = false;
                player1InputValues["PadDown"] = false;
                player1InputValues["A"] = false;
                player1InputValues["Start"] = false;

                // Fin du timeLeft
                timeLeft = MaxtimeLeft;
            }
            // Fin du modeMenu == false
        }

    }

    private bool GetPressValue(ButtonState btnState)
    {
        return btnState == ButtonState.Pressed ? true : false; 
    }

    private void manage_position(int pos)
    {
        switch (pos)
        {
            case 0:
                SceneManager.LoadScene("Scenes/GameScene", LoadSceneMode.Single);
                break;

            case 1:
                //SceneManager.LoadScene("Scenes/Credits", LoadSceneMode.Single);
                modeMenu = false;
                break;

            case 2:
                Application.Quit();
                break;

            default:
                break;
        }
            
    }
}
