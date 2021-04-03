using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Dynamic;

public class Logging : MonoBehaviour
{

    public string ausgabePfad = "F:\\";
    public string ausgabeDatei = "LOG.txt";
    public bool timeStampAlsDateipraefix = true;
    public bool vorhandeneDateiNeuAnlegen = false;
    private string loggingDatei;
    //StreamWriter sw;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialisiere()
    {

        string timeStamp = "";

        if (timeStampAlsDateipraefix)
        {
            timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss_");
            ausgabeDatei = timeStamp + ausgabeDatei;
        }

        loggingDatei = Path.Combine(ausgabePfad, ausgabeDatei);

        if (!File.Exists(loggingDatei) || vorhandeneDateiNeuAnlegen)
        {
            using (StreamWriter sw = File.CreateText(loggingDatei))
            { }
        }


    }

    public void wl (string text)
    {
        if (text != "")
         {
            using (StreamWriter sw = File.AppendText(loggingDatei))
            {
                sw.WriteLine(text);
            }
        }
    }


}
