using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : NetworkBehaviour
{
    [SerializeField] PlayerController player1;
    [SerializeField] PlayerController player2;
    [SerializeField] UIController ui;
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject waitingScreen;
    [SerializeField] TextMeshProUGUI joiningText;
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] Animator anim;
    [SerializeField] AudioClip battleMsc, gameOver;
    [SerializeField] AudioSource src;
    [SerializeField] TextMeshProUGUI rewardText;
    
    [SerializeField] Sprite[] qImages;
    [SerializeField] string[] answers0, answers1, answers2;
    [SerializeField] string[,] answers = new string[3,4];
    [SerializeField] int[] cAnswer;

    bool dead = false;
    public PlayerController currentPlayer;
    public int pIndex;
    float timer;
    int qIndex;

    void Awake()
    {
        currentPlayer = player1;
        pIndex = 1;
        timer = 0;
        qIndex = UnityEngine.Random.Range(0,3);
    }

    void Start(){
        // questionSetChange(7);
        // changeQ();
        audioEnabler();
    }

    void Update()
    {
        timer += Time.deltaTime;
        // Fireball
        if (currentPlayer.playerMana >= 20 && timer >= 1f)
        {
            ui.enableButton(1);
        }
        else if (currentPlayer.playerMana < 20 || timer < 1f)
        {
            ui.disableButton(1);
        }

        // Lightning
        if (currentPlayer.playerMana >= 60 && timer >= 2f)
        {
            ui.enableButton(2);
        }
        else if (currentPlayer.playerMana < 60 || timer < 2f)
        {
            ui.disableButton(2);
        }

        // Shield
        if (currentPlayer.playerMana >= 10 && timer >= 1f)
        {
            ui.enableButton(3);
        }
        else if (currentPlayer.playerMana < 10 || timer < 1f)
        {
            ui.disableButton(3);
        }

        // Heal
        if (currentPlayer.playerMana >= 40 && timer >= 2f)
        {
            ui.enableButton(4);
        }
        else if (currentPlayer.playerMana < 40 || timer < 2f)
        {
            ui.disableButton(4);
        }
        ui.setFill(timer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log(NetworkManager.ConnectedClients.Count);
            // anim.SetTrigger("C3Cast");
            // Debug.Log(player1.userName.text);
        }

        if (player1.playerHealth <= 0 && !dead)
        {
            dead = true;
            player1.GetComponent<SpriteRenderer>().color = Color.black;
            ui.offOptions();
            src.clip = gameOver;
            src.loop = false;
            src.Play();
            StartCoroutine(WinDow(1));
        }
        if (player2.playerHealth <= 0 && !dead)
        {
            dead = true;
            player2.GetComponent<SpriteRenderer>().color = Color.black;
            ui.offOptions();
            src.clip = gameOver;
            src.loop = false;
            src.Play();
            StartCoroutine(WinDow(2));
        }
    }

    private void audioEnabler(){
        int mscEnabled = PlayerPrefs.GetInt("msc", 1);
        int sfxEnabled = PlayerPrefs.GetInt("sfx", 1);
        if (mscEnabled == 0){
            src.enabled = false;
        }
        else if (mscEnabled == 1){
            src.enabled = true;
        }
        player1.audioSetter(sfxEnabled);
        player2.audioSetter(sfxEnabled);
    }

    public void correctAnswer(){
        increaseMana();
    }
    public void wrongAnswer(){

    }

    public void hurtPlayer(){
        hurtPlayerRpc(pIndex);
    }

    [Rpc(SendTo.Everyone)]
    public void hurtPlayerRpc(int p){
        if (p == 1){
            player1.hitAmount += 1;
        }
        else if (p == 2) {
            player2.hitAmount += 1;
        }
    }

    public void changeUsername(){
        string usern = PlayerPrefs.GetString("username", "[BLANK]");
        int currentCharChoice = PlayerPrefs.GetInt("charIndex");
        //changeUsernameRpc(pIndex, usern);
        if (IsServer){
            changeUsernameRpc(1, usern);
            changeCharacterRpc(1, currentCharChoice);
        }
        else if (IsClient){
            changeUsernameRpc(2, usern);
            changeCharacterRpc(2, currentCharChoice);
        }
    }

    [Rpc(SendTo.NotMe)]
    public void changeUsernameRpc(int p, string user){
        if (p == 1){
            player1.usernameChange(user);
        }
        else if (p == 2){
            player2.usernameChange(user);
        }
    }

    [Rpc(SendTo.NotMe)]
    public void changeCharacterRpc(int p, int charChoice){
        if (p == 1){
            player1.charChange(charChoice);
        }
        else if (p == 2){
            player2.charChange(charChoice);
        }
    }

    public void exitMatch()
    {
        // Destroy(lobM.gameObject);
        AuthenticationService.Instance.SignOut();
        NetworkManager.Shutdown();
        Destroy(NetworkManager.gameObject);
        // NetworkManager networkManager = GameObject.FindObjectOfType<NetworkManager>();
        // Destroy(networkManager.gameObject);
        SceneManager.LoadScene(1);
    }

    IEnumerator WinDow(int p)
    {
        if (p == 1)
        {
            winnerText.text = player2.userName.text + " Wins";
        }
        else if (p == 2)
        {
            winnerText.text = player1.userName.text + " Wins";
        }
        if (p == pIndex){
            int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
            coinOwned += 10;
            PlayerPrefs.SetInt("coinOwned", coinOwned);
            rewardText.text = "+ 10";
        }
        else if (p != pIndex){
            int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
            coinOwned += 50;
            PlayerPrefs.SetInt("coinOwned", coinOwned);
            rewardText.text = "+ 50";
        } 
        yield return new WaitForSeconds(1f);
        winScreen.SetActive(true);
    }

    public void isJoining()
    {
        joiningText.color = Color.white;
        joiningText.text = "Joining...";
    }

    public void waitingPlayer()
    {
        joiningText.text = "Waiting for P2";
    }

    [Rpc(SendTo.NotMe)]
    public void startMatchRpc()
    {
        waitingScreen.SetActive(false);
        src.clip = battleMsc;
        src.Play();
        changeUsername();
    }

    public void startMatch()
    {
        if (NetworkManager.ConnectedClients.Count == 2)
        {
            changeUsername();
            waitingScreen.SetActive(false);
            src.clip = battleMsc;
            src.Play();
            startMatchRpc();
        }
    }

    public void changePlayer(int p)
    {
        if (p == 1)
        {
            currentPlayer = player1;
        }
        else if (p == 2)
        {
            currentPlayer = player2;
        }
        pIndex = p;
    }

    [Rpc(SendTo.Everyone)]
    public void castAnimRpc(int p){
        if (p == 1){
            player1.castAnim();
        }
        else if (p == 2){
            player2.castAnim();
        }
    }

    [Rpc(SendTo.NotMe)]
    public void increaseManaRpc(int p)
    {
        if (p == 1)
        {
            player1.changeMana(+30);
        }
        else if (p == 2)
        {
            player2.changeMana(+30);
        }
    }

    public void increaseMana()
    {
        currentPlayer.changeMana(+30);
        increaseManaRpc(pIndex);
    }

    [Rpc(SendTo.NotMe)]
    public void fireballRpc(int p)
    {
        if (p == 1)
        {
            player1.summonFireballRight();
        }
        else if (p == 2)
        {
            player2.summonFireballLeft();
        }
    }

    [Rpc(SendTo.NotMe)]
    public void lightningRpc(int p)
    {
        // GameObject lg = null;
        if (p == 1)
        {
            // lg = Instantiate(lightning, player2.transform.position, Quaternion.identity);
            player1.changeMana(-60);
        }
        else if (p == 2)
        {
            // lg = Instantiate(lightning, player1.transform.position, Quaternion.identity);
            player2.changeMana(-60);
        }
        StartCoroutine(delayThing(p));
    }

    [Rpc(SendTo.NotMe)]
    public void shieldRpc(int p)
    {
        if (p == 1)
        {
            player1.summonShield();
        }
        else if (p == 2)
        {
            player2.summonShield();
        }
    }

    [Rpc(SendTo.NotMe)]
    public void healRpc(int p)
    {
        Debug.Log("aaaaaa");
        if (p == 1)
        {
            player1.heal();
        }
        else if (p == 2)
        {
            player2.heal();
        }
    }

    public void summonFireball()
    {
        //anim.SetTrigger("C3Cast");
        castAnimRpc(pIndex);
        castFBsfxRpc(pIndex);
        timer = 0;
        StartCoroutine(FireballDelay());
        //NetworkManager.Instantiate(NetworkPrefab);
        // fireballRpc(pIndex);
        // if (currentPlayer == player1)
        // {
        //     currentPlayer.summonFireballRight();
        // }
        // else if (currentPlayer == player2)
        // {
        //     currentPlayer.summonFireballLeft();
        // }
    }

    [Rpc(SendTo.Everyone)]
    public void castFBsfxRpc(int p){
        if (p == 1){
            player1.playFireballSfx();
        }
        else if (p == 2){
            player2.playFireballSfx();
        }
    }

    [Rpc(SendTo.Everyone)]
    public void castLGsfxRpc(int p){
        if (p == 1){
            player1.playLightningSfx();
        }
        else if (p == 2){
            player2.playLightningSfx();
        }
    }

    public void summonLightning()
    {
        lightningRpc(pIndex);
        // castLGsfxRpc(pIndex);
        currentPlayer.changeMana(-60);
        timer = 0;
        // GameObject lg = null;
        // if (pIndex == 1)
        // {
        //     lg = Instantiate(lightning, player2.transform.position, Quaternion.identity);
        // }
        // else if (pIndex == 2)
        // {
        //     lg = Instantiate(lightning, player1.transform.position, Quaternion.identity);
        // }
        //anim.SetTrigger("C3Cast");
        castAnimRpc(pIndex);
        StartCoroutine(delayThing(pIndex));
    }

    IEnumerator FireballDelay(){
        yield return new WaitForSeconds(0.75f);
        fireballRpc(pIndex);
        if (currentPlayer == player1)
        {
            currentPlayer.summonFireballRight();
        }
        else if (currentPlayer == player2)
        {
            currentPlayer.summonFireballLeft();
        }
    }

    IEnumerator delayThing(int p)
    {
        GameObject lg = null;
        yield return new WaitForSeconds(0.75f);
        if (p == 1)
        {
            player1.playLightningSfx();
            lg = Instantiate(lightning, player2.transform.position, Quaternion.identity);
        }
        else if (p == 2)
        {
            player2.playLightningSfx();
            lg = Instantiate(lightning, player1.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(lg);
        if (p == 1)
        {
            player2.changeHealth(-40);
        }
        else if (p == 2)
        {
            player1.changeHealth(-40);
        }
    }

    public void shield()
    {
        timer = -5f;
        currentPlayer.summonShield();
        shieldRpc(pIndex);
    }
    public void heal()
    {
        timer = 0f;
        currentPlayer.heal();
        healRpc(pIndex);
    }
}
