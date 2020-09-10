using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private SpriteRenderer playerSr;
    private bool isGrounded, isAtack;

    public float speed, jumpForce;
    public bool isLookLeft;
    public Transform groundCheck, mao;
    public GameObject hitBoxPrefab;
    public Color hitColor, noHitColor;
    public int maxHP;

	// Use this for initialization
	void Start ()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSr = GetComponent<SpriteRenderer>();
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _GameController.playerTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float speedY = playerRb.velocity.y;

        if(isAtack && isGrounded)
        {
            h = 0;
        }

        if(h > 0 && isLookLeft)
        {
            Flip();
        }
        else if(h < 0 && !isLookLeft)
        {
            Flip();
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            _GameController.playSFX(_GameController.sfxJump, 0.5f);
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        if(Input.GetButtonDown("Fire1") && !isAtack)
        {
            isAtack = true;
            _GameController.playSFX(_GameController.sfxAtack, 0.5f);
            playerAnimator.SetTrigger("atack");
        }

        playerRb.velocity = new Vector2(h * speed, speedY);
        playerAnimator.SetInteger("h", (int)h);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isAtack", isAtack);
	}

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "coletavel":
                _GameController.playSFX(_GameController.sfxCoin, 1);
                Destroy(col.gameObject);
                break;
            case "damage":                
                StartCoroutine("damageController");
                break;
        }
    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void EndAtack()
    {
        isAtack = false;
    }

    void hitBoxAtack()
    {
        GameObject hitBoxTemp = Instantiate(hitBoxPrefab, mao.position, transform.localRotation);
        Destroy(hitBoxTemp, 0.2f);
    }

    void FootStep()
    {
        _GameController.playSFX(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 1f);
    }

    IEnumerator damageController()
    {
        _GameController.playSFX(_GameController.sfxDamage, 0.5f);

        maxHP -= 1;
        if(maxHP <= 0)
        {
            Debug.LogError("GameOver");
        }

        this.gameObject.layer = LayerMask.NameToLayer("Invencivel");
        playerSr.color = hitColor;
        yield return new WaitForSeconds(0.3f);
        playerSr.color = noHitColor;

        for(int i = 0; i < 5; i++)
        {
            playerSr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        this.gameObject.layer = LayerMask.NameToLayer("Player");
        playerSr.color = Color.white;
    }
}
