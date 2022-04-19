using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class Enemy_Status : MonoBehaviour
{

    public float defalt_Health = 70;
    public float defalt_Speed = 3;
    public float defalt_Damage = 5;
    public float defalt_AtcSpeed = 1;
    public string enemyType1 = "defalt";
    
    public float aerial_Health = 50;
    public float aerial_Speed = 3;
    public float aerial_Damage = 15;
    public float aerial_AtcSpeed = 1;
    public string enemyType2 = "aerial";

    public float physical_Health = 120;
    public float physical_Speed = 3;
    public float physical_Damage = 5;
    public float physical_AtcSpeed = 1;
    public string enemyType3 = "physical";

    public float speed_Health = 60;
    public float speed_Speed = 6;
    public float speed_Damage = 5;
    public float speed_AtcSpeed = 1.5f;
    public string enemyType4 = "speed";

    public float explosion_Health = 50;
    public float explosion_Speed = 1f;
    public float explosion_Damage = 50;
    public float explosion_AtcSpeed = 1;
    public string enemyType5 = "explosion";

    public float reinforced_Health = 100;
    public float reinforced_Speed = 3;
    public float reinforced_Damage = 15;
    public float reinforced_AtcSpeed = 1.3f;
    public string enemyType6 = "reinforced";

    public float middle_Health = 1500f;
    public float middle_Speed = 3f;
    public float middle_Damage = 25f;
    public float middle_AtcSpeed = 1.3f;
    public string enemyType7 = "middle";

    public float final_Health = 2500f;
    public float final_Speed = 3f;
    public float final_Damage = 35f;
    public float final_AtcSpeed = 2f;
    public string enemyType8 = "final";


}