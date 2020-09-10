using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum musicFase
{
    FLORESTA, CAVERNA
}

public class GameController : MonoBehaviour
{
    private Camera cam;

    public float speedCam;
    public Transform playerTransform, limiteCamEsq, limiteCamDir, limiteCamSup, limiteCamInf;
    public GameObject[] fase;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip sfxJump;
    public AudioClip sfxAtack;
    public AudioClip[] sfxStep;
    public AudioClip sfxCoin;
    public AudioClip sfxEnemyDead;
    public AudioClip sfxDamage;
    public AudioClip musicFloresta, musicCaverna;
    public musicFase musicaAtual;
    

    // Use this for initialization
    void Start ()
    {
        cam = Camera.main;

        /*foreach(GameObject o in fase)
        {
            o.SetActive(false);
        }
        fase[0].SetActive(true);*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Update atrasado - Depois de executar todos os updates, executa esse (no mesmo frame);
    void LateUpdate()
    {
        CamController();
    }

    void CamController()
    {
        /*Básico
        Vector3 poscam = new Vector3(playerTransform.position.x, playerTransform.position.y, cam.transform.position.z);
        cam.transform.position = poscam;
        */

        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;

        if (cam.transform.position.x < limiteCamEsq.position.x && playerTransform.position.x < limiteCamEsq.position.x)
        {
            posCamX = limiteCamEsq.position.x;
        }
        else if (cam.transform.position.x > limiteCamDir.position.x && playerTransform.position.x > limiteCamDir.position.x)
        {
            posCamX = limiteCamDir.position.x;
        }

        if (cam.transform.position.y < limiteCamInf.position.y && playerTransform.position.y < limiteCamInf.position.y)
        {
            posCamY = limiteCamInf.position.y;
        }
        else if (cam.transform.position.y > limiteCamSup.position.y && playerTransform.position.y > limiteCamSup.position.y)
        {
            posCamY = limiteCamSup.position.y;
        }

        Vector3 poscam = new Vector3(posCamX, posCamY, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, poscam, speedCam * Time.deltaTime);
    }

    public void playSFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume);
    }

    public void TrocarMusica(musicFase novaMusica)
    {
        AudioClip clip = null;

        switch(novaMusica)
        {
            case musicFase.CAVERNA:
                clip = musicCaverna;
                break;
            case musicFase.FLORESTA:
                clip = musicFloresta;
                break;
        }
        StartCoroutine("ControleMusica", clip);
    }

    IEnumerator ControleMusica(AudioClip musica)
    {
        float volumeMaximo = musicSource.volume;

        for (float volume = volumeMaximo; volume > 0; volume -= 0.01f)
        {
            musicSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        musicSource.clip = musica;
        musicSource.Play();

        for (float volume = 0; volume < volumeMaximo; volume += 0.01f)
        {
            musicSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }
    }
}
