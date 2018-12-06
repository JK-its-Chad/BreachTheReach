using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIinteract : MonoBehaviour {

    public OVRInput.Controller controller;
    public string buttonName;

    [SerializeField] Transform start;
    [SerializeField] Transform end;

    [SerializeField] GameObject platform;
    [SerializeField] Transform spot;
    [SerializeField] GameObject player;
    [SerializeField] TurnManager bigBoss;
    GameObject holder;
    Vector3 spawn;

    LineRenderer line;
    public LayerMask button;
    public LayerMask plat;

    float timer = 0;

    // Use this for initialization
    void Start ()
    {
        line = GetComponent<LineRenderer>();
        spawn = platform.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if (Input.GetAxis(buttonName) >= 0.5 )
        {
            line.SetPosition(0, start.position);
            line.SetPosition(1, end.position);
            if(timer <= 0)
            {
                RaycastHit hitbutton;
                if (Physics.Raycast(start.position, start.forward, out hitbutton, 20, button))
                {
                    if (hitbutton.collider.gameObject.GetComponent<ButtonScript>())
                    {
                        hitbutton.collider.gameObject.GetComponent<ButtonScript>().Click();
                    }
                }
                timer = .5f;
            }

            RaycastHit hitplat;
            if (Physics.Raycast(start.position, start.forward, out hitplat, 20, plat))
            {
                holder = hitplat.collider.gameObject;
            }
            if(holder && bigBoss.playerSupport)
            {
                Vector3 slide = new Vector3(0, 0, OVRInput.GetLocalControllerVelocity(controller).z + OVRInput.GetLocalControllerVelocity(controller).y);

                holder.transform.Translate(slide * 100 * Time.deltaTime);
            }
        }
        if (Input.GetAxis(buttonName) < 0.5)
        {
            if(player.transform.position != spot.position && holder)
            {
                player.GetComponent<Player>().ToSupport();
            }
            timer = 0f;
            line.SetPosition(0, start.position);
            line.SetPosition(1, start.position);
            holder = null;
        }

        if(bigBoss.roundOver)
        {
            Return();
        }
    }

    void Return()
    {
        platform.transform.position = spawn;
    }


}
