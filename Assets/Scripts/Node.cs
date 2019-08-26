using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NODE_TYPES{
    SHOP,
    EVENT,
    BATTLE
}

public class Node : MonoBehaviour
{
    private NODE_TYPES _nodeType{get; set;}
    private bool _visited {get; set;}

    private List<Node> _neighbours;

    public Node(NODE_TYPES type){
        _nodeType = type;
    }

    public void Start(){

    }

    public void Update(){

    }

}