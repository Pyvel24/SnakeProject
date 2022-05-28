using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersView : MonoBehaviour
{
   [SerializeField]private GameObject contentGameObj;
   [SerializeField]private PlayerStatView PlayerStatPrefab;
   public Text playerCountText;

   private Dictionary<string, PlayerStatView> _playerStatViews = new Dictionary<string, PlayerStatView>();

   public void AddPlayer(string playerName)
   {
      var playerStatView = Instantiate(PlayerStatPrefab, contentGameObj.transform);
      _playerStatViews.Add(playerName, playerStatView);
      playerStatView.NameText.text = playerName;
   }

   public void SetValue(string playerName, string value)
   {
      if (_playerStatViews.TryGetValue(playerName, out var playerStatView))
      {
         playerStatView.InfoText.text = value;
      }
   }
}
