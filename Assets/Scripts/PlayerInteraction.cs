using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 3.0f; 
    public GameObject heldItem;
    public Transform rightHand;
    public Transform leftHand;
    public float degx;
    public float degy;
    public float degz;
    public List<GameObject> nearbyObjects = new List<GameObject>();


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyObjects.Count > 0)
        {
            PickUpWeapon(nearbyObjects[0]);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            dropObject(heldItem);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && !nearbyObjects.Contains(other.gameObject))
        {
            nearbyObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            nearbyObjects.Remove(other.gameObject);
        }
    }

    void PickUpWeapon(GameObject item)
    {
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
        Collider[] itemColliders = item.GetComponents<Collider>();
        foreach (Collider col in itemColliders)
        {
            col.enabled = false;
        }
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = true;
        }
        item.transform.rotation = Quaternion.identity;

        // Determine which hand to place the item in based on your game's logic.
        // Here, we are placing the item in the right hand.
        PlaceInHand(item, rightHand);
    }

    // Custom method to place an item in a specific hand.
    void PlaceInHand(GameObject item, Transform hand)
    {
        heldItem = item;
        // Position the item at the hand's position.
        item.transform.position = hand.position;

        // Make the item's parent the hand.
        item.transform.parent = hand;
    }
    void dropObject(GameObject item)
    {
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
        Collider[] itemColliders = item.GetComponents<Collider>();
        foreach (Collider col in itemColliders)
        {
            col.enabled = true;
        }
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = false;
        }
        item.transform.parent = null;
        heldItem = null;
    }
}
