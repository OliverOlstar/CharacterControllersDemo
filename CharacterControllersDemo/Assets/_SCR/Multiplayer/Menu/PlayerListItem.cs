using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text nameText = null;
    private Player player;

    public void Init(Player pPlayer)
    {
        player = pPlayer;
        nameText.text = player.NickName;
    }

    // If someone else left the room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    // If I left the room
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
