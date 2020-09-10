using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorcegoIA : MonoBehaviour
{
    private GameController _GameController;
    private Animator morcegoAnimator;
    private bool isFollow;

    public float speed;
    public bool isLookLeft;
    public GameObject hitBox;

    // Use this for initialization
    void Start ()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        morcegoAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isFollow)
        {
            transform.position = Vector3.MoveTowards(transform.position, _GameController.playerTransform.position, speed * Time.deltaTime);
        }

        if(transform.position.x < _GameController.playerTransform.position.x && isLookLeft)
        {
            Flip();
        }
        else if(transform.position.x > _GameController.playerTransform.position.x && !isLookLeft)
        {
            Flip();
        }
	}

    void OnBecameVisible()
    {
        isFollow = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "hitBox":
                Destroy(hitBox);
                _GameController.playSFX(_GameController.sfxEnemyDead, 0.3f);
                morcegoAnimator.SetTrigger("dead");
                break;
        }
    }

    void OnDead()
    {
        Destroy(this.gameObject);
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
