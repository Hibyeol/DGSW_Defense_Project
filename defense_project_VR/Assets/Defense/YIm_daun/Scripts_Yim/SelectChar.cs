using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour
{
    public Character character;
    Animator anim;

    public SelectChar[] chars;

    //Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if(DataMgr.instance.currentCharacter == character)
        {
            OnSelecct();
        }
        else
        {
            OnDeSelect();
        }
    }

    private void OnMouseUpAsButton()
    {
        DataMgr.instance.currentCharacter = character;
        OnSelecct();
        for(int i = 0; i<chars.Length; i++)
        {
            if(chars[i] != this)
            {
                chars[i].OnDeSelect();
            }
        }
    }
    
    void OnDeSelect()
    {
       anim.SetInteger("Animation_int", 9);
    }

    void OnSelecct()
    {
        anim.SetInteger("Animation_int", 0);
    }
}
