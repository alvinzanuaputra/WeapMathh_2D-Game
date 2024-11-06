using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    [SerializeField] GameObject modes;
    bool _isOpen = false;

    public void openCloseModes(){
        _isOpen = !_isOpen;
        modes.SetActive(_isOpen);
    }
}
