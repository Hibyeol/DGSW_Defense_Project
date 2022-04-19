using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GunBlueprint : MonoBehaviour
{
    public string gunName;
    public int gunType;
    public int magazine;
    public int damage;
    public float fireRate;
    public float fireRange;
    /*
    장전시간은 플레이어 애니메이터의 WeaponType_int(guntype)에
    의해 설정되도록 설계되어 있음
    */
}
