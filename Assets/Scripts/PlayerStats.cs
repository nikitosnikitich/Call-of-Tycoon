using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [SerializeField] private Image imageh;
    public int hung = 6;
    [SerializeField] private Sprite[] hungImages;
    private PlayerMovement PlayerMovementScript;

    private AudioSource zvuk;
    public AudioClip damageClip;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        imageh.sprite = hungImages[hung];
        StartCoroutine(Hunger());
        PlayerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        zvuk = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Hunger()
    {
        while (hung > 0)
        {
            yield return new WaitForSeconds(5f);
            hung -= 1;
            imageh.sprite = hungImages[hung];
            if(hung <= 0)
            {
                PlayerMovementScript.Starved();
            }
        }
    }
    public void EatFoodItem(int value)
    {
        if(hung + value <= 6)         // visual bug, no time to bool
        {
            hung += value;
        }
        else
        {
            hung = 6;
        }
        imageh.sprite = hungImages[hung];
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy") && hung > 0)
        {
            hung -= 1;
            imageh.sprite = hungImages[hung];
            zvuk.PlayOneShot(damageClip);
            if(hung <= 0)
            {
                PlayerMovementScript.Starved();
            }
        }
    }
}
