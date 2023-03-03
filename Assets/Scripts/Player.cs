using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject);
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinID)
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp()
    {
        maxHitPoint++;
        hitpoint = maxHitPoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitPoint)
            return;

        hitpoint += healingAmount;
        if (hitpoint > maxHitPoint)
            hitpoint = maxHitPoint;

        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);

    }
}
