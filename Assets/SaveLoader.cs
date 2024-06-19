using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] GameObject c1, c2, c3;
    int charIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        charIndex = PlayerPrefs.GetInt("charIndex");
        if (charIndex < 0 || charIndex > 2) {
            charIndex = 0;
        }
        changeChar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextChar() {
        charIndex = charIndex + 1;
        charIndex = charIndex % 3;
        changeChar();
    }

    public void prevChar() {
        charIndex = charIndex - 1;
        if (charIndex < 0) {
            charIndex = 2;
        }
        changeChar();
    }

    private void changeChar() {
        PlayerPrefs.SetInt("charIndex", charIndex);
        switch (charIndex) {
            case 0:
                c1.SetActive(true);
                c2.SetActive(false);
                c3.SetActive(false);
                break;
            case 1:
                c1.SetActive(false);
                c2.SetActive(true);
                c3.SetActive(false);
                break;
            case 2:
                c1.SetActive(false);
                c2.SetActive(false);
                c3.SetActive(true);
                break;
            default:
                break;
        }
    }
}
