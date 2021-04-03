using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;
using Newtonsoft.Json;

public class Konfigurator : MonoBehaviour
{

    //[Tooltip("Das genutzte Logging-Script muss in dem nutzenden Script instanziert werden.")]
    //public Logging log; // immer erst initialisieren via log.Initialisiere();

    [Tooltip("true = Erhoehung der Loggingtiefe")]
    public bool debug = false;

    public GUI_Innentuer gui;
    public GENERATOR_Innentuer generator;

    // Start is called before the first frame update
    void Awake()
    {
        //Initialisiere das Logging
        //log.Initialisiere();
    }


    void Update()
    {
        if (!gui.colorLerpAktive)
        {

           
            if (Input.GetKeyDown("0"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("0");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }


            if (Input.GetKeyDown("1"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("1");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            if (Input.GetKeyDown("2"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("2");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            if (Input.GetKeyDown("3"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("3");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            if (Input.GetKeyDown("4"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("4");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            if (Input.GetKeyDown("5"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("5");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            if (Input.GetKeyDown("6"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("6");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            if (Input.GetKeyDown("7"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("7");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }
            if (Input.GetKeyDown("8"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("8");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            if (Input.GetKeyDown("9"))
            {
                if (!generator.rotating)
                {
                    gui.ermitteleAktionAnhandEingabeUndAktivemMenue("9");
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }


            // Tastenaktionen F-Tasten

            // toggle Menü Hauptmenü
            if (Input.GetKeyDown("f1"))
            {
                if (!generator.rotating)
                {
                    gui.toggleMenue(gui.Hauptmenü.Name, gui.Canvas.AktuellAktivesMenu);
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            // Toggle Menü Innentür
            if (Input.GetKeyDown("f2"))
            {
                if (!generator.rotating)
                {
                    Debug.Log("F2 gedrückt!");
                    gui.toggleMenue(gui.Filtermenü.Name, gui.Canvas.AktuellAktivesMenu);
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }

            // Toggle Menü Konfigurator
            if (Input.GetKeyDown("f3"))
            {
                if (!generator.rotating)
                {
                    gui.toggleMenue(gui.ToggleMenü.Name, gui.Canvas.AktuellAktivesMenu);
                    RefreshTür();
                    generator.tuerAuf = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            generator.Rotation("zu");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            generator.Rotation("auf");
        }
    }

    public void MachF3()
    {
        gui.toggleMenue(gui.ToggleMenü.Name, gui.Canvas.AktuellAktivesMenu);
        RefreshTür();
        generator.tuerAuf = false;
    }
    public void MachTaste0()
    {
        gui.ermitteleAktionAnhandEingabeUndAktivemMenue("0");
        RefreshTür();
        generator.tuerAuf = false;
    }


    public void RefreshTür()
    {
        Debug.Log("Trying to refresh Tür!");
        //Debug.Log(JsonConvert.SerializeObject(gui.AusgewählteInnentür));
        // Achsenspiegelung: aktuellen Status ermitteln
        Vector3 localScale = GameObject.Find("INNENTUER").transform.localScale;

        // Achsenspiegelung: Vor Platzierung der Objektteile kurz in die Ausgangssituation setzen
        GameObject.Find("INNENTUER").transform.localScale = new Vector3(1, 1, 1);
        
        generator.innentuer = gui.AusgewählteInnentür;
        generator.ErsetzeAssetsUndTransformiere();
        generator.RefreshMaterialsForAllComponents();

        // Achsenspiegelung: Nach Platzierung der Objektteile wieder in den aktuellen Status versetzen
        GameObject.Find("INNENTUER").transform.localScale = localScale;

        // Achsenspiegelung: Update des Rotationswinkels
        generator.rotationWinkel = gui.rotationWinkel;
    }

}
