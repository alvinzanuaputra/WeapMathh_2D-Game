using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] Image loadFill;
    [SerializeField] string[] words;
    [SerializeField] TextMeshProUGUI textBox;
    float timer;
    int w1, w2;
    bool changed = false;
    int[] stutterr;
    LinkedList<int> stutterer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        w1 = Random.Range(0,9);
        w2 = Random.Range(0,9);
        while (w2 == w1){
            w2 = Random.Range(0,9);
        }
        textBox.text = words[w1];

        // int gapper = 1;
        // stutterer.AddFirst(gapper);
        // while (gapper < 10){

        // }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // loadFill.fillAmount = timer / 10f;

        if (timer >= 5 && !changed){
            textBox.text = words[w2];
            changed = true;
        }

        if (timer >= 0 && timer <= 2f){
            loadFill.fillAmount = 0.2f / 10f;
        }
        else if (timer >= 2 && timer <= 4f){
            loadFill.fillAmount = 2f / 10f;
        }
        else if (timer >= 4f && timer <= 7f){
            loadFill.fillAmount = 4f / 10f;
        }
        else if (timer >= 7f && timer <= 8f){
            loadFill.fillAmount = 7f / 10f;
        }
        else if (timer >= 8f && timer <= 9f){
            loadFill.fillAmount = 8f / 10f;
        }
        else if (timer >= 9f){
            loadFill.fillAmount = 9.2f / 10f;
        }

        if (timer >= 10f){
            SceneManager.LoadScene(1);
        }
    }
}
