using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupController
{
    public GameObject _attackPrefab;
    public GameObject _damagePrefab;
    public GameObject _dodgePrefab;
    public GameObject _defendPrefab;
    public GameObject _battleCanvas; 

    public GameObject _messagePrefab; 
    public PopupController(){
        _battleCanvas = GameObject.Find("BattleCanvas");
        _messagePrefab = GameObject.Find("_messagePopup");
        
    }

   public void Popup(ABILITIES _ability = ABILITIES.NONE, float damage = 0, float defend = 0, string message = ""){
       GameObject _popupObject;
       if (_ability == ABILITIES.ATTACK){
            _popupObject = Object.Instantiate(_attackPrefab, Vector3.zero, Quaternion.identity);
       }
       if (_ability == ABILITIES.DEFEND){
            _popupObject = Object.Instantiate(_defendPrefab, Vector3.zero, Quaternion.identity);
       }
       if (_ability == ABILITIES.DODGE){
            _popupObject = Object.Instantiate(_dodgePrefab, Vector3.zero, Quaternion.identity);
       }
       if (_ability == ABILITIES.NONE){
            Text _text = _messagePrefab.GetComponent<Text>();
            _text.text = message;
       }
   }
}
