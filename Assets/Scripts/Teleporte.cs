using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporte : MonoBehaviour
{
    private GameController _GameController;

    public Transform pontoSaida, posCamera;
    public Transform playerTransform, limiteCamEsq, limiteCamDir, limiteCamSup, limiteCamInf;
    public musicFase novaMusica;

    // Use this for initialization
    void Start ()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
	}
	
	// Update is called once per frame
	void Update ()
    {
        		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "Player":
                _GameController.TrocarMusica(musicFase.CAVERNA);
                col.transform.position = pontoSaida.position;
                Camera.main.transform.position = posCamera.position;
                _GameController.limiteCamEsq = limiteCamEsq;
                _GameController.limiteCamDir = limiteCamDir;
                _GameController.limiteCamSup = limiteCamSup;
                _GameController.limiteCamInf = limiteCamInf;
                break;
        }
    }
}
