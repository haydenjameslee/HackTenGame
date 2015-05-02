using UnityEngine;
using System.Collections;

public class PlayerController : Photon.MonoBehaviour
{
	private int _playerNum;
	private GameObject _camera;

	void Start ()
	{
		_playerNum = (int)photonView.instantiationData[0];
		_camera = GameObject.Find ("Main Camera");

		GameObject playerPosition = _playerNum == 1 ? GameObject.Find ("PlayerOnePosition") : GameObject.Find ("PlayerTwoPosition");
		SetPlayerPosition(playerPosition);
	}
	
	void Update ()
	{
	
	}

	void SetPlayerPosition(GameObject pos)
	{
		_camera.transform.position = pos.transform.position;
		_camera.transform.rotation = pos.transform.rotation;
		this.transform.SetParent(pos.transform);
	}

}
