using UnityEngine;

public class ElectricityWeapon : ElementalWeaponController, IPickable
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
        Attack();

        if (fireRate > 0)
        {
            fireRate -= Time.deltaTime;
        }
    }

    public override void SpecialAttack(Collision2D enemy)
    {
        
    }

    public override void Attack()
    {
        if (Input.GetMouseButton(0) && fireRate <= 0)
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

        Rotation();
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
}
