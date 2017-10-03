using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreTrigger : MonoBehaviour
{
	// Images de la barre
	public SpriteRenderer BackGround;
    public SpriteRenderer ForeGround;
	public SpriteRenderer Gauge;

    // Image du Filler
    public SpriteRenderer ImageFiller;

	// Le carré qui va remplir la jauge
	//public SpriteRenderer BarFiller;
    public SpriteRenderer EmplacementFiller;
	private Rect RectFiller;
	private Texture2D FillerTexture;

	// Niveaux de la barre
	// Max = le max...
	public int MaxGauge = 100;
	// Milieu à viser
	public int MiddleGauge = 50;
	// Décalage autour du milieu
	public int MiddleLength = 2;

	// x1 de 0 à 30
	private int Zone1Begin = 0;
	private int Zone1End;
	// x2 de 31 à *zone du milieu*
	private int Zone2Begin;
	private int Zone2End;
	// x3 dans la zone du milieu
	private int Zone3Begin;
	private int Zone3End;
	// x2 de *zone du milieu* à 66
	private int Zone4Begin;
	private int Zone4End;
	// x1 de 66 à 100
	private int Zone5Begin;
	private int Zone5End;

	// NewPush = si on vient de démarrer la barre,
	// on attend un premier appui de la touche pour
	// lancer les jauges au milieu
	private bool NewPush = true;

	// Est-on en montée ou en descente ?
	// true = montée, false = descente
	private bool UpSlide = true;

	// Appui sur un bouton
	// true = bouton appuyé
	// false = bouton pas appuyé
	private bool MyTouch = false;

	// Valeur courane de la jauge
	private int CurrentGaugeValue;

	// Use this for initialization
	void Start ()
	{
		// On affiche tout le temps le background
		BackGround.enabled = true;
        ForeGround.enabled = true;

		// La jauge et la couleur de remplissage, seulement à l'appui
        EmplacementFiller.enabled = false;
		//Gauge.enabled = false;

        // On 'naffiche jamais l'image directement
        ImageFiller.enabled = false;

		// x1 de 0 à 30
		int Zone1Begin = 0;
		int Zone1End = MaxGauge / 3;
		// x2 de 31 à *zone du milieu*
		int Zone2Begin = Zone1End;
		int Zone2End = MiddleGauge - MiddleLength;
		// x3 dans la zone du milieu
		int Zone3Begin = Zone2End;
		int Zone3End = MiddleGauge + MiddleLength;
		// x2 de *zone du milieu* à 66
		int Zone4Begin = Zone3End;
		int Zone4End = (MaxGauge / 3) * 2;
		// x1 de 66 à 100
		int Zone5Begin = Zone4End;
		int Zone5End = MaxGauge;

		// On crée une jauge colorée en jaune
		// pos_x, pos_y, width, heigth
		RectFiller = new Rect(0, -4, BackGround.sprite.textureRect.width, BackGround.sprite.texture.height);
		FillerTexture = new Texture2D(1, 1);
		FillerTexture.SetPixel(0, 0, Color.yellow);
		FillerTexture.Apply();
        EmplacementFiller.sprite = ImageFiller.sprite;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (NewPush)
		{
			// On vient de démarrer la barre,
			if (MyTouch)
			{
				// On vient d'appuyer !
				// on passe donc en mode "montée"
				UpSlide = true;
				NewPush = false;
				CurrentGaugeValue = MaxGauge / 2;

				float ratio = CurrentGaugeValue / MaxGauge;
				float RectWidth = ratio * BackGround.sprite.textureRect.width;
				RectFiller.width = RectWidth;

                EmplacementFiller.sprite = ImageFiller.sprite;
                EmplacementFiller.enabled = true;
				
				//Gauge.enabled = true;
			}

            /*
             * ******************
             * **** DEBUUUUG ****
             * ******************
             */
            MyTouch = true;
            /*
             * ******************
             * **** DEBUUUUG ****
             * ******************
             */
		}
		else
		{
			// La barre est démarrée depuis un certain temps
			if (UpSlide)
			{
				// On est en montée de la jauge
				CurrentGaugeValue += 4;
                if (CurrentGaugeValue >= MaxGauge)
                {
                    CurrentGaugeValue = MaxGauge;
                    UpSlide = false;
                }
			}
			else
			{
				// On est en descente de la jauge
                CurrentGaugeValue -= 4;
                if (CurrentGaugeValue <= 0)
                {
                    CurrentGaugeValue = 0;
                    UpSlide = true;
                }
			}

            float ratio = CurrentGaugeValue / MaxGauge;
            float RectWidth = ratio * BackGround.sprite.textureRect.width;
            RectFiller.width = RectWidth;

            EmplacementFiller.sprite = ImageFiller.sprite;
            EmplacementFiller.enabled = true;

		}

		MyTouch = false;
	}

	public int PushButton ()
	{
		MyTouch = true;
		return (CurrentGaugeValue);
	}
}
