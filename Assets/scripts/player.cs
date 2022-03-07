using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RDG;
public class player : MonoBehaviour
{
    #region
    //Regiao responsável pela física do jogo
    [SerializeField]
    private Rigidbody2D rbBola;
    [SerializeField]
    private float forca;
    [SerializeField]
    private Vector3 checkpoint;

    #endregion

    #region
    //Regiao responsáveis por informações de posições, ângulos e afins 
    [SerializeField]
    private Transform posicaoSeta;
    [SerializeField]
    private float angulo = 70;

    internal void adsFinished()
    {
        MonetizationManager.inst.aparecendoAnuncio = false;
        Time.timeScale = 1;
    }

    [SerializeField]
    private Transform localParaTp;
    [SerializeField] Transform seta;
    #endregion

    public delegate void AddScore();
    public event AddScore addScore;


    public delegate void Win();
    public event Win winned;
    public delegate void Lose();
    public event Lose losed;

    public delegate void Morte();
    public event Morte morte;
    public TrailRenderer tr;
    public GameObject collimorte;
    void Start()
    {
        Time.timeScale = 1;
        tr = GetComponent<TrailRenderer>();
        rbBola = GetComponent<Rigidbody2D>();
        rbBola.bodyType = RigidbodyType2D.Static;
        checkpoint = transform.position;
        posicionaSeta();
    }

    void posicionaSeta()
    {
        //imgDaSeta.rectTransform.position = transform.position;
        seta.transform.position = transform.position;
    }

    void Update()
    {
        if (!MonetizationManager.inst.isshow)
        {
            GameManager.inst.Time += Time.deltaTime;
        }
        
        posicionaSeta();
        if (this.transform.position.x > localParaTp.transform.position.x)
        {
            teletransporta(true);
        }
        else if (this.transform.position.x < -localParaTp.transform.position.x)
        {
            teletransporta(false);
        }
        
        if (GameManager.inst.podeJogar && !MonetizationManager.inst.isshow)
        {
            //GameManager.inst.Time += Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Input.mousePosition;
                Vector3 wp = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y));
                Vector3 vetSeta = transform.position - wp;
                vetSeta.Normalize();
                setAngulo(Mathf.Atan2(vetSeta.x, vetSeta.y) * Mathf.Rad2Deg);
                rotacaoSeta();
            
            }
            if (Input.GetMouseButtonUp(0))
            {
                tr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                GameManager.inst.podeJogar = false;
                float x, y;
                x = forca * Mathf.Sin(this.getAngulo() * Mathf.Deg2Rad);
                y = forca * Mathf.Cos(this.getAngulo() * Mathf.Deg2Rad);

                rbBola.bodyType = RigidbodyType2D.Dynamic;
                rbBola.velocity = (new Vector2(x, y));
                //imgDaSeta.gameObject.SetActive(false);
                seta.gameObject.SetActive(false);
                SoundManager.inst.Play(1);
            }
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.inst.Play(2);
            Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0.2f);
            if (!SoundManager.inst.isMuted())
            {
                Vibration.Vibrate(200);
            }
            
            if (checkpoint.y < transform.position.y)
            {
                collimorte.transform.position = transform.position - new Vector3(transform.position.x, 17f, transform.position.z);
            }
            addScore?.Invoke();
            rbBola.velocity = new Vector2(0, 0);
            transform.position = collision.gameObject.transform.position;
            rbBola.bodyType = RigidbodyType2D.Static;
            Destroy(collision.gameObject);
            checkpoint = transform.position;
            //imgDaSeta.gameObject.SetActive(true);
            seta.gameObject.SetActive(true);
            GameManager.inst.podeJogar = true;

            if (GameManager.inst.lvll.tipo == 3)
            {
                if (GameManager.inst.Score == GameManager.inst.lvll.scoreABater && GameManager.inst.Time <= GameManager.inst.lvll.TempoABater)
                {
                    SoundManager.inst.Play(3);
                    winned?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
                else if(GameManager.inst.Time > GameManager.inst.lvll.TempoABater) 
                {
                    SoundManager.inst.Play(4);
                    losed?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
            }


        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            rbBola.velocity = new Vector2(0, 0);
            transform.position = collision.gameObject.transform.position;
            rbBola.bodyType = RigidbodyType2D.Static;

            if(GameManager.inst.lvll.tipo == 1)
            {
                if(GameManager.inst.Score >= GameManager.inst.lvll.scoreABater)
                {
                    SoundManager.inst.Play(3);
                    winned?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
                else
                {
                    SoundManager.inst.Play(4);
                    losed?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
            }else if (GameManager.inst.lvll.tipo == 2)
            {
                if (GameManager.inst.Time <= GameManager.inst.lvll.TempoABater)
                {
                    SoundManager.inst.Play(3);
                    winned?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
                else
                {
                    SoundManager.inst.Play(4);
                    losed?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
            }else if (GameManager.inst.lvll.tipo == 3)
            {
                if (GameManager.inst.Score >= GameManager.inst.lvll.scoreABater && GameManager.inst.Time <= GameManager.inst.lvll.TempoABater)
                {
                    SoundManager.inst.Play(3);
                    winned?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
                else
                {
                    SoundManager.inst.Play(4);
                    losed?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
            }else if (GameManager.inst.lvll.tipo == 4)
            {
                if (GameManager.inst.Score >= GameManager.inst.lvll.scoreABater && GameManager.inst.Time <= GameManager.inst.lvll.TempoABater)
                {
                    SoundManager.inst.Play(3);
                    winned?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
                else
                {
                    SoundManager.inst.Play(4);
                    losed?.Invoke();
                    Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
                    Time.timeScale = 0;
                }
            }
        }
        else if (collision.gameObject.CompareTag("Delimiter"))
        {
            rbBola.velocity = new Vector2(0, 0);
            rbBola.bodyType = RigidbodyType2D.Static;
            transform.position = checkpoint;
            //imgDaSeta.gameObject.SetActive(true);
            seta.gameObject.SetActive(true);
            GameManager.inst.podeJogar = false;
            
            Camera.main.gameObject.GetComponent<camerashake>().setshakeDuration(0f);
            Time.timeScale = 0;
            morte?.Invoke();
        }
    }


    private float getAngulo()
    {
        return angulo;
    }
    IEnumerator AguardaParaAtivarShadowCastingMode()
    {
        yield return new WaitForSeconds(0.2f);
        tr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
    void setAngulo(float a)
    {
        this.angulo = a;
    }
    void rotacaoSeta()
    {
        //imgDaSeta.rectTransform.eulerAngles = new Vector3(0, 0, -angulo);
        seta.eulerAngles = new Vector3(0, 0, -angulo +90f);
    }
    private void teletransporta(bool v)
    {
        if (v)
        {
            tr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            this.transform.position = new Vector3(-localParaTp.transform.position.x, this.transform.position.y, this.transform.position.z);
            StartCoroutine("AguardaParaAtivarShadowCastingMode");
        }
        else
        {
            tr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            this.transform.position = new Vector3(localParaTp.transform.position.x, this.transform.position.y, this.transform.position.z);
            StartCoroutine("AguardaParaAtivarShadowCastingMode");
        }
    }
}
