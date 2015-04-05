using System.ComponentModel.Design.Serialization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Global : MonoBehaviour
{

    public static Global Instance;
    //Null
    public Sprite nullSprite;
    public Sprite nullBG;
	//Heima
    public Sprite[] HeimaSprites;

    //Cerise
    public Sprite[] CeriseSprites;

    public Dictionary<string,Character> CharacterList = new Dictionary<string, Character>();

    public Dictionary<string,Sprite> BackgroundList = new Dictionary<string, Sprite>();


    public void OnEnable()
    {
        Instance = this;

    }

    void OnDisable()
    {
        Instance = null;
    }

    public void Awake()
    {   
        Character nope = new Character("null", nullSprite);
        CharacterList.Add(nope.Name, nope);
        BackgroundList.Add("null", nullBG);

        InitializeHeima();
        InitializeCerise();
    }

    void InitializeHeima()
    {
        Character Heima = new Character("Heima", HeimaSprites);
        CharacterList.Add(Heima.Name,Heima);
    }

    void InitializeCerise()
    {
        Character Cerise = new Character("Cerise", CeriseSprites);
        CharacterList.Add(Cerise.Name, Cerise);
    }

    public Character GetChar(string n)
    {
        return CharacterList.ContainsKey(n) ? CharacterList[n] : CharacterList["null"];
    }

    public Sprite GetBG(string n)
    {
        return BackgroundList.ContainsKey(n) ? BackgroundList[n] : BackgroundList["null"];
    }
}
