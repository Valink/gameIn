using System.Collections;
using TMPro;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startVue;
    [SerializeField] private GameObject gameOverVue;

    [SerializeField] private TextMeshProUGUI placeholder;

    [SerializeField] private GameObject playerShip;

    [SerializeField] private GameObject explosionFX;
    [SerializeField] private float explosionDelay;
    [SerializeField] private int explosionNumber;
    [SerializeField] private float explodeSpawnWidth;
    [SerializeField] private float explodeSpawnHeight;

    bool isGameOver = false;
    bool isStarted = false;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()    
    {
        placeholder.text = "Type 'RESTART'";
        MusicManager.instance.EndGame();

        startVue.SetActive(false);
        gameOverVue.SetActive(true);

        if (!isGameOver) StartCoroutine(FinalExplosion());

        isGameOver = true;
        
    }

    IEnumerator FinalExplosion()
    {
        for(int i = 0; i < explosionNumber; i++)
        {
            yield return new WaitForSeconds(explosionDelay);

            Vector3 randomPos = new Vector3(Random.Range(-explodeSpawnWidth, explodeSpawnWidth),
                                            Random.Range(-explodeSpawnHeight, explodeSpawnHeight), 0);

            GameObject obj = Instantiate(explosionFX, playerShip.transform.position + randomPos, Quaternion.identity);
            obj.transform.localScale *= 2f;
            AudioManager.instance.PlayExplode();
        }
        playerShip.transform.position = new Vector3(999, 999, 0);
    }


    public void Start()    
    {
        placeholder.text = "Type 'START'";

        gameOverVue.SetActive(false);
        startVue.SetActive(false);
    }

    public void StartGame()
    {
        isStarted = true;

        placeholder.text = "MASH KEYBOARD !";
        MusicManager.instance.StartGame();

        gameOverVue.SetActive(false);
        startVue.SetActive(false);
    }

    public void Home()    
    {
        gameOverVue.SetActive(false);
        startVue.SetActive(true);
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