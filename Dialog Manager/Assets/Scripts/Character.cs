using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Character
{

    public string Name { get; set; }
    private Dictionary<string, Sprite> Portraits = new Dictionary<string, Sprite>();

    public Character(string n)
    {
        Name = n;
    }

    public Character(string n, Sprite port)
    {
        Name = n;

        AddPortrait(port.name, port);

    }

    public Character(string n, Sprite[] port)
    {
        Name = n;

        foreach (Sprite sprite in port)
        {
            AddPortrait(sprite.name, sprite);
        }
    }

    public void AddPortrait(string id, Sprite img)
    {
        Portraits.Add(id, img);
    }

    public Sprite GetPortriat(string id )
    {
        return Portraits.ContainsKey(id) ? Portraits[id] : null;
    }
}
