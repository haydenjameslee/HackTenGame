using UnityEngine;
using System.Collections;

public class MinionCannon : MonoBehaviour
{
	public int shootingRange = 3;
	public int spawnDelta = 1000;
	public int minVel = 1;
	public int maxVel = 10;
	public bool isCannonTwo;

	private float _nextSpawn = 100000000000000f;

	void Update ()
	{
		float now = this.now();

		if (now > _nextSpawn)
		{
			_nextSpawn = now + spawnDelta;

			int randX = Random.Range(-shootingRange, shootingRange);
			Vector3 spawnPosition = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z);

			int velocity = Random.Range(minVel, maxVel);

			if (isCannonTwo)
			{
				velocity = -velocity;
			}

			object[] photonData = new object[1];
			photonData[0] = (object)velocity;

			PhotonNetwork.Instantiate("Minion", spawnPosition, transform.rotation, 0, photonData);
		}
	}

	public void StartMinions ()
	{
		_nextSpawn = this.now ();
	}

	float now ()
	{
		return Time.time * 1000;
	}
}
