using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float moveInput;
    public float moveSpeed = 5f; // ��������� �����������
    public float jumpForce = 10f; // ���������� �������
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private bool isGrounded;
    private bool ShopIsNear = false;
    private bool SnackIsNear = false;
    private bool CityMallIsNear = false;
    bool isAlive;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject snackPanel;
    [SerializeField] private GameObject CityMallPanel;
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private GameObject DeathPanel;

    private InvetoryManagement InventoryManagementScript;

    private void Awake()
    {
        InventoryManagementScript = GameObject.Find("Inventory").GetComponent<InvetoryManagement>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shopPanel.SetActive(false);
        snackPanel.SetActive(false); 
        CityMallPanel.SetActive(false);
        playerAnimator = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
        DeathPanel.SetActive(false);
        isAlive = true;
    }
    
    void Update()
    {
        if (isAlive)
        {
            Move();
            Jump();
            TotalAnimation();
            // PressShift();

            //������ � ���������
            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {
            //     UseItem("COIN");
            // }
            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            //     UseItem("WIRESBOX");
            // }

            

            

            Panels();
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Shop"))
        {
            ShopIsNear = true;
        }
        if (coll.gameObject.CompareTag("Snack"))
        {
            SnackIsNear = true;
        }
        if (coll.gameObject.CompareTag("CityMall"))
        {
            CityMallIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Shop"))
        {
            ShopIsNear = false;
        }
        if (coll.gameObject.CompareTag("Snack"))
        {
            SnackIsNear = false;
        }
        if (coll.gameObject.CompareTag("CityMall"))
        {
            CityMallIsNear = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Item"))
        {
            string ItemName = coll.gameObject.name.ToUpper();
            Sprite ItemIcon = Resources.Load<Sprite>(ItemName);

            if (ItemIcon != null)
            {
                Item NewItem = new Item(ItemIcon, ItemName, 0);
                InventoryManagementScript.AddItem(NewItem);
                //����
                Destroy(coll.gameObject);
            }
            else
            {
                Debug.Log("������ ��� " + ItemName + "  �� ��������");
            }
        }

        if(coll.gameObject.CompareTag("Damager"))
        {
            //SceneManager.LoadScene(1);
            DeathPanel.SetActive(true);
            isAlive = false;
        }
    }


    public void UseItem(string itemName)
    {
        if (InventoryManagementScript.HasItem(itemName))        // �� � � �������� ����� �������
        {
            Sprite itemIcon = Resources.Load<Sprite>(itemName);
            Item itemToUse = new Item(itemIcon, itemName, 0);

            // ������������ �������� � ��������� ���� � ���������
            InventoryManagementScript.RemoveItem(itemToUse);

            // 䳿 ������� �� ���� ��������
            switch (itemName)
            {
                case "COIN":
                    // ������ ������
                    //RestoreStamina();
                    GameManagerScript.instance.AddMoney(50);
                    break;

                case "WIRESBOX":
                    // ������'�
                    //RestoreHealth();
                    GameManagerScript.instance.AddMoney(25);
                    break;

                // ����� �� ������ ����� ��������
                default:
                    Debug.Log("�������� �������: " + itemName);
                    //soundManagerScript.PlayItemUseSound();
                    break;
            }
        }

    }

    void TotalAnimation()
    {
        FallAnimation();

    }

    void FallAnimation()
    {
        playerAnimator.SetFloat("SpeedY", rb.velocity.y);
    }

    void Move()
    {
        // ���������� ����-������
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            playerSR.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            playerSR.flipX = false;
        }

        //rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        playerAnimator.SetFloat("SpeedX", Mathf.Abs(moveInput));
    }

    void Jump()
    {
        // �������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAnimator.Play("Jump");
            isGrounded = false;
        }
    }

    public void RestartButton()
    {
        isAlive = true;
        SceneManager.LoadScene(1);
    }

    public void Starved()
    {
        isAlive = false;
        DeathPanel.SetActive(true);
    }

    void PressShift()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed + 10;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed - 10;
        }
    }

    private void Panels()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            {
                shopPanel.SetActive(false);
            }
            if (ShopIsNear && Input.GetKeyDown(KeyCode.E))
            {
                shopPanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                snackPanel.SetActive(false);
            }
            if (SnackIsNear && Input.GetKeyDown(KeyCode.E))
            {
                snackPanel.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CityMallPanel.SetActive(false);
            }
            if (CityMallIsNear && Input.GetKeyDown(KeyCode.E))
            {
                CityMallPanel.SetActive(true);
            }
    }
}
