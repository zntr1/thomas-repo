using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;
using Newtonsoft.Json;

public class MenuGenerator : MonoBehaviour
{

    // ---------------------------------------------------------------------------------------------------
    // Klassen
    // ---------------------------------------------------------------------------------------------------


    // Klassen für die generelle Konfiguration der GUI-Elemente
    public class GuiCanvas
    {
        public string Name = "GUI";
        public int Width = 1920;
        public int Height = 1080;
        public string AktuellAktivesMenu { get; set; }
    }

    public class GuiHauptmenue
    {
        public string Name = "Hauptmenü";

        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static int VerschiebungX = 480;
        private static int VerschiebungY = 0;

        // Hauptmenue
        public string NameInCanvas = "Hauptmenue";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;
        public int Width = 960;
        public int Height = 1080;
        public Color32 Color = new Color32(0, 0, 0, 100);

        // HauptmenueHeaderContainer
        public string HeaderContainerNameInCanvas = "HauptmenueHeaderContainer";
        public string HeaderContainerParent = "Hauptmenue";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public int HeaderContainerHeight = 120;
        public Color32 HeaderContainerColor = new Color32(0, 0, 0, 250);

        // HauptmenueHeaderLogo
        public string HeaderLogoNameInCanvas = "HauptmenueHeaderLogo";
        public string HeaderLogoParent = "HauptmenueHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public int HeaderLogoWidth = 80;
        public int HeaderLogoHeight = 80;
        public Color32 HeaderLogoColor = new Color32(255, 255, 255, 255);
        public string HeaderLogoSourceImage = "Logos/cog-wheel-silhouette_white";

        // HauptmenueHeaderText
        public string HeaderTextNameInCanvas = "HauptmenueHeaderText";
        public string HeaderTextParent = "HauptmenueHeaderContainer";
        public string HeaderTextText = "Hauptmenü";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public int HeaderTextWidth = 780;
        public int HeaderTextHeight = 120;
        public Color32 HeaderTextColor = new Color32(255, 255, 255, 255);
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //HauptmenueInfoContainer
        public string InfoContainerNameInCanvas = "HauptmenueInfoContainer";
        public string InfoContainerParent = "Hauptmenue";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public int InfoContainerHeight = 100;
        public Color32 InfoContainerColor = new Color32(0, 0, 0, 230);

        // HauptmenueInfoText
        public string InfoTextNameInCanvas = "HauptmenueInfoText";
        public string InfoTextParent = "HauptmenueInfoContainer";
        public string InfoTextText = "generelle Einstellungen";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 123;
        public int InfoTextWidth = 780;
        public int InfoTextHeight = 120;
        public Color32 InfoTextColor = new Color32(255, 255, 255, 50);
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //HauptmenueInfoLogo
        public string InfoLogoNameInCanvas = "HauptmenueInfoLogo";
        public string InfoLogoParent = "HauptmenueInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public int InfoLogoWidth = 60;
        public int InfoLogoHeight = 100;
        public Color32 InfoLogoColor = new Color32(0, 0, 0, 0); // ausgeblendet
        public string InfoLogoSourceImage = "";//"Logos/list-with-dots_white";

        //HauptmenueHauptContainer
        public string HauptContainerNameInCanvas = "HauptmenueHauptContainer";
        public string HauptContainerParent = "Hauptmenue";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public int HauptContainerHeight = 920;
        public Color32 HauptContainerColor = new Color32(0, 000, 0, 100);

        // ab hier die einzelnen Zeilen

        //HauptmenueZeileContainer
        public int ZeileContainerAnzahlZeilen = 8; // momentan max. 8 wegen Höehe 1080 px
        public string ZeileContainerNameInCanvas = "HauptmenueZeileContainer";
        public string ZeileContainerParent = "HauptmenueHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public int ZeileContainerHeight = 100;
        public Color32 ZeileContainerColor = new Color32(0, 000, 0, 100);
        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        //HauptmenueZeileHeader
        public string ZeileHeaderNameInCanvas = "HauptmenueZeileHeader";
        public string ZeileHeaderParent = "HauptmenueZeileContainer";
        public string ZeileHeaderText = "";
        public int ZeileHeaderPosX = VerschiebungX + 80;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public int ZeileHeaderWidth = 600;
        public int ZeileHeaderHeight = 60;
        public Color32 ZeileHeaderColor = new Color32(255, 255, 255, 255);
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderFontSize = 45;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        ////HauptmenueZeileMaterial
        //public int ZeileMaterialAnzahlMaterialOptionenX = 4;
        //public int ZeileMaterialAnzahlMaterialOptionenY = 3;
        //public int ZeileMaterialAnzahlMaterialAbstandX = 10;
        //public int ZeileMaterialAnzahlMaterialAbstandY = 10;
        //public string ZeileMaterialNameInCanvas = "HauptmenueZeileMaterial";
        //public string ZeileMaterialParent = "HauptmenueZeileContainer";
        //public int ZeileMaterialPosX = VerschiebungX + 860;
        //public int ZeileMaterialPosY = VerschiebungY - 241;
        //public int ZeileMaterialWidth = 20;
        //public int ZeileMaterialHeight = 20;
        //public Color32 ZeileMaterialColor = new Color32(255, 255, 255, 255);

        //HauptmenueZeileDetail
        public string ZeileDetailNameInCanvas = "HauptmenueZeileDetail";
        public string ZeileDetailParent = "HauptmenueZeileContainer";
        public string ZeileDetailText = "";
        public int ZeileDetailPosX = VerschiebungX + 80;
        public int ZeileDetailPosY = VerschiebungY - 294;
        public int ZeileDetailWidth = 920;
        public int ZeileDetailHeight = 50;
        public Color32 ZeileDetailColor = new Color32(255, 255, 200, 255);
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailFontSize = 25;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //HauptmenueZeileLogo
        public string ZeileLogoNameInCanvas = "HauptmenueZeileDetailLogo";
        public string ZeileLogoParent = "HauptmenueZeileContainer";
        public int ZeileLogoPosX = VerschiebungX;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public int ZeileLogoWidth = 60;
        public int ZeileLogoHeight = 100;
        public Color32 ZeileLogoColor = new Color32(0, 0, 0, 150);
        public string ZeileLogoSourceImage = "";//"Logos/list-with-dots_white";

        // HauptmenueZeilenNummer
        public string ZeileNummerNameInCanvas = "HauptmenueZeileNummer";
        public string ZeileNummerParent = "HauptmenueZeileContainer";
        public string ZeileNummerText = "";
        public int ZeileNummerPosX = VerschiebungX + 12;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public int ZeileNummerWidth = 60;
        public int ZeileNummerHeight = 100;
        public Color32 ZeileNummerColor = new Color32(255, 255, 255, 255);
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    }

    public class GuiInnentuer
    {
        public string Name = "Innentür";

        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static int VerschiebungX = 0;
        private static int VerschiebungY = 0;

        // Innentuer
        public string NameInCanvas = "Innentuer";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;
        public int Width = 960;
        public int Height = 1080;
        public Color32 Color = new Color32(0, 0, 0, 100);

        // InnentuerHeaderContainer
        public string HeaderContainerNameInCanvas = "InnentuerHeaderContainer";
        public string HeaderContainerParent = "Innentuer";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public int HeaderContainerHeight = 120;
        public Color32 HeaderContainerColor = new Color32(0, 0, 0, 250);

        // InnentuerHeaderLogo
        public string HeaderLogoNameInCanvas = "InnentuerHeaderLogo";
        public string HeaderLogoParent = "InnentuerHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public int HeaderLogoWidth = 80;
        public int HeaderLogoHeight = 80;
        public Color32 HeaderLogoColor = new Color32(255, 255, 255, 255);
        public string HeaderLogoSourceImage = "Logos/numbered-list_white";

        // InnentuerHeaderText
        public string HeaderTextNameInCanvas = "InnentuerHeaderText";
        public string HeaderTextParent = "InnentuerHeaderContainer";
        public string HeaderTextText = "Innentür";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public int HeaderTextWidth = 780;
        public int HeaderTextHeight = 120;
        public Color32 HeaderTextColor = new Color32(255, 255, 255, 255);
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //InnentuerInfoContainer
        public string InfoContainerNameInCanvas = "InnentuerInfoContainer";
        public string InfoContainerParent = "Innentuer";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public int InfoContainerHeight = 100;
        public Color32 InfoContainerColor = new Color32(0, 0, 0, 230);

        // InnentuerInfoText
        public string InfoTextNameInCanvas = "InnentuerInfoText";
        public string InfoTextParent = "InnentuerInfoContainer";
        public string InfoTextText = "wählen Sie bitte Voreinstellungen";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 123;
        public int InfoTextWidth = 780;
        public int InfoTextHeight = 120;
        public Color32 InfoTextColor = new Color32(255, 255, 255, 50);
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //InnentuerInfoLogo
        public string InfoLogoNameInCanvas = "InnentuerInfoLogo";
        public string InfoLogoParent = "InnentuerInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public int InfoLogoWidth = 60;
        public int InfoLogoHeight = 100;
        public Color32 InfoLogoColor = new Color32(0, 0, 0, 0); //ausgeblendet
        public string InfoLogoSourceImage = "";//"Logos/list-with-dots_white";

        //InnentuerHauptContainer
        public string HauptContainerNameInCanvas = "InnentuerHauptContainer";
        public string HauptContainerParent = "Innentuer";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public int HauptContainerHeight = 920;
        public Color32 HauptContainerColor = new Color32(0, 000, 0, 100);

        // ab hier die einzelnen Zeilen

        //InnentuerZeileContainer
        public int ZeileContainerAnzahlZeilen = 8; // momentan max. 8 wegen Höehe 1080 px
        public string ZeileContainerNameInCanvas = "InnentuerZeileContainer";
        public string ZeileContainerParent = "InnentuerHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public int ZeileContainerHeight = 100;
        public Color32 ZeileContainerColor = new Color32(0, 000, 0, 100);
        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        //InnentuerZeileHeader
        public string ZeileHeaderNameInCanvas = "InnentuerZeileHeader";
        public string ZeileHeaderParent = "InnentuerZeileContainer";
        public string ZeileHeaderText = "";
        public int ZeileHeaderPosX = VerschiebungX + 80;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public int ZeileHeaderWidth = 600;
        public int ZeileHeaderHeight = 60;
        public Color32 ZeileHeaderColor = new Color32(255, 255, 255, 255);
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderFontSize = 45;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        ////InnentuerZeileMaterial
        //public int ZeileMaterialAnzahlMaterialOptionenX = 4;
        //public int ZeileMaterialAnzahlMaterialOptionenY = 3;
        //public int ZeileMaterialAnzahlMaterialAbstandX = 10;
        //public int ZeileMaterialAnzahlMaterialAbstandY = 10;
        //public string ZeileMaterialNameInCanvas = "InnentuerZeileMaterial";
        //public string ZeileMaterialParent = "InnentuerZeileContainer";
        //public int ZeileMaterialPosX = VerschiebungX + 860;
        //public int ZeileMaterialPosY = VerschiebungY - 241;
        //public int ZeileMaterialWidth = 20;
        //public int ZeileMaterialHeight = 20;
        //public Color32 ZeileMaterialColor = new Color32(255, 255, 255, 255);

        //InnentuerZeileDetail
        public string ZeileDetailNameInCanvas = "InnentuerZeileDetail";
        public string ZeileDetailParent = "InnentuerZeileContainer";
        public string ZeileDetailText = "";
        public int ZeileDetailPosX = VerschiebungX + 80;
        public int ZeileDetailPosY = VerschiebungY - 294;
        public int ZeileDetailWidth = 920;
        public int ZeileDetailHeight = 50;
        public Color32 ZeileDetailColor = new Color32(255, 255, 200, 255);
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailFontSize = 25;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //InnentuerZeileLogo
        public string ZeileLogoNameInCanvas = "InnentuerZeileDetailLogo";
        public string ZeileLogoParent = "InnentuerZeileContainer";
        public int ZeileLogoPosX = VerschiebungX;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public int ZeileLogoWidth = 60;
        public int ZeileLogoHeight = 100;
        public Color32 ZeileLogoColor = new Color32(0, 0, 0, 150);
        public string ZeileLogoSourceImage = "";//"Logos/list-with-dots_white";

