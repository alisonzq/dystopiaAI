using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource pickupSoundEffect;
    [SerializeField] private AudioSource sawSoundEffect;

    [SerializeField] private GameObject lifeBar;    //lifebar
    private float lifeBarMaxWidth;    //lifebar maximum size
    private float lifeBarMaxScale;
    private float lifeBarMaxPosition;
    [SerializeField] private int maxLife; //max life
    [SerializeField] private float invincibilityTime;
    [SerializeField] private int numberOfFlashes;
    private int life;
    
    private GameObject checkpoint;
    private GameObject healthPickup;
    private SpriteRenderer sprite;

    private bool respawning = false; //to prevent multi-hit from ground hazards
    private bool invincible = false; //to prevent multi-hit from hazards
    
    private bool groundHazardTouching = false;
    private bool hazardTouching = false;

    public Text[] consumables;
    public int[] consumablesValue;
    private int consumablesLength;

    private string lastLoadedScene;

    public bool askedRespawn = false;

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        life = maxLife;
        lifeBarMaxWidth = lifeBar.GetComponent<RectTransform>().rect.width;
        lifeBarMaxScale = lifeBar.transform.localScale.x;
        lifeBar.SetActive(true);
    }

    //Triggers
    private void OnTriggerEnter2D(Collider2D collision) {


        //checkpoint
        if (collision.gameObject.CompareTag("CheckPoint") && life != 0) {
            if (checkpoint != collision.gameObject) {
                checkpoint = collision.gameObject;
                collision.gameObject.GetComponent<Animator>().SetTrigger("deployCheckPoint");
            }
        }

        //life pickup
        if (collision.gameObject.CompareTag("HealthPickup") && life != maxLife) {
            healthPickup = collision.gameObject;

            //increase
            life++;
            lifeBar.GetComponent<RectTransform>().anchoredPosition = lifeBar.GetComponent<RectTransform>().anchoredPosition + new Vector2(lifeBarMaxWidth / (maxLife), 0f);
            lifeBar.transform.localScale = lifeBar.transform.localScale + new Vector3(lifeBarMaxScale / (maxLife), 0f, 0f);
       

            //physical dissapearance
            healthPickup.GetComponent<Animator>().SetTrigger("picked");
            healthPickup.GetComponent<CircleCollider2D>().enabled = false;
            healthPickup.GetComponent<CapsuleCollider2D>().enabled = false;
            healthPickup.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            pickupSoundEffect.Play();
        }

     

       //ground hazard trigger
       else if (collision.gameObject.CompareTag("GroundHazard") && !respawning) {
            groundHazardTouching = true;
        } 
        //hazard trigger
        else if (collision.gameObject.CompareTag("Hazard")) {
            hazardTouching = true;
        }
    }

    //Triggers exit
    private void OnTriggerExit2D(Collider2D collision) {
        //ground hazard trigger
        if (collision.gameObject.CompareTag("GroundHazard")) {
            groundHazardTouching = false;
        }
        //hazard trigger
        else if (collision.gameObject.CompareTag("Hazard")) {
            hazardTouching = false;
        }
    }


    //definitive death
    private void DefinitiveDeath() {
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
        lifeBar.SetActive(false);
        StartCoroutine(DeathScreen());
    }

    private void Update() {

        if (askedRespawn) {
            StartCoroutine(Respawn());
            askedRespawn = false;
        }

        //eating consumables
        if (Input.GetKeyDown(KeyCode.H)) {
            for (int consumablesIndex = 0; consumablesIndex < consumablesLength+1; consumablesIndex++) {
                if (int.Parse(consumables[consumablesIndex].text) > 0 && life < maxLife + 1 - consumablesValue[consumablesIndex]) {
                    lifeBar.GetComponent<RectTransform>().anchoredPosition = lifeBar.GetComponent<RectTransform>().anchoredPosition + consumablesValue[consumablesIndex]*(new Vector2(lifeBarMaxWidth / (maxLife), 0f));
                    lifeBar.transform.localScale = lifeBar.transform.localScale + consumablesValue[consumablesIndex] * (new Vector3(lifeBarMaxScale / (maxLife), 0f, 0f));
                    life = life + consumablesValue[consumablesIndex];
                    Debug.Log("Gus");
                    consumables[consumablesIndex].text = "" + (int.Parse(consumables[consumablesIndex].text) - 1);
                } else if (int.Parse(consumables[consumablesIndex].text) > 0 && life == maxLife) {
                        
                } else if (int.Parse(consumables[consumablesIndex].text) > 0 && life >= maxLife + 1 - consumablesValue[consumablesIndex]) {
                    int ammountToHeal = maxLife - life;
                    lifeBar.GetComponent<RectTransform>().anchoredPosition = lifeBar.GetComponent<RectTransform>().anchoredPosition + ammountToHeal * (new Vector2(lifeBarMaxWidth / (maxLife), 0f));
                    lifeBar.transform.localScale = lifeBar.transform.localScale + ammountToHeal * (new Vector3(lifeBarMaxScale / (maxLife), 0f, 0f));
                    life = maxLife;
                    Debug.Log("sus");
                    consumables[consumablesIndex].text = "" + (int.Parse(consumables[consumablesIndex].text) - 1);
                }

            }
        }

        if (groundHazardTouching == true && respawning == false) {
            groundHazardTouching = false;
            lifeBar.GetComponent<RectTransform>().anchoredPosition = lifeBar.GetComponent<RectTransform>().anchoredPosition - new Vector2(lifeBarMaxWidth / (maxLife), 0f);
            lifeBar.transform.localScale = lifeBar.transform.localScale - new Vector3(lifeBarMaxScale / (maxLife), 0f, 0f);
            if (life > 1) {
                life--;
                Die();
            } else if (life <= 1) {
                DefinitiveDeath();
            }
        }

        if (hazardTouching == true && invincible == false) {
            lifeBar.GetComponent<RectTransform>().anchoredPosition = lifeBar.GetComponent<RectTransform>().anchoredPosition - new Vector2(lifeBarMaxWidth / (maxLife), 0f);
            lifeBar.transform.localScale = lifeBar.transform.localScale - new Vector3(lifeBarMaxScale / (maxLife), 0f, 0f);
            if (life > 1) {
                life--;
                sawSoundEffect.Play();
                StartCoroutine(invincibilityFrames());
                Debug.Log("" + life);

            } else if (life <= 1) {
                DefinitiveDeath();
            }
        }

    }

    private IEnumerator invincibilityFrames() {
        invincible = true;
        for (int f = 0; f < numberOfFlashes; f++) {

            for (int i = 0; i < 10; i++) {
                sprite.color = sprite.color - new Color(0f, 0f, 0f, 0.1f);
                yield return new WaitForSeconds(0.0001f);
            }


            for (int l = 0; l < 10; l++) {
                sprite.color = sprite.color + new Color(0f, 0f, 0f, 0.1f);
                yield return new WaitForSeconds(0.0001f);
            }

        }
        
       
        invincible = false;
    }

    private void Die() {
        anim.SetTrigger("loseLife");
        rb.bodyType = RigidbodyType2D.Static;
        deathSoundEffect.Play();
        StartCoroutine(Respawn());
    }


    private IEnumerator Respawn() {
        respawning = true;
        yield return new WaitForSeconds(0.4f);
        transform.position = checkpoint.transform.position; 
        anim.SetTrigger("respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        respawning = false;
        anim.SetTrigger("hitable");
    }

    private IEnumerator DeathScreen() {

    yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
        lifeBar.SetActive(true);

    }


}
