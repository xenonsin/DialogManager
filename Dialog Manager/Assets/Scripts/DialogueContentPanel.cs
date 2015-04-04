using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueContentPanel : MonoBehaviour
{

    private ScrollRect _scrollRect;

    void OnEnable()
    {
        DialogueManager.TextAdded += FocusPanel;
    }

    void OnDisable()
    {
        DialogueManager.TextAdded -= FocusPanel;
    }

	// Use this for initialization
	void Start ()
	{

	    _scrollRect = GetComponent<ScrollRect>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FocusPanel()
    {
        Debug.Log("Hello");
    }
}