        // InnentuerZeilenNummer
        public string ZeileNummerNameInCanvas = "InnentuerZeileNummer";
        public string ZeileNummerParent = "InnentuerZeileContainer";
        public string ZeileNummerText = "";
        public int ZeileNummerPosX = VerschiebungX + 12;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public int ZeileNummerWidth = 60;
        public int ZeileNummerHeight = 100;
        public Color32 ZeileNummerColor = new Color32(255, 255, 255, 255);
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    }

    public class GuiKonfigurator
    {
        public string Name = "Konfigurator";
        public string[] AnzuzeigendeSpalteDerTabelleInnentuer = { "Zarge", "Tuerblatt", "DrueckerZier", "DrueckerFalz" };

        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static int VerschiebungX = 960;
        private static int VerschiebungY = 0;

        // Konfigurator
        public string NameInCanvas = "Konfigurator";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;
        public int Width = 960;
        public int Height = 1080;
        public Color32 Color = new Color32(0, 0, 0, 100);
        public Color32 ColorPressed = new Color32(135, 8, 11, 200);

        // KonfiguratorHeaderContainer
        public string HeaderContainerNameInCanvas = "KonfiguratorHeaderContainer";
        public string HeaderContainerParent = "Konfigurator";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public int HeaderContainerHeight = 120;
        public Color32 HeaderContainerColor = new Color32(0, 0, 0, 250);

        // KonfiguratorHeaderLogo
        public string HeaderLogoNameInCanvas = "KonfiguratorHeaderLogo";
        public string HeaderLogoParent = "KonfiguratorHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public int HeaderLogoWidth = 80;
        public int HeaderLogoHeight = 80;
        public Color32 HeaderLogoColor = new Color32(255, 255, 255, 255);
        public string HeaderLogoSourceImage = "Logos/list-with-dots_white";

        // KonfiguratorHeaderText
        public string HeaderTextNameInCanvas = "KonfiguratorHeaderText";
        public string HeaderTextParent = "KonfiguratorHeaderContainer";
        public string HeaderTextText = "Konfigurator";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public int HeaderTextWidth = 780;
        public int HeaderTextHeight = 120;
        public Color32 HeaderTextColor = new Color32(255, 255, 255, 255);
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorInfoContainer
        public string InfoContainerNameInCanvas = "KonfiguratorInfoContainer";
        public string InfoContainerParent = "Konfigurator";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public int InfoContainerHeight = 100;
        public Color32 InfoContainerColor = new Color32(0, 0, 0, 230);

        // KonfiguratorInfoText
        public string InfoTextNameInCanvas = "KonfiguratorInfoText";
        public string InfoTextParent = "KonfiguratorInfoContainer";
        public string InfoTextText = "Innentür";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 123;
        public int InfoTextWidth = 780;
        public int InfoTextHeight = 120;
        public Color32 InfoTextColor = new Color32(255, 255, 255, 255);
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorInfoLogo
        public string InfoLogoNameInCanvas = "KonfiguratorInfoLogo";
        public string InfoLogoParent = "KonfiguratorInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public int InfoLogoWidth = 60;
        public int InfoLogoHeight = 100;
        public Color32 InfoLogoColor = new Color32(0, 0, 0, 150);
        public string InfoLogoSourceImage = "";//"Logos/list-with-dots_white";

        // KonfiguratorInfoNummer
        public string InfoNummerNameInCanvas = "KonfiguratorInfoNummer";
        public string InfoNummerParent = "KonfiguratorInfoContainer";
        public string InfoNummerText = "0";
        public int InfoNummerPosX = VerschiebungX + 912;
        public int InfoNummerPosY = VerschiebungY - 132;
        public int InfoNummerWidth = 60;
        public int InfoNummerHeight = 100;
        public Color32 InfoNummerColor = new Color32(255, 255, 255, 255);
        public string InfoNummerFontType = "Arial.ttf";
        public int InfoNummerFontSize = 70;
        public FontStyle InfoNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorHauptContainer
        public string HauptContainerNameInCanvas = "KonfiguratorHauptContainer";
        public string HauptContainerParent = "Konfigurator";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public int HauptContainerHeight = 920;
        public Color32 HauptContainerColor = new Color32(0, 000, 0, 100);

        // ab hier die einzelnen Zeilen

        //KonfiguratorZeileContainer
        public int ZeileContainerAnzahlZeilen = 10; // momentan max. 8 wegen Höehe 1080 px
        public string ZeileContainerNameInCanvas = "KonfiguratorZeileContainer";
        public string ZeileContainerParent = "KonfiguratorHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public int ZeileContainerHeight = 100;
        public Color32 ZeileContainerColor = new Color32(0, 000, 0, 100);
        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        //KonfiguratorZeileHeader
        public string ZeileHeaderNameInCanvas = "KonfiguratorZeileHeader";
        public string ZeileHeaderParent = "KonfiguratorZeileContainer";
        public string ZeileHeaderText = "";
        public int ZeileHeaderPosX = VerschiebungX + 20;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public int ZeileHeaderWidth = 600;
        public int ZeileHeaderHeight = 40;
        public Color32 ZeileHeaderColor = new Color32(255, 255, 255, 255);
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderFontSize = 30;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorZeileMaterial
        public int ZeileMaterialAnzahlMaterialOptionenX = 4;
        public int ZeileMaterialAnzahlMaterialOptionenY = 2;
        public int ZeileMaterialAnzahlMaterialAbstandX = 10;
        public int ZeileMaterialAnzahlMaterialAbstandY = 10;
        public string ZeileMaterialNameInCanvas = "KonfiguratorZeileMaterial";
        public string ZeileMaterialParent = "KonfiguratorZeileContainer";
        public int ZeileMaterialPosX = VerschiebungX + 860;
        public int ZeileMaterialPosY = VerschiebungY - 241 ;
        public int ZeileMaterialWidth = 20;
        public int ZeileMaterialHeight = 20;
        public Color32 ZeileMaterialColor = new Color32(255, 255, 255, 255);

        //KonfiguratorZeileDetail
        public string ZeileDetailNameInCanvas = "KonfiguratorZeileDetail";
        public string ZeileDetailParent = "KonfiguratorZeileContainer";
        public string ZeileDetailText = "";
        public int ZeileDetailPosX = VerschiebungX + 20;
        public int ZeileDetailPosY = VerschiebungY -274;
        public int ZeileDetailWidth = 920;
        public int ZeileDetailHeight = 50;
        public Color32 ZeileDetailColor = new Color32(255, 255, 200, 255);
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailFontSize = 20;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic

        //KonfiguratorZeileLogo
        public string ZeileLogoNameInCanvas = "KonfiguratorZeileDetailLogo";
        public string ZeileLogoParent = "KonfiguratorZeileContainer";
        public int ZeileLogoPosX = VerschiebungX + 900;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public int ZeileLogoWidth = 60;
        public int ZeileLogoHeight = 100;
        public Color32 ZeileLogoColor = new Color32(0, 0, 0, 150);
        public string ZeileLogoSourceImage = "";//"Logos/list-with-dots_white";

        // KonfiguratorZeilenNummer
        public string ZeileNummerNameInCanvas = "KonfiguratorZeileNummer";
        public string ZeileNummerParent = "KonfiguratorZeileContainer";
        public string ZeileNummerText = "";
        public int ZeileNummerPosX = VerschiebungX + 912;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public int ZeileNummerWidth = 60;
        public int ZeileNummerHeight = 100;
        public Color32 ZeileNummerColor = new Color32(255, 255, 255, 255);
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
    }


    // Klassen für die inhaltliche Konfiguration der GUI-Elemente
    // die Konfiguration an sich erfolgt in der Methode konfiguration()
    public class HauptmenueParameter
    {
        public List<string> Menuepunkt { get; set; }
        public string[][] Toggle { get; set; }
        public string[][] ToggleAktion { get; set; }
        public int[] TogglePunktIndex { get; set; }
        public int MaximaleAnzahlMenuepunkte = 8;
        public int MaximaleAnzahlToogglePunkte = 10;
    }

    public class InnentuerParameter
    {
        public List<string> Menuepunkt { get; set; }
        public string[][] Toggle { get; set; }
        public string[][] ToggleAktion { get; set; }
        public int[] TogglePunktIndex { get; set; }
        public int MaximaleAnzahlMenuepunkte = 8;
        public int MaximaleAnzahlToogglePunkte = 10;
    }


    // Klassen, in denen die Auswahlergebnisse der Menüs Hauptmenue und Innentuer festgehalten werden
    public class HauptmenueOutputParameter
    {
        public string Konfiguratorschema { get; set; }
        public string Farbeschema { get; set; }
        public string Datenbank { get; set; } // noch keine Verwendung
    }

    public class InnentuerOutputParameter
    {
        public string HoeheDIN { get; set; }
        public string BreiteDIN { get; set; }
        public string Wandstaerke { get; set; }
        public string Bekleidungsbreite { get; set; }
        public string Oeffnungsrichtung { get; set; }
        public string Frontseite { get; set; }
    }


    // Klasse, in der die einzelnen Objektteile der aktuell getoggelte Innentür festgehalten werden
    // (entspricht der aktuell getoggelten Zeile der Tabelle Innentür) 
    public class AktuelleGetoggelteInnentuer
    {
        public int Index { get; set; }

        public List<string> Liste = new List<string>();
        public string Id { get; set; }
        public string Detail { get; set; }
        public string Zarge { get; set; }
        public string Tuerblatt { get; set; }
        public string Band1 { get; set; }
        public string Band2 { get; set; }
        public string Bandaufnahme1 { get; set; }
        public string Bandaufnahme2 { get; set; }
        public string DrueckerFalz { get; set; }
        public string DrueckerZier { get; set; }
        public string Schlosskasten { get; set; }
        public string Schliessblech { get; set; }
        public string Schwelle { get; set; }

    }

    public class AktuelleGetoggeltesMaterial
    {
        public string Zarge { get; set; }
        public string Tuerblatt { get; set; }
        public string Band1 { get; set; }
        public string Band2 { get; set; }
        public string Bandaufnahme1 { get; set; }
        public string Bandaufnahme2 { get; set; }
        public string DrueckerFalz { get; set; }
        public string DrueckerZier { get; set; }
        public string Schlosskasten { get; set; }
        public string Schliessblech { get; set; }
        public string Schwelle { get; set; }
    }



    // Klasse, in der für die aktuellen Ausgaben und aktuell getoggelten Materialien festgehalten werden
    public class AktuelleAnzeigeKonfigurator
    {
        public string InfoText { get; set; }
        public string [] ZeileHeader { get; set; }
        public string[] ZeileDetail { get; set; }
        public int [] AnzahlMaterialien { get; set; }
        public int [] AktuellesMaterialIndex { get; set; }
        public string [] AktuellesMaterialName { get; set; }
    }
    
    //int aktuellGetoggeltesMaterialZargeIndex = 0;
    //int aktuellGetoggeltesMaterialTuerblattIndex = 0;
    //int aktuellGetoggeltesMaterialDrueckerZierIndex = 0;
    //int aktuellGetoggeltesMaterialDrueckerFalzIndex = 0;

    // Klasse zur Ermittlung der Materialien aus dem Json-String
    public class MaterialList
    {
        public string[] Mat1 { get; set; }
        public string[] Mat2 { get; set; }
        public string[] Mat3 { get; set; }
        public string[] Mat4 { get; set; }
        public string[] Mat5 { get; set; }
    }



    // ---------------------------------------------------------------------------------------------------
    // Variablen
    // ---------------------------------------------------------------------------------------------------


    // Variablen für den Aufbau der GUI-Elemente
    private Canvas myCanvas;
    private GameObject myGO;
    private Image myImage;
    private Text myText;

    // Hilfsvariable: Solange true (= Lerp-Vorgang des vorherigen Tastatemdrucks ist noch nocht abgeschlossen),
    // werden alle Tastatureingaben blockiert
    public bool colorLerpAktive = false;


    // Instanziierung der Klassen
    GuiCanvas guiCanvas = new GuiCanvas();
    GuiHauptmenue guiHauptmenue = new GuiHauptmenue();
    GuiInnentuer guiInnentuer = new GuiInnentuer();
    GuiKonfigurator guiKonfigurator = new GuiKonfigurator();
    HauptmenueParameter hauptmenueParameter = new HauptmenueParameter();
    InnentuerParameter innentuerParameter = new InnentuerParameter();
    HauptmenueOutputParameter hauptmenueOutputParameter = new HauptmenueOutputParameter();
    InnentuerOutputParameter innentuerOutputParameter = new InnentuerOutputParameter();
    
