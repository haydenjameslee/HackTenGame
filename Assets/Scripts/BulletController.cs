using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
	public float speed;
	private float _lifetime = 5.0f;
	
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		Destroy(gameObject, _lifetime);
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.name == "Minion(Clone)")
		{
			Destroy(collision.collider.gameObject);
		}
		else
		{
			Destroy (this.gameObject);
		}
	}
}
