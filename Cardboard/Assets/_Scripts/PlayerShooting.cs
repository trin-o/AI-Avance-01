using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatus))]
public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] Transform turretPosition;
    [SerializeField] float bulletSpeed;
    [SerializeField] float coolDown;
    float timer;


    [Header("Pooling Settings")]
    [SerializeField] int amountToPool;
    [SerializeField] GameObject objectToPool;
    List<Bullet> pooledObjects;
    Transform poolParent;

    PlayerStatus PS;

    void Awake()
    {
        PS = GetComponent<PlayerStatus>();
        poolParent = new GameObject("Bullet Parent").transform;
    }

    void Start()
    {
        pooledObjects = new List<Bullet>();
        for (int i = 0; i < amountToPool; i++)
        {
            Bullet obj = new Bullet(objectToPool, poolParent);
            pooledObjects.Add(obj);
        }
    }

    void Update()
    {
        if (GameController.GC.state == GAME_STATE.JUGANDO)
        {
            timer += Time.deltaTime;
            if (PS.state == PLAYER_STATE.CONTROLANDO) Shoot();
            UpdateBullets();
        }
    }

    void Shoot()
    {
        if (timer >= coolDown)
        {
            timer = 0;

            Bullet bullet = GetPooledObject();
            if (bullet != null)
            {
                bullet.Spawn(turretPosition, bulletSpeed);
            }
        }
    }

    Bullet GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].Active)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    void UpdateBullets()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].Update();
        }
    }


}

public class Bullet
{
    public bool Active;
    public float BulletSpeed;

    GameObject obj;
    SpriteRenderer spr;
    bool wasVisible = false;

    public Bullet(GameObject prefab, Transform parent = null)
    {
        obj = Object.Instantiate(prefab, parent);
        obj.SetActive(false);

        spr = obj.GetComponent<SpriteRenderer>();
    }

    public void Spawn(Transform origin, float speed)
    {
        obj.transform.position = origin.position;
        obj.transform.rotation = origin.rotation;

        BulletSpeed = speed;

        obj.SetActive(true);
    }

    public void Update()
    {
        Active = obj.activeInHierarchy;
        if (Active)
        {
            if (!wasVisible)
            {
                if (spr.isVisible)
                    wasVisible = true;
            }
            else
            {
                if (!spr.isVisible)
                {
                    obj.SetActive(false);
                    wasVisible = false;
                }
            }

            obj.transform.position += obj.transform.right * BulletSpeed * Time.deltaTime;
        }
    }
}
