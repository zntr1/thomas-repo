using UnityEngine;

public partial class GUI_Innentuer
{
    public class GuiInnentuer
    {
        // Verschiebung von der linken obern Ecke (Anker entsprechend gesetzt)
        private static readonly int VerschiebungX = 0;
        private static readonly int VerschiebungY = 0;
        public Color32 Color = new Color32();
        public Color32 HauptContainerColor = new Color32();
        public int HauptContainerHeight = 920;

        //InnentuerHauptContainer
        public string HauptContainerNameInCanvas = "InnentuerHauptContainer";
        public string HauptContainerParent = "Innentuer";
        public int HauptContainerPosX = VerschiebungX;
        public int HauptContainerPosY = VerschiebungY - 220;
        public int HauptContainerWidth = 960;
        public Color32 HeaderContainerColor = new Color32();
        public int HeaderContainerHeight = 120;

        // InnentuerHeaderContainer
        public string HeaderContainerNameInCanvas = "InnentuerHeaderContainer";
        public string HeaderContainerParent = "Innentuer";
        public int HeaderContainerPosX = VerschiebungX;
        public int HeaderContainerPosY = VerschiebungY;
        public int HeaderContainerWidth = 960;
        public Color32 HeaderLogoColor = new Color32();
        public int HeaderLogoHeight = 80;

        // InnentuerHeaderLogo
        public string HeaderLogoNameInCanvas = "InnentuerHeaderLogo";
        public string HeaderLogoParent = "InnentuerHeaderContainer";
        public int HeaderLogoPosX = VerschiebungX + 20;
        public int HeaderLogoPosY = VerschiebungY - 20;
        public string HeaderLogoSourceImage = "Logos/numbered-list_white";
        public int HeaderLogoWidth = 80;
        public Color32 HeaderTextColor = new Color32();
        public int HeaderTextFontSize = 70;
        public FontStyle HeaderTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string HeaderTextFontType = "Arial.ttf";
        public int HeaderTextHeight = 120;

        // InnentuerHeaderText
        public string HeaderTextNameInCanvas = "InnentuerHeaderText";
        public string HeaderTextParent = "InnentuerHeaderContainer";
        public int HeaderTextPosX = VerschiebungX + 150;
        public int HeaderTextPosY = VerschiebungY - 23;
        public string HeaderTextText = "Innentür";
        public int HeaderTextWidth = 840;
        public int Height = 1080;
        public Color32 InfoContainerColor = new Color32();
        public int InfoContainerHeight = 100;

        //InnentuerInfoContainer
        public string InfoContainerNameInCanvas = "InnentuerInfoContainer";
        public string InfoContainerParent = "Innentuer";
        public int InfoContainerPosX = VerschiebungX;
        public int InfoContainerPosY = VerschiebungY - 120;
        public int InfoContainerWidth = 960;
        public Color32 InfoLogoColor = new Color32(); //ausgeblendet
        public int InfoLogoHeight = 100;

        //InnentuerInfoLogo
        public string InfoLogoNameInCanvas = "InnentuerInfoLogo";
        public string InfoLogoParent = "InnentuerInfoContainer";
        public int InfoLogoPosX = VerschiebungX + 900;
        public int InfoLogoPosY = VerschiebungY - 120;
        public string InfoLogoSourceImage = ""; //"Logos/list-with-dots_white";
        public int InfoLogoWidth = 60;
        public Color32 InfoTextColor = new Color32();
        public int InfoTextFontSize = 35;
        public FontStyle InfoTextFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string InfoTextFontType = "Arial.ttf";
        public int InfoTextHeight = 120;

        // InnentuerInfoText
        public string InfoTextNameInCanvas = "InnentuerInfoText";
        public string InfoTextParent = "InnentuerInfoContainer";
        public int InfoTextPosX = VerschiebungX + 20;
        public int InfoTextPosY = VerschiebungY - 131;
        public string InfoTextText = "wählen Sie bitte Voreinstellungen";
        public int InfoTextWidth = 840;
        public string Name = "Innentür";

        // Innentuer
        public string NameInCanvas = "Innentuer";
        public string Parent = "GUI";
        public int PosX = VerschiebungX;
        public int PosY = VerschiebungY;

        public int Width = 960;

        // vertikaler Abstand zwischen den einzelnen Containern
        public int ZeileContainerAbstandY = -4;

        // ab hier die einzelnen Zeilen

        //InnentuerZeileContainer
        public int ZeileContainerAnzahlZeilen = 8; // momentan max. 8 wegen Höehe 1080 px
        public Color32 ZeileContainerColor = new Color32();
        public int ZeileContainerHeight = 100;
        public string ZeileContainerNameInCanvas = "InnentuerZeileContainer";
        public string ZeileContainerParent = "InnentuerHauptContainer";
        public int ZeileContainerPosX = VerschiebungX;
        public int ZeileContainerPosY = VerschiebungY - 230;
        public int ZeileContainerWidth = 960;
        public Color32 ZeileDetailColor = new Color32();
        public int ZeileDetailFontSize = 25;
        public FontStyle ZeileDetailFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileDetailFontType = "Arial.ttf";
        public int ZeileDetailHeight = 50;

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
        public int ZeileDetailPosX = VerschiebungX + 80;
        public int ZeileDetailPosY = VerschiebungY - 294;
        public string ZeileDetailText = "";
        public int ZeileDetailWidth = 920;
        public Color32 ZeileHeaderColor = new Color32();
        public int ZeileHeaderFontSize = 45;
        public FontStyle ZeileHeaderFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileHeaderFontType = "Arial.ttf";
        public int ZeileHeaderHeight = 60;

        //InnentuerZeileHeader
        public string ZeileHeaderNameInCanvas = "InnentuerZeileHeader";
        public string ZeileHeaderParent = "InnentuerZeileContainer";
        public int ZeileHeaderPosX = VerschiebungX + 80;
        public int ZeileHeaderPosY = VerschiebungY + -240;
        public string ZeileHeaderText = "";
        public int ZeileHeaderWidth = 600;
        public Color32 ZeileLogoColor = new Color32();
        public int ZeileLogoHeight = 100;

        //InnentuerZeileLogo
        public string ZeileLogoNameInCanvas = "InnentuerZeileDetailLogo";
        public string ZeileLogoParent = "InnentuerZeileContainer";
        public int ZeileLogoPosX = VerschiebungX;
        public int ZeileLogoPosY = VerschiebungY - 230;
        public string ZeileLogoSourceImage = ""; //"Logos/list-with-dots_white";
        public int ZeileLogoWidth = 60;
        public Color32 ZeileNummerColor = new Color32();
        public int ZeileNummerFontSize = 70;
        public FontStyle ZeileNummerFontStyle = FontStyle.Normal; // Normal, Bold, Italic, BoldAndItalic
        public string ZeileNummerFontType = "Arial.ttf";
        public int ZeileNummerHeight = 100;

        // InnentuerZeilenNummer
        public string ZeileNummerNameInCanvas = "InnentuerZeileNummer";
        public string ZeileNummerParent = "InnentuerZeileContainer";
        public int ZeileNummerPosX = VerschiebungX + 12;
        public int ZeileNummerPosY = VerschiebungY - 238;
        public string ZeileNummerText = "";
        public int ZeileNummerWidth = 60;
    }
}