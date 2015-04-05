using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    private bool dialogueSystemActive = false;
    public GameObject dialogueSystem;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivateDialogueSystem(string ID)
    {
        if (dialogueSystemActive) return;
        dialogueSystemActive = true;
        dialogueSystem.SetActive(true);
        DialogueManager.Instance.InitializeNewConversation(ID);
    }

    public void DeActivateDialogueSystem()
    {
        if (!dialogueSystemActive) return;
        dialogueSystemActive = false;
        dialogueSystem.SetActive(false);
    }
}
