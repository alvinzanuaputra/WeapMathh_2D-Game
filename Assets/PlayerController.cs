using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject shield;

    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;
    [SerializeField] TextMeshProUGUI healthCounter;
    [SerializeField] TextMeshProUGUI manaCounter;
    [SerializeField] TextMeshProUGUI userName;
    [SerializeField] Animator char1;
    [SerializeField] Animator char2;
    [SerializeField] Animator char3;
    [SerializeField] Animator currentChar;

    public Transform spellOrigin;
    public float playerHealth;
    public float playerMana;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void charChange(int cIndex){
        char1.gameObject.SetActive(false);
        char2.gameObject.SetActive(false);
        char3.gameObject.SetActive(false);
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
            default:
                break;
        }
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
    }

    public void heal()
    {
        playerHealth += 30;
        changeMana(-40);
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
            playerHealth -= 30;
        }
    }
}
