using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    private bool dialogueSystemActive = false;
    public GameObject dialogueSystem;

    public GameObject background;
    private Image _background;

    public GameObject blackFade;
    private Image _blackFade;
    public bool darkerThanBlack = false;
    public bool whiterThanWhite = false;
    public float fadeInOutTime = 6f;

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
	    _background = background.GetComponent<Image>();
	    _blackFade = blackFade.GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update ()
	{
        if (darkerThanBlack)
            StartScene();

        if (whiterThanWhite)
            EndScene("null");
	}

    private void StartScene()

    {

        // Fade the texture to clear.

        FadeToClear();



        // If the texture is almost clear...

        if (_blackFade.color.a <= 0.05f)

        {

            // ... set the colour to clear and disable the GUITexture.

            _blackFade.color = Color.clear;

            _blackFade.enabled = false;

            ActivateDialogueSystem(1);

            // The scene is no longer starting.

            darkerThanBlack = false;

        }
    }

    public void EndScene (string id)

    {

        // Make sure the texture is enabled.

        _blackFade.enabled = true;
        DeActivateDialogueSystem();
 

        // Start fading towards black.

        FadeToBlack();

 

        // If the screen is almost black...

        if (_blackFade.color.a >= .99f)
        {
            //Change the bg to setup new scene.
            SetBackground(Global.Instance.GetBG(id));

            whiterThanWhite = false;
        }



    }

    public void FadeToClear()
    {

        // Lerp the colour of the texture between itself and transparent.

        _blackFade.color = Color.Lerp(_blackFade.color, Color.clear, Time.deltaTime/fadeInOutTime );

    }

    public void FadeToBlack()
    {

        // Lerp the colour of the texture between itself and black.

        _blackFade.color = Color.Lerp(_blackFade.color, Color.black, Time.deltaTime / fadeInOutTime);

    }

    public void SetBackground(Sprite ID)
    {
        _background.sprite = ID;
    }

    public void ActivateDialogueSystem(int ID)
    {
        if (dialogueSystemActive) return;
        dialogueSystemActive = true;
        dialogueSystem.SetActive(true);
        DialogueManager.Instance.InitializeNewScene(ID);
    }

    public void DeActivateDialogueSystem()
    {
        if (!dialogueSystemActive) return;
        dialogueSystemActive = false;
        dialogueSystem.SetActive(false);
    }
}
