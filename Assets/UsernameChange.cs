using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsernameChange : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TextMeshProUGUI username;
    [SerializeField] TextMeshProUGUI usernameProfile;

    private void Start() {
        string savedUsername = PlayerPrefs.GetString("username");
        if (savedUsername != "") {
            username.text = savedUsername;
            usernameProfile.text = savedUsername;
        }
    }

    public void changeUsername() {
        if (usernameInput != null) {
            PlayerPrefs.SetString("username", usernameInput.text);
            username.text = usernameInput.text;
            usernameProfile.text = usernameInput.text;
        }
    }
}
