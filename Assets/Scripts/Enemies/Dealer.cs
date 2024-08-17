using System;
using UnityEngine;

public class Dealer : Enemy
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private uint cooldown;
    [SerializeField] private Transform ammoFolder;
    private float time = 0;
    private void FixedUpdate()
    {
        int indexPlayer = ReadyToUseSkill();
        if (indexPlayer >= 0)
        {
            Attack(indexPlayer);
        }
        else
        {
            Walk();
        }
    }

    private void Attack(int indexPlayer)
    {
        if(Time.timeSinceLevelLoad > time + cooldown)
        {
            InitAmmo(players[indexPlayer].transform.position - transform.position);
            time = Time.timeSinceLevelLoad;
        }
    }

    private void InitAmmo(Vector3 position)
    {
        GameObject newAmmo = Instantiate(ammoPrefab, transform.position, Quaternion.identity, ammoFolder);
        if (newAmmo.TryGetComponent<AmmoMove>(out AmmoMove ammoMove))
        {
            ammoMove.SetTarget(position);
        }
        
    }
}
