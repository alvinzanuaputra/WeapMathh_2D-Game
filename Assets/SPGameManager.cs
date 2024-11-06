using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SPGameManager : MonoBehaviour
{
    [SerializeField] SPPlayerController player1;
    [SerializeField] SPBotController bot;
    [SerializeField] SPUIController ui;
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject waitingScreen;
    [SerializeField] TextMeshProUGUI joiningText;
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] AudioClip battleMsc, gameOver;
    [SerializeField] AudioSource src;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] GameObject countdown_screen;

    public bool player_dead = false;
    float timer;

    void Awake(){
        timer = 0;
    }

    void Start(){
        // questionSetChange(7);
        // changeQ();
        audioEnabler();
    }

    void Update(){
    }

    private void audioEnabler(){
        int mscEnabled = PlayerPrefs.GetInt("msc", 1);
        int sfxEnabled = PlayerPrefs.GetInt("sfx", 1);
        if (mscEnabled == 0){
            // src.enabled = false;
        }
        else if (mscEnabled == 1){
            src.enabled = true;
        }
        player1.audioSetter(sfxEnabled);
        bot.audioSetter(sfxEnabled);
    }

    public void player_fb_out() {
        StartCoroutine(Send_fb_out());
    }

    IEnumerator Send_fb_out() {
        bot.player_fireball_out = true;
        yield return new WaitForSeconds(1.5f);
        bot.player_fireball_out = false;
    }

    public void correctAnswer(){
        player1.changeMana(30);
    }

    public void wrongAnswer(){

    }

    public void exitMatch(){
        SceneManager.LoadScene(1);
    }

    public void death_call(int p) {
        ui.offOptions();
        src.clip = gameOver;
        src.loop = false;
        src.Play();
        StartCoroutine(WinDow(p));
    }

    IEnumerator WinDow(int p){
        if (p == 1){
            winnerText.text = "You Died";
        }
        else if (p == 2){
            winnerText.text = player1.userName.text + " Wins";
        }
        if (p == 1){
            int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
            coinOwned += 0;
            PlayerPrefs.SetInt("coinOwned", coinOwned);
            rewardText.text = "+ 0";
        }
        else if (p == 2){
            int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
            coinOwned += 1;
            PlayerPrefs.SetInt("coinOwned", coinOwned);
            rewardText.text = "+ 1";
        } 
        yield return new WaitForSeconds(1f);
        winScreen.SetActive(true);
    }

    public void initiate_battle() {
        src.Pause();
        player1.gameObject.SetActive(true);
        bot.gameObject.SetActive(true);
        countdown_screen.SetActive(true);
        StartCoroutine(battle_countdown());
    }

    IEnumerator battle_countdown() {
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "FIGHT";
        src.clip = battleMsc;
        src.Play();
        yield return new WaitForSeconds(1f);
        countdown_screen.SetActive(false);
        bot.roll_for_timer();
    }

    public void isJoining(){
        joiningText.color = Color.white;
        joiningText.text = "Joining...";
    }

    public void waitingPlayer(){
        joiningText.text = "Waiting for P2";
    }

    public void lightning_call(int p) {
        StartCoroutine(lightning_routine_p2(p));
    }

    IEnumerator lightning_routine_p2(int p) {
        GameObject lg = null;
        if (p == 1){
            player1.playLightningSfx();
            lg = Instantiate(lightning, bot.transform.position, Quaternion.identity);
        }
        else if (p == 2){
            lg = Instantiate(lightning, player1.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(lg);
        if (p == 1) {
            bot.change_health(-40);
        }
        else if (p == 2) {
            player1.changeHealth(-40);
        }
    }
}
