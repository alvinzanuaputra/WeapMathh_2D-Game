using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoader : MonoBehaviour
{
    const string glyphs= "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";

    [SerializeField] GameObject c1, c2, c3, c4, c5, c6, c7, c8, c9;
    [SerializeField] GameObject h1, h2, h3;
    [SerializeField] GameObject sc1, sc2, sc3, sc4, sc5, sc6, sc7, sc8, sc9;
    [SerializeField] GameObject sh1, sh2, sh3;
    [SerializeField] GameObject checkmark;
    [SerializeField] TextMeshProUGUI namesText;
    [SerializeField] TextMeshProUGUI gradeText;
    [SerializeField] TextMeshProUGUI idText;
    [SerializeField] TextMeshProUGUI genderText;
    [SerializeField] TextMeshProUGUI coinAmountText;
    [SerializeField] Button buyButton;
    [SerializeField] int c2price, c3price, c4price, c5price, c6price, c7price, c8price, c9price;
    [SerializeField] TextMeshProUGUI priceText;
    int charIndex = 0;
    int houseIndex = 0;
    bool isHouse = false;
    bool c2Own = false;
    bool c3Own = false;
    bool c4Own = false;
    bool c5Own = false;
    bool c6Own = false;
    bool c7Own = false;
    bool c8Own = false;
    bool c9Own = false;
    bool h2Own = false;
    bool h3Own = false;
    public bool proVersion = false;
    void Start()
    {
        charIndex = PlayerPrefs.GetInt("charIndex", 0);
        Debug.Log(charIndex);
        if (charIndex < 0 || charIndex > 8) {
            charIndex = 0;
        }
        houseIndex = PlayerPrefs.GetInt("houseIndex", 0);
        if (houseIndex < 0 || houseIndex > 2) {
            houseIndex = 0;
        }
        Debug.Log(charIndex);
        loadOwnedCharacters();
        applyChange();
        changeGrade(PlayerPrefs.GetInt("grade", 7));
        changeGender(PlayerPrefs.GetString("gender", "..."));
        idAssign();
        updateCoinAmount();
    }

    void Update(){
        if (isHouse){
            changeHouse();
        }
        else if (!isHouse) {
            changeChar();
        }
        selTextEnabler();
    }

    public void updateCoinAmount(){
        int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
        coinAmountText.text = coinOwned.ToString();
    }

    public void buyItem(){
        int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
        if (!isHouse){
            if (charIndex == 1){
                if (coinOwned >= c2price){
                    coinOwned -= c2price;
                    PlayerPrefs.SetInt("isOwnC2", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (charIndex == 2) {
                if (coinOwned >= c3price){
                    coinOwned -= c3price;
                    PlayerPrefs.SetInt("isOwnC3", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (charIndex == 3) {
                if (coinOwned >= c4price){
                    coinOwned -= c4price;
                    PlayerPrefs.SetInt("isOwnC4", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (charIndex == 4) {
                if (coinOwned >= c5price){
                    coinOwned -= c5price;
                    PlayerPrefs.SetInt("isOwnC5", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (charIndex == 5) {
                if (coinOwned >= c6price){
                    coinOwned -= c6price;
                    PlayerPrefs.SetInt("isOwnC6", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (charIndex == 6) {
                if (coinOwned >= c7price){
                    coinOwned -= c7price;
                    PlayerPrefs.SetInt("isOwnC7", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (charIndex == 7) {
                if (coinOwned >= c8price){
                    coinOwned -= c8price;
                    PlayerPrefs.SetInt("isOwnC8", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (charIndex == 8) {
                if (coinOwned >= c9price){
                    coinOwned -= c9price;
                    PlayerPrefs.SetInt("isOwnC9", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
        }
        else if (isHouse) {
            if (houseIndex == 1) {
                if (coinOwned >= 250){
                    coinOwned -= 250;
                    PlayerPrefs.SetInt("isOwnH2", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
            else if (houseIndex == 2) {
                if (coinOwned >= 250){
                    coinOwned -= 250;
                    PlayerPrefs.SetInt("isOwnH3", 1);
                    PlayerPrefs.SetInt("coinOwned", coinOwned);
                    updateCoinAmount();
                }
            }
        }
        loadOwnedCharacters();
    }

    private void loadOwnedCharacters() {
        int isOwnC2 = PlayerPrefs.GetInt("isOwnC2", 0);
        int isOwnC3 = PlayerPrefs.GetInt("isOwnC3", 0);
        int isOwnC4 = PlayerPrefs.GetInt("isOwnC4", 0);
        int isOwnC5 = PlayerPrefs.GetInt("isOwnC5", 0);
        int isOwnC6 = PlayerPrefs.GetInt("isOwnC6", 0);
        int isOwnC7 = PlayerPrefs.GetInt("isOwnC7", 0);
        int isOwnC8 = PlayerPrefs.GetInt("isOwnC8", 0);
        int isOwnC9 = PlayerPrefs.GetInt("isOwnC9", 0);
        if (isOwnC2 == 1 || proVersion) c2Own = true;
        if (isOwnC3 == 1 || proVersion) c3Own = true;
        if (isOwnC4 == 1 || proVersion) c4Own = true;
        if (isOwnC5 == 1 || proVersion) c5Own = true;
        if (isOwnC6 == 1 || proVersion) c6Own = true;
        if (isOwnC7 == 1 || proVersion) c7Own = true;
        if (isOwnC8 == 1 || proVersion) c8Own = true;
        if (isOwnC9 == 1 || proVersion) c9Own = true;

        int isOwnH2 = PlayerPrefs.GetInt("isOwnH2", 0);
        int isOwnH3 = PlayerPrefs.GetInt("isOwnH3", 0);
        if (isOwnH2 == 1 || proVersion) h2Own = true;
        if (isOwnH3 == 1 || proVersion) h3Own = true;
    }

    private void idAssign(){
        string idGet = PlayerPrefs.GetString("id", "0");
        if (idGet == "0"){
            setID();
        }
        idText.text = "ID: " + PlayerPrefs.GetString("id");
    }

    private void setID(){
        string myString = "";
        for(int i = 0; i < 12; i++){
            myString += glyphs[Random.Range(0, glyphs.Length)];
        }
        PlayerPrefs.SetString("id", myString);
    }

    public void changeGrade(int gr){
        PlayerPrefs.SetInt("grade", gr);
        gradeText.text = "Kelas " + gr;
    }

    public void changeGender(string gender){
        PlayerPrefs.SetString("gender", gender);
        genderText.text = gender;
    }

    public void next() {
        if(!isHouse){
            charIndex = charIndex + 1;
            charIndex = charIndex % 9;
        }
        else if(isHouse){
            houseIndex = houseIndex + 1;
            houseIndex = houseIndex % 3;
        }
    }

    public void prev() {
        if(!isHouse){
            charIndex = charIndex - 1;
            if (charIndex < 0) {
                charIndex = 8;
            }
        }
        else if (isHouse){
            houseIndex = houseIndex - 1;
            if (houseIndex < 0) {
                houseIndex = 2;
            }
        }
    }

    public void changeTab(){
        isHouse = !isHouse;
    }

    public void applyChange(){
        print(charIndex);
        switch (charIndex) {
            case 0:
                disableChar();
                c1.SetActive(true);
                PlayerPrefs.SetInt("charIndex", charIndex);
                break;
            case 1:
                if (c2Own)  {
                    disableChar();
                    c2.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            case 2:
                if (c3Own){
                    disableChar();
                    c3.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            case 3:
                if (c4Own){
                    disableChar();
                    c4.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            case 4:
                if (c5Own){
                    disableChar();
                    c5.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            case 5:
                if (c6Own){
                    disableChar();
                    c6.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            case 6:
                if (c7Own){
                    disableChar();
                    c7.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            case 7:
                if (c8Own){
                    disableChar();
                    c8.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            case 8:
                if (c9Own){
                    disableChar();
                    c9.SetActive(true);
                    PlayerPrefs.SetInt("charIndex", charIndex);
                }
                break;
            default:
                break;
        }
        switch (houseIndex) {
            case 0:
                disableHouse();
                h1.SetActive(true);
                PlayerPrefs.SetInt("houseIndex", houseIndex);
                break;
            case 1:
                if (h2Own) {
                    disableHouse();
                    h2.SetActive(true);
                    PlayerPrefs.SetInt("houseIndex", houseIndex);
                }
                break;
            case 2:
                if (h3Own) {
                    disableHouse();
                    h3.SetActive(true);
                    PlayerPrefs.SetInt("houseIndex", houseIndex);
                }
                break;
            default:
                break;
        }
    }


    private void disableHouse() {
        h1.SetActive(false);
        h2.SetActive(false);
        h3.SetActive(false); 
    }
    private void disableChar() {
        c1.SetActive(false);
        c2.SetActive(false);
        c3.SetActive(false); 
        c4.SetActive(false); 
        c5.SetActive(false); 
        c6.SetActive(false); 
        c7.SetActive(false); 
        c8.SetActive(false); 
        c9.SetActive(false); 
    }

    private void selTextEnabler(){
        if (isHouse){
            if (houseIndex == PlayerPrefs.GetInt("houseIndex")){
                checkmark.SetActive(true);
            }
            else if (houseIndex != PlayerPrefs.GetInt("houseIndex")){
                checkmark.SetActive(false);
            }
        }
        if (!isHouse){
            if (charIndex == PlayerPrefs.GetInt("charIndex")){
                checkmark.SetActive(true);
            }
            else if (charIndex != PlayerPrefs.GetInt("charIndex")){
                checkmark.SetActive(false);
            }
        }
    }

    private void changeHouse() {
        priceText.text = "250";
        switch (houseIndex) {
            case 0:
                namesText.text = "kalimantan";
                sh1.SetActive(true);
                sh2.SetActive(false);
                sh3.SetActive(false);
                buyButton.gameObject.SetActive(false);
                break;
            case 1:
                namesText.text = "sulawesi";
                sh1.SetActive(false);
                sh2.SetActive(true);
                sh3.SetActive(false);
                
                if (!h2Own){
                    sh2.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (h2Own){
                    sh2.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 2:
                namesText.text = "jawa";
                sh1.SetActive(false);
                sh2.SetActive(false);
                sh3.SetActive(true);
                if (!h3Own){
                    sh3.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (h3Own){
                    sh3.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    private void changeChar() {
        switch (charIndex) {
            case 0:
                namesText.text = "jawa barat";
                sc1.SetActive(true);
                sc2.SetActive(false);
                sc3.SetActive(false);
                sc4.SetActive(false);
                sc5.SetActive(false);
                sc6.SetActive(false);
                sc7.SetActive(false);
                sc8.SetActive(false);
                sc9.SetActive(false);
                buyButton.gameObject.SetActive(false);
                break;
            case 1:
                namesText.text = "sumatera barat";
                sc1.SetActive(false);
                sc2.SetActive(true);
                sc3.SetActive(false);
                sc4.SetActive(false);
                sc5.SetActive(false);
                sc6.SetActive(false);
                sc7.SetActive(false);
                sc8.SetActive(false);
                sc9.SetActive(false);
                priceText.text = c2price.ToString();
                if (!c2Own){
                    sc2.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c2Own){
                    sc2.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 2:
                namesText.text = "sumatera utara";
                sc1.SetActive(false);
                sc2.SetActive(false);
                sc3.SetActive(true);
                sc4.SetActive(false);
                sc5.SetActive(false);
                sc6.SetActive(false);
                sc7.SetActive(false);
                sc8.SetActive(false);
                sc9.SetActive(false);
                priceText.text = c3price.ToString();
                if (!c3Own){
                    sc3.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c3Own){
                    sc3.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 3:
                namesText.text = "palembang (L)";
                sc1.SetActive(false);
                sc2.SetActive(false);
                sc3.SetActive(false);
                sc4.SetActive(true);
                sc5.SetActive(false);
                sc6.SetActive(false);
                sc7.SetActive(false);
                sc8.SetActive(false);
                sc9.SetActive(false);
                priceText.text = c4price.ToString();
                if (!c4Own){
                    sc4.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c4Own){
                    sc4.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 4:
                namesText.text = "palembang (P)";
                sc1.SetActive(false);
                sc2.SetActive(false);
                sc3.SetActive(false);
                sc4.SetActive(false);
                sc5.SetActive(true);
                sc6.SetActive(false);
                sc7.SetActive(false);
                sc8.SetActive(false);
                sc9.SetActive(false);
                priceText.text = c5price.ToString();
                if (!c5Own){
                    sc5.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c5Own){
                    sc5.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 5:
                namesText.text = "betawi (L)";
                sc1.SetActive(false);
                sc2.SetActive(false);
                sc3.SetActive(false);
                sc4.SetActive(false);
                sc5.SetActive(false);
                sc6.SetActive(true);
                sc7.SetActive(false);
                sc8.SetActive(false);
                sc9.SetActive(false);
                priceText.text = c6price.ToString();
                if (!c6Own){
                    sc6.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c6Own){
                    sc6.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 6:
                namesText.text = "betawi (P)";
                sc1.SetActive(false);
                sc2.SetActive(false);
                sc3.SetActive(false);
                sc4.SetActive(false);
                sc5.SetActive(false);
                sc6.SetActive(false);
                sc7.SetActive(true);
                sc8.SetActive(false);
                sc9.SetActive(false);
                priceText.text = c7price.ToString();
                if (!c7Own){
                    sc7.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c7Own){
                    sc7.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 7:
                namesText.text = "sunda (L)";
                sc1.SetActive(false);
                sc2.SetActive(false);
                sc3.SetActive(false);
                sc4.SetActive(false);
                sc5.SetActive(false);
                sc6.SetActive(false);
                sc7.SetActive(false);
                sc8.SetActive(true);
                sc9.SetActive(false);
                priceText.text = c8price.ToString();
                if (!c8Own){
                    sc8.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c8Own){
                    sc8.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            case 8:
                namesText.text = "sunda (P)";
                sc1.SetActive(false);
                sc2.SetActive(false);
                sc3.SetActive(false);
                sc4.SetActive(false);
                sc5.SetActive(false);
                sc6.SetActive(false);
                sc7.SetActive(false);
                sc8.SetActive(false);
                sc9.SetActive(true);
                priceText.text = c9price.ToString();
                if (!c9Own){
                    sc9.GetComponent<SpriteRenderer>().color = Color.black;
                    buyButton.gameObject.SetActive(true);
                }
                else if (c9Own){
                    sc9.GetComponent<SpriteRenderer>().color = Color.white;
                    buyButton.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
