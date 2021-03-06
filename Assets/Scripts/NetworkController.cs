﻿using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	private string _room = "macrohard";
	public static int _playerNum;

	void Start ()
	{
		// PhotonNetwork.sendRate = 10;
		// PhotonNetwork.sendRateOnSerialize = 10;
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnJoinedLobby()
	{
		Debug.Log ("joined lobby");

		RoomOptions roomOptions = new RoomOptions() {};
		PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
	}
	
	void OnJoinedRoom()
	{
		_playerNum = PhotonNetwork.playerList.Length;
		Debug.Log (_playerNum);

		object[] photonData = new object[1];
		photonData[0] = (object)_playerNum;

		PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity, 0, photonData);
	}

}
