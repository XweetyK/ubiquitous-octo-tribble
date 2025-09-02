using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public enum Lang { EN, ES };

[System.Serializable]
public class DialBoxTL {
    public bool Active;
    [SerializeField] public Text DanteDialog;
    [SerializeField] public Text HeathDialog;
    [SerializeField] public Text YiSangDialog;
    [SerializeField] public Text GregorDialog;
    [SerializeField] public Text FaustDialog;
    [SerializeField] public Text DonDialog;
    [SerializeField] public Text IdealDialog;
}
[System.Serializable]
public class MenuTL {
    public bool Active;
    [SerializeField] public Text StartBtn;
    [SerializeField] public Text SettingsBtn;
    [SerializeField] public Text ExtraBtn;
    [SerializeField] public Text JukeboxBtn;
    [SerializeField] public Text QuitBtn;

    [SerializeField] public Text CathyTitle;
    [SerializeField] public Text CathyText;
    [SerializeField] public Text CathyYesBtn;
    [SerializeField] public Text CathyNoBtn;

    [SerializeField] public Text SettingsTitle;

    [SerializeField] public Text PopUpTitle;
    [SerializeField] public Text PopUpText;
    [SerializeField] public Text PopUpContinue;
}
[System.Serializable]
public class HelpTL {
    //-----------------------------------------
}
[System.Serializable]
public class TutorialTL {
    public bool Active;
    [SerializeField] public SpriteRenderer Title;
    [SerializeField] public SpriteRenderer[] Line;
    [SerializeField] public Sprite TitleES;
    [SerializeField] public Sprite TitleEN;
    [SerializeField] public Sprite[] LineES;
    [SerializeField] public Sprite[] LineEN;
}
[System.Serializable]
public class PauseTL {
    public bool Active;
    [SerializeField] public Text Title;
    [SerializeField] public Text ResumeBtn;
    [SerializeField] public Text MenuBtn;
}
[System.Serializable]
public class MirrorTL {
    public bool Active;
    [SerializeField] public SpriteRenderer[] Floor;
    [SerializeField] public Sprite FloorEN;
    [SerializeField] public Sprite FloorES;
}

public class Language : MonoBehaviour {
    [SerializeField] public Lang language;
    [SerializeField] Localization localization;
    public DialBoxTL DialogueBox;
    public MenuTL MainMenu;
    public HelpTL HelpMenu;
    public TutorialTL TutorialMenu;
    public PauseTL PauseMenu;
    public MirrorTL MirrorFloor;

    private void Awake() {
        language = (Lang)PlayerPrefs.GetInt("Language");

        if (MainMenu.Active) {
            UpdateMainMenuLanguage();
        }
        if (DialogueBox.Active) {
            UpdateDialogueLanguage();
        }
        if (PauseMenu.Active) {
            UpdatePauseLanguage();
        }
        if (MirrorFloor.Active) {
            UpdateMirrorFloor();
        }
    }

