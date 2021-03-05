using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Player : MonoBehaviour, IDamageable
{
    [HideInInspector]
    public static Player instance;

    public int health;
    public float speed;

    public GameObject shield;

    public Image[] healthCount;
    public Sprite healthOn, healthOff;

    public BoxCollider collider;

    public GameObject leftPose, rightPose;

    private Animator anim;
    private Coroutine coroutine;
    private Vector3 pos;

    [HideInInspector]
    public int money = 0;

    public float immortalTime;
    private bool immortality = false;

    private void Awake()
    {
        CheckPlayerPrefs();
        anim = GetComponent<Animator>();
        instance = this;
        pos = transform.position;
    }

    public void Move(Direction dir)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Moving(dir));
    }

    public void AcceptDamage()
    {
        if (!immortality)
        {
            SetImmortality();
            health -= 1;
            if (health <= 0)
            {
                health = 0;
                Death();
            }
            healthCount[health].sprite = healthOff;
            PlayerPrefs.SetInt("Hearts", health);
        }
    }

    private IEnumerator Moving(Direction dir)
    {

        if (dir.Equals(Direction.Right))
        {
            if (transform.position.x == 1 || pos.x == 1)
                yield break;
            else
                pos.x += 1;  
        }
        else
        {
            if (transform.position.x == -1 || pos.x == -1)
                yield break;
            else
                pos.x -= 1;
        }

        if (dir.Equals(Direction.Right))
            anim.SetBool("MoveRight", true);
        else
            anim.SetBool("MoveLeft", true);

        while (Vector3.Distance(transform.position, pos) > 0.04f)
        {
            Vector3 movement = dir.Equals(Direction.Right) ? new Vector3(speed, 0, 0) : new Vector3(-speed, 0, 0);
            transform.position += movement * Time.deltaTime;

            if (transform.position.x > 1 || transform.position.x < -1)
                break;

            yield return null;
        }
        transform.position = pos;

        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
    }


    //pickups
    public void ActivateShield()
    {
        if(!shield.activeSelf)
            shield.SetActive(true);
    }

    public void IncreaseHP()
    {
        if (health < 3)
        {
            healthCount[health].sprite = healthOn;
            health += 1;
            PlayerPrefs.SetInt("Hearts", health);
        }
    }

    public void IncreaseMoney(int money)
    {
        this.money += money;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 1);
    }


    //animations setting
    public void StartMoving()
    {
        anim.SetBool("StopMove", false);
        anim.SetBool("StartMove", true);
    }

    public void StopMove()
    {
        anim.SetBool("StartMove", false);
        anim.SetBool("StopMove", true);
    }

    public void SetLeftPose()
    {
        anim.SetBool("SetLeftPose", true);
        leftPose.SetActive(true);
        collider.size = new Vector3(0.4f, 1.26f, 0.3f);
        collider.center = new Vector3(0, 0.6f, 0);
    }

    public void SetRightPose()
    {
        anim.SetBool("SetRightPose", true);
        rightPose.SetActive(true);
        collider.size = new Vector3(0.4f, 1.26f, 0.3f);
        collider.center = new Vector3(0, 0.6f, 0);
    }

    public void SetEndPose()
    {
        anim.SetBool("SetRightPose", false);
        anim.SetBool("SetLeftPose", false);
        anim.SetBool("EndPose", true);

        collider.size = new Vector3(0.4f, 1.86f, 0.3f);
        collider.center = new Vector3(0, 0.93f, 0);

        if (leftPose.activeSelf) leftPose.SetActive(false);
        if (rightPose.activeSelf) rightPose.SetActive(false);
        StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate ()
        {
            anim.SetBool("EndPose", false);
        }));
    }

    public void Jump()
    {
        anim.SetBool("Jump", true);
        collider.center += new Vector3(0, 1, 0);
        StartCoroutine(CoroutineHelper.WaitFor(0.5f, delegate ()
        {
            anim.SetBool("Jump", false);
        }));
        StartCoroutine(CoroutineHelper.WaitFor(1, delegate ()
        {
            collider.center -= new Vector3(0, 1, 0);
        }));
    }

    public void Death()
    {
        GameController.instance.GameOver();
        StopMove();
    }

    //other setting
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole"))
        {
            health = 0;
            PlayerPrefs.SetInt("Hearts", 0);
            foreach(var c in healthCount)
            {
                c.sprite = healthOff;
            }

            StartCoroutine(Falling());
            Death();
        }
        if (other.CompareTag("Bridge"))
        {
            StopMove();
            StartCoroutine(CoroutineHelper.WaitFor(0.58f, delegate () 
            {
                Camera.instance.SetFinish();
                LevelGenerator.instance.StopRoad();
                IslandGenerator.instance.StopIslands();
                GameController.instance.ActivateCastles();
                SwipeManager.instance.SetFinish(true);
            }));
        }

        if (other.CompareTag("Finish"))
        {
            SetEndPose();
            StartCoroutine(CoroutineHelper.WaitFor(0.1f, delegate (){
                GameController.instance.DestroyCastle();
            }));
        }                   
    }

    private IEnumerator Falling()
    {
        while(transform.position.y > -3)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            yield return null;
        }
    }

    public void SetImmortality()
    {
        immortality = true;
        StartCoroutine(CoroutineHelper.WaitFor(0.8f, delegate ()
        {
            immortality = false;
        }));
    }

    private void CheckPlayerPrefs()
    {
        health = PlayerPrefs.GetInt("Hearts");
        for(int i = 0; i < health; i++)
        {
            healthCount[i].sprite = healthOn;
        }
        if (health == 0)
        {
            health = 1;
            healthCount[0].sprite = healthOn;
        }


    }
}


public interface IDamageable
{
    public void AcceptDamage();
}