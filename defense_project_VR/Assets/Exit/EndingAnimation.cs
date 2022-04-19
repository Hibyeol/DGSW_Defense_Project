using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EndingAnimation : MonoBehaviour
{

    int trigger = 0;

    Transform wing1;
    Transform wing2;
    Transform door;
    public Transform rot;
    public Transform pos;
    public Transform dp;
    public Transform campos;
    public Camera cam;
    public GameObject player;
    public GameObject vrPlayer;
    public Rig rig;
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        cam.transform.position = campos.position;
        //rig = player.transform.GetChild(1).Find("RigLayer_WeaopnAiming").GetComponent<Rig>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.sec <= 0 && trigger == 0)
        {
            trigger++;
            player.transform.GetComponent<PlayerController_kd>().enabled = false;
            player.transform.LookAt(transform);
            vrPlayer.SetActive(false);
            player.SetActive(true);

        }
        else if (trigger > 0)
        {
            campos.LookAt(transform);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, campos.rotation, 2f);

            transform.GetChild(2).Rotate(new Vector3(0.0f, speed * Time.deltaTime, 0.0f), Space.Self);
            transform.GetChild(3).Rotate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f), Space.Self);
            if (speed <= 2000)
            {
                speed += speed / 60;
            }
            
            if(Vector3.Distance(transform.position, pos.position) < 100)
            {
                vrPlayer.SetActive(true);
                player.SetActive(false);
            }

            if (speed > 450)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rot.rotation, 0.1f);
            }

            if (speed > 300)
            {
                transform.position = Vector3.Slerp(transform.position, pos.position, speed / 570000);
            }
            else if (speed < 100 && speed > 50)
            {
                transform.GetChild(1).position = dp.position;
            }
            else if (Vector3.Distance(player.transform.position, transform.GetChild(4).position) < 1f)
            {
                transform.GetChild(1).position = Vector3.Slerp(transform.GetChild(1).position, dp.position, 0.008f);
                Debug.Log(speed);
                player.transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed_f", 0.0f);
                player.SetActive(false);
            }
            else
            {
                //rig.weight = 0;
                player.transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed_f", 10.0f);
                iTween.MoveTo(player, transform.GetChild(4).position, 2);
            }


        }
    }
}