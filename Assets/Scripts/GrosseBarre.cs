using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrosseBarre : MonoBehaviour
{
	// L'image de fond
	public SpriteRenderer BackGround;

    // Le cadre devant
    public SpriteRenderer ForeGround;

	// Les déclarations "publiques" pour associer les
	// images depuis l'interface graphique de Unity
	public SpriteRenderer BlueBlock;
	public SpriteRenderer GreenBlock;
	public SpriteRenderer YellowBlock;
	public SpriteRenderer RedBlock;

	// Les 3 emplacements pour mettre les images
	public SpriteRenderer Emplacement1;
	public SpriteRenderer Emplacement2;
	public SpriteRenderer Emplacement3;

    // enum sur les couleurs
    public enum GrosseBarreColor
    {
        Rouge,
        Vert,
        Bleu,
        Jaune,
        Transparent
    };

	// Use this for initialization
	void Start ()
	{
		// On affiche tout le temps le background et le foreground
		BackGround.enabled = true;
        ForeGround.enabled = true;
	
		// On cache les 3 emplacements pour le moment
		Emplacement1.enabled = false;
		Emplacement2.enabled = false;
		Emplacement3.enabled = false;

		// On cache tout le temps les autres sprites
		BlueBlock.enabled = false;
		GreenBlock.enabled = false;
		YellowBlock.enabled = false;
		RedBlock.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{     

	}

    public void SetColor (int BoxIndex, GrosseBarreColor Color)
    {
        switch (BoxIndex)
        {
            case 0 :
                switch (Color)
                {
                    case GrosseBarreColor.Rouge :
                        Emplacement1.sprite = RedBlock.sprite;
                        Emplacement1.enabled = true;
                        break ;

                    case GrosseBarreColor.Vert :
                        Emplacement1.sprite = GreenBlock.sprite;
                        Emplacement1.enabled = true;
                        break ;

                    case GrosseBarreColor.Bleu :
                        Emplacement1.sprite = BlueBlock.sprite;
                        Emplacement1.enabled = true;
                        break ;

                    case GrosseBarreColor.Jaune:
                        Emplacement1.sprite = YellowBlock.sprite;
                        Emplacement1.enabled = true;
                        break ;

                    case GrosseBarreColor.Transparent:
                        Emplacement1.enabled = false;
                        break ;
                } 
                break ;

            case 1 :
                switch (Color)
                {
                    case GrosseBarreColor.Rouge :
                        Emplacement2.sprite = RedBlock.sprite;
                        Emplacement2.enabled = true;
                        break ;

                    case GrosseBarreColor.Vert :
                        Emplacement2.sprite = GreenBlock.sprite;
                        Emplacement2.enabled = true;
                        break ;

                    case GrosseBarreColor.Bleu :
                        Emplacement2.sprite = BlueBlock.sprite;
                        Emplacement2.enabled = true;
                        break ;

                    case GrosseBarreColor.Jaune:
                        Emplacement2.sprite = YellowBlock.sprite;
                        Emplacement2.enabled = true;
                        break ;

                    case GrosseBarreColor.Transparent:
                        Emplacement2.enabled = false;
                        break ;
                } 
                break ;
            
            //case 2:
            default :
                switch (Color)
                {
                    case GrosseBarreColor.Rouge :
                        Emplacement3.sprite = RedBlock.sprite;
                        Emplacement3.enabled = true;
                        break ;

                    case GrosseBarreColor.Vert :
                        Emplacement3.sprite = GreenBlock.sprite;
                        Emplacement3.enabled = true;
                        break ;

                    case GrosseBarreColor.Bleu :
                        Emplacement3.sprite = BlueBlock.sprite;
                        Emplacement3.enabled = true;
                        break ;

                    case GrosseBarreColor.Jaune:
                        Emplacement3.sprite = YellowBlock.sprite;
                        Emplacement3.enabled = true;
                        break ;

                    case GrosseBarreColor.Transparent:
                        Emplacement3.enabled = false;
                        break ;
                } 
                break ;
        }
    }
}