    private void UpdateMainMenuLanguage() {
        switch (language) {
            case Lang.EN:
                if (PlayerPrefs.HasKey("Story")) {
                    MainMenu.StartBtn.text = "-continue-";
                } else {
                    if (GlobalVar.Instance.IdealComplete) {
                        MainMenu.StartBtn.text = "-FREE RACE-";
                    } else {
                        MainMenu.StartBtn.text = "-START!-";
                    }
                }
                MainMenu.SettingsBtn.text = "-Settings-";
                MainMenu.ExtraBtn.text = "-Extras-";
                MainMenu.JukeboxBtn.text = "-Jukebox-";
                MainMenu.QuitBtn.text = "-Quit-";

                MainMenu.SettingsTitle.text = "Settings";

                MainMenu.CathyTitle.text = "Clear all cache";
                MainMenu.CathyText.text = "Are you sure you want to clear the cache?\nSome \"Ideal\" data might return.";
                MainMenu.CathyYesBtn.text = "Clear Cache";
                MainMenu.CathyNoBtn.text = "Cancel";

                MainMenu.PopUpTitle.text = "Hello everyone!";
                MainMenu.PopUpText.text = "We didn't expect limbus stable to grow this big in such a short time, but we are incredibly happy that our silly fan project is giving everyone a fun moment!\n" +
                    "\nwe are a small team and this is our very first project together, we're trying our best to update the game as soon as any issue pops out to bring everyone a good experience.\n" +
                    "\nSo we wanted to thank everyone for your support and your patience!! all your messages mean a lot to us < 3";
                MainMenu.PopUpContinue.text = "(press enter to continue)";
                break;
            case Lang.ES:
                if (PlayerPrefs.HasKey("Story")) {
                    MainMenu.StartBtn.text = "-continuar-";
                } else {
                    if (GlobalVar.Instance.IdealComplete) {
                        MainMenu.StartBtn.text = "-CARRERA LIBRE-";
                    } else {
                        MainMenu.StartBtn.text = "-COMENZAR!-";
                    }
                }
                MainMenu.SettingsBtn.text = "-Ajustes-";
                MainMenu.ExtraBtn.text = "-Extras-";
                MainMenu.JukeboxBtn.text = "-Tocadiscos-";
                MainMenu.QuitBtn.text = "-Salir-";

                MainMenu.SettingsTitle.text = "Ajustes";

                MainMenu.CathyTitle.text = "Limpiar la cache";
                MainMenu.CathyText.text = "Seguro que quieres limpiar la cache?\nAlgunos datos \"Ideales\" podrian regresar.";
                MainMenu.CathyYesBtn.text = "Limpiar";
                MainMenu.CathyNoBtn.text = "Cancelar";

                MainMenu.PopUpTitle.text = "Hola a todos!";
                MainMenu.PopUpText.text = "No esperabamos que limbus stable creciera tanto en tan poco tiempo, pero de verdad nos alegra que se esten divirtiendo con nuestro fangame!\n" +
                    "\nSomos un equipo chiquito y este es nuestro primer proyecto juntos. Hacemos lo posible para actualizar el juego cuanto antes para brindarles una buena experienca.\n" +
                    "\nQueriamos agradecerles a todos su apoyo y paciencia! Todos sus mensajes son muy importantes para nosotros <3";
                MainMenu.PopUpContinue.text = "(Presiona ENTER para continuar)";
                break;
        }
    }
    private void UpdateDialogueLanguage() {
        switch (language) {
            case Lang.EN:
                if (localization != null) {
                    localization.ActiveLanguage = "";
                }

                DialogueBox.DanteDialog.text = "Manager";
                DialogueBox.YiSangDialog.text = "Horse #1";
                DialogueBox.IdealDialog.text = "Horse #1";
                DialogueBox.FaustDialog.text = "Horse #2";
                DialogueBox.DonDialog.text = "Horse #3";
                DialogueBox.HeathDialog.text = "Horse #7";
                DialogueBox.GregorDialog.text = "Horse #13";
                break;
            case Lang.ES:
                if (localization != null) {
                    localization.ActiveLanguage = "ES";
                }

                DialogueBox.DanteDialog.text = "Gerente";
                DialogueBox.YiSangDialog.text = "Caballo #1";
                DialogueBox.IdealDialog.text = "Caballo #1";
                DialogueBox.FaustDialog.text = "Caballo #2";
                DialogueBox.DonDialog.text = "Caballo #3";
                DialogueBox.HeathDialog.text = "Caballo #7";
                DialogueBox.GregorDialog.text = "Caballo #13";
                break;
        }

    }
    private void UpdatePauseLanguage() {
        switch (language) {
            case Lang.EN:
                PauseMenu.Title.text = "-Pause-";
                PauseMenu.ResumeBtn.text = "(Resume)";
                PauseMenu.MenuBtn.text = "(Return to main menu)";
                break;
            case Lang.ES:
                PauseMenu.Title.text = "-Pausa-";
                PauseMenu.ResumeBtn.text = "(Continuar)";
                PauseMenu.MenuBtn.text = "(Menu Principal)";
                break;
        }
    }
    private void UpdateMirrorFloor() {
        switch (language) {
            case Lang.EN:
                for (int i = 0; i < MirrorFloor.Floor.Length; i++) {
                    MirrorFloor.Floor[i].sprite = MirrorFloor.FloorEN;
                }
                break;
            case Lang.ES:
                for (int i = 0; i < MirrorFloor.Floor.Length; i++) {
                    MirrorFloor.Floor[i].sprite = MirrorFloor.FloorES;
                }
                break;
        }
    }

}
