using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public GameObject gun;
    public Transform weaponParent, cam, player;

    public float dropForwardForce, dropUpwardForce;

    public GameObject pickUpText;
    public GameObject crossHair;

    // Start is called before the first frame update
    void Start()
    {
        gun.GetComponent<Rigidbody>().isKinematic = true;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Drop();

            crossHair.SetActive(false);
        }
    }

    void Equip()
    {
        gun.GetComponent<Rigidbody>().isKinematic = true;
        gun.GetComponent<MeshCollider>().convex = false;
        gun.GetComponent<MeshCollider>().enabled = false;
        gun.GetComponent<BoxCollider>().enabled = false;

        gun.transform.position = weaponParent.transform.position;
        gun.transform.rotation = weaponParent.transform.rotation;

        gun.transform.SetParent(weaponParent);
    }

    void Drop()
    {
        weaponParent.DetachChildren();

        gun.transform.eulerAngles = new Vector3(gun.transform.position.x, gun.transform.position.z, gun.transform.position.y);

        gun.GetComponent<Rigidbody>().isKinematic = false;
        gun.GetComponent<MeshCollider>().convex = true;
        gun.GetComponent<MeshCollider>().enabled = true;

        gun.GetComponent<BoxCollider>().enabled = true;

        //Gun carries momentum of player
        gun.GetComponent<Rigidbody>().velocity = player.GetComponent<CharacterController>().velocity;

        //AddForce
        gun.GetComponent<Rigidbody>().AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        gun.GetComponent<Rigidbody>().AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        gun.GetComponent<Rigidbody>().AddTorque(new Vector3(random, random, random) * 10);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             pickUpText.SetActive(true);

             if (Input.GetKey(KeyCode.E))
            {
                Equip();

                crossHair.SetActive(true);

                pickUpText.SetActive(false);

                GameObject.Find ("TrialsTimer").GetComponent<Timer>().enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUpText.SetActive(false);
        }
    }
}
