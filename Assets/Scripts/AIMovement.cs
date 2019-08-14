using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AIMovement : MonoBehaviour
{
    public enum State
    {
        MeleeAttack,        
        RangeAttack,
        Movement,
        MovementBack,
        Ulti,
        HeadButt
    }


    public Transform TargetHeadbutt;
    public GameObject ulti;
    public GameObject projectile;
    public Transform point;
    public Collider2D ColliderAttack;
    public int health;
    public Animator animator;
    public float speed;                         //speed AI
    public Transform target;                    //target AI
    public Vector3 distance;                    //jarak antara AI dan Target
    public Vector3 startPosition;               //Posisi awal dia berada
    public Vector3 tempTransform;               //temporary Transform untuk menampung variable saja
    public float randomize;                     //variable untuk menampung hasil random
    public State state;                         //untuk mengetahui state apa AI tersebut
    public float timer;
    public float timer1;
    public float timerStartGame;
    public float ultitimerchanneling;
    public float cooldownulti;
    public float WaitTimer;
    [Header("Melee Attack Combo")]
    public int maxCombo;
    public int percentage;                      //kemungkinan combo = percentage-1/percentage
    public int currentMaxCombo;
    public int checkCombo;
    bool canAttack;
    public Image hpbar;
    public AudioSource suaraNyerang;
    public GameObject ShieldUlti;

    public GameObject load;
    public string sceneName;

    void Start()
    {
        timerStartGame = 2f;
        state = State.Movement;
        health = 100;
        startPosition = transform.position;     //memasukkan startposition ke transform.position
        timer = 2f;                             //memasukkan timer menjadi 1f
        timer1 = 0.5f;
        ultitimerchanneling = 6f;
        cooldownulti = 3f;
        currentMaxCombo = maxCombo;
        canAttack = true;
    }


    void Update()
    {
        hpbar.fillAmount = health / 100f; 
    }


    void FixedUpdate()
    {
        if(health <= 0)
        {
            load.SetActive(true);
            StartCoroutine(BossDead());
        }

        timerStartGame -= Time.fixedDeltaTime;
        if (timerStartGame <= 0f)
        {
            CheckBehaviour();
        }
        
    }

    IEnumerator BossDead()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
    }

    void CheckBehaviour()
    {
        
            if (state == State.Movement) //cek kalo state nya bukan state movement back maupun range attack
            {
                if (CheckMovement())    //jalanin checkmovement liat apakah dia return true atau false, kalo true dia state jadi meleeattack kalo false jadi movement
                {
                    state = State.MeleeAttack;
                }
                else if (!CheckMovement())
                {
                    state = State.Movement;
                }
            }else if(state == State.Ulti)
            {
                 Ulti();
            }

            AIBehaviour();  //menjalankan aibehaviour
        
        
    }

    bool CheckMovement() 
    {
        distance = target.position - transform.position; //check jarak antara target dan AI
        if(Mathf.Abs(distance.x) <= 2f)  //check apakah distance nya kurang dari 2f, kalo iya true, kalo nggak false
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MovementBack()
    {
        if(state != State.HeadButt)
        {
            if (checkCombo == 0 || currentMaxCombo == 0)
            {
                currentMaxCombo = maxCombo;
                state = State.MovementBack; //harusnya dipanggil pas event nya kelar pas frame terakhir animasi serang
            }
            else
            {
                canAttack = true;
                currentMaxCombo--;
            }
        }
        else
        {
            state = State.MovementBack;
        }
       
    }

    void AIBehaviour()
    {

        if (state == State.Movement) //kalo statenya movement dia ke arah target
        {
           
                animator.Play("Walk");
                tempTransform = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
                transform.position = new Vector3(tempTransform.x, transform.position.y, transform.position.z);
            
        }
        else if (state == State.MeleeAttack)
        {
            if (canAttack == true)
            {
                animator.Play("Idle");
                timer1 -= Time.fixedDeltaTime;
                if (timer1 <= 0f)
                {
                    timer1 = 0.5f;
                    checkCombo = Random.Range(0, percentage);
                    ColliderAttack.enabled = true;
                    canAttack = false;
                    animator.Play("Attack");
                }
            }
            //animator.SetInteger("isAttacking", 1);
            //pake event dan animator ga usah diperhatiin

        }
        else if (state == State.MovementBack) //kalo statenya movement back dia balik ke tempat aasal
        {
            animator.Play("Walk");
            
            canAttack = true;
            ColliderAttack.enabled = false;
            //SetInteger("isAttacking", 2);
            tempTransform = Vector3.MoveTowards(transform.position, startPosition, speed * Time.fixedDeltaTime);
            transform.position = new Vector3(tempTransform.x, transform.position.y, transform.position.z);
            if (Mathf.Abs(transform.position.x - startPosition.x) < 0.2f) //kalo udah sampe tempat asal dia random, kalo dibawah 50 dia movement kalo diatas dia range attack
            {
                if(health > 30)
                {
                    randomize = Random.Range(0f, 180f);
                    if (randomize < 60f)
                    {
                        state = State.Movement;
                    }
                    else if(randomize > 60f && randomize < 120f)
                    {
                        animator.Play("RangedAttackReal");
                        state = State.RangeAttack;
                    }else{
                        animator.Play("Backward");
                        state = State.HeadButt;
                    }
                }
                else
                {
                    state = State.Ulti;
                }
            }
        }
        else if (state == State.RangeAttack)
        {
        //pake event dan animator ga usah diperhatiin
                
            timer -= Time.fixedDeltaTime;
            if (timer <= 0f)
            {
                timer = 2f;
                if(health > 30)
                {
                    randomize = Random.Range(0f, 180f);
                    if (randomize < 60f)
                    {
                        state = State.Movement;
                    }
                    else if (randomize > 60f && randomize < 120f)
                    {
                        animator.Play("RangedAttackReal");
                        state = State.RangeAttack;
                    }
                    else
                    {
                        state = State.HeadButt;
                    }

                }
                else
                {
                    state = State.Ulti;
                }
            }
        }else if(state == State.HeadButt)
        {
            animator.Play("Backward");
            tempTransform = Vector2.MoveTowards(transform.position, TargetHeadbutt.position, speed * 2.5f * Time.fixedDeltaTime);
            transform.position = new Vector3(tempTransform.x, transform.position.y, transform.position.z);
        }

    }
    
        void Ulti()
        {
            if (ultitimerchanneling > 0f)
            {
                animator.Play("RangedAttack");
                ShieldUlti.SetActive(true);
                ulti.SetActive(true);
                ultitimerchanneling -= Time.fixedDeltaTime;
            }
            else
            {
                ShieldUlti.SetActive(false);
                ulti.SetActive(false);
                animator.Play("Idle");
                cooldownulti -= Time.fixedDeltaTime;
                if (cooldownulti <= 0f)
                {
                    cooldownulti = 3f;
                    ultitimerchanneling = 6f;
                }
            }
            
           
        }

        void suaraProjectile()
        {
        suaraNyerang.Play();
        }

        void SpawnProjectile()
        {
            Instantiate(projectile, point.position, Quaternion.identity);
            animator.Play("Idle");
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Attack"))
            {
                health -= 2;
            }
        }

  
    }
