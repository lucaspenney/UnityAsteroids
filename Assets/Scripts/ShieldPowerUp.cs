using UnityEngine;
using System.Collections;

public class ShieldPowerUp : PowerUp, IPowerUp {

	public override void receivePowerUp(GameObject obj) {
		base.receivePowerUp(obj);
		Shield s = obj.GetComponentInChildren<Shield>();
		if (s != null) {
			s.health += 1000;
		}
		AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("sounds/powerup", typeof(AudioClip)), Camera.main.transform.position);
	}
}
