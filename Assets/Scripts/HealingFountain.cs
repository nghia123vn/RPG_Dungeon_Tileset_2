using UnityEngine;

public class HealingFountain : Collidable
{
	public int healingAmount = 1;

	private float healCooldown = 1.0f;
	private float lastHeal;

	protected override void OnCollide(Collider2D coll)
	{
		if (coll.name != "Player")
		{
			return;
			Debug.Log("coll.name != \"Player\"");
		}
		else
		{
			if ((Time.time - lastHeal) > healCooldown)
			{
				lastHeal = Time.time;
				GameManager.instance.player.Heal(healingAmount);
			}
			Debug.Log("coll.name == \"Player\"");
		}
	}
}
