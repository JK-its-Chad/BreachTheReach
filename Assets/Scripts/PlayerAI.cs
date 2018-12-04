using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {

    public int points = 0;

    [SerializeField] TextMesh pointText;
    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject Enemy3;

    void Start ()
    {
        OpenUI();
        points = 50;
    }
	

	void Update ()
    {
        pointText.text = points.ToString();
	}

    public void OpenUI()
    {
        Enemy1.SetActive(true);
        Enemy2.SetActive(true);
        Enemy3.SetActive(true);
    }

    public void CloseUI()
    {
        Enemy1.SetActive(false);
        Enemy2.SetActive(false);
        Enemy3.SetActive(false);
    }
}
