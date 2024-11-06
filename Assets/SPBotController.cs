using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SPBotController : MonoBehaviour
{
    [SerializeField] SPGameManager gm;
    [SerializeField] GameObject fireball, shield;
    [SerializeField] Animator[] chars = new Animator[9];
    [SerializeField] Animator current_char;
    [SerializeField] Image health_bar, mana_bar;
    [SerializeField] TextMeshProUGUI health_counter, mana_counter, health_change_text, mana_change_text;
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip fbSFX, lgSFX, hlSFX, shSFX;
    [SerializeField] Transform spell_origin;

    int d20Roll;
    float time_between_attacks = 99.0f;
    public float hp = 100f;
    float mp = 10f;
    public bool player_fireball_out = false;
    bool dead = false;

    // Start is called before the first frame update
    void Start(){
        hp = 100f;
        mp = 10f;
        set_character();
    }

    // Update is called once per frame
    void Update(){
        time_between_attacks -= Time.deltaTime;
        update_bars();

        if (time_between_attacks <= 0 && !dead && !gm.player_dead){
            roll_for_attack();
        }
    }

    void update_bars() {
        health_bar.fillAmount = hp / 100f;
        mana_bar.fillAmount = mp / 100f;
        health_counter.text = hp.ToString();
        mana_counter.text = mp.ToString();
    }

    void set_character() {
        for (int i = 0; i < 9; i++) {
            chars[i].gameObject.SetActive(false);
        }
        int char_selector = Random.Range(0,9);
        chars[char_selector].gameObject.SetActive(true);
        current_char = chars[char_selector];
    }

    public void audioSetter(int state){
        if (state == 0){
            src.enabled = false;
        }
        else if (state == 1){
            src.enabled = true;
        }
    }

    void add_mana(){
        change_mana(+30);
    }

    void change_mana(int mana_change){
        mp += mana_change;
        if (mp > 100){
            mp = 100;
        }
        else if (mp < 0) {
            mp = 0;
        }
        StartCoroutine(Mana_Text(mana_change));
    }

    IEnumerator Mana_Text(int mana_change) {
        mana_change_text.text = mana_change.ToString() + " MANA";
        mana_change_text.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        mana_change_text.gameObject.SetActive(false);
    }

    void heal_spell(){
        change_mana(-40);
        change_health(+30);
        src.clip = hlSFX;
        src.Play();
    }

    public void change_health(int health_change) {
        hp += health_change;
        if (hp > 100) {
            hp = 100;
        }
        if (hp <= 0) {
            hp = 0;
            current_char.GetComponent<SpriteRenderer>().color = Color.black;
            current_char.speed = 0;
            dead = true;
            gm.death_call(2);
        }
        StartCoroutine(Health_Text(health_change));
    }

    IEnumerator Health_Text(int health_change){
        health_change_text.text = health_change.ToString() + " HP";
        health_change_text.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        health_change_text.gameObject.SetActive(false);
    }

    public void roll_for_timer(){
        d20Roll = Random.Range(1,21);
        if (d20Roll <= 5){
            time_between_attacks = 5.0f;
        }
        else if (d20Roll <= 10) {
            time_between_attacks = 3.0f;
        }
        else if (d20Roll <= 16) {
            time_between_attacks = 2.0f;
        }
        else if (d20Roll > 16) {
            time_between_attacks = 1.0f;
        }
    }

    public void shield_trigger() {
        time_between_attacks = 99f;
        StartCoroutine(shield_routine());
    }

    IEnumerator shield_routine() {
        GameObject s = Instantiate(shield, transform.position, transform.rotation);
        yield return new WaitForSeconds(5);
        Destroy(s);
        roll_for_timer();
    }

    public void lightning_trigger() {
        change_mana(-60);
        StartCoroutine(lightning_routine_p1(2));
    }

    IEnumerator lightning_routine_p1 (int p) {
        current_char.SetTrigger("Cast");
        yield return new WaitForSeconds(0.75f);
        src.clip = lgSFX;
        src.Play();
        gm.lightning_call(2);
    }

    public void fireball_trigger() {
        change_mana(-30);
        StartCoroutine(fireball_routine());
    }

    IEnumerator fireball_routine() {
        current_char.SetTrigger("Cast");
        src.clip = fbSFX;
        src.Play();
        yield return new WaitForSeconds(0.75f);
        summon_fireball();
    }

    void summon_fireball(){
        GameObject fb = Instantiate(fireball, spell_origin.position, spell_origin.rotation);
        Rigidbody2D rb = fb.GetComponent<Rigidbody2D>();
        rb.rotation = 180f;
        rb.AddForce(spell_origin.right * -10f, ForceMode2D.Impulse);
        change_mana(-20);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Fireball") {
            change_health(-30);
        }
    }

    void roll_for_attack() {

        roll_for_timer();

        if (mp < 10) {
            add_mana();
        }

        else if (mp < 20) {
            if (player_fireball_out) {
                shield_trigger();
            }
            else if (!player_fireball_out) {
                add_mana();
            }
        }

        else if (mp < 40) {
            if (player_fireball_out) {
                shield_trigger();
            }
            else if (!player_fireball_out) {
                d20Roll = Random.Range(1, 21);
                if (d20Roll > 10) {
                    fireball_trigger();
                }
                else if (d20Roll <= 10) {
                    add_mana();
                }
            }
        }

        else if (mp < 60) {
            if (player_fireball_out) {
                shield_trigger();
            }
            else if (!player_fireball_out) {
                if (hp <= 60) {
                    heal_spell();
                }
                else if (hp > 60) {
                    d20Roll = Random.Range(1, 21);
                    if (d20Roll > 10) {
                        fireball_trigger();
                    }
                    else if (d20Roll <= 10) {
                        add_mana();
                    }
                }
            }
        }

        else if (mp >= 60) {
            if (player_fireball_out) {
                shield_trigger();
            }
            else if (!player_fireball_out) {
                if (hp <= 60) {
                    heal_spell();
                }
                else if (hp > 60) {
                    d20Roll = Random.Range(1, 21);
                    if (d20Roll > 10) {
                        fireball_trigger();
                    }
                    else if (d20Roll <= 10) {
                        lightning_trigger();
                    }
                }
            }
        }

    }
}
