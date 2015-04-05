using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueContentPanel : MonoBehaviour
{
    private ScrollRect _scrollRect;
    public GameObject historyText;
    public GameObject currentText;

    private Text _historyText;
    private Text _currentText;


	// Use this for initialization
	void Start ()
	{
	    _historyText = historyText.GetComponent<Text>();
	    _currentText = currentText.GetComponent<Text>();
	    _scrollRect = GetComponent<ScrollRect>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ContinueDialogue(string text)
    {
       AddToHistory(_currentText.text);
        _currentText.text = text;
    }

    public void AddToHistory(string text)
    {
        _historyText.text += "\n" + text + "\n";
    }
}
