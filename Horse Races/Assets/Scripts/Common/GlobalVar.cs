using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar : MonoBehaviour
{
    public static GlobalVar Instance { get; private set; }
    public bool IdealComplete { get; set; }
    public float BGMVolume { get; set; }
    public float SFXVolume { get; set; }
    public float MasterVolume { get; set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            if (!PlayerPrefs.HasKey("Ideal")) {
                PlayerPrefs.SetInt("Ideal", 0);
                IdealComplete = false;
            } else {
                IdealComplete = System.Convert.ToBoolean(PlayerPrefs.GetInt("Ideal"));
            }
        } else { Destroy(gameObject); }

        if (!PlayerPrefs.HasKey("BGM")) {
            PlayerPrefs.SetFloat("BGM", 0.5f);
        }
        if (!PlayerPrefs.HasKey("SFX")) {
            PlayerPrefs.SetFloat("SFX", 0.5f);
        }
        if (!PlayerPrefs.HasKey("MASTER")) {
            PlayerPrefs.SetFloat("MASTER", 0.8f);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
