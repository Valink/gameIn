using TMPro;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startVue;
    [SerializeField] private GameObject gameOverVue;

    [SerializeField] private TextMeshProUGUI placeholder;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()    
    {
        placeholder.text = "Type 'RESTART'";

        startVue.SetActive(false);
        gameOverVue.SetActive(true);
    }
    public void Start()    
    {
        placeholder.text = "Type 'START'";

        gameOverVue.SetActive(false);
        startVue.SetActive(false);
    }

    public void StartGame()
    {
        placeholder.text = "MASH KEYBOARD !";

        gameOverVue.SetActive(false);
        startVue.SetActive(false);
    }

    public void Home()    
    {
        gameOverVue.SetActive(false);
        startVue.SetActive(true);
    }
}