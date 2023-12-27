using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UI_Base
{


    public override bool Init()
    {
        if (_init)
            return false;

        return _init = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
