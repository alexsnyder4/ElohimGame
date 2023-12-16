using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameState gs;
    // Start is called before the first frame update
    void Start()
    {
        gs.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
