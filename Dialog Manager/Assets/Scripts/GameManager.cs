using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int startingDialogue = 2;
    public int CurrentDialogue{ get; set; }

    public int NextDialogue { get; set; }

    void OnEnable()
    {
        Instance = this;
    }

    void OnDisable()
    {
        Instance = null;
    }
	// Use this for initialization
	void Start () {
        //Let's start the game :D
	    UIManager.Instance.darkerThanBlack = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
