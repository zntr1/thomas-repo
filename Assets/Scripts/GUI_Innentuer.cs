using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public partial class GUI_Innentuer : MonoBehaviour
{
    private readonly AktuelleAnzeigeKonfigurator aktuelleAnzeigeKonfigurator = new AktuelleAnzeigeKonfigurator();

    private DbService dbService = new DbService();

    public AktuelleGetoggelteInnentuer AusgewählteInnentür = new AktuelleGetoggelteInnentuer();
    public AktuelleGetoggeltesMaterial aktuelleGetoggelteMaterial = new AktuelleGetoggeltesMaterial();

    public bool colorLerpAktive;
    private readonly List<T_Innentuer> ergebnisInnentuerNachCheckBekleidungsbreite = new List<T_Innentuer>();
    private readonly List<T_Innentuer> ergebnisInnentuerNachCheckBreite = new List<T_Innentuer>();
    private readonly List<T_Innentuer> ergebnisInnentuerNachCheckHoehe = new List<T_Innentuer>();
    private readonly List<T_Innentuer> ergebnisInnentuerNachCheckKantenrundung = new List<T_Innentuer>();
    private readonly List<T_Innentuer> ergebnisInnentuerNachCheckWandstaerke = new List<T_Innentuer>();

    public GuiCanvas Canvas = new GuiCanvas();
    public GuiHauptmenue Hauptmenü = new GuiHauptmenue();
    public GuiInnentuer Filtermenü = new GuiInnentuer();
    public GuiKonfigurator ToggleMenü = new GuiKonfigurator();

    private readonly HauptmenueOutputParameter hauptmenueOutputParameter = new HauptmenueOutputParameter();

    private readonly HauptmenueParameter hauptmenueParameter = new HauptmenueParameter();

    private List<T_Innentuer> gefilterteInnentueren = new List<T_Innentuer>();
    private readonly InnentuerOutputParameter innentuerOutputParameter = new InnentuerOutputParameter();
    private readonly InnentuerParameter innentuerParameter = new InnentuerParameter();


    // Variablen für den Aufbau der GUI-Elemente
    private Canvas myCanvas;
    private GameObject myGO;
    private Image myImage;
    private Text myText;

    // wird für Logik der Achsenspiegelung benötigt
    public float rotationWinkel = -90.0f;

    private List<T_Band> tabelleBand = new List<T_Band>();
    private List<T_Bandaufnahme> tabelleBandaufnahme = new List<T_Bandaufnahme>();
    private List<T_Druecker> tabelleDruecker = new List<T_Druecker>();
    private List<T_Hersteller> tabelleHersteller = new List<T_Hersteller>();
    private List<T_Innentuer> tabelleInnentuer = new List<T_Innentuer>();
    private List<T_MAT> tabelleMAT = new List<T_MAT>();
    private List<T_Objektteil> tabelleObjektteil = new List<T_Objektteil>();
    private List<T_Schliessblech> tabelleSchliessblech = new List<T_Schliessblech>();
    private List<T_Schlosskasten> tabelleSchlosskasten = new List<T_Schlosskasten>();
    private List<T_Schwelle> tabelleSchwelle = new List<T_Schwelle>();
    private List<T_Tuerblatt> tabelleTuerblatt = new List<T_Tuerblatt>();
    private List<T_Zarge> tabelleZarge = new List<T_Zarge>();


    private List<T_Innentuer> refreshTrefferNachFilter()
    {
        // gehe alle Zeilen der Tabelle Innentuer durch ..
        gefilterteInnentueren.Clear();
        ergebnisInnentuerNachCheckHoehe.Clear();
        ergebnisInnentuerNachCheckBreite.Clear();
        ergebnisInnentuerNachCheckWandstaerke.Clear();
        ergebnisInnentuerNachCheckBekleidungsbreite.Clear();
        ergebnisInnentuerNachCheckKantenrundung.Clear();

        //Debug.Log("-----------ergebnisInnentuer.Count START : " + ergebnisInnentuer.Count.ToString());


        foreach (var zeileInnentuer in tabelleInnentuer)
        {
            foreach (var zeileZarge in tabelleZarge)
            {
                if (zeileZarge.Id == zeileInnentuer.Zarge)
                    if (zeileZarge.HoeheDIN.ToString() == innentuerOutputParameter.HoeheDIN ||
                        innentuerOutputParameter.HoeheDIN == "%")
                    {
                        ergebnisInnentuerNachCheckHoehe.Add(zeileInnentuer);
                    }
            }
        }

        Debug.Log(
            $"-----------ergebnisInnentuerNachCheckHoehe.Count nach Hoehe : {ergebnisInnentuerNachCheckHoehe.Count}");
        Debug.Log($"-----------ergebnisInnentuer.Count nach Hoehe : {gefilterteInnentueren.Count}");
        gefilterteInnentueren = ergebnisInnentuerNachCheckHoehe;

        // wenn es mindesten einen Treffer nach Check Hoehe gab, mache weiter
        if (ergebnisInnentuerNachCheckHoehe.Count > 0)
        {
            foreach (var zeileInnentuerNachCheckHoehe in ergebnisInnentuerNachCheckHoehe)
            {
                foreach (var zeileZarge in tabelleZarge)
                {
                    if (zeileZarge.Id == zeileInnentuerNachCheckHoehe.Zarge)
                    //Debug.Log("-----------innentuerOutputParameter.BreiteDIN) : " + innentuerOutputParameter.BreiteDIN);
                    {
                        if (zeileZarge.BreiteDIN.ToString() != innentuerOutputParameter.BreiteDIN &&
                            innentuerOutputParameter.BreiteDIN != "%") continue;
                        ergebnisInnentuerNachCheckBreite.Add(zeileInnentuerNachCheckHoehe);
                    }
                }
            }

            gefilterteInnentueren = ergebnisInnentuerNachCheckBreite;
            Debug.Log(
                $"-----------ergebnisInnentuerNachCheckBreite.Count nach Breite : {ergebnisInnentuerNachCheckBreite.Count}");
            Debug.Log($"-----------ergebnisInnentuer.Count nach Breite : {gefilterteInnentueren.Count}");

            // wenn es mindesten einen Treffer nach Check Breite gab, mache weiter
            if (ergebnisInnentuerNachCheckBreite.Count > 0)
            {
                foreach (var zeileInnentuerNachCheckBreite in ergebnisInnentuerNachCheckBreite)
                {
                    foreach (var zeileZarge in tabelleZarge)
                    {
                        if (zeileZarge.Id == zeileInnentuerNachCheckBreite.Zarge)
                        //Debug.Log("-----------innentuerOutputParameter.Wandstärke) : " + innentuerOutputParameter.Wandstaerke);
                        {
                            if (zeileZarge.Wandstaerke.ToString() == innentuerOutputParameter.Wandstaerke ||
                                innentuerOutputParameter.Wandstaerke == "%")
                            {
                                ergebnisInnentuerNachCheckWandstaerke.Add(zeileInnentuerNachCheckBreite);
                            }
                        }
                    }
                }

                gefilterteInnentueren = ergebnisInnentuerNachCheckWandstaerke;
                Debug.Log(
                    $"-----------ergebnisInnentuerNachCheckWandstaerke.Count nachWandstaerke : {ergebnisInnentuerNachCheckWandstaerke.Count}");
                Debug.Log($"-----------ergebnisInnentuer.Count nach Wandstaerke : {gefilterteInnentueren.Count}");

                // wenn es mindesten einen Treffer nach Check Wandstaerke gab, mache weiter
                if (ergebnisInnentuerNachCheckWandstaerke.Count > 0)
                {
                    foreach (var zeileInnentuerNachCheckWandstaerke in ergebnisInnentuerNachCheckWandstaerke)
                    {
                        foreach (var zeileZarge in tabelleZarge)
                        {
                            if (zeileZarge.Id == zeileInnentuerNachCheckWandstaerke.Zarge)
                                //Debug.Log("-----------innentuerOutputParameter.Bekleidungsbreite) : " + innentuerOutputParameter.Bekleidungsbreite);
                                if (zeileZarge.Bekleidungsbreite.ToString() == innentuerOutputParameter.Bekleidungsbreite ||
                                    innentuerOutputParameter.Bekleidungsbreite == "%")
                                    ergebnisInnentuerNachCheckBekleidungsbreite.Add(zeileInnentuerNachCheckWandstaerke);
                        }
                    }

                    gefilterteInnentueren = ergebnisInnentuerNachCheckBekleidungsbreite;
                    Debug.Log(
                        $"-----------ergebnisInnentuerNachCheckBekleidungsbreite.Count nach Bekleidungsbreite : {ergebnisInnentuerNachCheckBekleidungsbreite.Count}");
                    Debug.Log(
                        $"-----------ergebnisInnentuer.Count nach Bekleidungsbreite : {gefilterteInnentueren.Count}");


                    // wenn es mindesten einen Treffer nach Check Bekleidungsbreite gab, mache weiter
                    if (ergebnisInnentuerNachCheckBekleidungsbreite.Count > 0)
                    {
                        foreach (var zeileInnentuerNachCheckBekleidungsbreite in
                            ergebnisInnentuerNachCheckBekleidungsbreite)
                        {
                            foreach (var zeileTuerblatt in tabelleTuerblatt)
                            {
                                if (zeileTuerblatt.Id == zeileInnentuerNachCheckBekleidungsbreite.Tuerblatt)
                                {
                                    Debug.Log(
                                        $"-----------innentuerOutputParameter.Kantenrundung) : {innentuerOutputParameter.Kantenrundung}");
                                    if (zeileTuerblatt.Kantenrundung.ToString() == innentuerOutputParameter.Kantenrundung ||
                                        innentuerOutputParameter.Kantenrundung == "%")
                                        ergebnisInnentuerNachCheckKantenrundung.Add(
                                            zeileInnentuerNachCheckBekleidungsbreite);
                                }
                            }
                        }

                        gefilterteInnentueren = ergebnisInnentuerNachCheckKantenrundung;
                        Debug.Log(
                            $"-----------ergebnisInnentuerNachCheckKantenrundung.Count nach Bekleidungsbreite : {ergebnisInnentuerNachCheckKantenrundung.Count}");
                        Debug.Log(
                            $"-----------ergebnisInnentuer.Count nach Bekleidungsbreite : {gefilterteInnentueren.Count}");
                    }
                }
                else
                {
                    gefilterteInnentueren.Clear();
                }
            }
            else
            {
                gefilterteInnentueren.Clear();
            }
        }
        else
        {
            gefilterteInnentueren.Clear();
        }

        return gefilterteInnentueren;
    }

    public void Start()
    {
        // Datenbankabfrage
        DownloadDatabaseTables();

        // GUI Generierung
        generiereGesamteGUI();

        // Konfiguration
        Konfigurationen();


        updateHauptmenue(hauptmenueParameter);
        updateInnentuer(innentuerParameter);

        // lege Startsituation fest
        Ausgangssituation();
    }


    private void Konfigurationen()
    {
        // Hauptmenü
        hauptmenueParameter.Menuepunkt = new List<string>();
        // Bezeichnung der Menüpunkte, wie sie angezeigt werden sollen
        hauptmenueParameter.Menuepunkt.Add("Konfiguratorschema");
        hauptmenueParameter.Menuepunkt.Add("Farbeschema");
        hauptmenueParameter.Menuepunkt.Add("Synchronisation Datenbank");

        // Bezeichnung der zu toggelnden Werte je Untermenü, wie sie angezeigt werden sollen
        hauptmenueParameter.Toggle = new[]
        {
            new[] {"Innentür"},
            new[] {"dunkel", "hell"},
            new[] {""}
        };

        // Bezeichnung der zu toggelnden Werte je Untermenü, wie sie in den Ausgabewerten für
        // [hauptmenueOutputParameter] gesetzt werden sollen
        hauptmenueParameter.ToggleAktion = new[]
        {
            new[] {"Innentür"},
            new[] {"dunkel", "hell"},
            new[] {"Thomas"}
        };

        hauptmenueParameter.TogglePunktIndex = new int[hauptmenueParameter.Menuepunkt.Count];
        hauptmenueParameter.TogglePunktIndex[0] = 0;
        hauptmenueParameter.TogglePunktIndex[1] = 0;
        hauptmenueParameter.TogglePunktIndex[2] = 0;


        // Innentuer
        innentuerParameter.Menuepunkt = new List<string>();
        innentuerParameter.Menuepunkt.Add("Höhe");
        innentuerParameter.Menuepunkt.Add("Breite");
        innentuerParameter.Menuepunkt.Add("Wandstärke");
        innentuerParameter.Menuepunkt.Add("Bekleidungsbreite");
        innentuerParameter.Menuepunkt.Add("Kantenrundung");
        innentuerParameter.Menuepunkt.Add("Öffnungsrichtung");
        innentuerParameter.Menuepunkt.Add("Frontseite");

        innentuerParameter.Toggle = new[]
        {
            //new string[] { "alle", "1875 mm (DIN) | 1885 mm (WÖM) | 1860 mm (TAM)", "2000 mm (DIN) | 2010 mm (WÖM) | 1985 mm (TAM)", "2125 mm (DIN) | 2135 mm (WÖM) | 2110 mm (TAM)", "2250 mm (DIN) | 2260 mm (WÖM) | 2235 mm (TAM)" },
            new[]
            {
                "alle", "2000 mm (DIN) | 2010 mm (WÖM) | 1985 mm (TAM)", "2125 mm (DIN) | 2135 mm (WÖM) | 2110 mm (TAM)"
            },
            //new string[] { "alle", "625 mm (DIN) | 635 mm (WÖM) | 610 mm (TAM)", "750 mm (DIN) | 760 mm (WÖM) | 735 mm (TAM)", "875 mm (DIN) | 885 mm (WÖM) | 860 mm (TAM)", "1000 mm (DIN) | 1010 mm (WÖM) | 985 mm (TAM)", "1125 mm (DIN) | 1135 mm (WÖM) | 1110 mm (TAM)" },
            new[]
            {
                "alle", "750 mm (DIN) | 760 mm (WÖM) | 735 mm (TAM)", "875 mm (DIN) | 885 mm (WÖM) | 860 mm (TAM)",
                "1000 mm (DIN) | 1010 mm (WÖM) | 985 mm (TAM)"
            },
            new[] {"alle", "115 mm", "180 mm", "200 mm", "265 mm", "300 mm"},
            new[] {"alle", "50 mm"},
            new[] {"alle", "keine", "8 mm"},
            new[] {"DIN links", "DIN rechts"},
            new[] {"Falzfront", "Zierfront"}
        };

        innentuerParameter.ToggleAktion = new[]
        {
            //new string[] { "%", "1875", "2000", "2125", "2250" },
            new[] {"%", "2000", "2125"},
            //new string[] { "%", "625", "750", "875", "1000", "1125" },
            new[] {"%", "750", "875", "1000"},
            new[] {"%", "115", "180", "200", "265", "300"},
            new[] {"%", "50"},
            new[] {"%", "0", "8"},
            new[] {"DIN links", "DIN rechts"},
            new[] {"Falzfront", "Zierfront"}
        };

        innentuerParameter.TogglePunktIndex = new int[innentuerParameter.Menuepunkt.Count];
        for (var i = 0; i < innentuerParameter.Menuepunkt.Count; i++) innentuerParameter.TogglePunktIndex[i] = 0;

        aktuelleAnzeigeKonfigurator.ZeileHeader = new string[ToggleMenü.ZeileContainerAnzahlZeilen];
        for (var i = 0; i < ToggleMenü.ZeileContainerAnzahlZeilen; i++)
            aktuelleAnzeigeKonfigurator.ZeileHeader[i] = "";

        aktuelleAnzeigeKonfigurator.ZeileDetail = new string[ToggleMenü.ZeileContainerAnzahlZeilen];
        for (var i = 0; i < ToggleMenü.ZeileContainerAnzahlZeilen; i++)
            aktuelleAnzeigeKonfigurator.ZeileDetail[i] = "";

        aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex = new int[ToggleMenü.ZeileContainerAnzahlZeilen];
        for (var i = 0; i < ToggleMenü.ZeileContainerAnzahlZeilen; i++)
            aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[i] = 0;

        aktuelleAnzeigeKonfigurator.AktuellesMaterialName = new string[ToggleMenü.ZeileContainerAnzahlZeilen];
        for (var i = 0; i < ToggleMenü.ZeileContainerAnzahlZeilen; i++)
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[i] = "";

        aktuelleAnzeigeKonfigurator.AnzahlMaterialien = new int[ToggleMenü.ZeileContainerAnzahlZeilen];
        for (var i = 0; i < ToggleMenü.ZeileContainerAnzahlZeilen; i++)
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[i] = 0;
    }


    // Konfiguration der Ausgangssituation
    private void Ausgangssituation()
    {
        // wird beim Toggeln zwischen den Menüpunkten (Hauptmenue, Innentuer und Konfigurator) benötigt    
        Canvas.AktuellAktivesMenu = "";

        AusgewählteInnentür.Index = 0;

        setzeFarbschema(hauptmenueOutputParameter.Farbeschema);

        innentuerOutputParameter.HoeheDIN = "%";
        innentuerOutputParameter.BreiteDIN = "%";
        innentuerOutputParameter.Oeffnungsrichtung = "%";
        innentuerOutputParameter.Wandstaerke = "%";
        innentuerOutputParameter.Bekleidungsbreite = "%";
        innentuerOutputParameter.Kantenrundung = "%";


        gefilterteInnentueren = refreshTrefferNachFilter();
        GameObject.Find(Filtermenü.InfoTextNameInCanvas).GetComponent<Text>().text =
            $"Treffer in der Datenbank mit aktuellen Kriterien: {gefilterteInnentueren.Count}";


        // blende alle Menüs aus
        GameObject.Find("Hauptmenue").GetComponent<CanvasGroup>().alpha = 0.0f;
        GameObject.Find("Innentuer").GetComponent<CanvasGroup>().alpha = 0.0f;
        GameObject.Find("Konfigurator").GetComponent<CanvasGroup>().alpha = 0.0f;

        GameObject.Find($"{Hauptmenü.ZeileDetailNameInCanvas}3").GetComponent<Text>().text =
            $"letzte Aktualisierung : {DateTime.Now:dd.MM.yyyy | HH:mm:ss}";

        AusgewählteInnentür.Schwelle.Bezeichnung = "";
        aktuelleGetoggelteMaterial.Schwelle = "";

        setzeFarbschema("dunkel");
    }


    // schreibe aus allen DB-Tabellen die Daten in lokale Tabellen

    private void DownloadDatabaseTables()
    {
        tabelleBand = dbService.GetBänder();
        tabelleBandaufnahme = dbService.GetBandaufnahmen();
        tabelleDruecker = dbService.GetDrücker();
        tabelleSchliessblech = dbService.GetSchliessbleche();
        tabelleSchlosskasten = dbService.GetSchlosskästen();
        tabelleSchwelle = dbService.GetSchwellen();
        tabelleTuerblatt = dbService.GetTürblätter();
        tabelleZarge = dbService.GetZargen();
        tabelleInnentuer = dbService.GetInnentüren();
        tabelleHersteller = dbService.GetHersteller();
        tabelleMAT = dbService.GetMaterials();
        tabelleObjektteil = dbService.GetObjektteile();
    }


    private void setzeFarbschema(string schema)
    {
        Debug.Log($"Setze Farbschema = {schema}");


        switch (schema)
        {
            case "dunkel":

                Hauptmenü.ZeileContainerColorToggle = new Color32(0, 0, 0, 255);

                // Hauptmenue
                GameObject.Find("Hauptmenue").GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                GameObject.Find("HauptmenueHeaderContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 250);
                GameObject.Find("HauptmenueHeaderLogo").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("HauptmenueHeaderText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("HauptmenueInfoContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 230);
                GameObject.Find("HauptmenueInfoLogo").GetComponent<Image>().color =
                    new Color32(0, 0, 0, 0); // ausgeblendet
                GameObject.Find("HauptmenueInfoText").GetComponent<Text>().color = new Color32(255, 255, 255, 50);
                GameObject.Find("HauptmenueHauptContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 100);

                for (var i = 1; i <= Hauptmenü.ZeileContainerAnzahlZeilen; i++)
                {
                    GameObject.Find($"HauptmenueZeileContainer{i}").GetComponent<Image>().color =
                        new Color32(0, 0, 0, 100);
                    GameObject.Find($"HauptmenueZeileHeader{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 255, 255);
                    GameObject.Find($"HauptmenueZeileDetail{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 200, 255);
                    GameObject.Find($"HauptmenueZeileDetailLogo{i}").GetComponent<Image>().color =
                        new Color32(0, 0, 0, 150);
                    GameObject.Find($"HauptmenueZeileNummer{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 255, 255);
                }

                // Innentuer
                GameObject.Find("Innentuer").GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                GameObject.Find("InnentuerHeaderContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 250);
                GameObject.Find("InnentuerHeaderLogo").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("InnentuerHeaderText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("InnentuerInfoContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 230);
                GameObject.Find("InnentuerInfoLogo").GetComponent<Image>().color =
                    new Color32(0, 0, 0, 0); // ausgeblendet
                GameObject.Find("InnentuerInfoText").GetComponent<Text>().color = new Color32(255, 255, 255, 50);
                GameObject.Find("InnentuerHauptContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 100);

                for (var i = 1; i <= Filtermenü.ZeileContainerAnzahlZeilen; i++)
                {
                    GameObject.Find($"InnentuerZeileContainer{i}").GetComponent<Image>().color =
                        new Color32(0, 0, 0, 100);
                    GameObject.Find($"InnentuerZeileHeader{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 255, 255);
                    GameObject.Find($"InnentuerZeileDetail{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 200, 255);
                    GameObject.Find($"InnentuerZeileDetailLogo{i}").GetComponent<Image>().color =
                        new Color32(0, 0, 0, 150);
                    GameObject.Find($"InnentuerZeileNummer{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 255, 255);
                }

                // Konfigurator
                GameObject.Find("Konfigurator").GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                GameObject.Find("KonfiguratorHeaderContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 250);
                GameObject.Find("KonfiguratorHeaderLogo").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("KonfiguratorHeaderText").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("KonfiguratorInfoContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 230);
                GameObject.Find("KonfiguratorInfoLogo").GetComponent<Image>().color =
                    new Color32(0, 0, 0, 0); // ausgeblendet
                GameObject.Find("KonfiguratorInfoText").GetComponent<Text>().color = new Color32(255, 255, 255, 50);
                GameObject.Find("KonfiguratorInfoNummer").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                GameObject.Find("KonfiguratorHauptContainer").GetComponent<Image>().color = new Color32(0, 0, 0, 100);

                for (var i = 1; i <= ToggleMenü.ZeileContainerAnzahlZeilen; i++)
                {
                    GameObject.Find($"KonfiguratorZeileContainer{i}").GetComponent<Image>().color =
                        new Color32(0, 0, 0, 100);
                    GameObject.Find($"KonfiguratorZeileHeader{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 255, 255);
                    GameObject.Find($"KonfiguratorZeileDetail{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 200, 255);
                    GameObject.Find($"KonfiguratorZeileDetailLogo{i}").GetComponent<Image>().color =
                        new Color32(0, 0, 0, 150);
                    GameObject.Find($"KonfiguratorZeileNummer{i}").GetComponent<Text>().color =
                        new Color32(255, 255, 255, 255);
                }

                for (var i = 1; i <= ToggleMenü.ZeileContainerAnzahlZeilen; i++)
                    for (var materialNr = 1;
                        materialNr <= ToggleMenü.ZeileMaterialAnzahlMaterialOptionenX *
                        ToggleMenü.ZeileMaterialAnzahlMaterialOptionenY;
                        materialNr++)
                        GameObject.Find($"KonfiguratorZeileMaterial{i}{materialNr}").GetComponent<Image>().color =
                            new Color32(255, 255, 255, 255);


                break;
            case "hell":

                Hauptmenü.ZeileContainerColorToggle = new Color32(255, 255, 255, 255);

                // Hauptmenue
                GameObject.Find("Hauptmenue").GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                GameObject.Find("HauptmenueHeaderContainer").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 250);
                GameObject.Find("HauptmenueHeaderLogo").GetComponent<Image>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("HauptmenueHeaderText").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("HauptmenueInfoContainer").GetComponent<Image>().color = new Color32(255, 200, 100, 50);
                GameObject.Find("HauptmenueInfoLogo").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 0); // ausgeblendet
                GameObject.Find("HauptmenueInfoText").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("HauptmenueHauptContainer").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 100);

                for (var i = 1; i <= Hauptmenü.ZeileContainerAnzahlZeilen; i++)
                {
                    GameObject.Find($"HauptmenueZeileContainer{i}").GetComponent<Image>().color =
                        new Color32(255, 255, 255, 100);
                    GameObject.Find($"HauptmenueZeileHeader{i}").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                    GameObject.Find($"HauptmenueZeileDetail{i}").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                    GameObject.Find($"HauptmenueZeileDetailLogo{i}").GetComponent<Image>().color =
                        new Color32(255, 200, 100, 150);
                    GameObject.Find($"HauptmenueZeileNummer{i}").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                }

                // Innentuer
                GameObject.Find("Innentuer").GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                GameObject.Find("InnentuerHeaderContainer").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 250);
                GameObject.Find("InnentuerHeaderLogo").GetComponent<Image>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("InnentuerHeaderText").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("InnentuerInfoContainer").GetComponent<Image>().color = new Color32(255, 200, 100, 50);
                GameObject.Find("InnentuerInfoLogo").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 0); // ausgeblendet
                GameObject.Find("InnentuerInfoText").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("InnentuerHauptContainer").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 100);

                for (var i = 1; i <= Filtermenü.ZeileContainerAnzahlZeilen; i++)
                {
                    GameObject.Find($"InnentuerZeileContainer{i}").GetComponent<Image>().color =
                        new Color32(255, 255, 255, 100);
                    GameObject.Find($"InnentuerZeileHeader{i}").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                    GameObject.Find($"InnentuerZeileDetail{i}").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                    GameObject.Find($"InnentuerZeileDetailLogo{i}").GetComponent<Image>().color =
                        new Color32(255, 200, 100, 150);
                    GameObject.Find($"InnentuerZeileNummer{i}").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                }

                // Konfigurator
                GameObject.Find("Konfigurator").GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                GameObject.Find("KonfiguratorHeaderContainer").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 250);
                GameObject.Find("KonfiguratorHeaderLogo").GetComponent<Image>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("KonfiguratorHeaderText").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("KonfiguratorInfoContainer").GetComponent<Image>().color =
                    new Color32(255, 200, 100, 50);
                GameObject.Find("KonfiguratorInfoLogo").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 0); // ausgeblendet
                GameObject.Find("KonfiguratorInfoText").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("KonfiguratorInfoNummer").GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                GameObject.Find("KonfiguratorHauptContainer").GetComponent<Image>().color =
                    new Color32(255, 255, 255, 100);

                for (var i = 1; i <= ToggleMenü.ZeileContainerAnzahlZeilen; i++)
                {
                    GameObject.Find($"KonfiguratorZeileContainer{i}").GetComponent<Image>().color =
                        new Color32(255, 255, 255, 100);
                    GameObject.Find($"KonfiguratorZeileHeader{i}").GetComponent<Text>().color =
                        new Color32(0, 0, 0, 255);
                    GameObject.Find($"KonfiguratorZeileDetail{i}").GetComponent<Text>().color =
                        new Color32(0, 0, 0, 255);
                    GameObject.Find($"KonfiguratorZeileDetailLogo{i}").GetComponent<Image>().color =
                        new Color32(255, 200, 100, 150);
                    GameObject.Find($"KonfiguratorZeileNummer{i}").GetComponent<Text>().color =
                        new Color32(0, 0, 0, 255);
                }

                for (var i = 1; i <= ToggleMenü.ZeileContainerAnzahlZeilen; i++)
                    for (var materialNr = 1;
                        materialNr <= ToggleMenü.ZeileMaterialAnzahlMaterialOptionenX *
                        ToggleMenü.ZeileMaterialAnzahlMaterialOptionenY;
                        materialNr++)
                        GameObject.Find($"KonfiguratorZeileMaterial{i}{materialNr}").GetComponent<Image>().color =
                            new Color32(0, 0, 0, 200);

                break;
            default:
                setzeFarbschema("dunkel");
                break;
        }
    }


    // Generiere Gesamte GUI
    private void generiereGesamteGUI()
    {
        //// GUI: Rahmen
        generiereGuiCanvas();

        //// GUI: Hauptmenue
        generiereHauptmenueContainer();
        generiereHauptmenueHeaderContainer();
        generiereHauptmenueHeaderLogo();
        generiereHauptmenueHeaderText();
        generiereHauptmenueInfoContainer();
        generiereHauptmenueInfoText();
        generiereHauptmenueInfoLogo();
        generiereHauptmenueHauptContainer();
        generiereHauptmenueHauptContainerZeilen(Hauptmenü.ZeileContainerAnzahlZeilen);

        //// GUI: Innentuer
        generiereInnentuerContainer();
        generiereInnentuerHeaderContainer();
        generiereInnentuerHeaderLogo();
        generiereInnentuerHeaderText();
        generiereInnentuerInfoContainer();
        generiereInnentuerInfoText();
        generiereInnentuerInfoLogo();
        generiereInnentuerHauptContainer();
        generiereInnentuerHauptContainerZeilen(Filtermenü.ZeileContainerAnzahlZeilen);

        // GUI: Konfigurator
        generiereKonfiguratorContainer();
        generiereKonfiguratorHeaderContainer();
        generiereKonfiguratorHeaderLogo();
        generiereKonfiguratorHeaderText();
        generiereKonfiguratorInfoContainer();
        generiereKonfiguratorInfoText();
        generiereKonfiguratorInfoLogo();
        generiereKonfiguratorInfoNummer();
        generiereKonfiguratorHauptContainer();
        generiereKonfiguratorHauptContainerZeilen(ToggleMenü.ZeileContainerAnzahlZeilen);
    }

    public void ermitteleAktionAnhandEingabeUndAktivemMenue(string eingabe)
    {
        switch (eingabe)
        {
            case "0":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("10");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("20");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name)
                        fuehreAktionAus("30");
                    break;
                }
            case "1":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("11");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("21");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("31");
                    break;
                }
            case "2":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("12");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("22");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("32");
                    break;
                }
            case "3":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("13");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("23");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("33");
                    break;
                }
            case "4":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("14");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("24");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("34");
                    break;
                }
            case "5":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("15");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("25");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("35");
                    break;
                }
            case "6":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("16");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("26");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("36");
                    break;
                }
            case "7":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("17");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("27");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("37");
                    break;
                }
            case "8":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("18");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("28");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("38");
                    break;
                }
            case "9":
                {
                    if (Canvas.AktuellAktivesMenu == Hauptmenü.Name)
                        fuehreAktionAus("19");
                    else if (Canvas.AktuellAktivesMenu == Filtermenü.Name)
                        fuehreAktionAus("29");
                    else if (Canvas.AktuellAktivesMenu == ToggleMenü.Name) fuehreAktionAus("39");
                    break;
                }
            default:
                {
                    Debug.Log("ACHTUNG: KEINE AKTION HINTERLEGT");
                    break;
                }
        }
    }


    // Konfiguration der zu den jeweiligen Menüpunkten zugehörigen auszuführenden Aktionen
    private void fuehreAktionAus(string aktion)
    {
        //Debug.Log("Führe Aktion: " + aktion + " aus");

        switch (aktion)
        {
            case "11":
                {
                    toggleMenuePunkt(1, 1);
                    hauptmenueOutputParameter.Konfiguratorschema =
                        hauptmenueParameter.ToggleAktion[0][hauptmenueParameter.TogglePunktIndex[0]];
                    StartCoroutine(LerpColor($"{Hauptmenü.ZeileContainerNameInCanvas}1"));
                    //Debug.Log("Setze Konfiguratorschema = " + hauptmenueOutputParameter.Konfiguratorschema);
                    break;
                }
            case "12":
                {
                    toggleMenuePunkt(1, 2);
                    hauptmenueOutputParameter.Farbeschema =
                        hauptmenueParameter.ToggleAktion[1][hauptmenueParameter.TogglePunktIndex[1]];
                    setzeFarbschema(hauptmenueOutputParameter.Farbeschema);
                    StartCoroutine(LerpColor($"{Hauptmenü.ZeileContainerNameInCanvas}2"));
                    //Debug.Log("Setze Farbschema = " + hauptmenueOutputParameter.Farbeschema);
                    break;
                }
            case "13":
                {
                    toggleMenuePunkt(1, 3);
                    hauptmenueOutputParameter.Datenbank =
                        hauptmenueParameter.ToggleAktion[2][hauptmenueParameter.TogglePunktIndex[2]];
                    StartCoroutine(LerpColor($"{Hauptmenü.ZeileContainerNameInCanvas}3"));
                    //Debug.Log("Synchronisation Datenbank = " + hauptmenueOutputParameter.Datenbank);
                    // Spezial
                    DownloadDatabaseTables();
                    GameObject.Find($"{Hauptmenü.ZeileDetailNameInCanvas}3").GetComponent<Text>().text =
                        $"letzte Aktualisierung : {DateTime.Now:dd.MM.yyyy | HH:mm:ss}";
                    //
                    break;
                }
            case "21":
                {
                    toggleMenuePunkt(2, 1);
                    innentuerOutputParameter.HoeheDIN =
                        innentuerParameter.ToggleAktion[0][innentuerParameter.TogglePunktIndex[0]];
                    StartCoroutine(LerpColor($"{Filtermenü.ZeileContainerNameInCanvas}1"));
                    //Debug.Log("Setze HoeheDIN = " + innentuerOutputParameter.HoeheDIN);
                    gefilterteInnentueren = refreshTrefferNachFilter();
                    GameObject.Find(Filtermenü.InfoTextNameInCanvas).GetComponent<Text>().text =
                        $"Treffer in der Datenbank mit aktuellen Kriterien: {gefilterteInnentueren.Count}";
                    break;
                }
            case "22":
                {
                    toggleMenuePunkt(2, 2);
                    innentuerOutputParameter.BreiteDIN =
                        innentuerParameter.ToggleAktion[1][innentuerParameter.TogglePunktIndex[1]];
                    StartCoroutine(LerpColor($"{Filtermenü.ZeileContainerNameInCanvas}2"));
                    //Debug.Log("Setze BreiteDIN = " + innentuerOutputParameter.BreiteDIN);
                    gefilterteInnentueren = refreshTrefferNachFilter();
                    GameObject.Find(Filtermenü.InfoTextNameInCanvas).GetComponent<Text>().text =
                        $"Treffer in der Datenbank mit aktuellen Kriterien: {gefilterteInnentueren.Count}";
                    break;
                }
            case "23":
                {
                    toggleMenuePunkt(2, 3);
                    innentuerOutputParameter.Wandstaerke =
                        innentuerParameter.ToggleAktion[2][innentuerParameter.TogglePunktIndex[2]];
                    StartCoroutine(LerpColor($"{Filtermenü.ZeileContainerNameInCanvas}3"));
                    //Debug.Log("Setze Wandstärke = " + innentuerOutputParameter.Wandstaerke);
                    gefilterteInnentueren = refreshTrefferNachFilter();
                    GameObject.Find(Filtermenü.InfoTextNameInCanvas).GetComponent<Text>().text =
                        $"Treffer in der Datenbank mit aktuellen Kriterien: {gefilterteInnentueren.Count}";
                    break;
                }
            case "24":
                {
                    toggleMenuePunkt(2, 4);
                    innentuerOutputParameter.Bekleidungsbreite =
                        innentuerParameter.ToggleAktion[3][innentuerParameter.TogglePunktIndex[3]];
                    StartCoroutine(LerpColor($"{Filtermenü.ZeileContainerNameInCanvas}4"));
                    //Debug.Log("Setze Bekleidungsbreite = " + innentuerOutputParameter.Bekleidungsbreite);
                    gefilterteInnentueren = refreshTrefferNachFilter();
                    GameObject.Find(Filtermenü.InfoTextNameInCanvas).GetComponent<Text>().text =
                        $"Treffer in der Datenbank mit aktuellen Kriterien: {gefilterteInnentueren.Count}";
                    break;
                }
            case "25":
                {
                    toggleMenuePunkt(2, 5);
                    innentuerOutputParameter.Kantenrundung =
                        innentuerParameter.ToggleAktion[4][innentuerParameter.TogglePunktIndex[4]];
                    StartCoroutine(LerpColor($"{Filtermenü.ZeileContainerNameInCanvas}5"));
                    //Debug.Log("Setze Kantenrundung = " + innentuerOutputParameter.Kantenrundung);
                    gefilterteInnentueren = refreshTrefferNachFilter();
                    GameObject.Find(Filtermenü.InfoTextNameInCanvas).GetComponent<Text>().text =
                        $"Treffer in der Datenbank mit aktuellen Kriterien: {gefilterteInnentueren.Count}";
                    break;
                }
            case "26":
                {
                    toggleMenuePunkt(2, 6);
                    innentuerOutputParameter.Oeffnungsrichtung =
                        innentuerParameter.ToggleAktion[5][innentuerParameter.TogglePunktIndex[5]];
                    StartCoroutine(LerpColor($"{Filtermenü.ZeileContainerNameInCanvas}6"));
                    //Debug.Log("Setze Öffnungsrichtung = " + innentuerOutputParameter.Oeffnungsrichtung);
                    toggleAchsenspiegelung("x");
                    break;
                }
            case "27":
                {
                    toggleMenuePunkt(2, 7);
                    innentuerOutputParameter.Frontseite =
                        innentuerParameter.ToggleAktion[6][innentuerParameter.TogglePunktIndex[6]];
                    StartCoroutine(LerpColor($"{Filtermenü.ZeileContainerNameInCanvas}7"));
                    //Debug.Log("Setze Frontseite = " + innentuerOutputParameter.Frontseite);
                    toggleAchsenspiegelung("z");
                    break;
                }
            case "30":
                {
                    toggleMenuePunkt(3, 0);
                    StartCoroutine(LerpColor(ToggleMenü.InfoContainerNameInCanvas));
                    //Debug.Log("Toggle Innentuer = " + "");
                    toggleInnentuerImKonfigurator();
                    break;
                }
            case "31":
                {
                    toggleMenuePunkt(3, 1);
                    StartCoroutine(LerpColor($"{ToggleMenü.ZeileContainerNameInCanvas}1"));
                    //Debug.Log("Toggle Material Zarge = " + "");
                    toggleMaterialImKonfigurator(1);
                    toggleMaterialImKonfigurator(2); // Kombitoggeln Tuerblatt une Zarge
                    break;
                }
            case "32":
                {
                    toggleMenuePunkt(3, 2);
                    StartCoroutine(LerpColor($"{ToggleMenü.ZeileContainerNameInCanvas}2"));
                    //Debug.Log("Toggle Material Tuerblatt = " + "");
                    toggleMaterialImKonfigurator(2);
                    toggleMaterialImKonfigurator(1); // Kombitoggeln Tuerblatt une Zarge
                    break;
                }
            case "33":
                {
                    toggleMenuePunkt(3, 3);
                    StartCoroutine(LerpColor($"{ToggleMenü.ZeileContainerNameInCanvas}3"));
                    //Debug.Log("Toggle Material DrueckerFalz = " + "");
                    toggleMaterialImKonfigurator(3);
                    break;
                }
            case "34":
                {
                    toggleMenuePunkt(3, 4);
                    StartCoroutine(LerpColor($"{ToggleMenü.ZeileContainerNameInCanvas}4"));
                    //Debug.Log("Toggle Material Band1 = " + "");
                    toggleMaterialImKonfigurator(4);
                    break;
                }
            default:
                {
                    Debug.Log("ACHTUNG: KEINE AKTION DEFINIERT");
                    break;
                }
        }
    }


    // Update Materials
    private void übergibParameterAusMenüInnentuerInMenüKonfigurator(List<T_Innentuer> gefilterteInnentueren)
    {
        //Debug.Log("ergebnisInnentuer :" + ergebnisInnentuer.Count.ToString());

        string matJsonString;


        aktuelleAnzeigeKonfigurator.InfoText = "Innentür";
        if (gefilterteInnentueren.Count == 0)
        {
            aktuelleAnzeigeKonfigurator.InfoText += "\nKeine Treffer bei den aktuellen Suchkriterien.";
            VersteckeKonfiguratorMenüEinträge();
        }
        else
        {
            aktuelleAnzeigeKonfigurator.InfoText += $" ({AusgewählteInnentür.Index + 1} von {gefilterteInnentueren.Count})";
            aktuelleAnzeigeKonfigurator.InfoText += $"\n{gefilterteInnentueren[AusgewählteInnentür.Index].Detail}";

            // Zeile 1 Zarge
            var lokaleZarge = gefilterteInnentueren[AusgewählteInnentür.Index].Zarge;
            if (lokaleZarge != null) { RefreshDetailsAndUpdateMaterialsForZarge(lokaleZarge); }

            // Zeile 2 Tuerblatt
            var lokalesTuerblatt = gefilterteInnentueren[AusgewählteInnentür.Index].Tuerblatt;
            if (lokalesTuerblatt != null) {
                RefreshDetailsAndUpdateMaterialsForTürblatt(lokalesTuerblatt); }

            // Zeile 3 DrueckerFalz
            var lokaleDrueckerFalz = gefilterteInnentueren[AusgewählteInnentür.Index].DrueckerFalz;
            if (lokaleDrueckerFalz != null) { RefreshDetailsAndUpdateMaterialsForDrueckerFalz(lokaleDrueckerFalz);}

            // Zeile 4 DrueckerZier
            var lokaleDrueckerZier = gefilterteInnentueren[AusgewählteInnentür.Index].DrueckerZier;
            if (lokaleDrueckerZier != null) { RefreshDetailsAndUpdateMaterialsForDrueckerZier(lokaleDrueckerZier); }

            // Zeile 5 Band1
            var lokalesBand1 = gefilterteInnentueren[AusgewählteInnentür.Index].Band1;
            if (lokalesBand1 != null) { RefreshDetailsAndUpdateMaterialsForBand1(lokalesBand1); }

            // Zeile 6 Band2
            var lokalesBand2 = gefilterteInnentueren[AusgewählteInnentür.Index].Band2;
            if (lokalesBand2 != null) { RefreshDetailsAndUpdateMaterialsForBand2(lokalesBand2); }

            // Zeile 7 Bandaufnahme1
            var lokaleBandaufnahme1 = gefilterteInnentueren[AusgewählteInnentür.Index].Bandaufnahme1;
            if (lokaleBandaufnahme1 != null) { RefreshDetailsAndUpdateMaterialsForBandaufnahme1(lokaleBandaufnahme1);}

            // Zeile 8 Bandaufnahme2
            var lokaleBandaufnahme2 = gefilterteInnentueren[AusgewählteInnentür.Index].Bandaufnahme2;
            if (lokaleBandaufnahme2 != null) { RefreshDetailsAndUpdateMaterialsForBandaufnahme2(lokaleBandaufnahme2);}

            // Zeile 9 Schlosskasten
            var lokalerSchlosskasten = gefilterteInnentueren[AusgewählteInnentür.Index].Schlosskasten;
            if (lokalerSchlosskasten != null) { RefreshDetailsAndUpdateMaterialsForSchlosskasten(lokalerSchlosskasten); }

            // Zeile 10 Schliessblech
            var lokalesSchliessblech = gefilterteInnentueren[AusgewählteInnentür.Index].Schliessblech;
            if (lokalesSchliessblech != null) { RefreshDetailsAndUpdateMaterialsForSchliessblech(lokalesSchliessblech); }

            // Zeile 10 Schwelle
            var lokaleSchwelle = gefilterteInnentueren[AusgewählteInnentür.Index].Schwelle;
            if (lokaleSchwelle != null) { RefreshDetailsAndUpdateMaterialsForSchwelle(lokaleSchwelle); }

            updateKonfigurator();
        }
    }


    // Refresh And Update Türobjekte
    private void RefreshDetailsAndUpdateMaterialsForSchwelle(string lokaleSchwelle)
    {
        T_Objektteil schwelleGrob = tabelleObjektteil.Find(x => x.Id == lokaleSchwelle);
        RefreshDetailsForSchwelle(schwelleGrob);
        ErmittleMaterialPfadeAusMaterialJson(schwelleGrob, AusgewählteInnentür.Schwelle, 10);
    }
    private void RefreshDetailsAndUpdateMaterialsForSchliessblech(string lokalesSchliessblech)
    {
        T_Objektteil schliessblechGrob = tabelleObjektteil.Find(x => x.Id == lokalesSchliessblech);
        RefreshDetailsForSchliessblech(schliessblechGrob);
        ErmittleMaterialPfadeAusMaterialJson(schliessblechGrob, AusgewählteInnentür.Schliessblech, 9);
    }
    private void RefreshDetailsAndUpdateMaterialsForSchlosskasten(string lokalerSchlosskasten)
    {
        T_Objektteil schlosskastenGrob = tabelleObjektteil.Find(x => x.Id == lokalerSchlosskasten);
        RefreshDetailsForSchlosskasten(schlosskastenGrob);
        ErmittleMaterialPfadeAusMaterialJson(schlosskastenGrob, AusgewählteInnentür.Schlosskasten, 8);
    }
    private void RefreshDetailsAndUpdateMaterialsForBandaufnahme2(string lokalesBandaufnahme2)
    {
        T_Objektteil bandaufnahme2Grob = tabelleObjektteil.Find(x => x.Id == lokalesBandaufnahme2);
        RefreshDetailsForBandaufnahme2(bandaufnahme2Grob);
        ErmittleMaterialPfadeAusMaterialJson(bandaufnahme2Grob, AusgewählteInnentür.Bandaufnahme2, 7);
    }
    private void RefreshDetailsAndUpdateMaterialsForBandaufnahme1(string lokalesBandaufnahme1)
    {
        T_Objektteil bandaufnahme1Grob = tabelleObjektteil.Find(x => x.Id == lokalesBandaufnahme1);
        RefreshDetailsForBandaufnahme1(bandaufnahme1Grob);
        ErmittleMaterialPfadeAusMaterialJson(bandaufnahme1Grob, AusgewählteInnentür.Bandaufnahme1, 6);
    }
    private void RefreshDetailsAndUpdateMaterialsForBand2(string lokalesBand2)
    {
        T_Objektteil band2Grob = tabelleObjektteil.Find(x => x.Id == lokalesBand2);
        RefreshDetailsForBand2(band2Grob);
        ErmittleMaterialPfadeAusMaterialJson(band2Grob, AusgewählteInnentür.Band2, 5);
    }
    private void RefreshDetailsAndUpdateMaterialsForBand1(string lokalesBand1)
    {
        //aktuelleAnzeigeKonfigurator.ZeileHeader[4] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1;
        T_Objektteil band1Grob = tabelleObjektteil.Find(x => x.Id == lokalesBand1);

        RefreshDetailsForBand1(band1Grob);
        ErmittleMaterialPfadeAusMaterialJson(band1Grob, AusgewählteInnentür.Band1, 4);
    }
    private void RefreshDetailsAndUpdateMaterialsForDrueckerZier(string lokaleDrueckerZier)
    {
        T_Objektteil drueckerZierGrob = tabelleObjektteil.Find(x => x.Id == lokaleDrueckerZier);
        T_Druecker drueckerZierDetail = tabelleDruecker.Find(x => x.Id == lokaleDrueckerZier);

        RefreshDetailsForDrueckerZier(drueckerZierGrob, drueckerZierDetail);
        ErmittleMaterialPfadeAusMaterialJson(drueckerZierGrob, AusgewählteInnentür.DrueckerZier, 3);
    }
    private void RefreshDetailsAndUpdateMaterialsForDrueckerFalz(string lokaleDrueckerFalz)
    {
        T_Objektteil drueckerFalzGrob = tabelleObjektteil.Find(x => x.Id == lokaleDrueckerFalz);
        T_Druecker drueckerFalzDetail = tabelleDruecker.Find(x => x.Id == lokaleDrueckerFalz);

        RefreshDetailsForDrueckerFalz(drueckerFalzGrob, drueckerFalzDetail);
        ErmittleMaterialPfadeAusMaterialJson(drueckerFalzGrob, AusgewählteInnentür.DrueckerFalz, 2);
    }
    private void RefreshDetailsAndUpdateMaterialsForTürblatt(string lokalesTuerblatt)
    {
        T_Objektteil tuerblattGrob = tabelleObjektteil.Find(x => x.Id == lokalesTuerblatt);
        T_Tuerblatt tuerblattDetail = tabelleTuerblatt.Find(x => x.Id == lokalesTuerblatt);

        RefreshDetailsForTürblatt(tuerblattGrob, tuerblattDetail);
        ErmittleMaterialPfadeAusMaterialJson(tuerblattGrob, AusgewählteInnentür.Tuerblatt, 1);
    }
    private void RefreshDetailsForTürblatt(T_Objektteil tuerblattGrob, T_Tuerblatt tuerblattDetail)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[1] = "Türblatt: ";
        aktuelleAnzeigeKonfigurator.ZeileDetail[1] = tuerblattGrob.Detail;
        aktuelleAnzeigeKonfigurator.ZeileDetail[1] += $" | Hersteller: {tuerblattGrob.Hersteller}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[1] += $" | Gewicht: {tuerblattGrob.Gewicht} g";
        aktuelleAnzeigeKonfigurator.ZeileDetail[1] += $"\nDIN HxB: {tuerblattDetail.HoeheDIN}x";
        aktuelleAnzeigeKonfigurator.ZeileDetail[1] += $"{tuerblattDetail.BreiteDIN} mm";
        aktuelleAnzeigeKonfigurator.ZeileDetail[1] += $" | Kantenrundung: {tuerblattDetail.Kantenrundung} mm";
        aktuelleAnzeigeKonfigurator.ZeileDetail[1] += $" | Drückerhöhe: {tuerblattDetail.Drueckerhoehe} mm";
    }
    private void RefreshDetailsAndUpdateMaterialsForZarge(string lokaleZarge)
    {
        T_Objektteil zargeGrob = tabelleObjektteil.Find(x => x.Id == lokaleZarge);
        T_Zarge zargeDetail = tabelleZarge.Find(x => x.Id == lokaleZarge);

        RefreshDetailsForZarge(zargeGrob, zargeDetail);
        ErmittleMaterialPfadeAusMaterialJson(zargeGrob, AusgewählteInnentür.Zarge, 0);
    }


    // Refresh UI-Details
    private void RefreshDetailsForSchwelle(T_Objektteil schwelleGrob)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[10] = $"Schwelle: {schwelleGrob.Detail}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[10] = schwelleGrob.Detail;
    }
    private void RefreshDetailsForSchliessblech(T_Objektteil schliessblechGrob)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[9] = $"Schließblech: {schliessblechGrob.Detail}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[9] = schliessblechGrob.Detail;
    }
    private void RefreshDetailsForBandaufnahme2(T_Objektteil bandaufnahme2Grob)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[7] = $"Bandaufnahme 2: {bandaufnahme2Grob.Detail}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[7] = bandaufnahme2Grob.Detail;
    }
    private void RefreshDetailsForSchlosskasten(T_Objektteil schlosskastenGrob)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[8] = $"Schloßkasten: {schlosskastenGrob.Detail}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[8] = schlosskastenGrob.Detail;
    }
    private void RefreshDetailsForBandaufnahme1(T_Objektteil bandaufnahme1Grob)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[6] = $"Bandaufnahme 1: {bandaufnahme1Grob.Detail}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[6] = bandaufnahme1Grob.Detail;
    }
    private void RefreshDetailsForBand2(T_Objektteil band2Grob)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[5] = $"Band 2: {band2Grob.Detail}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[5] = band2Grob.Detail;
    }
    private void RefreshDetailsForBand1(T_Objektteil band1Grob)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[4] = $"Band 1: {band1Grob.Detail}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[4] = band1Grob.Detail;
    }
    private void RefreshDetailsForDrueckerZier(T_Objektteil drueckerZierGrob, T_Druecker drueckerZierDetail)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[3] = "Drücker Zier";
        aktuelleAnzeigeKonfigurator.ZeileDetail[3] = drueckerZierGrob.Detail;
        aktuelleAnzeigeKonfigurator.ZeileDetail[3] += $" | Hersteller: {drueckerZierGrob.Hersteller}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[3] += $" | Gewicht: {drueckerZierGrob.Gewicht} g";
        aktuelleAnzeigeKonfigurator.ZeileDetail[3] += $"\nLänge: {drueckerZierDetail.Laenge}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[3] += $" | Lochabstand: {drueckerZierDetail.Lochabstand} mm";
    }
    private void RefreshDetailsForDrueckerFalz(T_Objektteil drueckerFalzGrob, T_Druecker drueckerFalzDetail)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[2] = "Drücker Falz";
        aktuelleAnzeigeKonfigurator.ZeileDetail[2] = drueckerFalzGrob.Detail;
        aktuelleAnzeigeKonfigurator.ZeileDetail[2] += $" | Hersteller: {drueckerFalzGrob.Hersteller}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[2] += $" | Gewicht: {drueckerFalzGrob.Gewicht} g";
        aktuelleAnzeigeKonfigurator.ZeileDetail[2] += $"\nLänge: {drueckerFalzDetail.Laenge}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[2] += $" | Lochabstand: {drueckerFalzDetail.Lochabstand} mm";
    }
    private void RefreshDetailsForZarge(T_Objektteil zargeGrob, T_Zarge zargeDetail)
    {
        aktuelleAnzeigeKonfigurator.ZeileHeader[0] = "Zarge";
        aktuelleAnzeigeKonfigurator.ZeileDetail[0] = zargeGrob.Detail;
        aktuelleAnzeigeKonfigurator.ZeileDetail[0] += $" | Hersteller: {zargeGrob.Hersteller}";
        aktuelleAnzeigeKonfigurator.ZeileDetail[0] += $" | Gewicht: {zargeGrob.Gewicht} g";
        aktuelleAnzeigeKonfigurator.ZeileDetail[0] += $"\nDIN HxB: {zargeDetail.HoeheDIN}x";
        aktuelleAnzeigeKonfigurator.ZeileDetail[0] += $"{zargeDetail.BreiteDIN} mm";
        aktuelleAnzeigeKonfigurator.ZeileDetail[0] += $" | Wandstärke: {zargeDetail.Wandstaerke} mm";
        aktuelleAnzeigeKonfigurator.ZeileDetail[0] += $" | Bekleidungsbreite: {zargeDetail.Bekleidungsbreite} mm";
    }


    private void ErmittleMaterialPfadeAusMaterialJson(T_Objektteil objektTeil, TürObjekt tuerTeil, int index)
    {
        List<MaterialKombination> materialKombis =
            JsonConvert.DeserializeObject<List<MaterialKombination>>(objektTeil.MAT);

        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[index] = materialKombis.Count;
        tuerTeil.MaterialKombination.Material1 = null;
        tuerTeil.MaterialKombination.Material2 = null;
        tuerTeil.MaterialKombination.Material3 = null;

        var zargeMaterialKombi = materialKombis[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[index]];
        if (zargeMaterialKombi.Material1 != null)
        {
            tuerTeil.MaterialKombination.Material1 = tabelleMAT.Find(x => x.Id == zargeMaterialKombi.Material1).Id;
        }

        if (zargeMaterialKombi.Material2 != null)
        {
            tuerTeil.MaterialKombination.Material2 =
                tabelleMAT.Find(x => x.Id == zargeMaterialKombi.Material2).Id;
        }

        if (zargeMaterialKombi.Material3 != null)
        {
            tuerTeil.MaterialKombination.Material3 =
                tabelleMAT.Find(x => x.Id == zargeMaterialKombi.Material3).Id;
        }
    }

    private void VersteckeKonfiguratorMenüEinträge()
    {
        for (var i = 1; i <= ToggleMenü.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find($"KonfiguratorZeileContainer{i}").GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }


    // Togglings

    public void toggleMenue(string menueName, string aktivesMenue)
    {
        Debug.Log("Toggle aufgerufen!");
        //Debug.Log("rufe toggleMenue auf mit den Parametern ( menueName : " + menueName + " - aktivesMenue : " + aktivesMenue + " )");
        switch (menueName)
        {
            // Block Hauptmenü
            case "Hauptmenü":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        ShowHauptmenü();
                        Canvas.AktuellAktivesMenu = Hauptmenü.Name;
                    }
                    else if (aktivesMenue == Hauptmenü.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        HideHauptmenü();
                        Canvas.AktuellAktivesMenu = "";
                    }
                    else if (aktivesMenue == Filtermenü.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        ShowHauptmenü();
                        Canvas.AktuellAktivesMenu = Hauptmenü.Name;
                        HideFiltermenü();
                        HideTogglemenü();
                    }
                    else if (aktivesMenue == ToggleMenü.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        ShowHauptmenü();
                        Canvas.AktuellAktivesMenu = Hauptmenü.Name;
                        HideFiltermenü();
                        HideTogglemenü();
                    }

                    break;
                }

            // Filtermenü
            case "Innentür":
                {
                    Debug.Log("Filtermenü aufgerufen!");

                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        ShowFiltermenü();
                        Canvas.AktuellAktivesMenu = Filtermenü.Name;
                    }
                    else if (aktivesMenue == Hauptmenü.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        ShowFiltermenü();
                        Canvas.AktuellAktivesMenu = Filtermenü.Name;
                        HideHauptmenü();
                    }
                    else if (aktivesMenue == Filtermenü.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        HideFiltermenü();

                        // Bei erneutem Klick, wird das Fenster geschlossen und der andere Part getoggled
                        if (IsTogglemenüBlurred())
                        {
                            ShowTogglemenü();
                            Canvas.AktuellAktivesMenu = ToggleMenü.Name;
                        }
                        else
                        {
                            Canvas.AktuellAktivesMenu = "";
                        }
                    }
                    else if (aktivesMenue == ToggleMenü.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        ShowFiltermenü();
                        Canvas.AktuellAktivesMenu = Filtermenü.Name;
                        BlurTogglemenü();
                    }

                    break;
                }

            // Togglemenü
            case "Konfigurator":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        ShowTogglemenü();
                        Canvas.AktuellAktivesMenu = ToggleMenü.Name;
                        // Spezial
                        AusgewählteInnentür.Index = 0;
                        übergibParameterAusMenüInnentuerInMenüKonfigurator(gefilterteInnentueren);
                        
                    }
                    else if (aktivesMenue == Hauptmenü.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        ShowTogglemenü();
                        Canvas.AktuellAktivesMenu = ToggleMenü.Name;
                        // Spezial
                        AusgewählteInnentür.Index = 0;
                        übergibParameterAusMenüInnentuerInMenüKonfigurator(gefilterteInnentueren);

                        HideHauptmenü();
                    }
                    else if (aktivesMenue == Filtermenü.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        ShowTogglemenü();
                        Canvas.AktuellAktivesMenu = ToggleMenü.Name;
                        // Spezial
                        //Debug.Log("ergebnisInnentuer :" + ergebnisInnentuer.Count.ToString());
                        AusgewählteInnentür.Index = 0;
                        übergibParameterAusMenüInnentuerInMenüKonfigurator(gefilterteInnentueren);
                        
                        BlurFiltermenu();
                    }
                    else if (aktivesMenue == ToggleMenü.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        HideTogglemenü();
                        if (IsFiltermenüBlurred())
                        {
                            ShowFiltermenü();
                            Canvas.AktuellAktivesMenu = Filtermenü.Name;
                        }
                        else
                        {
                            Canvas.AktuellAktivesMenu = "";
                        }
                    }

                    break;
                }


            default:
                {
                    Debug.Log("ACHTUNG: Keinen case gefunden in: [toggleMenue]");
                    break;
                }
        }
    }

    private bool IsFiltermenüBlurred()
    {
        return GameObject.Find(Filtermenü.NameInCanvas).GetComponent<CanvasGroup>().alpha == 0.5f;
    }

    private bool IsTogglemenüBlurred()
    {
        return GameObject.Find(ToggleMenü.NameInCanvas).GetComponent<CanvasGroup>().alpha == 0.5f;
    }

    private void BlurFiltermenu()
    {
        GameObject.Find(Filtermenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void BlurTogglemenü()
    {
        GameObject.Find(ToggleMenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void ShowFiltermenü()
    {
        GameObject.Find(Filtermenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
    }
    private void HideFiltermenü()
    {
        GameObject.Find(Filtermenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    private void HideTogglemenü()
    {
        GameObject.Find(ToggleMenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    private void ShowTogglemenü()
    {
        GameObject.Find(ToggleMenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    private void ShowHauptmenü()
    {
        GameObject.Find(Hauptmenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    private void HideHauptmenü()
    {
        GameObject.Find(Hauptmenü.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    private void toggleMenuePunkt(int menue, int untermenue)
    {
        //Debug.Log("aktueller Index: " + innentuerParameter.TogglePunktIndex[untermenue - 1].ToString());
        //Debug.Log("Anzahl Toggles" + innentuerParameter.Toggle[untermenue - 1].Count);
        switch (menue)
        {
            case 1:
                {
                    if (hauptmenueParameter.TogglePunktIndex[untermenue - 1] <
                        hauptmenueParameter.Toggle[untermenue - 1].Count() - 1)
                        hauptmenueParameter.TogglePunktIndex[untermenue - 1] += 1;
                    else
                        hauptmenueParameter.TogglePunktIndex[untermenue - 1] = 0;
                    GameObject.Find(Hauptmenü.ZeileDetailNameInCanvas + untermenue).GetComponent<Text>().text =
                        hauptmenueParameter.Toggle[untermenue - 1][hauptmenueParameter.TogglePunktIndex[untermenue - 1]];
                    break;
                }
            case 2:
                {
                    if (innentuerParameter.TogglePunktIndex[untermenue - 1] <
                        innentuerParameter.Toggle[untermenue - 1].Count() - 1)
                        innentuerParameter.TogglePunktIndex[untermenue - 1] += 1;
                    else
                        innentuerParameter.TogglePunktIndex[untermenue - 1] = 0;
                    GameObject.Find(Filtermenü.ZeileDetailNameInCanvas + untermenue).GetComponent<Text>().text =
                        innentuerParameter.Toggle[untermenue - 1][innentuerParameter.TogglePunktIndex[untermenue - 1]];
                    break;
                }
            case 3:
                {
                    break;
                }
        }
    }

    // 30
    private void toggleInnentuerImKonfigurator()
    {

        if (gefilterteInnentueren.Count >= 0 && GameObject.Find(ToggleMenü.NameInCanvas).GetComponent<CanvasGroup>().alpha == 1.0f)
        {

            if (AusgewählteInnentür.Index < gefilterteInnentueren.Count - 1)
            {
                AusgewählteInnentür.Index++;
                übergibParameterAusMenüInnentuerInMenüKonfigurator(gefilterteInnentueren);
            }
            else
            {
                AusgewählteInnentür.Index = 0;
                übergibParameterAusMenüInnentuerInMenüKonfigurator(gefilterteInnentueren);
            }
        }
    }

    private void toggleMaterialImKonfigurator(int menuepunkt)
    {
        if (aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[menuepunkt - 1] <
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[menuepunkt - 1] - 1)
            aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[menuepunkt - 1]++;
        else
            aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[menuepunkt - 1] = 0;
        Debug.Log(
            $"##### ##### ##### aktuellGetoggeltesMaterial zu Menüpunkt {(menuepunkt - 1)} : {aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[menuepunkt - 1]} : {aktuelleAnzeigeKonfigurator.AktuellesMaterialName[menuepunkt - 1]}");

        //updateKonfigurator();
        übergibParameterAusMenüInnentuerInMenüKonfigurator(gefilterteInnentueren);
    }

    private void toggleAchsenspiegelung(string achse)
    {
        // ermittele den Vektor localScale von INNENTUER
        var v = GameObject.Find("INNENTUER").GetComponent<Transform>().localScale;

        // negiere abhängig von der Achse entsprechend x oder z
        if (achse == "x")
            v.x *= -1;
        else
            v.z *= -1;

        // aktualisere Vektor localScale von INNENTUER
        GameObject.Find("INNENTUER").transform.localScale = v;

        // Achsenspiegelung: abhängig von der Spieglung, muss auch der Winkel des Rotationspunktes gesetzt werden
        if (v.x == 1 && v.z == 1 || v.x == -1 && v.z == -1)
            rotationWinkel = -90.0f;
        else
            rotationWinkel = 90.0f;
    }


    // Updates
    private void updateKonfigurator()
    {
        GameObject.Find(ToggleMenü.InfoTextNameInCanvas).GetComponent<Text>().text =
            aktuelleAnzeigeKonfigurator.InfoText;

        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (var zeileNr = 1; zeileNr <= ToggleMenü.ZeileContainerAnzahlZeilen; zeileNr++)
        {
            GameObject.Find(ToggleMenü.ZeileHeaderNameInCanvas + zeileNr).GetComponent<Text>().text = "";
            GameObject.Find(ToggleMenü.ZeileDetailNameInCanvas + zeileNr).GetComponent<Text>().text = "";
            GameObject.Find(ToggleMenü.ZeileContainerNameInCanvas + zeileNr).GetComponent<CanvasGroup>().alpha =
                0.0f;

            // blende alle Materialien des Menüpunktes aus
            for (var materialNr = 1;
                materialNr <= ToggleMenü.ZeileMaterialAnzahlMaterialOptionenX *
                ToggleMenü.ZeileMaterialAnzahlMaterialOptionenY;
                materialNr++)
                GameObject.Find($"KonfiguratorZeileMaterial{zeileNr}{materialNr}").GetComponent<CanvasGroup>().alpha =
                    0.0f;

            // blende alle Materialien des Menüpunktes aus
            for (var materialNr = 1;
                materialNr <= aktuelleAnzeigeKonfigurator.AnzahlMaterialien[zeileNr - 1];
                materialNr++)
                GameObject.Find($"KonfiguratorZeileMaterial{zeileNr}{materialNr}").GetComponent<CanvasGroup>().alpha =
                    0.2f;
        }


        // ermittele für alle Zeilen (auch die nicht anzuzeigenden) die Parameter

        for (var zeileNr = 0; zeileNr <= ToggleMenü.ZeileContainerAnzahlZeilen - 1; zeileNr++)
        {
            //GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = tabelleObjektteil.Find(x => x.Id == "Band_001").Detail;
            GameObject.Find(ToggleMenü.ZeileHeaderNameInCanvas + (zeileNr + 1)).GetComponent<Text>().text =
                aktuelleAnzeigeKonfigurator.ZeileHeader[zeileNr];
            GameObject.Find(ToggleMenü.ZeileDetailNameInCanvas + (zeileNr + 1)).GetComponent<Text>().text =
                aktuelleAnzeigeKonfigurator.ZeileDetail[zeileNr];
            //// zeige das aktuelle Material im Infofeld an
            //GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text += "\n" + aktuelleAnzeigeKonfigurator.AnzahlMaterialien[zeileNr];
            //GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text += " | " + aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[zeileNr].ToString();
            //GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text += " | " + aktuelleAnzeigeKonfigurator.AktuellesMaterialName[zeileNr];

            //Debug.Log("KonfiguratorZeileMaterial" + (zeileNr + 1).ToString() + (aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[zeileNr] + 1).ToString());
            GameObject.Find(
                    $"KonfiguratorZeileMaterial{(zeileNr + 1)}{(aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[zeileNr] + 1)}")
                .GetComponent<CanvasGroup>().alpha = 1.0f;
        }

        // blende alle relevanten zeilen wieder ein
        for (var zeileNr = 0; zeileNr <= ToggleMenü.AnzuzeigendeSpalteDerTabelleInnentuer.Count() - 1; zeileNr++)
            GameObject.Find(ToggleMenü.ZeileContainerNameInCanvas + (zeileNr + 1)).GetComponent<CanvasGroup>()
                .alpha = 1.0f;


        updateAktuellGetoggelteInnentuer();
    }

    // Updated das Aktuelle InnentuerObjekt
    private void updateAktuellGetoggelteInnentuer()
    {
        AusgewählteInnentür.Zarge.Bezeichnung = gefilterteInnentueren[AusgewählteInnentür.Index].Zarge;
        AusgewählteInnentür.Tuerblatt.Bezeichnung = gefilterteInnentueren[AusgewählteInnentür.Index].Tuerblatt;
        AusgewählteInnentür.DrueckerFalz.Bezeichnung = gefilterteInnentueren[AusgewählteInnentür.Index].DrueckerFalz;
        AusgewählteInnentür.DrueckerZier.Bezeichnung = gefilterteInnentueren[AusgewählteInnentür.Index].DrueckerZier;
        AusgewählteInnentür.Band1.Bezeichnung = gefilterteInnentueren[AusgewählteInnentür.Index].Band1;
        AusgewählteInnentür.Band2.Bezeichnung = gefilterteInnentueren[AusgewählteInnentür.Index].Band2;
        AusgewählteInnentür.Bandaufnahme1.Bezeichnung =
            gefilterteInnentueren[AusgewählteInnentür.Index].Bandaufnahme1;
        AusgewählteInnentür.Bandaufnahme2.Bezeichnung =
            gefilterteInnentueren[AusgewählteInnentür.Index].Bandaufnahme2;
        AusgewählteInnentür.Schlosskasten.Bezeichnung =
            gefilterteInnentueren[AusgewählteInnentür.Index].Schlosskasten;
        AusgewählteInnentür.Schliessblech.Bezeichnung =
            gefilterteInnentueren[AusgewählteInnentür.Index].Schliessblech;
        AusgewählteInnentür.Schwelle.Bezeichnung = "";
    }


    private void updateHauptmenue(HauptmenueParameter parameter)
    {
        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (var i = 1; i <= Hauptmenü.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find(Hauptmenü.ZeileHeaderNameInCanvas + i).GetComponent<Text>().text = "";
            GameObject.Find(Hauptmenü.ZeileContainerNameInCanvas + i).GetComponent<CanvasGroup>().alpha = 0.0f;
            // hier nich alle Matrialien
        }


        for (var i = 1; i <= parameter.Menuepunkt.Count; i++)
        {
            GameObject.Find(Hauptmenü.ZeileHeaderNameInCanvas + i).GetComponent<Text>().text =
                parameter.Menuepunkt[i - 1];
            GameObject.Find(Hauptmenü.ZeileContainerNameInCanvas + i).GetComponent<CanvasGroup>().alpha = 1.0f;
            GameObject.Find(Hauptmenü.ZeileDetailNameInCanvas + i).GetComponent<Text>().text =
                hauptmenueParameter.Toggle[i - 1][hauptmenueParameter.TogglePunktIndex[i - 1]];
        }
    }

    private void updateInnentuer(InnentuerParameter parameter)
    {
        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (var i = 1; i <= Filtermenü.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find(Filtermenü.ZeileHeaderNameInCanvas + i).GetComponent<Text>().text = "";
            GameObject.Find(Filtermenü.ZeileContainerNameInCanvas + i).GetComponent<CanvasGroup>().alpha = 0.0f;
            // hier nich alle Matrialien
        }

        for (var i = 1; i <= parameter.Menuepunkt.Count; i++)
        {
            GameObject.Find(Filtermenü.ZeileHeaderNameInCanvas + i).GetComponent<Text>().text =
                parameter.Menuepunkt[i - 1];
            GameObject.Find(Filtermenü.ZeileContainerNameInCanvas + i).GetComponent<CanvasGroup>().alpha = 1.0f;
            GameObject.Find(Filtermenü.ZeileDetailNameInCanvas + i).GetComponent<Text>().text =
                innentuerParameter.Toggle[i - 1][innentuerParameter.TogglePunktIndex[i - 1]];
        }
    }


    private IEnumerator LerpColor(string gameObjectName)
    {
        // wenn diese drei Zeilen ausserhalb des IEnumarators als globale variablen definiere, wären Änderungen erst
        // nach Neustart von Unity übernommen (?)
        var duration = 0.2f; // Zeit in Sekunden.
        var
            smoothness =
                0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
        var floatColor = Hauptmenü.ZeileContainerColorToggle;

        colorLerpAktive = true;
        myImage = GameObject.Find(gameObjectName).GetComponent<Image>();
        Color32 colorOld = myImage.color;

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        var increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            myImage.color = Color32.Lerp(myImage.color, floatColor, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        progress = 0;
        while (progress < 1)
        {
            myImage.color = Color32.Lerp(myImage.color, colorOld, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        colorLerpAktive = false;
    }

    // generiere GameObject Canvas
    private void generiereGuiCanvas()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Canvas.Name;
        myGO.layer = 5; // 5:UI

        // Canvas
        myGO.AddComponent<Canvas>();
        myCanvas = myGO.GetComponent<Canvas>();

        // Canvas renderMode
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Canvas RectTransform
        myCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        myCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myCanvas.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Canvas Scaler
        myGO.AddComponent<CanvasScaler>();
        CanvasScaler myCanvasScaler;
        myCanvasScaler = myGO.GetComponent<CanvasScaler>();
        myCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        myCanvasScaler.referenceResolution = new Vector2(Canvas.Width, Canvas.Height);

        // Canvas GraphicRaycaster
        myGO.AddComponent<GraphicRaycaster>();
    }


    private void generiereHauptmenueContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(Hauptmenü.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Hauptmenü.Width, Hauptmenü.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.Parent).transform);

        // Image color
        myImage.color = Hauptmenü.Color;
    }

    private void generiereHauptmenueHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(Hauptmenü.HeaderContainerPosX,
            Hauptmenü.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.HeaderContainerWidth, Hauptmenü.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.HeaderContainerParent).transform);

        // Image color
        myImage.color = Hauptmenü.HeaderContainerColor;
    }

    private void generiereHauptmenueHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.HeaderLogoPosX, Hauptmenü.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.HeaderLogoWidth, Hauptmenü.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        var FULLHP = Resources.Load<Sprite>(Hauptmenü.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.HeaderLogoParent).transform);

        // Image color
        myImage.color = Hauptmenü.HeaderLogoColor;
    }

    private void generiereHauptmenueHeaderText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.HeaderTextPosX, Hauptmenü.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.HeaderTextWidth, Hauptmenü.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Hauptmenü.HeaderTextFontType) as Font;
        myText.fontSize = Hauptmenü.HeaderTextFontSize;
        myText.fontStyle = Hauptmenü.HeaderTextFontStyle;
        myText.color = Hauptmenü.HeaderTextColor;

        // Text
        myText.text = Hauptmenü.HeaderTextText;
    }

    private void generiereHauptmenueInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.InfoContainerPosX, Hauptmenü.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.InfoContainerWidth, Hauptmenü.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.InfoContainerParent).transform);

        // Image color
        myImage.color = Hauptmenü.InfoContainerColor;
    }

    private void generiereHauptmenueInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.InfoTextPosX, Hauptmenü.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.InfoTextWidth, Hauptmenü.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Hauptmenü.InfoTextFontType) as Font;
        myText.fontSize = Hauptmenü.InfoTextFontSize;
        myText.fontStyle = Hauptmenü.InfoTextFontStyle;
        myText.color = Hauptmenü.InfoTextColor;

        // Text
        myText.text = Hauptmenü.InfoTextText;
    }

    private void generiereHauptmenueInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.InfoLogoPosX, Hauptmenü.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.InfoLogoWidth, Hauptmenü.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        var FULLHP = Resources.Load<Sprite>(Hauptmenü.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.InfoLogoParent).transform);

        // Image color
        myImage.color = Hauptmenü.InfoLogoColor;
    }

    private void generiereHauptmenueHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.HauptContainerPosX, Hauptmenü.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.HauptContainerWidth, Hauptmenü.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.HauptContainerParent).transform);

        // Image color
        myImage.color = Hauptmenü.HauptContainerColor;
    }

    private void generiereHauptmenueHauptContainerZeilen(int anzahlZeilen)
    {
        for (var i = 1; i <= anzahlZeilen; i++)
        {
            // übergib: GameObject-Name(!) / Zeilennummer, beginnen mit 1 / Index der aktuell getoggelten Innentür
            generiereHauptmenueZeileContainer(i);
            //    // übergib: GameObject-Parent(!) / Zeilennummer, beginnen mit 1 /
            generiereHauptmenueZeileHeader(i);
            //    generiereHauptmenueZeileMaterial(i);
            generiereHauptmenueZeileDetail(i);
            generiereHauptmenueZeileLogo(i);
            generiereHauptmenueZeileNummer(i);
        }
    }

    private void generiereHauptmenueZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Hauptmenü.ZeileContainerPosY + Hauptmenü.ZeileContainerAbstandY * nummer +
                            -Hauptmenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.ZeileContainerNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.ZeileContainerWidth, Hauptmenü.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.ZeileContainerParent).transform);

        // Image color
        myImage.color = Hauptmenü.ZeileContainerColor;
    }

    private void generiereHauptmenueZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.ZeileHeaderNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        var neuePositionY = Hauptmenü.ZeileHeaderPosY + Hauptmenü.ZeileContainerAbstandY * nummer +
                            -Hauptmenü.ZeileContainerHeight * (nummer - 1);

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.ZeileHeaderWidth, Hauptmenü.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.ZeileHeaderParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Hauptmenü.ZeileHeaderFontType) as Font;
        myText.fontSize = Hauptmenü.ZeileHeaderFontSize;
        myText.fontStyle = Hauptmenü.ZeileHeaderFontStyle;
        myText.color = Hauptmenü.ZeileHeaderColor;

        myText.text = "Header";
    }

    private void generiereHauptmenueZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Hauptmenü.ZeileDetailPosY + Hauptmenü.ZeileContainerAbstandY * nummer +
                            -Hauptmenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.ZeileDetailNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.ZeileDetailWidth, Hauptmenü.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.ZeileDetailParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Hauptmenü.ZeileDetailFontType) as Font;
        myText.fontSize = Hauptmenü.ZeileDetailFontSize;
        myText.fontStyle = Hauptmenü.ZeileDetailFontStyle;
        myText.color = Hauptmenü.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    private void generiereHauptmenueZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Hauptmenü.ZeileLogoPosY + Hauptmenü.ZeileContainerAbstandY * nummer +
                            -Hauptmenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.ZeileLogoNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.ZeileLogoWidth, Hauptmenü.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiHauptmenue.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.ZeileLogoParent + nummer).transform);

        // Image color
        myImage.color = Hauptmenü.ZeileLogoColor;
    }

    private void generiereHauptmenueZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Hauptmenü.ZeileNummerPosY + Hauptmenü.ZeileContainerAbstandY * nummer +
                            -Hauptmenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Hauptmenü.ZeileNummerNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Hauptmenü.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Hauptmenü.ZeileNummerWidth, Hauptmenü.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Hauptmenü.ZeileNummerParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Hauptmenü.ZeileNummerFontType) as Font;
        myText.fontSize = Hauptmenü.ZeileNummerFontSize;
        myText.fontStyle = Hauptmenü.ZeileNummerFontStyle;
        myText.color = Hauptmenü.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }


    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Innentuer
    // ---------------------------------------------------------------------------------------------------

    private void generiereInnentuerContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(Filtermenü.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Filtermenü.Width, Filtermenü.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Filtermenü.Parent).transform);

        // Image color
        myImage.color = Filtermenü.Color;
    }

    private void generiereInnentuerHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.HeaderContainerPosX, Filtermenü.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.HeaderContainerWidth, Filtermenü.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Filtermenü.HeaderContainerParent).transform);

        // Image color
        myImage.color = Filtermenü.HeaderContainerColor;
    }

    private void generiereInnentuerHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.HeaderLogoPosX, Filtermenü.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.HeaderLogoWidth, Filtermenü.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        var FULLHP = Resources.Load<Sprite>(Filtermenü.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Filtermenü.HeaderLogoParent).transform);

        // Image color
        myImage.color = Filtermenü.HeaderLogoColor;
    }

    private void generiereInnentuerHeaderText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.HeaderTextPosX, Filtermenü.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.HeaderTextWidth, Filtermenü.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Filtermenü.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Filtermenü.HeaderTextFontType) as Font;
        myText.fontSize = Filtermenü.HeaderTextFontSize;
        myText.fontStyle = Filtermenü.HeaderTextFontStyle;
        myText.color = Filtermenü.HeaderTextColor;

        // Text
        myText.text = Filtermenü.HeaderTextText;
    }

    private void generiereInnentuerInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.InfoContainerPosX, Filtermenü.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.InfoContainerWidth, Filtermenü.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Filtermenü.InfoContainerParent).transform);

        // Image color
        myImage.color = Filtermenü.InfoContainerColor;
    }

    private void generiereInnentuerInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.InfoTextPosX, Filtermenü.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.InfoTextWidth, Filtermenü.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Filtermenü.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Filtermenü.InfoTextFontType) as Font;
        myText.fontSize = Filtermenü.InfoTextFontSize;
        myText.fontStyle = Filtermenü.InfoTextFontStyle;
        myText.color = Filtermenü.InfoTextColor;

        // Text
        myText.text = Filtermenü.InfoTextText;
    }

    private void generiereInnentuerInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.InfoLogoPosX, Filtermenü.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.InfoLogoWidth, Filtermenü.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        var FULLHP = Resources.Load<Sprite>(Filtermenü.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Filtermenü.InfoLogoParent).transform);

        // Image color
        myImage.color = Filtermenü.InfoLogoColor;
    }

    private void generiereInnentuerHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.HauptContainerPosX, Filtermenü.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.HauptContainerWidth, Filtermenü.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Filtermenü.HauptContainerParent).transform);

        // Image color
        myImage.color = Filtermenü.HauptContainerColor;
    }

    private void generiereInnentuerHauptContainerZeilen(int anzahlZeilen)
    {
        for (var i = 1; i <= anzahlZeilen; i++)
        {
            // übergib: GameObject-Name(!) / Zeilennummer, beginnen mit 1 / Index der aktuell getoggelten Innentür
            generiereInnentuerZeileContainer(i);
            //    // übergib: GameObject-Parent(!) / Zeilennummer, beginnen mit 1 /
            generiereInnentuerZeileHeader(i);
            //    generiereInnentuerZeileMaterial(i);
            generiereInnentuerZeileDetail(i);
            generiereInnentuerZeileLogo(i);
            generiereInnentuerZeileNummer(i);
        }
    }

    private void generiereInnentuerZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Filtermenü.ZeileContainerPosY + Filtermenü.ZeileContainerAbstandY * nummer +
                            -Filtermenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.ZeileContainerNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.ZeileContainerWidth, Filtermenü.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(Filtermenü.ZeileContainerParent).transform);

        // Image color
        myImage.color = Filtermenü.ZeileContainerColor;
    }

    private void generiereInnentuerZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.ZeileHeaderNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        var neuePositionY = Filtermenü.ZeileHeaderPosY + Filtermenü.ZeileContainerAbstandY * nummer +
                            -Filtermenü.ZeileContainerHeight * (nummer - 1);

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.ZeileHeaderWidth, Filtermenü.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Filtermenü.ZeileHeaderParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Filtermenü.ZeileHeaderFontType) as Font;
        myText.fontSize = Filtermenü.ZeileHeaderFontSize;
        myText.fontStyle = Filtermenü.ZeileHeaderFontStyle;
        myText.color = Filtermenü.ZeileHeaderColor;

        myText.text = "Header";
    }

    private void generiereInnentuerZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Filtermenü.ZeileDetailPosY + Filtermenü.ZeileContainerAbstandY * nummer +
                            -Filtermenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.ZeileDetailNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.ZeileDetailWidth, Filtermenü.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Filtermenü.ZeileDetailParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Filtermenü.ZeileDetailFontType) as Font;
        myText.fontSize = Filtermenü.ZeileDetailFontSize;
        myText.fontStyle = Filtermenü.ZeileDetailFontStyle;
        myText.color = Filtermenü.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    private void generiereInnentuerZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Filtermenü.ZeileLogoPosY + Filtermenü.ZeileContainerAbstandY * nummer +
                            -Filtermenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.ZeileLogoNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.ZeileLogoWidth, Filtermenü.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiInnentuer.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Filtermenü.ZeileLogoParent + nummer).transform);

        // Image color
        myImage.color = Filtermenü.ZeileLogoColor;
    }

    private void generiereInnentuerZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = Filtermenü.ZeileNummerPosY + Filtermenü.ZeileContainerAbstandY * nummer +
                            -Filtermenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = Filtermenü.ZeileNummerNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(Filtermenü.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(Filtermenü.ZeileNummerWidth, Filtermenü.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(Filtermenü.ZeileNummerParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), Filtermenü.ZeileNummerFontType) as Font;
        myText.fontSize = Filtermenü.ZeileNummerFontSize;
        myText.fontStyle = Filtermenü.ZeileNummerFontStyle;
        myText.color = Filtermenü.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }


    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Konfigurator
    // ---------------------------------------------------------------------------------------------------


    private void generiereKonfiguratorContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(ToggleMenü.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ToggleMenü.Width, ToggleMenü.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.Parent).transform);

        // Image color
        myImage.color = ToggleMenü.Color;
    }

    private void generiereKonfiguratorHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(ToggleMenü.HeaderContainerPosX,
            ToggleMenü.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ToggleMenü.HeaderContainerWidth,
            ToggleMenü.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.HeaderContainerParent).transform);

        // Image color
        myImage.color = ToggleMenü.HeaderContainerColor;
    }

    private void generiereKonfiguratorHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.HeaderLogoPosX, ToggleMenü.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.HeaderLogoWidth, ToggleMenü.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        var FULLHP = Resources.Load<Sprite>(ToggleMenü.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.HeaderLogoParent).transform);

        // Image color
        myImage.color = ToggleMenü.HeaderLogoColor;
    }

    private void generiereKonfiguratorHeaderText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.HeaderTextPosX, ToggleMenü.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.HeaderTextWidth, ToggleMenü.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), ToggleMenü.HeaderTextFontType) as Font;
        myText.fontSize = ToggleMenü.HeaderTextFontSize;
        myText.fontStyle = ToggleMenü.HeaderTextFontStyle;
        myText.color = ToggleMenü.HeaderTextColor;

        // Text
        myText.text = ToggleMenü.HeaderTextText;
    }

    private void generiereKonfiguratorInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(ToggleMenü.InfoContainerPosX,
            ToggleMenü.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.InfoContainerWidth, ToggleMenü.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.InfoContainerParent).transform);

        // Image color
        myImage.color = ToggleMenü.InfoContainerColor;
    }

    private void generiereKonfiguratorInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.InfoTextPosX, ToggleMenü.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.InfoTextWidth, ToggleMenü.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), ToggleMenü.InfoTextFontType) as Font;
        myText.fontSize = ToggleMenü.InfoTextFontSize;
        myText.fontStyle = ToggleMenü.InfoTextFontStyle;
        myText.color = ToggleMenü.InfoTextColor;

        // Text
        myText.text = ToggleMenü.InfoTextText;
    }

    private void generiereKonfiguratorInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.InfoLogoPosX, ToggleMenü.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.InfoLogoWidth, ToggleMenü.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        var FULLHP = Resources.Load<Sprite>(ToggleMenü.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.InfoLogoParent).transform);

        // Image color
        myImage.color = ToggleMenü.InfoLogoColor;
    }

    private void generiereKonfiguratorInfoNummer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.InfoNummerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.InfoNummerPosX, ToggleMenü.InfoNummerPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.InfoNummerWidth, ToggleMenü.InfoNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.InfoNummerParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), ToggleMenü.InfoNummerFontType) as Font;
        myText.fontSize = ToggleMenü.InfoNummerFontSize;
        myText.fontStyle = ToggleMenü.InfoNummerFontStyle;
        myText.color = ToggleMenü.InfoNummerColor;

        // Text
        myText.text = ToggleMenü.InfoNummerText;
    }

    private void generiereKonfiguratorHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(ToggleMenü.HauptContainerPosX,
            ToggleMenü.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ToggleMenü.HauptContainerWidth,
            ToggleMenü.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.HauptContainerParent).transform);

        // Image color
        myImage.color = ToggleMenü.HauptContainerColor;
    }

    private void generiereKonfiguratorHauptContainerZeilen(int anzahlZeilen)
    {
        for (var i = 1; i <= anzahlZeilen; i++)
        {
            // übergib: GameObject-Name(!) / Zeilennummer, beginnen mit 1 / Index der aktuell getoggelten Innentür
            generiereKonfiguratorZeileContainer(i);
            // übergib: GameObject-Parent(!) / Zeilennummer, beginnen mit 1 /
            generiereKonfiguratorZeileHeader(i);
            generiereKonfiguratorZeileMaterial(i);
            generiereKonfiguratorZeileDetail(i);
            generiereKonfiguratorZeileLogo(i);
            generiereKonfiguratorZeileNummer(i);
        }
    }

    private void generiereKonfiguratorZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = ToggleMenü.ZeileContainerPosY + ToggleMenü.ZeileContainerAbstandY * nummer +
                            -ToggleMenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.ZeileContainerNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ToggleMenü.ZeileContainerWidth,
            ToggleMenü.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.ZeileContainerParent).transform);

        // Image color
        myImage.color = ToggleMenü.ZeileContainerColor;
    }

    private void generiereKonfiguratorZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.ZeileHeaderNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        var neuePositionY = ToggleMenü.ZeileHeaderPosY + ToggleMenü.ZeileContainerAbstandY * nummer +
                            -ToggleMenü.ZeileContainerHeight * (nummer - 1);

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.ZeileHeaderWidth, ToggleMenü.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.ZeileHeaderParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), ToggleMenü.ZeileHeaderFontType) as Font;
        myText.fontSize = ToggleMenü.ZeileHeaderFontSize;
        myText.fontStyle = ToggleMenü.ZeileHeaderFontStyle;
        myText.color = ToggleMenü.ZeileHeaderColor;

        myText.text = "Header";
    }

    private void generiereKonfiguratorZeileMaterial(int nummer)
    {
        int neuePositionY;
        int neuePositionX;

        var i = 0;
        for (var y = 1; y <= ToggleMenü.ZeileMaterialAnzahlMaterialOptionenY; y++)

            for (var x = 1; x <= ToggleMenü.ZeileMaterialAnzahlMaterialOptionenX; x++)
            {
                i++;
                // Berechnung Position des aktuellen Containers
                neuePositionY =
                    -((y - 1) * (ToggleMenü.ZeileMaterialAnzahlMaterialAbstandY +
                                 ToggleMenü.ZeileMaterialHeight)) + ToggleMenü.ZeileMaterialPosY +
                    ToggleMenü.ZeileContainerAbstandY * nummer + -ToggleMenü.ZeileContainerHeight * (nummer - 1);
                neuePositionX =
                    -((ToggleMenü.ZeileMaterialAnzahlMaterialOptionenX - x) *
                      (ToggleMenü.ZeileMaterialAnzahlMaterialAbstandX + ToggleMenü.ZeileMaterialWidth)) +
                    ToggleMenü.ZeileMaterialPosX;

                // Game Object
                myGO = new GameObject();
                myGO.name = ToggleMenü.ZeileMaterialNameInCanvas + nummer + i;
                myGO.layer = 5; // 5:UI

                // Image
                myGO.AddComponent<Image>();

                //Image myImage;
                myImage = myGO.GetComponent<Image>();
                myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(neuePositionX, neuePositionY, 0);
                myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ToggleMenü.ZeileMaterialWidth,
                    ToggleMenü.ZeileMaterialHeight);
                myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
                myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
                myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

                // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
                myGO.AddComponent<CanvasGroup>();

                // mache das GameObject Info-Container Image zum child des Menu
                myGO.transform.SetParent(GameObject.Find(ToggleMenü.ZeileMaterialParent + nummer).transform);

                // Image color
                myImage.color = ToggleMenü.ZeileMaterialColor;
            }
    }

    private void generiereKonfiguratorZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = ToggleMenü.ZeileDetailPosY + ToggleMenü.ZeileContainerAbstandY * nummer +
                            -ToggleMenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.ZeileDetailNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.ZeileDetailWidth, ToggleMenü.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.ZeileDetailParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), ToggleMenü.ZeileDetailFontType) as Font;
        myText.fontSize = ToggleMenü.ZeileDetailFontSize;
        myText.fontStyle = ToggleMenü.ZeileDetailFontStyle;
        myText.color = ToggleMenü.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    private void generiereKonfiguratorZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = ToggleMenü.ZeileLogoPosY + ToggleMenü.ZeileContainerAbstandY * nummer +
                            -ToggleMenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.ZeileLogoNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.ZeileLogoWidth, ToggleMenü.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiKonfigurator.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.ZeileLogoParent + nummer).transform);

        // Image color
        myImage.color = ToggleMenü.ZeileLogoColor;
    }

    private void generiereKonfiguratorZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        var neuePositionY = ToggleMenü.ZeileNummerPosY + ToggleMenü.ZeileContainerAbstandY * nummer +
                            -ToggleMenü.ZeileContainerHeight * (nummer - 1);

        // Game Object
        myGO = new GameObject();
        myGO.name = ToggleMenü.ZeileNummerNameInCanvas + nummer;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition =
            new Vector3(ToggleMenü.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta =
            new Vector2(ToggleMenü.ZeileNummerWidth, ToggleMenü.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(ToggleMenü.ZeileNummerParent + nummer).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), ToggleMenü.ZeileNummerFontType) as Font;
        myText.fontSize = ToggleMenü.ZeileNummerFontSize;
        myText.fontStyle = ToggleMenü.ZeileNummerFontStyle;
        myText.color = ToggleMenü.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }

}