using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public Transform spawn;
    public Transform player;

    // Gestion de la condition de victoire
    private bool isWinned = false;
    private int winnerPlayer;

    public Text timerText;

    private float startTime;
    public float gameDuration = 120;

    // Image waiting for end
    private int maxWaitingForEnd = 25;
    private int waitingForEnd = 0;
    private int maxScore;
    private ItemBehaviorScript[] items;

    // Use this for initialization
    void Start()
    {
        player.position = new Vector3(spawn.position.x, spawn.position.y, spawn.position.z);

        startTime = Time.time;
        items = FindObjectsOfType<ItemBehaviorScript>();
        maxScore = GetMaxScore();
    }
	
    // Update is called once per frame
    void Update()
    {
        var textToPrint = TimerTewt();
        var defScore = GetDefenderScore();
        textToPrint += "\nJ1 : " + (int) (defScore * 100 / maxScore) + "%";
        textToPrint += "\nJ2 : " + (int) ((maxScore - defScore) * 100 / maxScore) + "%";
        timerText.text = textToPrint;

        if (Time.time > startTime + gameDuration)
            Winner();
        
        // WinnerPlayer = 1;

    }

    private string TimerTewt()
    {
        int timer = (int) (startTime + gameDuration - Time.time);

        var secs = timer % 60;
        var disec = secs / 10;
        secs = secs % 10;

        var mins = (timer/ 60) % 60;
        var dimin = mins / 10;
        mins = mins % 10;

        return "" + dimin + mins + ":" + disec + secs;
    }

    void Winner()
    {
        if (GetDefenderScore() > maxScore / 2)
        {
            GameStaticData.PlayerWinner = 1;
        }
        else
        {
            GameStaticData.PlayerWinner = 2;
        }

        if (waitingForEnd == maxWaitingForEnd)
        {
            SceneManager.LoadScene("Scenes/GameOver", LoadSceneMode.Single);
        }
        else
            waitingForEnd++;

    }

    private int GetDefenderScore()
    {
        int res = 0;

        foreach (var item in items)
        {
            res += (int) item.hp;
        }

        return res;
    }

    private int GetMaxScore()
    {
        int res = 0;

        foreach (var item in items)
        {
            res += (int) item.MaxHP;
        }

        return res;
    }
}
