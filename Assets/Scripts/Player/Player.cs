using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IDamageable
{
    [HideInInspector]
    public static Player instance;

    public int health;
    public float speed;

    public GameObject shield;

    private Animator anim;
    private Coroutine coroutine;
    private Vector3 pos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        instance = this;
        pos = transform.position;
    }

    private void Update()
    {
        if(health <= 0)
        {
            RoadGenerator.instance.StopRoad();
        }
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
        health -= 1;
    }

    private IEnumerator Moving(Direction dir)
    {

        if (dir.Equals(Direction.Right))
        {
            if (transform.position.x == 1 || pos.x == 1)
                yield break;
            else
                pos += Vector3.right;  
        }
        else
        {
            if (transform.position.x == -1 || pos.x == -1)
                yield break;
            else
                pos -= Vector3.right;
        }

        Debug.Log(pos);

        while (Vector3.Distance(transform.position, pos) > 0.01f)
        {
            Vector3 movement = dir.Equals(Direction.Right) ? new Vector3(speed, 0, 0) : new Vector3(-speed, 0, 0);
            transform.position += movement * Time.deltaTime;
            yield return null;
        }
        transform.position = pos;
    }

    public void ActivateShield()
    {
        if(!shield.activeSelf)
            shield.SetActive(true);
    }

    public void StartMoving()
    {
        anim.enabled = true;
    }
}


public interface IDamageable
{
    public void AcceptDamage();
}