    AktuelleGetoggelteInnentuer aktuelleGetoggelteInnentuer = new AktuelleGetoggelteInnentuer();
    AktuelleGetoggeltesMaterial aktuelleGetoggelteMaterial = new AktuelleGetoggeltesMaterial();
    AktuelleAnzeigeKonfigurator aktuelleAnzeigeKonfigurator = new AktuelleAnzeigeKonfigurator();


    MaterialList materialListe = new MaterialList();

    List<T_Innentuer> ergebnisInnentuer = new List<T_Innentuer>();

    List<T_Innentuer> ergebnisInnentuerNachCheckHoehe = new List<T_Innentuer>();
    List<T_Innentuer> ergebnisInnentuerNachCheckBreite = new List<T_Innentuer>();
    List<T_Innentuer> ergebnisInnentuerNachCheckWandstaerke = new List<T_Innentuer>();
    List<T_Innentuer> ergebnisInnentuerNachCheckBekleidungsbreite = new List<T_Innentuer>();



    // Datenbank
    // lokale Listen (1:1 Abbilder der der remote Tabellen)
    List<T_Band> tabelleBand = new List<T_Band>();
    List<T_Bandaufnahme> tabelleBandaufnahme = new List<T_Bandaufnahme>();
    List<T_Druecker> tabelleDruecker = new List<T_Druecker>();
    List<T_Schliessblech> tabelleSchliessblech = new List<T_Schliessblech>();
    List<T_Schlosskasten> tabelleSchlosskasten = new List<T_Schlosskasten>();
    List<T_Schwelle> tabelleSchwelle = new List<T_Schwelle>();
    List<T_Tuerblatt> tabelleTuerblatt = new List<T_Tuerblatt>();
    List<T_Zarge> tabelleZarge = new List<T_Zarge>();
    List<T_Innentuer> tabelleInnentuer = new List<T_Innentuer>();
    List<T_Hersteller> tabelleHersteller = new List<T_Hersteller>();
    List<T_MAT> tabelleMAT = new List<T_MAT>();
    List<T_Objektteil> tabelleObjektteil = new List<T_Objektteil>();







 

    // ---------------------------------------------------------------------------------------------------
    // Initialaktionen
    // ---------------------------------------------------------------------------------------------------

    void Start()
    {
        // Datenbankabfrage
        initiiereDatenbankAbfragen();

        // GUI: Rahmen
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
        generiereHauptmenueHauptContainerZeilen(guiHauptmenue.ZeileContainerAnzahlZeilen);

        //// GUI: Innentuer
        generiereInnentuerContainer();
        generiereInnentuerHeaderContainer();
        generiereInnentuerHeaderLogo();
        generiereInnentuerHeaderText();
        generiereInnentuerInfoContainer();
        generiereInnentuerInfoText();
        generiereInnentuerInfoLogo();
        generiereInnentuerHauptContainer();
        generiereInnentuerHauptContainerZeilen(guiInnentuer.ZeileContainerAnzahlZeilen);

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
        generiereKonfiguratorHauptContainerZeilen(guiKonfigurator.ZeileContainerAnzahlZeilen);

        konfigurationen();

        updateHauptmenue(hauptmenueParameter);
        updateInnentuer(innentuerParameter);

        // lege Startsituation fest
        ausgangssituation();
       // zeigeMenuAn(aktuellesMenu);

        //// ermittele die höchste MenüPunkt-ID
        //if (innentuerPunkt.Count > 0)
        //{
        //    var item = innentuerPunkt[innentuerPunkt.Count - 1];
        //    hoechsteMenuId = item.Id;
        //}

    }


