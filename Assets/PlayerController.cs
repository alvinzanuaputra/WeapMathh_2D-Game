using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject shield;

    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;
    [SerializeField] TextMeshProUGUI healthCounter;
    [SerializeField] TextMeshProUGUI manaCounter;
    [SerializeField] public TextMeshProUGUI userName;
    [SerializeField] Animator char1;
    [SerializeField] Animator char2;
    [SerializeField] Animator char3;
    [SerializeField] Animator char4;
    [SerializeField] Animator char5;
    [SerializeField] Animator char6;
    [SerializeField] Animator char7;
    [SerializeField] Animator char8;
    [SerializeField] Animator char9;
    [SerializeField] Animator currentChar;
    [SerializeField] TextMeshProUGUI manaText, hpText;

    [SerializeField] AudioSource src;
    [SerializeField] AudioClip fbSFX, lgSFX, hlSFX, shSFX;

    public Transform spellOrigin;
    public float playerHealth;
    public float playerMana;
    public int hitAmount;
    private float hitTimer;

    // Start is called before the first frame update
    void Start()
    {
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
        healthBar.fillAmount = playerHealth / 100f;
        manaBar.fillAmount = playerMana / 100f;
        healthCounter.text = playerHealth.ToString();
        manaCounter.text = playerMana.ToString();
        checkForHurt();
    }

    private void checkForHurt(){
        if (hitAmount > 0){
            hitTimer += Time.deltaTime;
        }
        else if (hitAmount == 0) {
            hitTimer = 0;
        }
        if (hitAmount == 2) {
            changeHealth(-30);
            hitAmount = 0;
        }
        if (hitTimer >= 1){
            hitAmount = 0;
        }
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
        char1.gameObject.SetActive(false);
        char2.gameObject.SetActive(false);
        char3.gameObject.SetActive(false);
        char4.gameObject.SetActive(false);
        char5.gameObject.SetActive(false);
        char6.gameObject.SetActive(false);
        char7.gameObject.SetActive(false);
        char8.gameObject.SetActive(false);
        char9.gameObject.SetActive(false);
        switch(cIndex){
            case 0:
                currentChar = char1;
                break;
            case 1:
                currentChar = char2;
                break;
            case 2:
                currentChar = char3;
                break;
            case 3:
                currentChar = char4;
                break;
            case 4:
                currentChar = char5;
                break;
            case 5:
                currentChar = char6;
                break;
            case 6:
                currentChar = char7;
                break;
            case 7:
                currentChar = char8;
                break;
            case 8:
                currentChar = char9;
                break;
            default:
                break;
        }
        // test
        // currentChar = char3;
        //currentChar.GetComponent<GameObject>().SetActive(true);
        currentChar.gameObject.SetActive(true);
    }

    public void usernameChange(string user){
        userName.text = user;
    }

    public void changeMana(int manaChange)
    {
        playerMana += manaChange;
        if (playerMana < 0)
        {
            playerMana = 0;
        }
        if (playerMana > 100)
        {
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
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        if (playerHealth > 100)
        {
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

    public void heal()
    {
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

    public void summonFireballRight()
    {
        GameObject fb = Instantiate(fireball, spellOrigin.position, spellOrigin.rotation);
        Rigidbody2D rb = fb.GetComponent<Rigidbody2D>();
        rb.AddForce(spellOrigin.right * 10f, ForceMode2D.Impulse);
        changeMana(-20);
    }

    public void summonFireballLeft()
    {
        GameObject fb = Instantiate(fireball, spellOrigin.position, spellOrigin.rotation);
        Rigidbody2D rb = fb.GetComponent<Rigidbody2D>();
        rb.rotation = 180f;
        rb.AddForce(spellOrigin.right * -10f, ForceMode2D.Impulse);
        changeMana(-20);
    }

    public void summonShield()
    {
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
            hitAmount += 1;
            gm.hurtPlayer();
        }
    }
}
