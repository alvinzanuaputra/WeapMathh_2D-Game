using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void mp_scene(){
        SceneManager.LoadScene(2);
    }

    public void sp_scene(){
        SceneManager.LoadScene(3);
    }
}
