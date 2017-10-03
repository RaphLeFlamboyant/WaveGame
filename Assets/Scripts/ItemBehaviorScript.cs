using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemBehaviorScript : MonoBehaviour
{
    // Type de l'objet / Quelle vague va l'abîmer
    public WaveScript.WaveType sensitivityType;

    // Sprite de l'objet dans chaque état
    public Sprite cleanSprite;
    public Sprite brokenSprite;
    public Sprite destroyedSprite;

    private Dictionary<int, Sprite> spriteHpThreashold;

    private SpriteRenderer renderer;

    private float maxHP;
    public float MaxHP 
    {
        get{
            return maxHP;
        }
    }

    public float hp = 5;

    void Awake()
    {
        maxHP = GameObject.FindObjectOfType<EnvironmentData>().itemHP;
        hp = GameObject.FindObjectOfType<EnvironmentData>().itemHP;
    }

    // Use this for initialization
    void Start()
    {

        renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.sprite = cleanSprite;

        spriteHpThreashold = new Dictionary<int, Sprite>()
        {
            {0, destroyedSprite},
            {13, brokenSprite},
            {15, cleanSprite}
        };
        //var sr = GetComponent<SpriteRenderer> ();
        //sr.color = new Color (1, 0.78f, 0.82f, 1);

        renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;

        RefreshSprite();
    }
	
    // Update is called once per frame
    void Update()
    {
        RefreshSprite();

        renderer.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        /*
        ** Collision entre une Wave et un Item
        */

        // Layer du truc qui collisionne est à Wave (donc est une Wave)
        if (coll.gameObject.layer == LayerMask.NameToLayer("Wave"))
        {

            //var wave = coll.transform.root.GetComponentInChildren<WaveScript>();
            var wave = coll.GetComponent<WaveScript>();

            if (wave.waveType == sensitivityType)
            {
                hp -= wave.damages > hp ? hp : wave.damages;

            }
        }
    }

    private void RefreshSprite()
    {
        Sprite sprite = null;
        foreach (var threashold in spriteHpThreashold.Keys)
        {
            if (hp <= threashold)
            {
                sprite = spriteHpThreashold[threashold];
                break;
            }
        }

        if (renderer.sprite != sprite)
        {
            renderer.sprite = sprite;

            GetComponent<AudioSource>().Play();
        }
    }

    public void Repair(float heal)
    {
        if (hp > 0)
            hp = (heal + hp > maxHP) ? maxHP : heal + hp;
    }
}
