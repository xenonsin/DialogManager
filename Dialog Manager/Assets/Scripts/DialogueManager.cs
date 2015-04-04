using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public delegate void Action();
    public static event Action TextAdded;

    public GameObject charPortrait;
    public Transform contentPanel;
    public GameObject continueButton;
    public GameObject sampleText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddText()
    {
        GameObject newText = Instantiate(sampleText) as GameObject;
        //Text text = newText.GetComponent<Text>();
        //text.text = " hi";
        Transform text = newText.GetComponent<Transform>();
        
        newText.transform.SetParent(contentPanel.transform, false);

        if (TextAdded != null)
            TextAdded();
    }
}
