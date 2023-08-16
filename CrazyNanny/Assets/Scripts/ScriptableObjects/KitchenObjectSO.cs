using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()] //this class will appear in create menu
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
