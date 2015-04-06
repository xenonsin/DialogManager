using System;
using System.Text.RegularExpressions;
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

    public void InitializeNewScene(int id)
    {

        //Get parsed line
        ClearHistroyAndCurrent();

        DisplayDialogue(id);

        
        
    }

    public void DisplayDialogue(int id)
    {
        Dialogue dialogue = GameManager.Instance.GetDialog(id);
        SetPortrait(dialogue.SpeakerSprite);

        SetNPCResponse(dialogue.SpeakerText);

        UIManager.Instance.SetBackground(dialogue.BackGround);
        
       
        if (dialogue.ChoiceText.Count == 1)
            CharacterDoesNotHaveAResponse(dialogue);          
        else
            CharacterHasAResponse(dialogue);
            

    }

    void CharacterHasAResponse(Dialogue d)
    {
        ClearCharResponse();
        SetCharacterResponses(d.ChoiceText, d.ChoiceFunc);
    }

    void CharacterDoesNotHaveAResponse(Dialogue d)
    {
        string func = d.ChoiceFunc[0].Trim();
        ClearCharResponse();

        string resultString = Regex.Match(func, @"\d+").Value;
        int nextScene = Int32.Parse(resultString);
        if (func.Contains("Next"))         
            SetContinueDialogueButton(nextScene);
    }


    public void EndDialogue()
    {
        //switch scenes?

    }


    public void ClearCharResponse()
    { 

        //SetEndDialogueButton();

        foreach (Transform child in contentPanel)
        {
            if (child.name.Contains("Character Choice"))
                Destroy(child.transform.gameObject);
        }

        SetCurrentTextMinHeight(0);

    }

    void SetContinueDialogueButton(int nextScene)
    {
        Canvas.ForceUpdateCanvases();
        continueButton.SetActive(true);
        _continueButtonText.text = "Continue";
        continueButton.GetComponent<Button>().onClick.AddListener(() => DisplayDialogue(nextScene)); // go to next
    }

    void SetEndDialogueButton()
    {
        Canvas.ForceUpdateCanvases();
        continueButton.SetActive(true);
        _continueButtonText.text = "End Dialogue";
        continueButton.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.DeActivateDialogueSystem());
    }

    //void ParseNumbersFromString (string[])

    public void SetCharacterResponses(List<string> characterResponses, List<string> characterFuncs)
    {
        continueButton.SetActive(false);
        int numberOfResponses = 0;
        foreach (var line in characterResponses)
        {
            GameObject newResponse = Instantiate(characterResponsePrefab) as GameObject;
            newResponse.GetComponent<Text>().text = line;

            string resultString = Regex.Match(characterFuncs[numberOfResponses], @"\d+").Value;
            Debug.Log(line);
            int nextScene = Int32.Parse(resultString);

            newResponse.GetComponent<Button>().onClick.AddListener(() => DisplayDialogue(nextScene));

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
        if (text != "")
            _historyText.text += "\n" + text + "\n";
        
    }

    public void ClearHistroyAndCurrent()
    {
        _currentText.text = "";
        _historyText.text = "";

    }

    public void SetNPCResponse(string text)
    {
        AddToHistory(_currentText.text);
        _currentText.text = text;
    }
}
