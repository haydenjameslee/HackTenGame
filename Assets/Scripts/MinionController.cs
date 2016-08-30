using UnityEngine;
using System.Collections;

public class MinionController : Photon.MonoBehaviour {
	
	// Use this for initialization
	void Start ()
	{
		int runVelocity = (int)photonView.instantiationData[0];
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, runVelocity);

		int avatarIdx = runVelocity > 0 ? 0 : 1;

		SetAvatar(avatarIdx);
	}
	
	void SetAvatar (int avatarIdx)
	{
		transform.GetChild(avatarIdx).gameObject.SetActive(true);
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.name == "MinionBoundary")
		{
			Destroy(this.gameObject);
		}
	}
}
