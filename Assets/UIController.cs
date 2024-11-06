using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    [SerializeField] Button qButton;
    [SerializeField] TextMeshProUGUI qText;

    [SerializeField] Image[] g7Set; 
    [SerializeField] Image[] g8Set; 
    [SerializeField] Image[] g9Set;
    [SerializeField] string[,] answers = new string[20,4];
    [SerializeField] TextMeshProUGUI[] answerText = new TextMeshProUGUI[4];
    int[] cAnswer = new int[14];
    int qIndex;
    int sIndex;
    float questionCD = 2f;

    void Start(){
        //fbButton.GetComponent<Image>().fillAmount = 0.5f;
    }

    private void Update(){
        questionCD += Time.deltaTime;
        if (questionCD >= 3f){
            qButton.interactable = true;
            qText.text = "ANSWER QUESTION / REGAIN MANA";
        }
        else if (questionCD < 3f){
            qButton.interactable = false;
            qText.text = "COOLDOWN";
        }
    }

    public void changeQset(int set){
        switch (set){
            case 7:
                answers[0,0] = "Gambar A";
                answers[0,1] = "Gambar B";
                answers[0,2] = "Gambar C";
                answers[0,3] = "Gambar A & C";
                cAnswer[0] = 3;
                
                answers[1,0] = "Lingkaran";
                answers[1,1] = "Persegi";
                answers[1,2] = "Persegi Panjang";
                answers[1,3] = "Segitiga";
                cAnswer[1] = 2;

                answers[2,0] = "46";
                answers[2,1] = "36";
                answers[2,2] = "33";
                answers[2,3] = "32";
                cAnswer[2] = 3;

                answers[3,0] = "50";
                answers[3,1] = "60";
                answers[3,2] = "70";
                answers[3,3] = "32";
                cAnswer[3] = 2;

                answers[4,0] = "5";
                answers[4,1] = "10";
                answers[4,2] = "12";
                answers[4,3] = "9";
                cAnswer[4] = 1;

                answers[5,0] = "7x + 5y";
                answers[5,1] = "7x + 4y";
                answers[5,2] = "5x + 7y";
                answers[5,3] = "4x + 7y";
                cAnswer[5] = 0;

                answers[6,0] = "1000";
                answers[6,1] = "2000";
                answers[6,2] = "3000";
                answers[6,3] = "4000";
                cAnswer[6] = 1;

                answers[7,0] = "8";
                answers[7,1] = "10";
                answers[7,2] = "12";
                answers[7,3] = "14";
                cAnswer[7] = 2;

                answers[8,0] = "10";
                answers[8,1] = "20";
                answers[8,2] = "30";
                answers[8,3] = "40";
                cAnswer[8] = 1;

                answers[9,0] = "30";
                answers[9,1] = "60";
                answers[9,2] = "115";
                answers[9,3] = "224";
                cAnswer[9] = 1;

                sIndex = 7;
                break;

            case 8:
                answers[0,0] = "6";
                answers[0,1] = "7";
                answers[0,2] = "8";
                answers[0,3] = "9";
                cAnswer[0] = 1;
                
                answers[1,0] = "12";
                answers[1,1] = "14";
                answers[1,2] = "15";
                answers[1,3] = "16";
                cAnswer[1] = 2;

                answers[2,0] = "3000";
                answers[2,1] = "3500";
                answers[2,2] = "4500";
                answers[2,3] = "5000";
                cAnswer[2] = 3;

                answers[3,0] = "4000 + 3000";
                answers[3,1] = "2500 + 2000";
                answers[3,2] = "3000 + 2000";
                answers[3,3] = "3500 + 1500";
                cAnswer[3] = 2;

                answers[4,0] = "10";
                answers[4,1] = "20";
                answers[4,2] = "30";
                answers[4,3] = "40";
                cAnswer[4] = 0;

                answers[5,0] = "134";
                answers[5,1] = "144";
                answers[5,2] = "154";
                answers[5,3] = "164";
                cAnswer[5] = 2;

                answers[6,0] = "40";
                answers[6,1] = "44";
                answers[6,2] = "52";
                answers[6,3] = "56";
                cAnswer[6] = 2;

                answers[7,0] = "120";
                answers[7,1] = "130";
                answers[7,2] = "140";
                answers[7,3] = "150";
                cAnswer[7] = 0;

                answers[8,0] = "9 akar 6";
                answers[8,1] = "8 akar 7";
                answers[8,2] = "23";
                answers[8,3] = "43";
                cAnswer[8] = 1;

                sIndex = 8;
                break;
            
            case 9:
                answers[0,0] = "Lingkaran";
                answers[0,1] = "Persegi Panjang";
                answers[0,2] = "Segitiga";
                answers[0,3] = "Oval";
                cAnswer[0] = 0;

                answers[1,0] = "32 cm^2";
                answers[1,1] = "96 cm^2";
                answers[1,2] = "48 cm^2";
                answers[1,3] = "50 cm^2";
                cAnswer[1] = 2;
                
                answers[2,0] = "30 cm";
                answers[2,1] = "34 cm";
                answers[2,2] = "32 cm";
                answers[2,3] = "40 cm";
                cAnswer[2] = 1;
                
                answers[3,0] = "2";
                answers[3,1] = "3";
                answers[3,2] = "4";
                answers[3,3] = "1";
                cAnswer[3] = 0;
                
                answers[4,0] = "24 cm^2";
                answers[4,1] = "96 cm^2";
                answers[4,2] = "72 cm^2";
                answers[4,3] = "48 cm^2";
                cAnswer[4] = 1;
                
                answers[5,0] = "40 cm";
                answers[5,1] = "96 cm";
                answers[5,2] = "56 cm";
                answers[5,3] = "14 cm^2";
                cAnswer[5] = 0;
                
                answers[6,0] = "Sudut berpelurus";
                answers[6,1] = "Sudut siku-siku";
                answers[6,2] = "Sudut tumpul";
                answers[6,3] = "Sudut lancip";
                cAnswer[6] = 3;
                
                answers[7,0] = "Setiap kotaknya ani membagi menjadi 8 bagian, sehingga 8 bagian dikali 3 kotak akan menghasilkan 24 potong, sisanya Ani";
                answers[7,1] = "Setiap kotaknya ani membagi menjadi 7 bagian, sehingga 7 bagian dikali 3 kotak akan menghasilkan 21 bagian(potong)";
                answers[7,2] = "Ani hanya membagi kepada teman dekatnya saja yakni 7 orang, sehingga dia membagi nya menjadi 7 bagian saja";
                answers[7,3] = "Ani tidak suka membagikan makannaya kepada temannya, tanpa sepengetahuan ibunya, karena dia suka dengan brem";
                cAnswer[7] = 1;
                
                answers[8,0] = "126 cm2";
                answers[8,1] = "132 cm2";
                answers[8,2] = "140 cm2";
                answers[8,3] = "156 cm2";
                cAnswer[8] = 0;
                
                answers[9,0] = "3/2 Liter ";
                answers[9,1] = "2 Liter ";
                answers[9,2] = "6 Liter ";
                answers[9,3] = "27/2 Liter ";
                cAnswer[9] = 2;
                
                answers[10,0] = "Garis Berpotongan";
                answers[10,1] = "Garis Sejajar";
                answers[10,2] = "Garis Berimpit";
                answers[10,3] = "Garis Tegak Lurus";
                cAnswer[10] = 1;
                
                answers[11,0] = "Sudut Lancip";
                answers[11,1] = "Sudut Tumpul";
                answers[11,2] = "Sudut Siku-siku";
                answers[11,3] = "Sudut Berpelurus";
                cAnswer[11] = 0;
                
                answers[12,0] = "25°";
                answers[12,1] = "50°";
                answers[12,2] = "75°";
                answers[12,3] = "30°";
                cAnswer[12] = 3;
                
                answers[13,0] = "Refleksi";
                answers[13,1] = "Rotasi";
                answers[13,2] = "Dilatasi";
                answers[13,3] = "Traslasi";
                cAnswer[13] = 0;
                
                sIndex = 9;
                break;

            default:
                break;
        }
        changeQuestion();
    }

    public void changeQuestion(){
        Image[] questionImage = new Image[20];
        if (sIndex == 7){
            questionImage = g7Set;
            qIndex = UnityEngine.Random.Range(0, 10);
        }
        else if (sIndex == 8){
            questionImage = g8Set;
            qIndex = UnityEngine.Random.Range(0, 9);
        }
        else if (sIndex == 9){
            questionImage = g9Set;
            qIndex = UnityEngine.Random.Range(0, 14);
        }

        foreach (var p in questionImage){
            p.gameObject.SetActive(false);
        }
        questionImage[qIndex].gameObject.SetActive(true);

        for (int i = 0; i < 4; i++){
            answerText[i].text = answers[qIndex, i];
        }
        // cAnswer = CA;
    }

    public void answer(int choice){
        if (choice == cAnswer[qIndex]){
            gm.correctAnswer();
        }
        else{
            gm.wrongAnswer();
            questionCD = 0;
        }
        changeQuestion();
        closeQuestionPanel();
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
