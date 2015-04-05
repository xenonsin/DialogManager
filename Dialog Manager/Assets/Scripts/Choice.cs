using UnityEngine;
using System.Collections;

public class Choice
{

    public string Text { get; set; }
    public string Funct { get; set; }

    public Choice()
    {
        Text = "";
        Funct = "";
    }
    public Choice (string t, string f)
    {
        Text = t;
        Funct = f;
    }


}
