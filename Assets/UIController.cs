using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject questionPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameManager gm;
    [SerializeField] PlayerController player;
    [SerializeField] Button fbButton;
    [SerializeField] Button lgButton;
    [SerializeField] Button shButton;
    [SerializeField] Button hlButton;

    void Start(){
        //fbButton.GetComponent<Image>().fillAmount = 0.5f;
    }

    public void openQuestionPanel(){
        questionPanel.SetActive(true);
        optionsPanel.SetActive(false);

    }

    public void closeQuestionPanel(){
        questionPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void offOptions(){
        optionsPanel.SetActive(false);
    }

    public void setFill(float timer){
        fbButton.GetComponent<Image>().fillAmount = timer/1f;
        lgButton.GetComponent<Image>().fillAmount = timer/2f;
        shButton.GetComponent<Image>().fillAmount = timer/1f;
        hlButton.GetComponent<Image>().fillAmount = timer/0.5f;
    }

    public void enableButton(int buttonIndex){
        switch (buttonIndex){
            case 1:
                fbButton.interactable = true;
                break;
            case 2:
                lgButton.interactable = true;
                break;
            case 3:
                shButton.interactable = true;
                break;
            case 4:
                hlButton.interactable = true;
                break;
            
            default:
                break;
        }
    }

    public void disableButton(int buttonIndex){
        switch (buttonIndex){
            case 1:
                fbButton.interactable = false;
                break;
            case 2:
                lgButton.interactable = false;
                break;
            case 3:
                shButton.interactable = false;
                break;
            case 4:
                hlButton.interactable = false;
                break;

            default:
                break;
        }
    }
}
