using UnityEngine;
using System.Collections;

public class PlayerController : Photon.MonoBehaviour
{
	private int _playerNum;
	private GameObject _camera;
	private Transform _centerTransform;

	void Start ()
	{
		_playerNum = (int)photonView.instantiationData[0];

		GameObject playerPosition = _playerNum == 1 ? GameObject.Find ("PlayerOnePosition") : GameObject.Find ("PlayerTwoPosition");

		if (photonView.isMine)
		{
			_camera = GameObject.Find ("OVRCameraRig");
			_centerTransform = _camera.transform.Find("TrackingSpace/CenterEyeAnchor");
			SetPlayerPosition(playerPosition);
		}

		SetAvatar(_playerNum);

		if (_playerNum == 2)
		{
			StartGame();
		}
	}

	void SetPlayerPosition(GameObject pos)
	{
		_camera.transform.position = pos.transform.position;
		_camera.transform.rotation = pos.transform.rotation;

		Transform cameraAnchor = _camera.transform.Find("TrackingSpace/CenterEyeAnchor");
		this.transform.SetParent(cameraAnchor);
		this.transform.position = pos.transform.position;
		this.transform.rotation = pos.transform.rotation;
	}

	void SetAvatar (int avatarIdx)
	{
		if (!photonView.isMine)
		{
			transform.Find("Avatar").GetChild(avatarIdx - 1).gameObject.SetActive(true);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(_centerTransform.position);
			stream.SendNext(_centerTransform.rotation);
		}
		else
		{
			// Network player, receive data
			this.transform.position = (Vector3) stream.ReceiveNext();
			this.transform.rotation = (Quaternion) stream.ReceiveNext();
		}
	}

	void StartGame ()
	{
		if (!photonView.isMine)
		{
			Debug.Log ("start minions from player one");
			GameObject.Find ("MinionCannonOne").GetComponent<MinionCannon>().StartMinions();
		}
		else
		{
			Debug.Log ("start minions from player two");
			GameObject.Find ("MinionCannonTwo").GetComponent<MinionCannon>().StartMinions();
		}
	}

	void Update ()
	{
		if (photonView.isMine && Input.GetButtonDown("Fire1"))
		{
			Vector3 bulletStartPosition = _centerTransform.position;
			Quaternion bulletStartRotation = _centerTransform.rotation;
			PhotonNetwork.Instantiate("Bullet", bulletStartPosition, bulletStartRotation, 0);
		}
	}
}
