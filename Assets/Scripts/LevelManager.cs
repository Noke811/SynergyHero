using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currentWave;
    public int lastWave = 15;
    public int remainLives;
    public int lives = 5;
    public int currency;

    [Header("References")]
    [SerializeField] private GameObject gameSetUI;
    [SerializeField] private TextMeshProUGUI gameSetText;
    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currentWave = 1;
        currency = 100;
        remainLives = lives;
        gameSetUI.SetActive(false);
    }

    public void IncreaseWave()
    {
        currentWave++;
    }

    public void DecreaseLives()
    {
        remainLives--;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to purchase this item");
            return false;
        }
    }

    public void GameSet()
    {
        if(remainLives < 1)
        {
            gameSetUI.gameObject.GetComponent<Image>().color = loseColor;
            gameSetText.text = "You Lose!";
        }
        else
        {
            gameSetUI.gameObject.GetComponent<Image>().color = winColor;
            gameSetText.text = "You Win!";
        }
        gameSetUI.SetActive(true);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GameRestart()
    {
        currentWave = 1;
        currency = 100;
        remainLives = lives;
        gameSetUI.SetActive(false);

        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");

        foreach (GameObject hero in heroes)
        {
            Destroy(hero);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
