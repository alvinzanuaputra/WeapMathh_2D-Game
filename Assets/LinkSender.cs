using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkSender : MonoBehaviour
{
    public void openLink(string link){
        Application.OpenURL(link);
    }
}
