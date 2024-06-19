using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
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
    [SerializeField] GameObject lobM;
    [SerializeField] Animator anim;

    public PlayerController currentPlayer;
    public int pIndex;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayer = player1;
        pIndex = 1;
        timer = 0;
    }

    // Update is called once per frame
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
        if (currentPlayer.playerMana >= 40 || timer >= 0.5f)
        {
            ui.enableButton(4);
        }
        else if (currentPlayer.playerMana < 40 || timer >= 0.5f)
        {
            ui.disableButton(4);
        }
        ui.setFill(timer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log(NetworkManager.ConnectedClients.Count);
            // anim.SetTrigger("C3Cast");
        }

        if (player1.playerHealth <= 0)
        {
            player1.GetComponent<SpriteRenderer>().color = Color.black;
            ui.offOptions();
            StartCoroutine(WinDow(1));
        }
        if (player2.playerHealth <= 0)
        {
            player2.GetComponent<SpriteRenderer>().color = Color.black;
            ui.offOptions();
            StartCoroutine(WinDow(2));
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
        SceneManager.LoadScene(0);
    }

    IEnumerator WinDow(int p)
    {
        if (p == 1)
        {
            winnerText.text = "P2 Wins";
        }
        else if (p == 2)
        {
            winnerText.text = "P1 Wins";
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
        changeUsername();
    }

    public void startMatch()
    {
        if (NetworkManager.ConnectedClients.Count == 2)
        {
            changeUsername();
            waitingScreen.SetActive(false);
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

    public void hurtEnemy()
    {
        /*if(player.playerMana >= 20)
        {
            enemyHealth -= 20;
            player.decreaseMana();
        }*/
    }

    [Rpc(SendTo.NotMe)]
    public void increaseManaRpc(int p)
    {
        if (p == 1)
        {
            player1.changeMana(30);
        }
        else if (p == 2)
        {
            player2.changeMana(30);
        }
    }

    public void increaseMana()
    {
        currentPlayer.changeMana(30);
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
    public void summonLightning()
    {
        lightningRpc(pIndex);
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
        yield return new WaitForSeconds(0.7f);
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
        yield return new WaitForSeconds(0.7f);
        if (p == 1)
        {
            lg = Instantiate(lightning, player2.transform.position, Quaternion.identity);
        }
        else if (p == 2)
        {
            lg = Instantiate(lightning, player1.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(lg);
        if (p == 1)
        {
            player2.playerHealth -= 40;
        }
        else if (p == 2)
        {
            player1.playerHealth -= 40;
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
        currentPlayer.heal();
        healRpc(pIndex);
    }
}