    void konfigurationen()
    {

        // Hauptmenü
        hauptmenueParameter.Menuepunkt = new List<string>();
        // Bezeichnung der Menüpunkte, wie sie angezeigt werden sollen
        hauptmenueParameter.Menuepunkt.Add("Konfiguratorschema");
        hauptmenueParameter.Menuepunkt.Add("Farbeschema");
        hauptmenueParameter.Menuepunkt.Add("Synchronisation Datenbank");

        // Bezeichnung der zu toggelnden Werte je Untermenü, wie sie angezeigt werden sollen
        hauptmenueParameter.Toggle = new string[][] {
            new string[] { "Innentür" },
            new string[] { "dunkel", "hell" },
            new string[] { "" }
        };

        // Bezeichnung der zu toggelnden Werte je Untermenü, wie sie in den Ausgabewerten für
        // [hauptmenueOutputParameter] gesetzt werden sollen
        hauptmenueParameter.ToggleAktion = new string[][] {
            new string[] { "Innentür" },
            new string[] { "dunkel", "hell" },
            new string[] { "Thomas" }
        };

        hauptmenueParameter.TogglePunktIndex = new int[hauptmenueParameter.Menuepunkt.Count()];
        hauptmenueParameter.TogglePunktIndex[0] = 0;
        hauptmenueParameter.TogglePunktIndex[1] = 0;
        hauptmenueParameter.TogglePunktIndex[2] = 0;


        // Innentuer
        innentuerParameter.Menuepunkt = new List<string>();
        innentuerParameter.Menuepunkt.Add("Höhe");
        innentuerParameter.Menuepunkt.Add("Breite");
        innentuerParameter.Menuepunkt.Add("Wandstärke");
        innentuerParameter.Menuepunkt.Add("Bekleidungsbreite");
        innentuerParameter.Menuepunkt.Add("Öffnungsrichtung");
        innentuerParameter.Menuepunkt.Add("Frontseite");

        innentuerParameter.Toggle = new string[][] {
            new string[] { "alle", "1875 mm (DIN) | 1885 mm (WÖM) | 1860 mm (TAM)", "2010 mm (DIN) | 2010 mm (WÖM) | 1985 mm (TAM)", "2125 mm (DIN) | 2135 mm (WÖM) | 2110 mm (TAM)", "2250 mm (DIN) | 2260 mm (WÖM) | 2235 mm (TAM)" },
            new string[] { "alle", "625 mm (DIN) | 635 mm (WÖM) | 610 mm (TAM)", "750 mm (DIN) | 760 mm (WÖM) | 735 mm (TAM)", "875 mm (DIN) | 885 mm (WÖM) | 860 mm (TAM)", "1000 mm (DIN) | 1010 mm (WÖM) | 985 mm (TAM)", "1125 mm (DIN) | 1135 mm (WÖM) | 1110 mm (TAM)" },
            new string[] { "alle", "115 mm", "180 mm", "200 mm", "265 mm", "300 mm"  },
            new string[] { "alle", "50 mm" },
            new string[] { "DIN links", "DIN rechts" },
            new string[] { "Falzfront", "Zierfront" }
        };

        innentuerParameter.ToggleAktion = new string[][] {
            new string[] { "%", "1875", "2000", "2125", "2250" },
            new string[] { "%", "625", "750", "875", "1000", "1125" },
            new string[] { "%", "115", "180", "200", "265", "300"  },
            new string[] { "%", "50" },
            new string[] { "DIN links", "DIN rechts" },
            new string[] { "Falzfront", "Zierfront" }
        };

        innentuerParameter.TogglePunktIndex = new int[innentuerParameter.Menuepunkt.Count()];
        for (int i = 0; i < innentuerParameter.Menuepunkt.Count(); i++)
        {
            innentuerParameter.TogglePunktIndex[i] = 0;
        }

        aktuelleAnzeigeKonfigurator.ZeileHeader = new string[guiKonfigurator.ZeileContainerAnzahlZeilen];
        for (int i = 0; i < guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
        {
            aktuelleAnzeigeKonfigurator.ZeileHeader[i] = "";
        }
        
        aktuelleAnzeigeKonfigurator.ZeileDetail = new string[guiKonfigurator.ZeileContainerAnzahlZeilen];
        for (int i = 0; i < guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
        {
            aktuelleAnzeigeKonfigurator.ZeileDetail[i] = "";
        }

        aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex = new int[guiKonfigurator.ZeileContainerAnzahlZeilen];
        for (int i = 0; i < guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
        {
            aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[i] = 0;
        }
        
        aktuelleAnzeigeKonfigurator.AktuellesMaterialName = new string[guiKonfigurator.ZeileContainerAnzahlZeilen];
        for (int i = 0; i < guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
        {
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[i] = "";
        }

        aktuelleAnzeigeKonfigurator.AnzahlMaterialien = new int[guiKonfigurator.ZeileContainerAnzahlZeilen];
        for (int i = 0; i < guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
        {
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[i] = 0;
        }


    }


    // Konfiguration der Ausgangssituation
    void ausgangssituation()
    {

        // wird beim Toggeln zwischen den Menüpunkten (Hauptmenue, Innentuer und Konfigurator) benötigt    
        guiCanvas.AktuellAktivesMenu = "";

        aktuelleGetoggelteInnentuer.Index = 0;

        innentuerOutputParameter.HoeheDIN = "%";
        innentuerOutputParameter.BreiteDIN = "%";
        innentuerOutputParameter.Oeffnungsrichtung = "%";
        innentuerOutputParameter.Wandstaerke = "%";
        innentuerOutputParameter.Bekleidungsbreite = "%";


        ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
        GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();


        // blende alle Menüs aus
        GameObject.Find("Hauptmenue").GetComponent<CanvasGroup>().alpha = 0.0f;
        GameObject.Find("Innentuer").GetComponent<CanvasGroup>().alpha = 0.0f;
        GameObject.Find("Konfigurator").GetComponent<CanvasGroup>().alpha = 0.0f;

        GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + "3").GetComponent<Text>().text = "letzte Aktualisierung : " + DateTime.Now.ToString("dd.MM.yyyy | HH:mm:ss");

        aktuelleGetoggelteInnentuer.Schwelle = "";
        aktuelleGetoggelteMaterial.Schwelle = "";

    }


    // ---------------------------------------------------------------------------------------------------
    // Checks je Frame
    // ---------------------------------------------------------------------------------------------------

    void Update()
    {
        //lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 11));
        // Tastenaktionen 0-9
        if (!colorLerpAktive)
        {
            if (Input.GetKeyDown("0"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("0");
            }

            if (Input.GetKeyDown("1"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("1");
            }

            if (Input.GetKeyDown("2"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("2");
            }
            if (Input.GetKeyDown("3"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("3");
            }

            if (Input.GetKeyDown("4"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("4");
            }

            if (Input.GetKeyDown("5"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("5");
            }

            if (Input.GetKeyDown("6"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("6");
            }

            if (Input.GetKeyDown("7"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("7");
            }
            if (Input.GetKeyDown("8"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("8");
            }

            if (Input.GetKeyDown("9"))
            {
                ermitteleAktionAnhandEingabeUndAktivemMenue("9");
            }


            // Tastenaktionen F-Tasten

            // toggle Menü Hauptmenü
            if (Input.GetKeyDown("f1"))
            {
                toggleMenue(guiHauptmenue.Name, guiCanvas.AktuellAktivesMenu);
            }

            // Toggle Menü Innentür
            if (Input.GetKeyDown("f2"))
            {
                toggleMenue(guiInnentuer.Name, guiCanvas.AktuellAktivesMenu);
            }

            // Toggle Menü Konfigurator
            if (Input.GetKeyDown("f3"))
            {
                toggleMenue(guiKonfigurator.Name, guiCanvas.AktuellAktivesMenu);
            }


        }

    }




    // ---------------------------------------------------------------------------------------------------
    // MAIN
    // ---------------------------------------------------------------------------------------------------


    // schreibe aus allen DB-Tabellen die Daten in lokale Tabellen
    void initiiereDatenbankAbfragen()
    {
        tabelleBand = dBAbfrageBand();
        tabelleBandaufnahme = dBAbfrageBandaufnahme();
        tabelleDruecker = dBAbfrageDruecker();
        tabelleSchliessblech = dBAbfrageSchliessblech();
        tabelleSchlosskasten = dBAbfrageSchlosskasten();
        tabelleSchwelle = dBAbfrageSchwelle();
        tabelleTuerblatt = dBAbfrageTuerblatt();
        tabelleZarge = dBAbfrageZarge();
        tabelleInnentuer = dBAbfrageInnentuer();
        tabelleHersteller = dBAbfrageHersteller();
        tabelleMAT = dBAbfrageMAT();
        tabelleObjektteil = dBAbfrageObjektteil();
    }


    void toggleMenue(string menueName, string aktivesMenue)
    {

        //Debug.Log("rufe toggleMenue auf mit den Parametern ( menueName : " + menueName + " - aktivesMenue : " + aktivesMenue + " )");
        switch (menueName)
        {

            // Block Hauptmenü
            case "Hauptmenü":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiHauptmenue.Name;
                    }
                    else if (aktivesMenue == guiHauptmenue.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        guiCanvas.AktuellAktivesMenu = "";
                    }
                    else if (aktivesMenue == guiInnentuer.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiHauptmenue.Name;
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    else if (aktivesMenue == guiKonfigurator.Name)
                    {
                        //Debug.Log("case: Hauptmenü - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiHauptmenue.Name;
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    break;
                }


            case "Innentür":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiInnentuer.Name;
                    }
                    else if (aktivesMenue == guiHauptmenue.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiInnentuer.Name;
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    else if (aktivesMenue == guiInnentuer.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        if (GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha == 0.5f)
                        {
                            GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                            guiCanvas.AktuellAktivesMenu = guiKonfigurator.Name;
                        }
                        else
                        {
                            guiCanvas.AktuellAktivesMenu = "";
                        }
                    }
                    else if (aktivesMenue == guiKonfigurator.Name)
                    {
                        //Debug.Log("case: Innentür - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiInnentuer.Name;
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.5f;
                    }
                    break;
                }


            case "Konfigurator":
                {
                    if (aktivesMenue == "")
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiKonfigurator.Name;
                        // Spezial
                        aktuelleGetoggelteInnentuer.Index = 0;
                        uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
                        //
                    }
                    else if (aktivesMenue == guiHauptmenue.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiKonfigurator.Name;
                        // Spezial
                        aktuelleGetoggelteInnentuer.Index = 0;
                        uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
                        //
                        GameObject.Find(guiHauptmenue.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                    }
                    else if (aktivesMenue == guiInnentuer.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                        guiCanvas.AktuellAktivesMenu = guiKonfigurator.Name;
                        // Spezial
                        //Debug.Log("ergebnisInnentuer :" + ergebnisInnentuer.Count().ToString());
                        aktuelleGetoggelteInnentuer.Index = 0;
                        uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
                        //
                        GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.5f;
                    }
                    else if (aktivesMenue == guiKonfigurator.Name)
                    {
                        //Debug.Log("case: Konfigurator - aktivesMenue : " + aktivesMenue);
                        GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha = 0.0f;
                        if (GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha == 0.5f)
                        {
                            GameObject.Find(guiInnentuer.NameInCanvas).GetComponent<CanvasGroup>().alpha = 1.0f;
                            guiCanvas.AktuellAktivesMenu = guiInnentuer.Name;
                        }
                        else
                        {
                            guiCanvas.AktuellAktivesMenu = "";
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


    void ermitteleAktionAnhandEingabeUndAktivemMenue(string eingabe)
    {
        switch (eingabe)
        {
            case "0":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("10");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("20");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("30");
                    }
                    break;
                }
            case "1":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("11");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("21");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("31");
                    }
                    break;
                }
            case "2":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("12");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("22");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("32");
                    }
                    break;
                }
            case "3":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("13");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("23");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("33");
                    }
                    break;
                }
            case "4":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("14");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("24");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("34");
                    }
                    break;
                }
            case "5":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("15");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("25");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("35");
                    }
                    break;
                }
            case "6":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("16");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("26");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("36");
                    }
                    break;
                }
            case "7":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("17");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("27");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("37");
                    }
                    break;
                }
            case "8":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("18");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("28");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("38");
                    }
                    break;
                }
            case "9":
                {
                    if (guiCanvas.AktuellAktivesMenu == guiHauptmenue.Name)
                    {
                        fuehreAktionAus("19");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiInnentuer.Name)
                    {
                        fuehreAktionAus("29");
                    }
                    else if (guiCanvas.AktuellAktivesMenu == guiKonfigurator.Name)
                    {
                        fuehreAktionAus("39");
                    }
                    break;
                }
            default:
                {
                    Debug.Log("ACHTUNG: KEINE AKTION HINTERLEGT");
                    break;
                }
        }



    }


    //Konfiguration der zu den jeweiligen Menüpunkten zugehörigen auszuführenden Aktionen
    void fuehreAktionAus(string aktion)
    {
        //Debug.Log("Führe Aktion: " + aktion + " aus");

        switch (aktion)
        {

            case "11":
                {
                    toggleMenuePunkt(1, 1);
                    hauptmenueOutputParameter.Konfiguratorschema = hauptmenueParameter.ToggleAktion[0][hauptmenueParameter.TogglePunktIndex[0]];
                    StartCoroutine(LerpColor(guiHauptmenue.ZeileContainerNameInCanvas + "1"));
                    //Debug.Log("Setze Konfiguratorschema = " + hauptmenueOutputParameter.Konfiguratorschema);
                    break;
                }
            case "12":
                {

                    toggleMenuePunkt(1, 2);
                    hauptmenueOutputParameter.Farbeschema = hauptmenueParameter.ToggleAktion[1][hauptmenueParameter.TogglePunktIndex[1]];
                    StartCoroutine(LerpColor(guiHauptmenue.ZeileContainerNameInCanvas + "2"));
                    //Debug.Log("Setze Farbschema = " + hauptmenueOutputParameter.Farbeschema);
                    break;
                }
            case "13":
                {
                    toggleMenuePunkt(1, 3);
                    hauptmenueOutputParameter.Datenbank = hauptmenueParameter.ToggleAktion[2][hauptmenueParameter.TogglePunktIndex[2]];
                    StartCoroutine(LerpColor(guiHauptmenue.ZeileContainerNameInCanvas + "3"));
                    //Debug.Log("Synchronisation Datenbank = " + hauptmenueOutputParameter.Datenbank);
                    // Spezial
                    initiiereDatenbankAbfragen();
                    GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + "3").GetComponent<Text>().text = "letzte Aktualisierung : " + DateTime.Now.ToString("dd.MM.yyyy | HH:mm:ss");
                    //
                    break;
                }
            case "21":
                {
                    toggleMenuePunkt(2, 1);
                    innentuerOutputParameter.HoeheDIN = innentuerParameter.ToggleAktion[0][innentuerParameter.TogglePunktIndex[0]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "1"));
                    //Debug.Log("Setze HoeheDIN = " + innentuerOutputParameter.HoeheDIN);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "22":
                {
                    toggleMenuePunkt(2, 2);
                    innentuerOutputParameter.BreiteDIN = innentuerParameter.ToggleAktion[1][innentuerParameter.TogglePunktIndex[1]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "2"));
                    //Debug.Log("Setze BreiteDIN = " + innentuerOutputParameter.BreiteDIN);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "23":
                {
                    toggleMenuePunkt(2, 3);
                    innentuerOutputParameter.Wandstaerke = innentuerParameter.ToggleAktion[2][innentuerParameter.TogglePunktIndex[2]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "3"));
                    //Debug.Log("Setze Wandstärke = " + innentuerOutputParameter.Wandstaerke);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "24":
                {
                    toggleMenuePunkt(2, 4);
                    innentuerOutputParameter.Bekleidungsbreite = innentuerParameter.ToggleAktion[3][innentuerParameter.TogglePunktIndex[3]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "4"));
                    //Debug.Log("Setze Bekleidungsbreite = " + innentuerOutputParameter.Bekleidungsbreite);
                    ergebnisInnentuer = ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien();
                    GameObject.Find(guiInnentuer.InfoTextNameInCanvas).GetComponent<Text>().text = "Treffer in der Datenbank mit aktuellen Kriterien: " + ergebnisInnentuer.Count().ToString();
                    break;
                }
            case "25":
                {
                    toggleMenuePunkt(2, 5);
                    innentuerOutputParameter.Oeffnungsrichtung = innentuerParameter.ToggleAktion[4][innentuerParameter.TogglePunktIndex[4]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "5"));
                    //Debug.Log("Setze Öffnungsrichtung = " + innentuerOutputParameter.Oeffnungsrichtung);
                    break;
                }
            case "26":
                {
                    toggleMenuePunkt(2, 6);
                    innentuerOutputParameter.Frontseite = innentuerParameter.ToggleAktion[5][innentuerParameter.TogglePunktIndex[5]];
                    StartCoroutine(LerpColor(guiInnentuer.ZeileContainerNameInCanvas + "6"));
                    //Debug.Log("Setze Frontseite = " + innentuerOutputParameter.Frontseite);
                    break;
                }
            case "30":
                {
                    toggleMenuePunkt(3, 0);
                    StartCoroutine(LerpColor(guiKonfigurator.InfoContainerNameInCanvas));
                    //Debug.Log("Toggle Innentuer = " + "");
                    toggleInnentuerImKonfigurator();
                    break;
                }
            case "31":
                { 
                    toggleMenuePunkt(3, 1);
                    StartCoroutine(LerpColor(guiKonfigurator.ZeileContainerNameInCanvas + "1"));
                    //Debug.Log("Toggle Material Zarge = " + "");
                    toggleMaterialImKonfigurator(1);
                    break;
                }
            case "32":
                {
                    toggleMenuePunkt(3, 2);
                    StartCoroutine(LerpColor(guiKonfigurator.ZeileContainerNameInCanvas + "2"));
                    //Debug.Log("Toggle Material Tuerblatt = " + "");
                    toggleMaterialImKonfigurator(2);
                    break;
                }
            case "33":
                {
                    toggleMenuePunkt(3, 3);
                    StartCoroutine(LerpColor(guiKonfigurator.ZeileContainerNameInCanvas + "3"));
                    //Debug.Log("Toggle Material DrueckerFalz = " + "");
                    toggleMaterialImKonfigurator(3);
                    break;
                }
            case "34":
                {
                    toggleMenuePunkt(3, 4);
                    StartCoroutine(LerpColor(guiKonfigurator.ZeileContainerNameInCanvas + "4"));
                    //Debug.Log("Toggle Material DrueckerZier = " + "");
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


    void uebergiebParameterAusMenueInnentuerInMenueKonfigurator(List<T_Innentuer> ergebnisInnentuer)
    {
        MaterialList [] materialListeFuerDieseSpalte;
        //Debug.Log("ergebnisInnentuer :" + ergebnisInnentuer.Count().ToString());

        aktuelleAnzeigeKonfigurator.InfoText = "Innentür";
        if (ergebnisInnentuer.Count() == 0)
        {
            aktuelleAnzeigeKonfigurator.InfoText += "\nKeine Treffer bei den aktuellen Suchkriterien.";
            for (int i = 1; i <= guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
            {
                GameObject.Find("KonfiguratorZeileContainer" + i.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;
            }
        }
        else
        {

            aktuelleAnzeigeKonfigurator.InfoText += " (" + (aktuelleGetoggelteInnentuer.Index + 1).ToString() + " von " + ergebnisInnentuer.Count().ToString() + ")";
            aktuelleAnzeigeKonfigurator.InfoText += "\n" + ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Id;

            // Zeile 1 Zarge
            aktuelleAnzeigeKonfigurator.ZeileHeader[0] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Zarge;
            aktuelleAnzeigeKonfigurator.ZeileDetail[0] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Zarge).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Zarge).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[0] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[0]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[0] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 2 Tuerblatt
            aktuelleAnzeigeKonfigurator.ZeileHeader[1] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Tuerblatt;
            aktuelleAnzeigeKonfigurator.ZeileDetail[1] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Tuerblatt).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Tuerblatt).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[1] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[1]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[1] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 3 DrueckerFalz
            aktuelleAnzeigeKonfigurator.ZeileHeader[2] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerFalz;
            aktuelleAnzeigeKonfigurator.ZeileDetail[2] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerFalz).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerFalz).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[2] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[2]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[2] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 4 DrueckerFalz
            aktuelleAnzeigeKonfigurator.ZeileHeader[3] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerZier;
            aktuelleAnzeigeKonfigurator.ZeileDetail[3] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerZier).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerZier).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[3] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[3]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[3] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 5 Band1
            aktuelleAnzeigeKonfigurator.ZeileHeader[4] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1;
            aktuelleAnzeigeKonfigurator.ZeileDetail[4] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[4] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[4]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[4] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 6 Band2
            aktuelleAnzeigeKonfigurator.ZeileHeader[5] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band2;
            aktuelleAnzeigeKonfigurator.ZeileDetail[5] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band2).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band2).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[5] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[5]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[5] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 7 Bandaufnahme1
            aktuelleAnzeigeKonfigurator.ZeileHeader[6] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme1;
            aktuelleAnzeigeKonfigurator.ZeileDetail[6] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme1).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme1).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[6] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[6]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[6] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 8 Bandaufnahme2
            aktuelleAnzeigeKonfigurator.ZeileHeader[7] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme2;
            aktuelleAnzeigeKonfigurator.ZeileDetail[7] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme2).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme2).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[7] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[7]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[7] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 9 Schlosskasten
            aktuelleAnzeigeKonfigurator.ZeileHeader[8] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schlosskasten;
            aktuelleAnzeigeKonfigurator.ZeileDetail[8] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schlosskasten).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schlosskasten).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[8] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[8]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[8] = materialListeFuerDieseSpalte[0].Mat1.Count();

            // Zeile 10 Schliessblech
            aktuelleAnzeigeKonfigurator.ZeileHeader[9] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schliessblech;
            aktuelleAnzeigeKonfigurator.ZeileDetail[9] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schliessblech).Detail;
            materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schliessblech).MAT);
            aktuelleAnzeigeKonfigurator.AktuellesMaterialName[9] = materialListeFuerDieseSpalte[0].Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[9]];
            aktuelleAnzeigeKonfigurator.AnzahlMaterialien[9] = materialListeFuerDieseSpalte[0].Mat1.Count();

            //// Zeile 11 Schwelle
            //aktuelleAnzeigeKonfigurator.ZeileHeader[10] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schwelle;
            //aktuelleAnzeigeKonfigurator.ZeileDetail[10] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schwelle).Detail;
            //materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schwelle).MAT);
            //aktuelleAnzeigeKonfigurator.AktuellesMaterialName[10] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[10]];
            //aktuelleAnzeigeKonfigurator.AnzahlMaterialien[10] = materialListeFuerDieseSpalte.Mat1.Count();


            updateKonfigurator();
        }
    }


    //void uebergiebParameterAusMenueInnentuerInMenueKonfigurator2(List<T_Innentuer> ergebnisInnentuer)
    //{
    //    MaterialList materialListeFuerDieseSpalte;
    //    //Debug.Log("ergebnisInnentuer :" + ergebnisInnentuer.Count().ToString());

    //    aktuelleAnzeigeKonfigurator.InfoText = "Innentür";
    //    if (ergebnisInnentuer.Count() == 0)
    //    {
    //        aktuelleAnzeigeKonfigurator.InfoText += "\nKeine Treffer bei den aktuellen Suchkriterien.";
    //        for (int i = 1; i <= guiKonfigurator.ZeileContainerAnzahlZeilen; i++)
    //        {
    //            GameObject.Find("KonfiguratorZeileContainer" + i.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;
    //        }
    //    }
    //    else
    //    {

    //        aktuelleAnzeigeKonfigurator.InfoText += " (" + (aktuelleGetoggelteInnentuer.Index + 1).ToString() + " von " + ergebnisInnentuer.Count().ToString() + ")";
    //        aktuelleAnzeigeKonfigurator.InfoText += "\n" + ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Id;

    //        // Zeile 1 Zarge
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[0] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Zarge;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[0] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Zarge).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Zarge).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[0] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[0]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[0] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 2 Tuerblatt
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[1] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Tuerblatt;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[1] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Tuerblatt).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Tuerblatt).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[1] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[1]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[1] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 3 DrueckerFalz
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[2] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerFalz;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[2] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerFalz).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerFalz).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[2] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[2]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[2] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 4 DrueckerFalz
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[3] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerZier;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[3] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerZier).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerZier).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[3] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[3]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[3] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 5 Band1
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[4] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[4] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[4] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[4]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[4] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 6 Band2
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[5] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band2;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[5] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band2).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band2).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[5] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[5]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[5] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 7 Bandaufnahme1
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[6] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme1;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[6] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme1).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme1).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[6] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[6]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[6] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 8 Bandaufnahme2
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[7] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme2;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[7] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme2).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme2).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[7] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[7]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[7] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 9 Schlosskasten
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[8] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schlosskasten;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[8] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schlosskasten).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schlosskasten).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[8] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[8]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[8] = materialListeFuerDieseSpalte.Mat1.Count();

    //        // Zeile 10 Schliessblech
    //        aktuelleAnzeigeKonfigurator.ZeileHeader[9] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schliessblech;
    //        aktuelleAnzeigeKonfigurator.ZeileDetail[9] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schliessblech).Detail;
    //        materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schliessblech).MAT);
    //        aktuelleAnzeigeKonfigurator.AktuellesMaterialName[9] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[9]];
    //        aktuelleAnzeigeKonfigurator.AnzahlMaterialien[9] = materialListeFuerDieseSpalte.Mat1.Count();

    //        //// Zeile 11 Schwelle
    //        //aktuelleAnzeigeKonfigurator.ZeileHeader[10] = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schwelle;
    //        //aktuelleAnzeigeKonfigurator.ZeileDetail[10] = tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schwelle).Detail;
    //        //materialListeFuerDieseSpalte = ermitteleMaterialien(tabelleObjektteil.Find(x => x.Id == ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schwelle).MAT);
    //        //aktuelleAnzeigeKonfigurator.AktuellesMaterialName[10] = materialListeFuerDieseSpalte.Mat1[aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[10]];
    //        //aktuelleAnzeigeKonfigurator.AnzahlMaterialien[10] = materialListeFuerDieseSpalte.Mat1.Count();


    //        updateKonfigurator();
    //    }
    //}


    void toggleInnentuerImKonfigurator()
    {

        // toggle nur, wenn es mindestens eine Zeile in der Ergebnistabelle gibt 
        // und wenn Konfigurator-Menü Sichtbar ist
        if ((ergebnisInnentuer.Count() >= 0) && (GameObject.Find(guiKonfigurator.NameInCanvas).GetComponent<CanvasGroup>().alpha == 1.0f))
        {
            // Info Anzeige
            if (aktuelleGetoggelteInnentuer.Index < (ergebnisInnentuer.Count()-1))
            {
                aktuelleGetoggelteInnentuer.Index++;
                uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
            }
            else
            {
                aktuelleGetoggelteInnentuer.Index = 0;
                uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);
            }
        }

    }


    void toggleMaterialImKonfigurator(int menuepunkt)
    {

        if (aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[(menuepunkt -1)] < (aktuelleAnzeigeKonfigurator.AnzahlMaterialien[(menuepunkt - 1)] - 1))
        {
            aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[(menuepunkt - 1)]++;
        }
        else
        {
            aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[(menuepunkt - 1)] = 0;
        }
        Debug.Log("##### ##### ##### aktuellGetoggeltesMaterial zu Menüpunkt " + (menuepunkt - 1).ToString() + " : " + aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[(menuepunkt - 1)] + " : " + aktuelleAnzeigeKonfigurator.AktuellesMaterialName[(menuepunkt - 1)]);

        //updateKonfigurator();
        uebergiebParameterAusMenueInnentuerInMenueKonfigurator(ergebnisInnentuer);

    }


    void updateKonfigurator()
    {


        GameObject.Find(guiKonfigurator.InfoTextNameInCanvas).GetComponent<Text>().text = aktuelleAnzeigeKonfigurator.InfoText;

        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (int zeileNr = 1; zeileNr <= guiKonfigurator.ZeileContainerAnzahlZeilen; zeileNr++)
        {
            GameObject.Find(guiKonfigurator.ZeileHeaderNameInCanvas + zeileNr.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + zeileNr.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiKonfigurator.ZeileContainerNameInCanvas + zeileNr.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;

            // blende alle Materialien des Menüpunktes aus
            for (int materialNr = 1; materialNr <= (guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenX * guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenY); materialNr++)
            {
                GameObject.Find("KonfiguratorZeileMaterial" + zeileNr + materialNr).GetComponent<CanvasGroup>().alpha = 0.0f;
            }

            // blende alle Materialien des Menüpunktes aus
            for (int materialNr = 1; materialNr <= aktuelleAnzeigeKonfigurator.AnzahlMaterialien[(zeileNr - 1)]; materialNr++)
            {
                GameObject.Find("KonfiguratorZeileMaterial" + zeileNr + materialNr).GetComponent<CanvasGroup>().alpha = 0.2f;
            }
        }


        // ermittele für alle Zeilen (auch die nicht anzuzeigenden) die Parameter

        for (int zeileNr = 0; zeileNr <= (guiKonfigurator.ZeileContainerAnzahlZeilen - 1); zeileNr++)
        {

            //GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = tabelleObjektteil.Find(x => x.Id == "Band_001").Detail;
            GameObject.Find(guiKonfigurator.ZeileHeaderNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text = aktuelleAnzeigeKonfigurator.ZeileHeader[zeileNr];
            GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text = aktuelleAnzeigeKonfigurator.ZeileDetail[zeileNr];
            // zeige das aktuelle Material im Infofeld an
            GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text += "\n" + aktuelleAnzeigeKonfigurator.AnzahlMaterialien[zeileNr];
            GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text += " | " + aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[zeileNr].ToString();
            GameObject.Find(guiKonfigurator.ZeileDetailNameInCanvas + (zeileNr + 1).ToString()).GetComponent<Text>().text += " | " + aktuelleAnzeigeKonfigurator.AktuellesMaterialName[zeileNr];

            //Debug.Log("KonfiguratorZeileMaterial" + (zeileNr + 1).ToString() + (aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[zeileNr] + 1).ToString());
            GameObject.Find("KonfiguratorZeileMaterial" + (zeileNr + 1).ToString() + (aktuelleAnzeigeKonfigurator.AktuellesMaterialIndex[zeileNr] +1).ToString()).GetComponent<CanvasGroup>().alpha = 1.0f;


        }

        // blende alle relevanten zeilen wieder ein
        for (int zeileNr = 0; zeileNr <= (guiKonfigurator.AnzuzeigendeSpalteDerTabelleInnentuer.Count() - 1); zeileNr++)
        {

            GameObject.Find(guiKonfigurator.ZeileContainerNameInCanvas + (zeileNr + 1).ToString()).GetComponent<CanvasGroup>().alpha = 1.0f;
        }


        updateAktuellGetoggelteInnentuer();
        updateAktuellGetoggeltesMaterial();

        // Philipp
        updateSzene(aktuelleGetoggelteInnentuer, aktuelleGetoggelteMaterial);
        // 
        
    }

    // Philipp

    void updateAktuellGetoggelteInnentuer()
    {
        aktuelleGetoggelteInnentuer.Zarge = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Zarge;
        aktuelleGetoggelteInnentuer.Tuerblatt = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Tuerblatt;
        aktuelleGetoggelteInnentuer.DrueckerFalz = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerFalz;
        aktuelleGetoggelteInnentuer.DrueckerZier = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].DrueckerZier;
        aktuelleGetoggelteInnentuer.Band1 = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band1;
        aktuelleGetoggelteInnentuer.Band2 = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Band2;
        aktuelleGetoggelteInnentuer.Bandaufnahme1 = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme1;
        aktuelleGetoggelteInnentuer.Bandaufnahme2 = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Bandaufnahme2;
        aktuelleGetoggelteInnentuer.Schlosskasten = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schlosskasten;
        aktuelleGetoggelteInnentuer.Schliessblech = ergebnisInnentuer[aktuelleGetoggelteInnentuer.Index].Schliessblech;
        aktuelleGetoggelteInnentuer.Schwelle = "";
    }

    void updateAktuellGetoggeltesMaterial()
    {
        aktuelleGetoggelteMaterial.Zarge = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[0];
        aktuelleGetoggelteMaterial.Tuerblatt = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[1];
        aktuelleGetoggelteMaterial.DrueckerFalz = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[2];
        aktuelleGetoggelteMaterial.DrueckerZier = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[3];
        aktuelleGetoggelteMaterial.Band1 = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[4];
        aktuelleGetoggelteMaterial.Band2 = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[5];
        aktuelleGetoggelteMaterial.Bandaufnahme1 = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[6];
        aktuelleGetoggelteMaterial.Bandaufnahme2 = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[7];
        aktuelleGetoggelteMaterial.Schlosskasten = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[8];
        aktuelleGetoggelteMaterial.Schliessblech = aktuelleAnzeigeKonfigurator.AktuellesMaterialName[9];
        aktuelleGetoggelteMaterial.Schwelle = "";
    }

    void updateSzene(AktuelleGetoggelteInnentuer aktuelleGetoggelteInnentuer, AktuelleGetoggeltesMaterial aktuelleGetoggelteMaterial )
    {
        Debug.Log("----- Parameter: aktuelleGetoggelteInnentuer ------------------------------------------------");
        Debug.Log("aktuelleGetoggelteInnentuer.Zarge :" + aktuelleGetoggelteInnentuer.Zarge);
        Debug.Log("aktuelleGetoggelteInnentuer.Tuerblatt :" + aktuelleGetoggelteInnentuer.Tuerblatt);
        Debug.Log("aktuelleGetoggelteInnentuer.DrueckerFalz :" + aktuelleGetoggelteInnentuer.DrueckerFalz);
        Debug.Log("aktuelleGetoggelteInnentuer.DrueckerZier :" + aktuelleGetoggelteInnentuer.DrueckerZier);
        Debug.Log("aktuelleGetoggelteInnentuer.Band1 :" + aktuelleGetoggelteInnentuer.Band1);
        Debug.Log("aktuelleGetoggelteInnentuer.Band2 :" + aktuelleGetoggelteInnentuer.Band2);
        Debug.Log("aktuelleGetoggelteInnentuer.Bandaufnahme1 :" + aktuelleGetoggelteInnentuer.Bandaufnahme1);
        Debug.Log("aktuelleGetoggelteInnentuer.Bandaufnahme2 :" + aktuelleGetoggelteInnentuer.Bandaufnahme2);
        Debug.Log("aktuelleGetoggelteInnentuer.Schlosskasten :" + aktuelleGetoggelteInnentuer.Schlosskasten);
        Debug.Log("aktuelleGetoggelteInnentuer.Schliessblech :" + aktuelleGetoggelteInnentuer.Schliessblech);
        Debug.Log("aktuelleGetoggelteInnentuer.Schwelle :" + aktuelleGetoggelteInnentuer.Schwelle);
        Debug.Log("----- Parameter: aktuelleGetoggelteMaterial ------------------------------------------------");
        Debug.Log("aktuelleGetoggelteMaterial.Zarge :" + aktuelleGetoggelteMaterial.Zarge);
        Debug.Log("aktuelleGetoggelteMaterial.Tuerblatt :" + aktuelleGetoggelteMaterial.Tuerblatt);
        Debug.Log("aktuelleGetoggelteMaterial.DrueckerFalz :" + aktuelleGetoggelteMaterial.DrueckerFalz);
        Debug.Log("aktuelleGetoggelteMaterial.DrueckerZier :" + aktuelleGetoggelteMaterial.DrueckerZier);
        Debug.Log("aktuelleGetoggelteMaterial.Band1 :" + aktuelleGetoggelteMaterial.Band1);
        Debug.Log("aktuelleGetoggelteMaterial.Band2 :" + aktuelleGetoggelteMaterial.Band2);
        Debug.Log("aktuelleGetoggelteMaterial.Bandaufnahme1 :" + aktuelleGetoggelteMaterial.Bandaufnahme1);
        Debug.Log("aktuelleGetoggelteMaterial.Bandaufnahme2 :" + aktuelleGetoggelteMaterial.Bandaufnahme2);
        Debug.Log("aktuelleGetoggelteMaterial.Schlosskasten :" + aktuelleGetoggelteMaterial.Schlosskasten);
        Debug.Log("aktuelleGetoggelteMaterial.Schliessblech :" + aktuelleGetoggelteMaterial.Schliessblech);
        Debug.Log("aktuelleGetoggelteMaterial.Schwelle :" + aktuelleGetoggelteMaterial.Schwelle);
    }


    // ENDE Philipp

    void toggleMenuePunkt(int menue, int untermenue)
    {
        //Debug.Log("aktueller Index: " + innentuerParameter.TogglePunktIndex[untermenue - 1].ToString());
        //Debug.Log("Anzahl Toggles" + innentuerParameter.Toggle[untermenue - 1].Count());
        switch (menue)
        {
            case 1:
                {
                    if (hauptmenueParameter.TogglePunktIndex[untermenue - 1] < (hauptmenueParameter.Toggle[untermenue - 1].Count() - 1))
                    {
                        hauptmenueParameter.TogglePunktIndex[untermenue - 1] += 1;
                    }
                    else
                    {
                        hauptmenueParameter.TogglePunktIndex[untermenue - 1] = 0;
                    }
                    GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + untermenue.ToString()).GetComponent<Text>().text = hauptmenueParameter.Toggle[untermenue - 1][hauptmenueParameter.TogglePunktIndex[untermenue - 1]];
                    break;
                }
            case 2:
                {
                    if (innentuerParameter.TogglePunktIndex[untermenue - 1] < (innentuerParameter.Toggle[untermenue - 1].Count() - 1))
                    {
                        innentuerParameter.TogglePunktIndex[untermenue - 1] += 1;
                    }
                    else
                    {
                        innentuerParameter.TogglePunktIndex[untermenue - 1] = 0;
                    }
                    GameObject.Find(guiInnentuer.ZeileDetailNameInCanvas + untermenue.ToString()).GetComponent<Text>().text = innentuerParameter.Toggle[untermenue - 1][innentuerParameter.TogglePunktIndex[untermenue - 1]];
                    break;
                }
            case 3:
                {
                    break;
                }
            default:
                {
                    break;
                }

        }



    }



    MaterialList ermitteleMaterialien2(string matJsonString)
    {
        MaterialList materialListe = JsonConvert.DeserializeObject<MaterialList>(matJsonString);
        return materialListe;
    }


    MaterialList[] ermitteleMaterialien(string matJsonString)
    {
        MaterialList [] materialListe = JsonConvert.DeserializeObject<MaterialList [] >(matJsonString);
        return materialListe;
    }


    // ---------------------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------------



    void updateHauptmenue(HauptmenueParameter parameter)
    {

        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (int i = 1; i <= guiHauptmenue.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find(guiHauptmenue.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiHauptmenue.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;
            // hier nich alle Matrialien
        }


        for (int i = 1; i <= parameter.Menuepunkt.Count(); i++)
        {
            GameObject.Find(guiHauptmenue.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = parameter.Menuepunkt[i - 1];
            GameObject.Find(guiHauptmenue.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 1.0f;
            GameObject.Find(guiHauptmenue.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = hauptmenueParameter.Toggle[i - 1][hauptmenueParameter.TogglePunktIndex[i - 1]];
        }
    }

    void updateInnentuer(InnentuerParameter parameter)
    {

        //setze in den Zeilen alle Texte zurück und blende alle Zeilen aus
        for (int i = 1; i <= guiInnentuer.ZeileContainerAnzahlZeilen; i++)
        {
            GameObject.Find(guiInnentuer.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = "";
            GameObject.Find(guiInnentuer.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 0.0f;
            // hier nich alle Matrialien
        }

        for (int i = 1; i <= parameter.Menuepunkt.Count(); i++)
        {
            GameObject.Find(guiInnentuer.ZeileHeaderNameInCanvas + i.ToString()).GetComponent<Text>().text = parameter.Menuepunkt[i - 1];
            GameObject.Find(guiInnentuer.ZeileContainerNameInCanvas + i.ToString()).GetComponent<CanvasGroup>().alpha = 1.0f;
            GameObject.Find(guiInnentuer.ZeileDetailNameInCanvas + i.ToString()).GetComponent<Text>().text = innentuerParameter.Toggle[i - 1][innentuerParameter.TogglePunktIndex[i - 1]];
        }
    }





    // ---------------------------------------------------------------------------------------------------
    // CoRoutine
    // ---------------------------------------------------------------------------------------------------


    IEnumerator LerpColor(string gameObjectName)
    {

        // wenn diese drei Zeilen ausserhalb des IEnumarators als globale variablen definiere, wären Änderungen erst
        // nach Neustart von Unity übernommen (?)
        float duration = 0.2f; // Zeit in Sekunden.
        float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
        Color32 floatColor = new Color32(0, 0, 0, 255);

        colorLerpAktive = true;
        myImage = GameObject.Find(gameObjectName).GetComponent<Image>();
        Color32 colorOld = myImage.color;

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
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



    // ---------------------------------------------------------------------------------------------------
    // generiere GUI
    // ---------------------------------------------------------------------------------------------------


    // generiere GameObject Canvas
    void generiereGuiCanvas()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiCanvas.Name;
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
        myCanvasScaler.referenceResolution = new Vector2(guiCanvas.Width, guiCanvas.Height) ;

        // Canvas GraphicRaycaster
        myGO.AddComponent<GraphicRaycaster>();
    }



    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Hauptmenue
    // ---------------------------------------------------------------------------------------------------


    void generiereHauptmenueContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.Width, guiHauptmenue.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.Parent).transform);

        // Image color
        myImage.color = guiHauptmenue.Color;
    }

    void generiereHauptmenueHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HeaderContainerPosX, guiHauptmenue.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HeaderContainerWidth, guiHauptmenue.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HeaderContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.HeaderContainerColor;
    }

    void generiereHauptmenueHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HeaderLogoPosX, guiHauptmenue.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HeaderLogoWidth, guiHauptmenue.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiHauptmenue.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HeaderLogoParent).transform);

        // Image color
        myImage.color = guiHauptmenue.HeaderLogoColor;
    }

    void generiereHauptmenueHeaderText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HeaderTextPosX, guiHauptmenue.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HeaderTextWidth, guiHauptmenue.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.HeaderTextFontType) as Font;
        myText.fontSize = guiHauptmenue.HeaderTextFontSize;
        myText.fontStyle = guiHauptmenue.HeaderTextFontStyle;
        myText.color = guiHauptmenue.HeaderTextColor;

        // Text
        myText.text = guiHauptmenue.HeaderTextText;
    }

    void generiereHauptmenueInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.InfoContainerPosX, guiHauptmenue.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.InfoContainerWidth, guiHauptmenue.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.InfoContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.InfoContainerColor;
    }

    void generiereHauptmenueInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.InfoTextPosX, guiHauptmenue.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.InfoTextWidth, guiHauptmenue.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.InfoTextFontType) as Font;
        myText.fontSize = guiHauptmenue.InfoTextFontSize;
        myText.fontStyle = guiHauptmenue.InfoTextFontStyle;
        myText.color = guiHauptmenue.InfoTextColor;

        // Text
        myText.text = guiHauptmenue.InfoTextText;
    }

    void generiereHauptmenueInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.InfoLogoPosX, guiHauptmenue.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.InfoLogoWidth, guiHauptmenue.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiHauptmenue.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.InfoLogoParent).transform);

        // Image color
        myImage.color = guiHauptmenue.InfoLogoColor;
    }

    void generiereHauptmenueHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.HauptContainerPosX, guiHauptmenue.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.HauptContainerWidth, guiHauptmenue.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.HauptContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.HauptContainerColor;
    }

    void generiereHauptmenueHauptContainerZeilen(int anzahlZeilen)
    {
        for (int i = 1; i <= anzahlZeilen; i++)
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

    void generiereHauptmenueZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileContainerPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileContainerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileContainerWidth, guiHauptmenue.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileContainerParent).transform);

        // Image color
        myImage.color = guiHauptmenue.ZeileContainerColor;
    }

    void generiereHauptmenueZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileHeaderNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileHeaderPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileHeaderWidth, guiHauptmenue.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileHeaderParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.ZeileHeaderFontType) as Font;
        myText.fontSize = guiHauptmenue.ZeileHeaderFontSize;
        myText.fontStyle = guiHauptmenue.ZeileHeaderFontStyle;
        myText.color = guiHauptmenue.ZeileHeaderColor;

        myText.text = "Header";
    }

    void generiereHauptmenueZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileDetailPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileDetailNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileDetailWidth, guiHauptmenue.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileDetailParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.ZeileDetailFontType) as Font;
        myText.fontSize = guiHauptmenue.ZeileDetailFontSize;
        myText.fontStyle = guiHauptmenue.ZeileDetailFontStyle;
        myText.color = guiHauptmenue.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    void generiereHauptmenueZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileLogoPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileLogoNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileLogoWidth, guiHauptmenue.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiHauptmenue.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileLogoParent + nummer.ToString()).transform);

        // Image color
        myImage.color = guiHauptmenue.ZeileLogoColor;
    }

    void generiereHauptmenueZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiHauptmenue.ZeileNummerPosY + (guiHauptmenue.ZeileContainerAbstandY * nummer) + (-guiHauptmenue.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiHauptmenue.ZeileNummerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiHauptmenue.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiHauptmenue.ZeileNummerWidth, guiHauptmenue.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiHauptmenue.ZeileNummerParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiHauptmenue.ZeileNummerFontType) as Font;
        myText.fontSize = guiHauptmenue.ZeileNummerFontSize;
        myText.fontStyle = guiHauptmenue.ZeileNummerFontStyle;
        myText.color = guiHauptmenue.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }



    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Innentuer
    // ---------------------------------------------------------------------------------------------------

    void generiereInnentuerContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.Width, guiInnentuer.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.Parent).transform);

        // Image color
        myImage.color = guiInnentuer.Color;
    }

    void generiereInnentuerHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HeaderContainerPosX, guiInnentuer.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HeaderContainerWidth, guiInnentuer.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HeaderContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.HeaderContainerColor;
    }

    void generiereInnentuerHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HeaderLogoPosX, guiInnentuer.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HeaderLogoWidth, guiInnentuer.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiInnentuer.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HeaderLogoParent).transform);

        // Image color
        myImage.color = guiInnentuer.HeaderLogoColor;
    }

    void generiereInnentuerHeaderText()
    {   // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HeaderTextPosX, guiInnentuer.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HeaderTextWidth, guiInnentuer.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.HeaderTextFontType) as Font;
        myText.fontSize = guiInnentuer.HeaderTextFontSize;
        myText.fontStyle = guiInnentuer.HeaderTextFontStyle;
        myText.color = guiInnentuer.HeaderTextColor;

        // Text
        myText.text = guiInnentuer.HeaderTextText;
    }

    void generiereInnentuerInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.InfoContainerPosX, guiInnentuer.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.InfoContainerWidth, guiInnentuer.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.InfoContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.InfoContainerColor;
    }

    void generiereInnentuerInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.InfoTextPosX, guiInnentuer.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.InfoTextWidth, guiInnentuer.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.InfoTextFontType) as Font;
        myText.fontSize = guiInnentuer.InfoTextFontSize;
        myText.fontStyle = guiInnentuer.InfoTextFontStyle;
        myText.color = guiInnentuer.InfoTextColor;

        // Text
        myText.text = guiInnentuer.InfoTextText;
    }

    void generiereInnentuerInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.InfoLogoPosX, guiInnentuer.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.InfoLogoWidth, guiInnentuer.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiInnentuer.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.InfoLogoParent).transform);

        // Image color
        myImage.color = guiInnentuer.InfoLogoColor;
    }

    void generiereInnentuerHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.HauptContainerPosX, guiInnentuer.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.HauptContainerWidth, guiInnentuer.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.HauptContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.HauptContainerColor;
    }

    void generiereInnentuerHauptContainerZeilen(int anzahlZeilen)
    {
        for (int i = 1; i <= anzahlZeilen; i++)
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

    void generiereInnentuerZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileContainerPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileContainerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileContainerWidth, guiInnentuer.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileContainerParent).transform);

        // Image color
        myImage.color = guiInnentuer.ZeileContainerColor;
    }

    void generiereInnentuerZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileHeaderNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileHeaderPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileHeaderWidth, guiInnentuer.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileHeaderParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.ZeileHeaderFontType) as Font;
        myText.fontSize = guiInnentuer.ZeileHeaderFontSize;
        myText.fontStyle = guiInnentuer.ZeileHeaderFontStyle;
        myText.color = guiInnentuer.ZeileHeaderColor;

        myText.text = "Header";
    }

    void generiereInnentuerZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileDetailPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileDetailNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileDetailWidth, guiInnentuer.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileDetailParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.ZeileDetailFontType) as Font;
        myText.fontSize = guiInnentuer.ZeileDetailFontSize;
        myText.fontStyle = guiInnentuer.ZeileDetailFontStyle;
        myText.color = guiInnentuer.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    void generiereInnentuerZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileLogoPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileLogoNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileLogoWidth, guiInnentuer.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiInnentuer.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileLogoParent + nummer.ToString()).transform);

        // Image color
        myImage.color = guiInnentuer.ZeileLogoColor;


    }

    void generiereInnentuerZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiInnentuer.ZeileNummerPosY + (guiInnentuer.ZeileContainerAbstandY * nummer) + (-guiInnentuer.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiInnentuer.ZeileNummerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiInnentuer.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiInnentuer.ZeileNummerWidth, guiInnentuer.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiInnentuer.ZeileNummerParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiInnentuer.ZeileNummerFontType) as Font;
        myText.fontSize = guiInnentuer.ZeileNummerFontSize;
        myText.fontStyle = guiInnentuer.ZeileNummerFontStyle;
        myText.color = guiInnentuer.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }




    // ---------------------------------------------------------------------------------------------------
    // generiere GUI: Konfigurator
    // ---------------------------------------------------------------------------------------------------


    void generiereKonfiguratorContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.NameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform

        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.PosX, 0, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.Width, guiKonfigurator.Height);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.Parent).transform);

        // Image color
        myImage.color = guiKonfigurator.Color;
    }

    void generiereKonfiguratorHeaderContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HeaderContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HeaderContainerPosX, guiKonfigurator.HeaderContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HeaderContainerWidth, guiKonfigurator.HeaderContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HeaderContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.HeaderContainerColor;
    }

    void generiereKonfiguratorHeaderLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HeaderLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HeaderLogoPosX, guiKonfigurator.HeaderLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HeaderLogoWidth, guiKonfigurator.HeaderLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiKonfigurator.HeaderLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HeaderLogoParent).transform);

        // Image color
        myImage.color = guiKonfigurator.HeaderLogoColor;
    }

    void generiereKonfiguratorHeaderText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HeaderTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HeaderTextPosX, guiKonfigurator.HeaderTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HeaderTextWidth, guiKonfigurator.HeaderTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HeaderTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.HeaderTextFontType) as Font;
        myText.fontSize = guiKonfigurator.HeaderTextFontSize;
        myText.fontStyle = guiKonfigurator.HeaderTextFontStyle;
        myText.color = guiKonfigurator.HeaderTextColor;

        // Text
        myText.text = guiKonfigurator.HeaderTextText;
    }

    void generiereKonfiguratorInfoContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoContainerPosX, guiKonfigurator.InfoContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoContainerWidth, guiKonfigurator.InfoContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.InfoContainerColor;
    }

    void generiereKonfiguratorInfoText()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoTextNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoTextPosX, guiKonfigurator.InfoTextPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoTextWidth, guiKonfigurator.InfoTextHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoTextParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.InfoTextFontType) as Font;
        myText.fontSize = guiKonfigurator.InfoTextFontSize;
        myText.fontStyle = guiKonfigurator.InfoTextFontStyle;
        myText.color = guiKonfigurator.InfoTextColor;

        // Text
        myText.text = guiKonfigurator.InfoTextText;
    }

    void generiereKonfiguratorInfoLogo()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoLogoNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoLogoPosX, guiKonfigurator.InfoLogoPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoLogoWidth, guiKonfigurator.InfoLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
        // Logo
        Sprite FULLHP = Resources.Load<Sprite>(guiKonfigurator.InfoLogoSourceImage);
        myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoLogoParent).transform);

        // Image color
        myImage.color = guiKonfigurator.InfoLogoColor;
    }

    void generiereKonfiguratorInfoNummer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.InfoNummerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.InfoNummerPosX, guiKonfigurator.InfoNummerPosY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.InfoNummerWidth, guiKonfigurator.InfoNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.InfoNummerParent).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.InfoNummerFontType) as Font;
        myText.fontSize = guiKonfigurator.InfoNummerFontSize;
        myText.fontStyle = guiKonfigurator.InfoNummerFontStyle;
        myText.color = guiKonfigurator.InfoNummerColor;

        // Text
        myText.text = guiKonfigurator.InfoNummerText;
    }

    void generiereKonfiguratorHauptContainer()
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.HauptContainerNameInCanvas;
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.HauptContainerPosX, guiKonfigurator.HauptContainerPosY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.HauptContainerWidth, guiKonfigurator.HauptContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.HauptContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.HauptContainerColor;
    }

    void generiereKonfiguratorHauptContainerZeilen(int anzahlZeilen)
    {
        for (int i = 1; i <= anzahlZeilen; i++)
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

    void generiereKonfiguratorZeileContainer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileContainerPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileContainerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();
        myImage = myGO.GetComponent<Image>();

        // Image RectTransform
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileContainerPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileContainerWidth, guiKonfigurator.ZeileContainerHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Image zum child des Menu
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileContainerParent).transform);

        // Image color
        myImage.color = guiKonfigurator.ZeileContainerColor;
    }

    void generiereKonfiguratorZeileHeader(int nummer)
    {
        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileHeaderNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileHeaderPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileHeaderPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileHeaderWidth, guiKonfigurator.ZeileHeaderHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileHeaderParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.ZeileHeaderFontType) as Font;
        myText.fontSize = guiKonfigurator.ZeileHeaderFontSize;
        myText.fontStyle = guiKonfigurator.ZeileHeaderFontStyle;
        myText.color = guiKonfigurator.ZeileHeaderColor;

        myText.text = "Header";
    }

    void generiereKonfiguratorZeileMaterial(int nummer)
    {
        int neuePositionY;
        int neuePositionX;

        int i = 0;
        for (int y = 1; y <= guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenY; y++)

        {
            for (int x = 1; x <= guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenX; x++)
            {
                i++;
                // Berechnung Position des aktuellen Containers
                neuePositionY = -((y - 1) * (guiKonfigurator.ZeileMaterialAnzahlMaterialAbstandY + guiKonfigurator.ZeileMaterialHeight)) + guiKonfigurator.ZeileMaterialPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));
                neuePositionX = -((guiKonfigurator.ZeileMaterialAnzahlMaterialOptionenX - x) * (guiKonfigurator.ZeileMaterialAnzahlMaterialAbstandX + guiKonfigurator.ZeileMaterialWidth)) + guiKonfigurator.ZeileMaterialPosX;

                // Game Object
                myGO = new GameObject();
                myGO.name = guiKonfigurator.ZeileMaterialNameInCanvas + nummer.ToString() + i.ToString();
                myGO.layer = 5; // 5:UI

                // Image
                myGO.AddComponent<Image>();

                //Image myImage;
                myImage = myGO.GetComponent<Image>();
                myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(neuePositionX, neuePositionY, 0);
                myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileMaterialWidth, guiKonfigurator.ZeileMaterialHeight);
                myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
                myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
                myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

                // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
                myGO.AddComponent<CanvasGroup>();

                // mache das GameObject Info-Container Image zum child des Menu
                myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileMaterialParent + nummer.ToString()).transform);

                // Image color
                myImage.color = guiKonfigurator.ZeileMaterialColor;
            }
        }
    }

    void generiereKonfiguratorZeileDetail(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileDetailPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileDetailNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileDetailPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileDetailWidth, guiKonfigurator.ZeileDetailHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileDetailParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.ZeileDetailFontType) as Font;
        myText.fontSize = guiKonfigurator.ZeileDetailFontSize;
        myText.fontStyle = guiKonfigurator.ZeileDetailFontStyle;
        myText.color = guiKonfigurator.ZeileDetailColor;

        // Text
        myText.text = "Hier kommen ganz viele Details rein";
    }

    void generiereKonfiguratorZeileLogo(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileLogoPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileLogoNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Image
        myGO.AddComponent<Image>();

        //Image myImage;
        myImage = myGO.GetComponent<Image>();
        myImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileLogoPosX, neuePositionY, 0);
        myImage.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileLogoWidth, guiKonfigurator.ZeileLogoHeight);
        myImage.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myImage.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // Logo
        //Sprite FULLHP = Resources.Load<Sprite>(guiKonfigurator.ZeileLogoSourceImage);
        //myImage.GetComponent<Image>().sprite = FULLHP;

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileLogoParent + nummer.ToString()).transform);

        // Image color
        myImage.color = guiKonfigurator.ZeileLogoColor;


    }

    void generiereKonfiguratorZeileNummer(int nummer)
    {
        // Berechnung Position des aktuellen Containers
        int neuePositionY = guiKonfigurator.ZeileNummerPosY + (guiKonfigurator.ZeileContainerAbstandY * nummer) + (-guiKonfigurator.ZeileContainerHeight * (nummer - 1));

        // Game Object
        myGO = new GameObject();
        myGO.name = guiKonfigurator.ZeileNummerNameInCanvas + nummer.ToString();
        myGO.layer = 5; // 5:UI

        // Text
        myGO.AddComponent<Text>();
        myText = myGO.GetComponent<Text>();

        // Image RectTransform
        myText.GetComponent<RectTransform>().anchoredPosition = new Vector3(guiKonfigurator.ZeileNummerPosX, neuePositionY, 0);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2(guiKonfigurator.ZeileNummerWidth, guiKonfigurator.ZeileNummerHeight);
        myText.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
        myText.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);

        // CanvasGroup (wird benötigt, um via alpha ein- und auszublenden)
        myGO.AddComponent<CanvasGroup>();

        // mache das GameObject Info-Container Text zum child des MenueInfoContainer
        myGO.transform.SetParent(GameObject.Find(guiKonfigurator.ZeileNummerParent + nummer.ToString()).transform);

        // Schrift
        myText.font = Resources.GetBuiltinResource(typeof(Font), guiKonfigurator.ZeileNummerFontType) as Font;
        myText.fontSize = guiKonfigurator.ZeileNummerFontSize;
        myText.fontStyle = guiKonfigurator.ZeileNummerFontStyle;
        myText.color = guiKonfigurator.ZeileNummerColor;

        // Text
        myText.text = nummer.ToString();
    }



    // ---------------------------------------------------------------------------------------------------
    // Datenbank-Abfragen
    // ---------------------------------------------------------------------------------------------------

    List<T_Band> dBAbfrageBand()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Band";
        //Debug.Log(query);
        List<T_Band> queryResult = restClient.GetExecuteResultsBand(query);
        return queryResult;
    }

    List<T_Bandaufnahme> dBAbfrageBandaufnahme()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Bandaufnahme";
        //Debug.Log(query);
        List<T_Bandaufnahme> queryResult = restClient.GetExecuteResultsBandaufnahme(query);
        return queryResult;
    }

    List<T_Druecker> dBAbfrageDruecker()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Druecker";
        //Debug.Log(query);
        List<T_Druecker> queryResult = restClient.GetExecuteResultsDruecker(query);
        return queryResult;
    }

    List<T_Schliessblech> dBAbfrageSchliessblech()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Schliessblech";
        //Debug.Log(query);
        List<T_Schliessblech> queryResult = restClient.GetExecuteResultsSchliessblech(query);
        return queryResult;
    }

    List<T_Schlosskasten> dBAbfrageSchlosskasten()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Schlosskasten";
        //Debug.Log(query);
        List<T_Schlosskasten> queryResult = restClient.GetExecuteResultsSchlosskasten(query);
        return queryResult;
    }

    List<T_Schwelle> dBAbfrageSchwelle()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Schwelle";
        //Debug.Log(query);
        List<T_Schwelle> queryResult = restClient.GetExecuteResultsSchwelle(query);
        return queryResult;
    }

    List<T_Tuerblatt> dBAbfrageTuerblatt()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Tuerblatt";
        //Debug.Log(query);
        List<T_Tuerblatt> queryResult = restClient.GetExecuteResultsTuerblatt(query);
        return queryResult;
    }

    List<T_Zarge> dBAbfrageZarge()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Zarge";
        //Debug.Log(query);
        List<T_Zarge> queryResult = restClient.GetExecuteResultsZarge(query);
        return queryResult;
    }

    List<T_Innentuer> dBAbfrageInnentuer()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Innentuer";
        //Debug.Log(query);
        List<T_Innentuer> queryResult = restClient.GetExecuteResultsInnentuer(query);
        return queryResult;
    }

    List<T_Objektteil> dBAbfrageObjektteil()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Objektteil";
        //Debug.Log(query);
        List<T_Objektteil> queryResult = restClient.GetExecuteResultsObjektteil(query);
        return queryResult;
    }

    List<T_Hersteller> dBAbfrageHersteller()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.Hersteller";
        //Debug.Log(query);
        List<T_Hersteller> queryResult = restClient.GetExecuteResultsHersteller(query);
        return queryResult;
    }

    List<T_MAT> dBAbfrageMAT()
    {
        RestClientManager restClient = new RestClientManager();
        string query = "SELECT * FROM Thomas.MAT";
        //Debug.Log(query);
        List<T_MAT> queryResult = restClient.GetExecuteResultsMAT(query);
        return queryResult;
    }

    List<T_Innentuer> ermitteleAlleTrefferAusTabelleInnentuerMitAuswahlkriterien()
    {
        //RestClientManager restClient = new RestClientManager();
        //string query = "SELECT Thomas.Innentuer.* FROM Thomas.Innentuer INNER JOIN Thomas.Zarge ON Thomas.Innentuer.Zarge = Thomas.Zarge.Id INNER JOIN Thomas.Tuerblatt ON Thomas.Innentuer.Tuerblatt = Thomas.Tuerblatt.Id WHERE (Thomas.Tuerblatt.HoeheDIN LIKE '" + innentuerOutputParameter.HoeheDIN + "' AND Thomas.Tuerblatt.BreiteDIN LIKE '" + innentuerOutputParameter.BreiteDIN + "' AND Thomas.Zarge.HoeheDIN LIKE '" + innentuerOutputParameter.HoeheDIN + "' AND Thomas.Zarge.BreiteDIN LIKE '" + innentuerOutputParameter.BreiteDIN + "' AND Thomas.Zarge.Wandstaerke LIKE '" + innentuerOutputParameter.Wandstaerke + "' AND Thomas.Zarge.Bekleidungsbreite LIKE '" + innentuerOutputParameter.Bekleidungsbreite + "' )";
        //Debug.Log(query);

        //List<T_Innentuer> queryResult = restClient.GetExecuteResultsInnentuer(query);

        //foreach (T_Innentuer zeile in queryResult)
        //{
        //    Debug.Log(zeile.Id + " - " + zeile.Detail + " - " + zeile.Zarge + " - " + zeile.Tuerblatt + " - " + zeile.Band1 + " - " + zeile.Band2 + " - " + zeile.Bandaufnahme1 + " - " + zeile.Bandaufnahme2 + " - " + zeile.DrueckerFalz + " - " + zeile.DrueckerZier + " - " + zeile.Schlosskasten + " - " + zeile.Schliessblech + " - " + zeile.Schwelle);
        //    AktuelleGetoggelteInnentuer.Zarge = zeile.Zarge;
        //    AktuelleGetoggelteInnentuer.Tuerblatt = zeile.Tuerblatt;
        //    AktuelleGetoggelteInnentuer.DrueckerFalz = zeile.DrueckerFalz;
        //    AktuelleGetoggelteInnentuer.DrueckerZier = zeile.DrueckerZier;
        //}
        //return queryResult;

        // gehe alle Zeilen der Tabelle Innentuer durch ..
        ergebnisInnentuer.Clear();
        ergebnisInnentuerNachCheckHoehe.Clear();
        ergebnisInnentuerNachCheckBreite.Clear();
        ergebnisInnentuerNachCheckWandstaerke.Clear();
        ergebnisInnentuerNachCheckBekleidungsbreite.Clear();

        //Debug.Log("-----------ergebnisInnentuer.Count() START : " + ergebnisInnentuer.Count().ToString());


        foreach (T_Innentuer zeileInnentuer in tabelleInnentuer)
        {
            // .. und ermittele aus der entsprechenden Spalte die Zargen-ID
            // gehe alle Zeilen der Tabelle Zargen durch ..

            foreach (T_Zarge zeileZarge in tabelleZarge)
            {
                // .. und prüfe nur in den Zeilen mit der relevanten Zargen-ID, ..
                if (zeileZarge.Id == zeileInnentuer.Zarge)
                {
                    //Debug.Log("-----------innentuerOutputParameter.HoeheDIN) : " + innentuerOutputParameter.HoeheDIN);
                    // ob die Höhe der Zargen-Id der momentan eingestelletn Höhe ist
                    if ((zeileZarge.HoeheDIN.ToString() == innentuerOutputParameter.HoeheDIN) || (innentuerOutputParameter.HoeheDIN == "%"))
                    {
                        ergebnisInnentuerNachCheckHoehe.Add(zeileInnentuer);
                    }
                }
            }
        }
        //Debug.Log("-----------ergebnisInnentuerNachCheckHoehe.Count() nach Hoehe : " + ergebnisInnentuerNachCheckHoehe.Count().ToString());
        //Debug.Log("-----------ergebnisInnentuer.Count() nach Hoehe : " + ergebnisInnentuer.Count().ToString());
        ergebnisInnentuer = ergebnisInnentuerNachCheckHoehe;

        // wenn es mindesten einen Treffer nach Check Hoehe gab, mache weiter
        if (ergebnisInnentuerNachCheckHoehe.Count() > 0)
        {
            foreach (T_Innentuer zeileInnentuerNachCheckHoehe in ergebnisInnentuerNachCheckHoehe)
            {
                foreach (T_Zarge zeileZarge in tabelleZarge)
                {
                    if (zeileZarge.Id == zeileInnentuerNachCheckHoehe.Zarge)
                    {
                        //Debug.Log("-----------innentuerOutputParameter.BreiteDIN) : " + innentuerOutputParameter.BreiteDIN);
                        if ((zeileZarge.BreiteDIN.ToString() == innentuerOutputParameter.BreiteDIN) || (innentuerOutputParameter.BreiteDIN == "%"))
                        {
                            ergebnisInnentuerNachCheckBreite.Add(zeileInnentuerNachCheckHoehe);
                        }
                    }
                }
            }
            ergebnisInnentuer = ergebnisInnentuerNachCheckBreite;
            //Debug.Log("-----------ergebnisInnentuerNachCheckBreite.Count() nach Breite : " + ergebnisInnentuerNachCheckBreite.Count().ToString());
            //Debug.Log("-----------ergebnisInnentuer.Count() nach Breite : " + ergebnisInnentuer.Count().ToString());

            // wenn es mindesten einen Treffer nach Check Breite gab, mache weiter
            if (ergebnisInnentuerNachCheckBreite.Count() > 0)
            {
                foreach (T_Innentuer zeileInnentuerNachCheckBreite in ergebnisInnentuerNachCheckBreite)
                {
                    foreach (T_Zarge zeileZarge in tabelleZarge)
                    {
                        if (zeileZarge.Id == zeileInnentuerNachCheckBreite.Zarge)
                        {
                            //Debug.Log("-----------innentuerOutputParameter.Wandstärke) : " + innentuerOutputParameter.Wandstaerke);
                            if ((zeileZarge.Wandstaerke.ToString() == innentuerOutputParameter.Wandstaerke) || (innentuerOutputParameter.Wandstaerke == "%"))
                            {
                                ergebnisInnentuerNachCheckWandstaerke.Add(zeileInnentuerNachCheckBreite);
                            }
                        }
                    }
                }
                ergebnisInnentuer = ergebnisInnentuerNachCheckWandstaerke;
                //Debug.Log("-----------ergebnisInnentuerNachCheckWandstaerke.Count() nachWandstaerke : " + ergebnisInnentuerNachCheckWandstaerke.Count().ToString());
                //Debug.Log("-----------ergebnisInnentuer.Count() nach Wandstaerke : " + ergebnisInnentuer.Count().ToString());

                // wenn es mindesten einen Treffer nach Check Wandstaerke gab, mache weiter
                if (ergebnisInnentuerNachCheckWandstaerke.Count() > 0)
                {
                    foreach (T_Innentuer zeileInnentuerNachCheckWandstaerke in ergebnisInnentuerNachCheckWandstaerke)
                    {
                        foreach (T_Zarge zeileZarge in tabelleZarge)
                        {
                            if (zeileZarge.Id == zeileInnentuerNachCheckWandstaerke.Zarge)
                            {
                                //Debug.Log("-----------innentuerOutputParameter.Bekleidungsbreite) : " + innentuerOutputParameter.Bekleidungsbreite);
                                if ((zeileZarge.Bekleidungsbreite.ToString() == innentuerOutputParameter.Bekleidungsbreite) || (innentuerOutputParameter.Bekleidungsbreite == "%"))
                                {
                                    ergebnisInnentuerNachCheckBekleidungsbreite.Add(zeileInnentuerNachCheckWandstaerke);
                                }
                            }
                        }
                    }
                    ergebnisInnentuer = ergebnisInnentuerNachCheckBekleidungsbreite;
                    //Debug.Log("-----------ergebnisInnentuerNachCheckBekleidungsbreite.Count() nach Bekleidungsbreite : " + ergebnisInnentuerNachCheckBekleidungsbreite.Count().ToString());
                    //Debug.Log("-----------ergebnisInnentuer.Count() nach Bekleidungsbreite : " + ergebnisInnentuer.Count().ToString());

                }
                else
                {
                    ergebnisInnentuer.Clear();
                }
            }
            else
            {
                ergebnisInnentuer.Clear();
            }
        }
        else
        {
            ergebnisInnentuer.Clear();
        }               

        return ergebnisInnentuer;
    }

}
