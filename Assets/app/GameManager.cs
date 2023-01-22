using System.Collections;
using System;
using TMPro;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverVue;

    [SerializeField] private GameObject playerShip;

    [SerializeField] private GameObject explosionFX;
    [SerializeField] private float explosionDelay;
    [SerializeField] private int explosionNumber;
    [SerializeField] private float explodeSpawnWidth;
    [SerializeField] private float explodeSpawnHeight;

    bool isGameOver = false;
    bool isStarted = false;

    [SerializeField] public TMP_Text levelText;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public int Level;
    [SerializeField] public int Score;

    public static GameManager Instance;

    [SerializeField] private TMP_InputField input;   
    private TMP_Text placeholder;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameOverVue.SetActive(false);

        placeholder = input.placeholder.GetComponent<TMP_Text>();
        placeholder.text = "Type 'start'";
    }

    public void GameOver()    
    {
        input.text = string.Empty;

        placeholder.text = "Type 'RESTART'";
        MusicManager.instance.EndGame();

        gameOverVue.SetActive(true);

        if (!isGameOver) StartCoroutine(FinalExplosion());

        isGameOver = true;
        
    }

    IEnumerator FinalExplosion()
    {
        for(int i = 0; i < explosionNumber; i++)
        {
            yield return new WaitForSeconds(explosionDelay);

            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-explodeSpawnWidth, explodeSpawnWidth),
                                            UnityEngine.Random.Range(-explodeSpawnHeight, explodeSpawnHeight), 0);

            GameObject obj = Instantiate(explosionFX, playerShip.transform.position + randomPos, Quaternion.identity);
            obj.transform.localScale *= 2f;
            AudioManager.instance.PlayExplode();
        }
        playerShip.transform.position = new Vector3(999, 999, 0);
    }




    public void StartGame()
    {
        isStarted = true;

        placeholder.text = "MASH KEYBOARD !";
        MusicManager.instance.StartGame();

        gameOverVue.SetActive(false);
    }

    internal void IncrementLevel()
    {
        Level++;
        levelText.text = Level.ToString();
    }

    internal void IncrementScore()
    {
        Score++;
        scoreText.text = Score.ToString();
    }

    public bool GetGameOverState()
    {
        return isGameOver;
    }

    public bool GetIsStarted()
    {
        return isStarted;
    }
}