using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public GameObject thisGun, weaponParent;
    public Camera cam;
    public ParticleSystem muzzleFlash;

    public GameObject crossHair;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weaponParent.transform.childCount > 0 && thisGun == weaponParent.transform.GetChild(0).gameObject)
        {
            Shoot();

            //Audio
            AudioManager.instance.playOneShot("event:/FX/Weapons/Gun/Shoot");
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
