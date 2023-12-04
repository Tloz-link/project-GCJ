using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UI_Test : UI_Popup
{
    enum Texts
    {
        Text
    }

    const string googleSheetURL = "https://docs.google.com/spreadsheets/d/1Cgp5thQ0-9BwhZmlmrKTOe-A1iGrorWZ-YDUHugMBhc/export?format=tsv&range=";
    string data = "";

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        StartCoroutine("GetSheetDataCo", "A1");
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetSheetDataCo(string range)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(googleSheetURL + range))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                data = www.downloadHandler.text;
            }
        }

        GetText((int)Texts.Text).text = "Value : " + data;
    }
}
