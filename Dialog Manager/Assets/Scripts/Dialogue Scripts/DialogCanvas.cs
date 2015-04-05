using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogCanvas : MonoBehaviour
{

    public GameObject charPortrait;
    public GameObject charName;
    public GameObject charDialog;


    private Image _portrait;
    private Text _name;
    private Text _dialog;
	// Use this for initialization
	void Start ()
	{
	    _portrait = charPortrait.GetComponent<Image>();
	    _name = charName.GetComponent<Text>();
	    _dialog = charDialog.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BeginTalk(Sprite img, string name, string dialog)
    {
        SetPortrait(img);
        SetName(name);
        SetDialog(dialog);
    }

    public void SetPortrait(Sprite img)
    {
        _portrait.sprite = img;
    }

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetDialog(string dialog)
    {
        _dialog.text = dialog;
    }
}
