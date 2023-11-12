using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunslingerBandit : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBursts;

    public void Attack()
    {
        // Vector2 targetDirection = PlayerController.Instance.transofrm.position - transform.position;

        // GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // newBullet.transform.right = targetDirection;

        // if (newBullet.TryGetComponent(out BulletShotgun bulletShotgun))
        // {
        //     bulletShotgun.UpdateMoveSpeed(bulletMoveSpeed);
        // }
    }
    
}
