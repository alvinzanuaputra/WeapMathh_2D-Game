using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SPPlayerController : MonoBehaviour
{
    [SerializeField] SPGameManager gm;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject shield;

    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;
    [SerializeField] TextMeshProUGUI healthCounter;
    [SerializeField] TextMeshProUGUI manaCounter;
    [SerializeField] public TextMeshProUGUI userName;
    [SerializeField] Animator[] chars = new Animator[9];
    [SerializeField] Animator currentChar;
    [SerializeField] TextMeshProUGUI manaText, hpText;
    [SerializeField] SPUIController ui;

    [SerializeField] AudioSource src;
    [SerializeField] AudioClip fbSFX, lgSFX, hlSFX, shSFX;

    public Transform spellOrigin;
    public float playerHealth;
    public float playerMana;
    public int hitAmount;
    private float hitTimer;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        hitTimer = 0;
        hitAmount = 0;
        userName.text = PlayerPrefs.GetString("username", "[BLANK]");
        playerHealth = 100f;
        playerMana = 10f;
        charChange(PlayerPrefs.GetInt("charIndex", 0));
        //char1.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        update_bars();
        update_cooldown();
    }

    void update_cooldown() {
        timer += Time.deltaTime;
        if (playerMana >= 20 && timer >= 1f){
            ui.enableButton(1);
        }
        else if (playerMana < 20 || timer < 1f){
            ui.disableButton(1);
        }

        // Lightning
        if (playerMana >= 60 && timer >= 2f){
            ui.enableButton(2);
        }
        else if (playerMana < 60 || timer < 2f){
            ui.disableButton(2);
        }

        // Shield
        if (playerMana >= 10 && timer >= 1f){
            ui.enableButton(3);
        }
        else if (playerMana < 10 || timer < 1f){
            ui.disableButton(3);
        }

        // Heal
        if (playerMana >= 40 && timer >= 2f){
            ui.enableButton(4);
        }
        else if (playerMana < 40 || timer < 2f){
            ui.disableButton(4);
        }
        ui.setFill(timer);
    }

    void update_bars() {
        healthBar.fillAmount = playerHealth / 100f;
        manaBar.fillAmount = playerMana / 100f;
        healthCounter.text = playerHealth.ToString();
        manaCounter.text = playerMana.ToString();
    }

    public void audioSetter(int state){
        if (state == 0){
            src.enabled = false;
        }
        else if (state == 1){
            src.enabled = true;
        }
    }

    public void charChange(int cIndex){
        for (int i = 0; i < 9; i++){
            chars[i].gameObject.SetActive(false);
        }
        currentChar = chars[cIndex];
        currentChar.gameObject.SetActive(true);
    }

    public void usernameChange(string user){
        userName.text = user;
    }

    public void changeMana(int manaChange)
    {
        playerMana += manaChange;
        if (playerMana < 0){
            playerMana = 0;
        }
        if (playerMana > 100){
            playerMana = 100;
        }
        StartCoroutine(ManaT(manaChange));
    }

    IEnumerator ManaT(int manaC){
        manaText.text = manaC.ToString() + " MANA";
        manaText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        manaText.gameObject.SetActive(false);
    }

    public void changeHealth(int healthChange){
        playerHealth += healthChange;
        if (playerHealth < 0){
            playerHealth = 0;
            gm.death_call(1);
            gm.player_dead = true;
            currentChar.GetComponent<SpriteRenderer>().color = Color.black;
            currentChar.speed = 0;
        }
        if (playerHealth > 100){
            playerHealth = 100;
        }
        StartCoroutine(HealthT(healthChange));
    }

    IEnumerator HealthT(int healthC){
        hpText.text = healthC.ToString() + " HP";
        hpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        hpText.gameObject.SetActive(false);
    }

    public void heal(){
        timer = 0f;
        changeHealth(+30);
        changeMana(-40);
        src.clip = hlSFX;
        src.Play();
    }

    public void playFireballSfx(){
        src.clip = fbSFX;
        src.Play();
    }

    public void playLightningSfx(){
        src.clip = lgSFX;
        src.Play();
    }

    public void lightning_trigger() {
        timer = 0f;
        changeMana(-60);
        StartCoroutine(lightning_routine_p1());
    }

    IEnumerator lightning_routine_p1() {
        castAnim();
        playLightningSfx();
        yield return new WaitForSeconds(0.75f);
        //spawn lightning
        gm.lightning_call(1);
    }

    public void fireball_trigger() {
        timer = 0f;
        changeMana(-30);
        StartCoroutine(fireball_routine());
    }

    IEnumerator fireball_routine() {
        castAnim();
        playFireballSfx();
        gm.player_fb_out();
        yield return new WaitForSeconds(0.75f);
        summonFireball();
    }

    public void summonFireball()
    {
        GameObject fb = Instantiate(fireball, spellOrigin.position, spellOrigin.rotation);
        Rigidbody2D rb = fb.GetComponent<Rigidbody2D>();
        rb.AddForce(spellOrigin.right * 10f, ForceMode2D.Impulse);
    }

    public void summonShield()
    {
        ui.change_answer_cd(-1f);
        timer = -5f;
        changeMana(-10);
        src.clip = shSFX;
        src.Play();
        StartCoroutine(ShieldSummon());
    }

    IEnumerator ShieldSummon()
    {
        GameObject s = Instantiate(shield, transform.position, transform.rotation);
        yield return new WaitForSeconds(5);
        Destroy(s);
    }

    public void castAnim(){
        currentChar.SetTrigger("Cast");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            changeHealth(-30);
        }
    }
}
