using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character" ,menuName = "New Character")]
public class Character : ScriptableObject {

    public string _name;

    public Sprite _iconUnSelected;
    public Sprite _iconSelected;

}
