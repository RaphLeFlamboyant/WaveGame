using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateUpdater : MonoBehaviour {
    private bool doingLeft = false;
    private bool doingRight = false;
    private bool doingTop = false;
    private bool doingBottom = false;

    private bool launching = false;

    public void UpdateStateOnWave(WaveScript wave)
    {
        launching = true;
        var animator = GetComponent<Animator>();

        if (wave.waveType == WaveScript.WaveType.Electric)
        {
            doingTop = true;
            animator.SetBool("doingTop", true);
        }
        else if (wave.waveType == WaveScript.WaveType.Fire)
        {
            doingRight = true;
            animator.SetBool("doingRight", true);
        }
        else if (wave.waveType == WaveScript.WaveType.Water)
        {
            doingLeft = true;
            animator.SetBool("doingLeft", true);
        }
        else if (wave.waveType == WaveScript.WaveType.Ground)
        {
            doingBottom = true;
            animator.SetBool("doingBottom", true);
        }        
    }

    // Update is called once per frame
    void Update () {
        var animator = GetComponent<Animator> ();

        if (launching)
        {
            var lastUpdate = GetComponentInParent<FoePlayerController>().lastWaveTime;

            if (lastUpdate + 0.5 < Time.time)
            {
                doingTop = false;
                animator.SetBool("doingTop", false);
                doingRight = false;
                animator.SetBool("doingRight", false);
                doingLeft = false;
                animator.SetBool("doingLeft", false);
                doingBottom = false;
                animator.SetBool("doingBottom", false);

                launching = false;
            }
        }
    }
}
