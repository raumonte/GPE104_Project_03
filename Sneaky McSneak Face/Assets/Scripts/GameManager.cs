using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public string gameState = "Start Screen";
    public static GameManager Instance;
    public GameObject titleScreen;
    public GameObject gameUI;
    public GameObject player;
    public GameObject playerPf;
    public GameObject playerPosition;
    public GameObject playerDeathScreen;
    public GameObject gameOverScreen;
    public int lives = 3;

        // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogError("Game Manager tried to make a second one.");
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == "Start Screen")
        {
            //do the state behavior
            StartScreen();
            //check transitions
        }
        else if (gameState == "InitializeGame")
        {
            //do the state behavior
            InitializeGame();
            //check transitions
            ChangeState("SpawnPlayer");
        }
        else if (gameState == "SpawnPlayer")
        {
            //do the state behavior
            SpawnPlayer();
            //check transitions
            ChangeState("Gameplay");
        }
        else if (gameState == "Gameplay")
        {
            //Do the state behavior
            Gameplay();
            //Check transitions
            if (player == null && lives > 0)
            {
                ChangeState("PlayerDeath");
            }
            else if (player == null && lives <= 0)
            {
                ChangeState("GameOver");
            }

            
        }
        else if (gameState == "PlayerDeath")
        {
            //do the state behavior
            PlayerDeath();
            //check transitions
            if (Input.anyKeyDown)
            {
                ChangeState("SpawnPlayer");
            }
        }
        else if (gameState == "GameOver")
        {
            //do the state behavior
            GameOver();
            //check transitions
            if (Input.anyKeyDown)
            {
                ChangeState("Start Screen");
            }
        }
        else
        {
            Debug.LogWarning("Game manager tried to change to a non-existant state" + gameState);
        }
    }
    public void ChangeState(string newState)
    {
        gameState = newState;
    }
    public void StartGame()
    {
        Debug.Log("Button Pressed");
        ChangeState("Initialize Game");
    }
    public void StartScreen()
    {
        //TO show the menu.
        if (titleScreen.activeSelf)
        {
            titleScreen.SetActive(true);
        }
    }
    public void InitializeGame()
    {
        // Reset variables

        //Turn off menu
        titleScreen.SetActive(false);
        //Turn on game UI
        gameUI.SetActive(true);
    }
    public void SpawnPlayer()
    {
        player = Instantiate(playerPf, playerPosition.transform.position, Quaternion.identity);
    }
    public void Gameplay()
    {

    }
    public void PlayerDeath()
    {
        if (!playerDeathScreen.activeSelf)
        {
            playerDeathScreen.SetActive(true);
        }
    }
    public void GameOver()
    {
        if (!gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
