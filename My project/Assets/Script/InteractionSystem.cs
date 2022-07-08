using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Parameters Fields")]
    // Start is called before the first frame update
    //Detection Transform
    //Detection Radius
    //Detection Layer
    //List of picked item
    [SerializeField] private Transform detectionTarget;
    [SerializeField] private const float detectionRadius = 0.2f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private GameObject dectectedItems;
    [Header("Examine Fields")]
    [SerializeField] private GameObject examineWindow;
    [SerializeField] private Image examineImage;
    [SerializeField] private TMP_Text examineText;
    [Header("Other")]
    [SerializeField] public List<GameObject> pickedItems;
    [SerializeField] public bool isExamining;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectItem())
        {
            if (InteracInput())
            {
                dectectedItems.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteracInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectItem()
    {
        Collider2D item =  Physics2D.OverlapCircle(detectionTarget.position, detectionRadius, detectionLayer);
        if(item == null)
        {
            dectectedItems = null;
            return false;
        }
        else
        {
            dectectedItems = item.gameObject;
            return true;
        }
    }

    public void PickUpItem(GameObject item)
    {
        pickedItems.Add(item);
    }

    public void ExamineItem(Item item)
    {
        if (!isExamining)
        {
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examineText.text = item.descriptionText;
            examineWindow.SetActive(true);
            //show the item image in the middle
            isExamining = true;
        }
        else
        {
            //display an examine window
           
            examineWindow.SetActive(false);
            isExamining = false;
        }
        
    }
}
