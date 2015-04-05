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
    
    //Camille
    public Sprite[] CamilleSprites;

    //Backgrounds
    public Sprite[] BackGrounds;

    public Dictionary<string, Sprite> SpriteList = new Dictionary<string, Sprite>();

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
        InitializeCamille();
        InitializeBG();
    }

    void InitializeBG()
    {
        foreach (var backGround in BackGrounds)
        {
            BackgroundList.Add(backGround.name,backGround);
        }
    }

    void InitializeHeima()
    {
        //Character Heima = new Character("Heima", HeimaSprites);
        //CharacterList.Add("Heima", Heima);

        foreach (Sprite sprite in HeimaSprites)
        {
            SpriteList.Add(sprite.name, sprite);
        }
    }

    void InitializeCerise()
    {
        //Character Cerise = new Character("Cerise", CeriseSprites);
        //CharacterList.Add("Cerise", Cerise);
        foreach (Sprite sprite in CeriseSprites)
        {
            SpriteList.Add(sprite.name, sprite);
        }
    }

    void InitializeCamille()
    {
        //Character Camille = new Character("Camille", CamilleSprites);
        //CharacterList.Add("Camille", Camille);
        foreach (Sprite sprite in CamilleSprites)
        {
            SpriteList.Add(sprite.name, sprite);
        }
    }

    public Character GetChar(string n)
    {
        n.Trim();
        string[] line = n.Split("-"[0]);
        return CharacterList.ContainsKey(line[0]) ? CharacterList[line[0]] : CharacterList["null"];
    }

    public Sprite GetBG(string n)
    {
        return BackgroundList.ContainsKey(n) ? BackgroundList[n] : BackgroundList["null"];
    }
}
