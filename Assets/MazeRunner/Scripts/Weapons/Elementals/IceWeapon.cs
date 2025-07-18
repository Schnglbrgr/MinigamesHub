using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceWeapon : ElementalWeaponController, IPickable
{

    private void Awake()
    {
        healthWeapon = elementalWeapon.healthWeapon;

        damage = elementalWeapon.damage;

        durationAttack = elementalWeapon.durationAttack;

        collectWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectWeapon>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        colorBullet = headWeapon.GetComponent<SpriteRenderer>().color;

        hpBar.gameObject.SetActive(true);

        hpBar.value = healthWeapon / elementalWeapon.healthWeapon;

    }

    private void OnDisable()
    {
        hpBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }

        Rotation();

    }

    public override void SpecialAttack(Collision2D enemy)
    {

        enemy.gameObject.GetComponent<EnemyController>().speed =  1.5f;
        enemy.gameObject.GetComponent<EnemyController>().damage = 5;

        enemy.gameObject.GetComponent<EnemyController>().animationController.SetBool("isFrozen", true);

        StartCoroutine(ReturnValues(durationAttack,enemy));

    }

    public override void Attack(InputAction.CallbackContext context)
    {
        if (fireRate <= 0)
        {
            currentBullet = poolManager.PoolInstance(elementalBullet);

            currentBullet.transform.position = shootPoint.position;

            currentBullet.transform.rotation = gameObject.transform.rotation;

            currentBullet.GetComponent<ElementalBullet>().damage = damage;

            currentBullet.GetComponent<ElementalBullet>().currentPrefab = elementalBullet;

            currentBullet.GetComponent<ElementalBullet>().elementalWeapon = GetComponent<ElementalWeaponController>();

            currentBullet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorBullet;

            fireRate = elementalWeapon.fireRate;

            HealthWeapon();
        }
    }

    public override void HealthWeapon()
    {
        healthWeapon--;

        hpBar.value = healthWeapon / elementalWeapon.healthWeapon;

        if (healthWeapon <= 0)
        {
            collectWeapon.currentWeapon = null;

            transform.SetParent(null);

            collectWeapon.dropWeaponText.SetActive(false);

            collectWeapon.DestroyWeaponInv();

            gameObject.SetActive(false);
        }

    }


    public override void ControlEnable(bool turnOff_On)
    {
        this.enabled = turnOff_On;
    }

    public void TakeItem()
    {
        collectWeapon.TakeWeapon(gameObject);
    }

    IEnumerator ReturnValues(float durationAttack, Collision2D enemy)
    {
        yield return new WaitForSeconds(1);

        //enemy.gameObject.GetComponent<EnemyController>().speed = 3;

        //enemy.gameObject.GetComponent<EnemyController>().damage = 10;

        //senemy.gameObject.GetComponent<EnemyController>().animationController.SetBool("isFrozen", false);
    }

}
