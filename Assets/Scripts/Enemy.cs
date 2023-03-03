using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Login
    public float triggerLenght = 1;
    public float chaseLeght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTranform;
    private Vector3 stratingPostion;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTranform = GameManager.instance.player.transform;
        stratingPostion = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    //is the player in range?
    private void FixedUpdate()
    {
        if (Vector3.Distance(playerTranform.position, stratingPostion) < chaseLeght)
        {
            if (Vector3.Distance(playerTranform.position, stratingPostion) < triggerLenght)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTranform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(stratingPostion - transform.position);
            }
        }
        else
        {
            UpdateMotor(stratingPostion - transform.position);
            chasing = false;
        }

        //check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;

        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
