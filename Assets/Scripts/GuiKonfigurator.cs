using UnityEngine;

public partial class GUI_Innentuer
{
    public class GuiKonfigurator
    {
        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static readonly int VerschiebungX = 960;
        private static readonly int VerschiebungY = 0;
        public string[] AnzuzeigendeSpalteDerTabelleInnentuer = {"Zarge", "Tuerblatt", "Band1", "DrueckerFalz"};
        public Color32 Color = new Color32();
        public Color32 HauptContainerColor = new Color32();
        public int HauptContainerHeight = 920;

        //KonfiguratorHauptContainer
        public string HauptContainerNameInCanvas = "KonfiguratorHauptContainer";
        public string HauptContainerParent = "Konfigurator";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public Color32 HeaderContainerColor = new Color32();
        public int HeaderContainerHeight = 120;

        // KonfiguratorHeaderContainer
        public string HeaderContainerNameInCanvas = "KonfiguratorHeaderContainer";
        public string HeaderContainerParent = "Konfigurator";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public Color32 HeaderLogoColor = new Color32();
        public int HeaderLogoHeight = 80;

        // KonfiguratorHeaderLogo
        public string HeaderLogoNameInCanvas = "KonfiguratorHeaderLogo";
        public string HeaderLogoParent = "KonfiguratorHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public string HeaderLogoSourceImage = "Logos/list-with-dots_white";
        public int HeaderLogoWidth = 80;
        public Color32 HeaderTextColor = new Color32();
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextHeight = 120;

        // KonfiguratorHeaderText
        public string HeaderTextNameInCanvas = "KonfiguratorHeaderText";
        public string HeaderTextParent = "KonfiguratorHeaderContainer";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public string HeaderTextText = "Konfigurator";
        public int HeaderTextWidth = 840;
        public int Height = 1080;
        public Color32 InfoContainerColor = new Color32();
        public int InfoContainerHeight = 100;

        //KonfiguratorInfoContainer
        public string InfoContainerNameInCanvas = "KonfiguratorInfoContainer";
        public string InfoContainerParent = "Konfigurator";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public Color32 InfoLogoColor = new Color32();
        public int InfoLogoHeight = 100;

        //KonfiguratorInfoLogo
        public string InfoLogoNameInCanvas = "KonfiguratorInfoLogo";
        public string InfoLogoParent = "KonfiguratorInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public string InfoLogoSourceImage = ""; //"Logos/list-with-dots_white";
        public int InfoLogoWidth = 60;
        public Color32 InfoNummerColor = new Color32();
        public int InfoNummerFontSize = 70;
        public FontStyle InfoNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string InfoNummerFontType = "Arial.ttf";
        public int InfoNummerHeight = 100;

        // KonfiguratorInfoNummer
        public string InfoNummerNameInCanvas = "KonfiguratorInfoNummer";
        public string InfoNummerParent = "KonfiguratorInfoContainer";
        public int InfoNummerPosX = VerschiebungX + 912;
        public int InfoNummerPosY = VerschiebungY - 132;
        public string InfoNummerText = "0";
        public int InfoNummerWidth = 60;
        public Color32 InfoTextColor = new Color32();
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextHeight = 120;

        // KonfiguratorInfoText
        public string InfoTextNameInCanvas = "KonfiguratorInfoText";
        public string InfoTextParent = "KonfiguratorInfoContainer";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 131;
        public string InfoTextText = "Innentür";
        public int InfoTextWidth = 840;
        public string Name = "Konfigurator";

        // Konfigurator
        public string NameInCanvas = "Konfigurator";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;

        public int Width = 960;

        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        // ab hier die einzelnen Zeilen

        //KonfiguratorZeileContainer
        public int ZeileContainerAnzahlZeilen = 10; // momentan max. 8 wegen Höehe 1080 px
        public Color32 ZeileContainerColor = new Color32();
        public int ZeileContainerHeight = 100;
        public string ZeileContainerNameInCanvas = "KonfiguratorZeileContainer";
        public string ZeileContainerParent = "KonfiguratorHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public Color32 ZeileDetailColor = new Color32();
        public int ZeileDetailFontSize = 20;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailHeight = 50;

        //KonfiguratorZeileDetail
        public string ZeileDetailNameInCanvas = "KonfiguratorZeileDetail";
        public string ZeileDetailParent = "KonfiguratorZeileContainer";
        public int ZeileDetailPosX = VerschiebungX + 20;
        public int ZeileDetailPosY = VerschiebungY - 274;
        public string ZeileDetailText = "";
        public int ZeileDetailWidth = 920;
        public Color32 ZeileHeaderColor = new Color32();
        public int ZeileHeaderFontSize = 30;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderHeight = 40;

        //KonfiguratorZeileHeader
        public string ZeileHeaderNameInCanvas = "KonfiguratorZeileHeader";
        public string ZeileHeaderParent = "KonfiguratorZeileContainer";
        public int ZeileHeaderPosX = VerschiebungX + 20;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public string ZeileHeaderText = "";
        public int ZeileHeaderWidth = 600;
        public Color32 ZeileLogoColor = new Color32();
        public int ZeileLogoHeight = 100;

        //KonfiguratorZeileLogo
        public string ZeileLogoNameInCanvas = "KonfiguratorZeileDetailLogo";
        public string ZeileLogoParent = "KonfiguratorZeileContainer";
        public int ZeileLogoPosX = VerschiebungX + 900;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public string ZeileLogoSourceImage = ""; //"Logos/list-with-dots_white";
        public int ZeileLogoWidth = 60;
        public int ZeileMaterialAnzahlMaterialAbstandX = 10;
        public int ZeileMaterialAnzahlMaterialAbstandY = 10;

        //KonfiguratorZeileMaterial
        public int ZeileMaterialAnzahlMaterialOptionenX = 4;
        public int ZeileMaterialAnzahlMaterialOptionenY = 3;
        public Color32 ZeileMaterialColor = new Color32();
        public int ZeileMaterialHeight = 20;
        public string ZeileMaterialNameInCanvas = "KonfiguratorZeileMaterial";
        public string ZeileMaterialParent = "KonfiguratorZeileContainer";
        public int ZeileMaterialPosX = VerschiebungX + 860;
        public int ZeileMaterialPosY = VerschiebungY - 241;
        public int ZeileMaterialWidth = 20;
        public Color32 ZeileNummerColor = new Color32();
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerHeight = 100;

        // KonfiguratorZeilenNummer
        public string ZeileNummerNameInCanvas = "KonfiguratorZeileNummer";
        public string ZeileNummerParent = "KonfiguratorZeileContainer";
        public int ZeileNummerPosX = VerschiebungX + 912;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public string ZeileNummerText = "";
        public int ZeileNummerWidth = 60;
    }
}