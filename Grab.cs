using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Camera cameraX;
    public float speed = 10f;
    public float rotateSpeed = 1f;
    float dist;
    bool swordF = true;
    bool holdF = false;
    string itemName = null;
    GameObject Item;
    Vector3 distToItem;





    void Start()
    {
        
    }

   
    void Update()
    {

        if(!holdF)
        {
        GrabItem();
        }
        else if (itemName != null)
        {
            Hold();
            Drop();
        }

    }

    void GrabItem()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraX.transform.position, cameraX.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {

            dist = Vector3.Distance(cameraX.transform.position, hit.transform.position);

            if (dist <= 6f && Input.GetKeyDown(KeyCode.E))
            {
                itemName = hit.transform.name;
                holdF = true;
                UnityEngine.Debug.Log("ok");
                Item = GameObject.Find(itemName);
                if (Item.GetComponent<Rigidbody>() != null)
                {
                    Item.GetComponent<Rigidbody>().useGravity = false;
                    Item.GetComponent<Rigidbody>().freezeRotation = true;
                }
                distToItem = new Vector3(
                Item.transform.position.x - cameraX.transform.position.x,
                Item.transform.position.y - cameraX.transform.position.y,
                Item.transform.position.z - cameraX.transform.position.z);
             }
        }
    }

    void Hold()
    {
        Ray ray = new Ray(cameraX.transform.position, cameraX.transform.forward);

        Item.transform.position = Vector3.Lerp(Item.transform.position, ray.GetPoint(dist), speed * Time.deltaTime);
        if (swordF)
        {
            Item.transform.rotation = Quaternion.Slerp(Item.transform.rotation, cameraX.transform.rotation, rotateSpeed * Time.deltaTime);
        }
    }
    void Drop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Item.GetComponent<Rigidbody>() != null)
            {
                Item.GetComponent<Rigidbody>().useGravity = true;
            }
            itemName = null;
            holdF = false;
            Item = null;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (Item.GetComponent<Rigidbody>() != null)
            {
                Item.GetComponent<Rigidbody>().useGravity = true;
                Item.GetComponent<Rigidbody>().freezeRotation = false;
                Item.GetComponent<Rigidbody>().AddForce(cameraX.transform.forward * speed, ForceMode.Impulse);
                itemName = null;
                holdF = false;
                Item = null;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
           
        }
            
    }

}
