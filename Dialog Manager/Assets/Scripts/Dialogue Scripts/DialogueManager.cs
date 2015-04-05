using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance;

    //public delegate void Action(string text);
    //public static event Action TextAdded;

    public Sprite TestSprite;
    public string TestString;
    public string[] TestResponses = new string[2] {"hello", "fuck you"};

    //Continue or End Dialogue
    public GameObject continueButton;
    private Text _continueButtonText;

    //Scroll View  Used for Height Reference
    public GameObject ScrollView;
    private RectTransform _scrollViewRectTransform;
    private float _scrollViewHeight;

    //Content Panel
    public Transform contentPanel;
    public GameObject historyText;
    public GameObject currentText;
    private Text _historyText;
    private Text _currentText;
    private LayoutElement _currentTextLayoutElement;

    //Character Portrait 
    public GameObject charPortrait;
    private Image _portrait;

    //Character Responses
    public GameObject characterResponsePrefab;
    private LayoutElement _characterResponseLayoutElement;
    private float _characterResponseMinHeight;

    //Scroll bar
    public GameObject scrollPanel;
    private ScrollRect _scrollRect;

    void OnEnable()
    {
        Instance = this;
    }

    void OnDisable()
    {
        Instance = null;
    }

	// Use this for initialization
	void Awake ()
	{
	    _continueButtonText = continueButton.GetComponentInChildren<Text>();

        _historyText = historyText.GetComponent<Text>();

        _currentText = currentText.GetComponent<Text>();
	    _currentTextLayoutElement = currentText.GetComponent<LayoutElement>();

	    _portrait = charPortrait.GetComponent<Image>();

	    _scrollViewRectTransform = ScrollView.GetComponent<RectTransform>();
	    _scrollViewHeight = _scrollViewRectTransform.sizeDelta.y;

	    _characterResponseLayoutElement = characterResponsePrefab.GetComponent<LayoutElement>();
	    _characterResponseMinHeight = _characterResponseLayoutElement.minHeight;

	    _scrollRect = scrollPanel.GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitializeNewScene()
    {

        //Get parsed line
        ClearHistroyAndCurrent();

        DisplayDialogue();
        
    }

    public void DisplayDialogue()
    {
        Dialogue dialogue = GameManager.Instance.GetDialog(GameManager.Instance.NextDialogue);
        SetPortrait(dialogue.Speaker.GetPortriat(dialogue.Speaker.Name));

        if (dialogue.ChoiceText.Count == 0)
            ClearCharResponse();
        else
            SetCharacterResponses(dialogue.ChoiceText);
        
        SetNPCResponse(dialogue.SpeakerText);
        
    }

    public void EndDialogue()
    {
        //switch scenes?

    }


    public void ClearCharResponse()
    { 

        SetEndDialogueButton();

        foreach (Transform child in contentPanel)
        {
            if (child.name.Contains("Character Choice"))
                Destroy(child.transform.gameObject);
        }

        SetCurrentTextMinHeight(0);

    }

    void SetContinueDialogueButton()
    {
        continueButton.SetActive(true);
        _continueButtonText.text = "Continue";
        continueButton.GetComponent<Button>().onClick.AddListener(() => DisplayDialogue()); // go to next
    }

    void SetEndDialogueButton()
    {
        continueButton.SetActive(true);
        _continueButtonText.text = "End Dialogue";
        continueButton.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.DeActivateDialogueSystem());
    }


    public void SetCharacterResponses(List<string> characterResponses)
    {
        continueButton.SetActive(false);
        int numberOfResponses = 0;
        foreach (var line in characterResponses)
        {
            GameObject newResponse = Instantiate(characterResponsePrefab) as GameObject;
            newResponse.GetComponent<Text>().text = line;
            //parse
            //newResponse.GetComponent<Button>().onClick.AddListener(() => SetNPCResponseWithNoCharResponse("Go fuckyourself pinoy hobo."));
            newResponse.transform.SetParent(contentPanel, false);
            numberOfResponses++;
        }

        SetCurrentTextMinHeight(numberOfResponses);
    }


    void SetCurrentTextMinHeight(int numberOfCharacterResponses)
    {
        //Character responses have a minHeight of 20.
        _currentTextLayoutElement.minHeight = _scrollViewHeight - _characterResponseMinHeight * numberOfCharacterResponses;

        //fornow
        ResetScrollBar();
        Canvas.ForceUpdateCanvases();

    }

    void ResetScrollBar()
    {
        Canvas.ForceUpdateCanvases();

        _scrollRect.verticalScrollbar.value = 0;
        Canvas.ForceUpdateCanvases();
    }

    public void SetPortrait(Sprite img)
    {
        _portrait.sprite = img;
    }

    public void AddToHistory(string text)
    {
        if (_currentText.text != "")
            _historyText.text += "\n" + text + "\n";
        
    }

    public void ClearHistroyAndCurrent()
    {
        _currentText.text = "";
        _historyText.text = null;

    }

    public void SetNPCResponse(string text)
    {
        AddToHistory(_currentText.text);
        _currentText.text = text;
    }
}
