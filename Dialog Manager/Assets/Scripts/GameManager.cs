using System;
using System.Runtime.Remoting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int startingDialogue = 3;
    public int CurrentDialogue = 3;

    public int NextDialogue { get; set; }

    public TextAsset CsvAsset;
    public bool Ready;
    public bool GameStart;

    public Dictionary<int, Dialogue> DialogueList = new Dictionary<int, Dialogue>();


    public Dialogue GetDialog(int id)
    {
        return DialogueList.ContainsKey(id) ? DialogueList[id] : null;
    }

    void OnEnable()
    {
        Instance = this;
    }

    void OnDisable()
    {
        Instance = null;
    }

    void Awake()
    {
    }
	// Use this for initialization
	void Start () {
        Parse();
	    //Let's start the game :D
	    //UIManager.Instance.startingScene = true;

	}
	
	// Update is called once per frame
	void Update () {
	    if (Ready && !GameStart)
	    {
	        UIManager.Instance.startingScene = true;
	        GameStart = true;
	    }

	}

    void Parse()
    {
        string[,] grid = CSVReader.SplitCsvGrid(CsvAsset.text);


        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            Dialogue newDialogue = new Dialogue();
            
            
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {
                string temp = grid[x, y].Trim();

                if (temp != "" || temp != "none")
                {
                    switch (x)
                    {
                        case 0:
                            newDialogue.Speaker = Global.Instance.GetChar(temp);
                            break;
                        case 1:
                            int parsedInt = 0;
                            int.TryParse(temp, out parsedInt);
                            newDialogue.ID = parsedInt;
                            break;
                        case 2:
                            newDialogue.BackGround = Global.Instance.GetBG(temp);
                            break;
                        case 3:
                            newDialogue.SpeakerText = temp;
                            break;
                        case 4:
                            newDialogue.ParseChoiceText(temp);
                            break;
                        case 5:
                            newDialogue.ParseChoiceFunc(temp);
                            newDialogue.FixChoices();
                            break;
                    }
                }
            }

            DialogueList.Add(newDialogue.ID, newDialogue);
        }
        Debug.Log("hi");
        Ready = true;
    }

}
