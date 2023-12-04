using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Test : UI_Popup
{
    enum Texts
    {
        Text
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        GetText((int)Texts.Text).text = "Value : 100";
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
