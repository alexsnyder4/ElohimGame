using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameState gs;

    string username;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gs.playerName = username;
        SceneManager.LoadSceneAsync(1);
    }

    public void ReadPlayerInput(string s)
    {
        username = s;
    }
}
