using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoePlayerController : MonoBehaviour {

    public GameInputManager inputManager;
    public GrosseBarre stackIndicator;
    public float waveCooldown = 2;

    public AudioClip shout1Clip;
    public AudioClip shout2Clip;
    public AudioClip shout3Clip;

    public float lastWaveTime;
    private int playerID;
    private List<WaveScript.WaveType> powerStack = new List<WaveScript.WaveType>();
    private GrosseBarre horBar;

	// Use this for initialization
	void Start () {
        lastWaveTime = 0;
        powerStack.Clear();
        horBar = GameObject.Find("BarreHorizontale").GetComponent<GrosseBarre>();
	}
	
	// Update is called once per frame
    void Update () {
        playerID = GameObject.Find("Game Controller").GetComponent<EnvironmentData>().playerAttacking;

        var sons = GetComponentsInChildren<WaveScript>();

        if (Time.time - lastWaveTime > waveCooldown)
        {
            foreach (var wave in sons)
            {
                if (AskActionAutorization(wave))
                {
                    lastWaveTime = Time.time;

                    GetComponentInChildren<WizardStateUpdater>().UpdateStateOnWave(wave);

                    var effect = GetComponent<AudioSource>();
                    effect.clip = (Time.time * 1000 % 3) >= 2 ? shout1Clip : (Time.time * 1000 % 3) >= 1 ? shout2Clip: shout3Clip;
                    effect.Play();

                    if (powerStack.Count == 3)
                    {
                        int pow = 1;

                        for (int i = powerStack.Count - 1; i >= 0 && powerStack[i] == wave.waveType; i--)
                        {
                            pow++;    
                        }

                        wave.damages = pow;
                        wave.StartGrowing(true);

                        horBar.SetColor(0, GrosseBarre.GrosseBarreColor.Transparent);
                        horBar.SetColor(1, GrosseBarre.GrosseBarreColor.Transparent);
                        horBar.SetColor(2, GrosseBarre.GrosseBarreColor.Transparent);

                        powerStack.Clear();
                    }
                    else
                    {
                        wave.damages = 1;
                        wave.StartGrowing();
                        horBar.SetColor(powerStack.Count, Transco(wave.waveType));
                        powerStack.Add(wave.waveType);
                    }
                }
            }
        }
	}

    public bool AskActionAutorization(WaveScript wave)
    {
        string inputActivation = wave.inputActivation;

        if (Time.time - lastWaveTime > waveCooldown && inputManager.IsPressed(inputActivation, playerID))
        {

            return true;
        }

        return false;
    }

    private GrosseBarre.GrosseBarreColor Transco(WaveScript.WaveType type)
    {
        if (type == WaveScript.WaveType.Electric)
            return GrosseBarre.GrosseBarreColor.Jaune;
        if (type == WaveScript.WaveType.Fire)
            return GrosseBarre.GrosseBarreColor.Rouge;
        if (type == WaveScript.WaveType.Ground)
            return GrosseBarre.GrosseBarreColor.Vert;
        if (type == WaveScript.WaveType.Water)
            return GrosseBarre.GrosseBarreColor.Bleu;
        
        return GrosseBarre.GrosseBarreColor.Transparent;
    }
}
