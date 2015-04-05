
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Dialogue
{
    public int ID { get; set; }
    public Character Speaker { get; set; }

    public Sprite SpeakerSprite { get; set; }

    public Sprite BackGround { get; set; }

    public string SpeakerText { get; set; }
   

    public List<Choice> CharacterChoice = new List<Choice>();

    public List<string> ChoiceText = new List<string>(); 

    public List<string> ChoiceFunc = new List<string>();


    public void AddChoice(Choice c)
    {
        CharacterChoice.Add(c);
    }

    public void ParseChoiceText(string s)
    {
        string[] line = s.Split("|"[0]);

        foreach (var s1 in line)
        {
            ChoiceText.Add(s1);
        }
    }

    public void ParseChoiceFunc(string s)
    {
        string[] line = s.Split("|"[0]);

        foreach (var s1 in line)
        {
            ChoiceFunc.Add(s1);
        }
    }

    public void FixChoices()
    {
        if (ChoiceText.Count != ChoiceFunc.Count) return;

        for (int i = 0; i < ChoiceText.Count; i++)
        {
            Choice c = new Choice(ChoiceText[i], ChoiceFunc[i]);
            CharacterChoice.Add(c);
        }
    }

    
}

