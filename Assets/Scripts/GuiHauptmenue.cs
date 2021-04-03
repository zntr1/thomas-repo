using UnityEngine;

public partial class GUI_Innentuer
{
    public class GuiHauptmenue
    {
        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static readonly int VerschiebungX = 480;
        private static readonly int VerschiebungY = 0;
        public Color32 Color = new Color32();
        public Color32 HauptContainerColor = new Color32();
        public int HauptContainerHeight = 920;

        //HauptmenueHauptContainer
        public string HauptContainerNameInCanvas = "HauptmenueHauptContainer";
        public string HauptContainerParent = "Hauptmenue";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public Color32 HeaderContainerColor = new Color32();
        public int HeaderContainerHeight = 120;

        // HauptmenueHeaderContainer
        public string HeaderContainerNameInCanvas = "HauptmenueHeaderContainer";
        public string HeaderContainerParent = "Hauptmenue";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public Color32 HeaderLogoColor = new Color32();
        public int HeaderLogoHeight = 80;

        // HauptmenueHeaderLogo
        public string HeaderLogoNameInCanvas = "HauptmenueHeaderLogo";
        public string HeaderLogoParent = "HauptmenueHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public string HeaderLogoSourceImage = "Logos/cog-wheel-silhouette_white";
        public int HeaderLogoWidth = 80;
        public Color32 HeaderTextColor = new Color32();
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextHeight = 120;

        // HauptmenueHeaderText
        public string HeaderTextNameInCanvas = "HauptmenueHeaderText";
        public string HeaderTextParent = "HauptmenueHeaderContainer";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public string HeaderTextText = "Hauptmenü";
        public int HeaderTextWidth = 840;
        public int Height = 1080;
        public Color32 InfoContainerColor = new Color32();
        public int InfoContainerHeight = 100;

        //HauptmenueInfoContainer
        public string InfoContainerNameInCanvas = "HauptmenueInfoContainer";
        public string InfoContainerParent = "Hauptmenue";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public Color32 InfoLogoColor = new Color32(); // ausgeblendet
        public int InfoLogoHeight = 100;

        //HauptmenueInfoLogo
        public string InfoLogoNameInCanvas = "HauptmenueInfoLogo";
        public string InfoLogoParent = "HauptmenueInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public string InfoLogoSourceImage = ""; //"Logos/list-with-dots_white";
        public int InfoLogoWidth = 60;
        public Color32 InfoTextColor = new Color32();
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextHeight = 120;

        // HauptmenueInfoText
        public string InfoTextNameInCanvas = "HauptmenueInfoText";
        public string InfoTextParent = "HauptmenueInfoContainer";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 131;
        public string InfoTextText = "generelle Einstellungen";
        public int InfoTextWidth = 840;
        public string Name = "Hauptmenü";

        // Hauptmenue
        public string NameInCanvas = "Hauptmenue";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;

        public int Width = 960;

        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        // ab hier die einzelnen Zeilen

        //HauptmenueZeileContainer
        public int ZeileContainerAnzahlZeilen = 8; // momentan max. 8 wegen Höehe 1080 px
        public Color32 ZeileContainerColor = new Color32();
        public Color32 ZeileContainerColorToggle; // nur in Hauptmenü 
        public int ZeileContainerHeight = 100;
        public string ZeileContainerNameInCanvas = "HauptmenueZeileContainer";
        public string ZeileContainerParent = "HauptmenueHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public Color32 ZeileDetailColor = new Color32();
        public int ZeileDetailFontSize = 25;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailHeight = 50;

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
        public int ZeileDetailPosX = VerschiebungX + 80;
        public int ZeileDetailPosY = VerschiebungY - 294;
        public string ZeileDetailText = "";
        public int ZeileDetailWidth = 920;
        public Color32 ZeileHeaderColor = new Color32();
        public int ZeileHeaderFontSize = 45;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderHeight = 60;

        //HauptmenueZeileHeader
        public string ZeileHeaderNameInCanvas = "HauptmenueZeileHeader";
        public string ZeileHeaderParent = "HauptmenueZeileContainer";
        public int ZeileHeaderPosX = VerschiebungX + 80;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public string ZeileHeaderText = "";
        public int ZeileHeaderWidth = 600;
        public Color32 ZeileLogoColor = new Color32();
        public int ZeileLogoHeight = 100;

        //HauptmenueZeileLogo
        public string ZeileLogoNameInCanvas = "HauptmenueZeileDetailLogo";
        public string ZeileLogoParent = "HauptmenueZeileContainer";
        public int ZeileLogoPosX = VerschiebungX;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public string ZeileLogoSourceImage = ""; //"Logos/list-with-dots_white";
        public int ZeileLogoWidth = 60;
        public Color32 ZeileNummerColor = new Color32();
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerHeight = 100;

        // HauptmenueZeilenNummer
        public string ZeileNummerNameInCanvas = "HauptmenueZeileNummer";
        public string ZeileNummerParent = "HauptmenueZeileContainer";
        public int ZeileNummerPosX = VerschiebungX + 12;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public string ZeileNummerText = "";
        public int ZeileNummerWidth = 60;
    }
}