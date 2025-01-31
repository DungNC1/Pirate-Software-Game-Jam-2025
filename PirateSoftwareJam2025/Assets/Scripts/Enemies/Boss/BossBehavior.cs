using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private float randomMoveTimer;

    [Header("Bullet Attributes")]
    public GameObject bullet;
    [SerializeField] private float fireRate;

    [Header("Boss Moves")]
    [SerializeField] private float minRandomMoveTime;
    [SerializeField] private float maxRandomMoveTime;

    [Header("Spawn Minions Move")]
    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private int minionSpawnAmount;

    [Header("Look Toward Player")]
    private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float rotationModifier;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        DirectPatern();
    }

    private void Update()
    {
        float randomMoveTime = Random.Range(minRandomMoveTime, maxRandomMoveTime);

        int randomMove = Random.Range(0, 6);

        if (randomMoveTimer <= 0)
        {
            if (randomMove == 0)
            {
                CirclePatern();
            }
            else if (randomMove == 1)
            {
                RandomPatern();
            }
            else if (randomMove == 2)
            {
                Teleport();
            }
            else if (randomMove == 3)
            {
                SpinningPatern();
            }
            else if (randomMove == 4)
            {
                SpawnMinion();
            }
            else if (randomMove == 5)
            {
                DirectPatern();
            }

            randomMoveTimer = randomMoveTime;
        }
        else
        {
            randomMoveTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }

    }

    private void CirclePatern()
    {
        Boss.instance.angleSpread = 359f;
        Boss.instance.oscillate = false;
        Boss.instance.stagger = false;
        Boss.instance.projectilesPerBurst = 30;
        Boss.instance.Attack();
    }

    private void RandomPatern()
    {
        Boss.instance.angleSpread = 60f;
        Boss.instance.oscillate = true;
        Boss.instance.stagger = true;
        Boss.instance.projectilesPerBurst = 10;
        Boss.instance.Attack();
    }

    private void Teleport()
    {
        transform.position = new Vector3(Random.Range(-11, 11), Random.Range(4, -4f));
    }

    private void SpinningPatern()
    {
        Boss.instance.angleSpread = 359f;
        Boss.instance.oscillate = true;
        Boss.instance.stagger = true;
        Boss.instance.projectilesPerBurst = 20;
        Boss.instance.Attack();
    }

    private void SpawnMinion()
    {
        for (int i = 0; i < minionSpawnAmount; i++)
        {
            Vector3 postion = transform.position;
            postion.x = postion.x + Random.Range(-2, 2);
            postion.y = postion.y + Random.Range(-2, 2);
            Instantiate(minionPrefab, postion, Quaternion.identity);
        }
    }

    private void DirectPatern()
    {
        Boss.instance.angleSpread = 45f;
        Boss.instance.oscillate = false;
        Boss.instance.stagger = false;
        Boss.instance.projectilesPerBurst = 5;
        Boss.instance.Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(22, 8, 0));
    }
}