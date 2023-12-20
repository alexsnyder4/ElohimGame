using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius;
    public GameObject heldItem;
    public Transform rightHand;
    public Transform leftHand;
    public InventoryManager inventoryManager;
    public Collider[] nearbyColliders;
    public Button[] hotbarbtns = new Button[5];
    public PlayerUI ui;

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        interactionRadius = 3.0f;
    }

    void Update()
    {
        nearbyColliders = Physics.OverlapSphere(transform.position, interactionRadius, LayerMask.GetMask("Items", "Weapons"));

        foreach (Collider collider in nearbyColliders)
        {
            if (collider.CompareTag("Weapon") || collider.CompareTag("Item"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUpWeapon(collider);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && heldItem != null)
        {
            dropObject(heldItem.GetComponent<Collider>());
        }
        if(!ui.invActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                hotbarbtns[0].onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                hotbarbtns[1].onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                hotbarbtns[2].onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                hotbarbtns[3].onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                hotbarbtns[4].onClick.Invoke();
            }
        }
    }

    void PickUpWeapon(Collider itemCollider)
    {
        ItemController itemController = itemCollider.GetComponent<ItemController>();

        if (itemController != null && itemController.items != null)
        {
            inventoryManager.Add(itemController.items);
            Destroy(itemCollider.gameObject);
        }
    }

    void dropObject(Collider itemCollider)
    {
        ItemController itemController = itemCollider.GetComponent<ItemController>();
        if (itemController != null && itemController.items != null)
        {
            inventoryManager.Remove(itemController.items);

            Rigidbody itemRigidbody = itemCollider.GetComponent<Rigidbody>();
            Collider[] itemColliders = itemCollider.GetComponents<Collider>();
            foreach (Collider col in itemColliders)
            {
                col.enabled = true;
            }
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = false;
            }

            itemCollider.transform.parent = null;
            heldItem = null;
        }
    }
}
