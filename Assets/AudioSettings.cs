using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public bool musicEnabled;
    public bool sfxEnabled;
    [SerializeField] AudioSource msc, sfx;
    [SerializeField] Toggle mscT, sfxT;

    // Start is called before the first frame update
    void Start()
    {
        int mscE = PlayerPrefs.GetInt("msc", 1);
        int sfxE = PlayerPrefs.GetInt("sfx", 1);
        if (mscE == 1){
            musicEnabled = true;
            mscT.isOn = true;
        }
        else if (mscE == 0){
            musicEnabled = false;
            mscT.isOn = false;
        }
        if (sfxE == 1){
            sfxEnabled = true;
            sfxT.isOn = true;
        }
        else if (sfxE == 0){
            sfxEnabled = false;
            sfxT.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (musicEnabled){
            msc.enabled = true;
        }
        else if (!musicEnabled){
            msc.enabled = false;
        }

        if (sfxEnabled){
            sfx.enabled = true;
        }
        else if (!sfxEnabled){
            sfx.enabled = false;
        }
    }

    public void changeMsc(bool enabler){
        musicEnabled = enabler;
        if (musicEnabled) {
            PlayerPrefs.SetInt("msc", 1);
        }
        else if (!musicEnabled) {
            PlayerPrefs.SetInt("msc", 0);
        }
    }

    public void changeSfx(bool enabler){
        sfxEnabled = enabler;
        if (sfxEnabled) {
            PlayerPrefs.SetInt("sfx", 1);
        }
        else if (!sfxEnabled) {
            PlayerPrefs.SetInt("sfx", 0);
        }
    }
}
