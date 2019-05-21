using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Collider2D drag;
    public LayerMask layer;
    [SerializeField]
    private bool clicked = false;

    private Touch touch;

    public LineRenderer lineFront;
    public LineRenderer lineBack;

    private Ray leftCatapultRay;
    private CircleCollider2D passaroCol;
    private Vector2 catapultToBird;
    private Vector3 pointL;

    private SpringJoint2D spring;
    private Vector2 preVel;
    private Rigidbody2D passaroRB;
    [SerializeField]
    private GameObject bomb;

    //limite elastico
    private Transform catapult;
    private Ray rayToMT;

    //Rastro
    private TrailRenderer rastro;

    public Rigidbody2D catapultRB;
    public bool estouPronto = false;

    public AudioSource audioPassaro;
    public GameObject AudioMortePassaro;

    void Awake() {

        spring = GetComponent<SpringJoint2D>();
        lineFront  = GameObject.FindWithTag("LF").GetComponent<LineRenderer>();
        lineBack   = GameObject.FindWithTag("LB").GetComponent<LineRenderer>();
        catapultRB = GameObject.FindWithTag("LB").GetComponent<Rigidbody2D>();
        audioPassaro = GetComponent<AudioSource>();
        spring.connectedBody = catapultRB;

        drag = GetComponent<Collider2D>();

        leftCatapultRay = new Ray(lineFront.transform.position, Vector3.zero);
        passaroCol = GetComponent<CircleCollider2D>();

        
        passaroRB = GetComponent<Rigidbody2D>();

        clicked = false;

        catapult = spring.connectedBody.transform;
        rayToMT = new Ray(catapult.position, Vector3.zero);

        rastro = GetComponentInChildren<TrailRenderer>();
        rastro.enabled = false;
    }

    void Start()
    {
        SetupLine();
    }

    void Update()
    {
    
        LineUpdate();
        SpringEffect();
 
        preVel = passaroRB.velocity;

#if UNITY_ANDROID

        if (Input.touchCount > 0 && estouPronto) {
            touch = Input.GetTouch(0); 
            Vector2 wp       = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, Mathf.Infinity, layer.value);

            if (hit.collider != null) {
                clicked = true;
            }

            if (clicked) {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
                    Vector3 tpos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                    catapultToBird = tpos - catapult.position;
                    if (catapultToBird.sqrMagnitude > 9f)
                    {//3 para 3*3 valor limite
                        rayToMT.direction = catapultToBird;
                        tpos = rayToMT.GetPoint(3f);
                    }
                    transform.position = tpos;
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                passaroRB.isKinematic = false;
                clicked = false;
                MataPassaro();
            }
        }
#endif

#if UNITY_EDITOR

        if (clicked)
        {
            Dragging();
        }
#endif
        MataPassaro();

        if (!passaroRB.isKinematic) {
            Vector3 posCam = Camera.main.transform.position;
            posCam.x = transform.position.x;
            posCam.x = Mathf.Clamp(posCam.x, GAME_MANAGER.instance.objE.position.x, GAME_MANAGER.instance.objD.position.x);
            Camera.main.transform.position = posCam;
        }    


    }

    void SetupLine() {
        lineFront.SetPosition(0, lineFront.transform.position);
        lineBack.SetPosition(0, lineBack.transform.position);
    }

    void LineUpdate() {

        if (transform.name == GAME_MANAGER.instance.nomePassaro) {
            catapultToBird = transform.position - lineFront.transform.position;
            leftCatapultRay.direction = catapultToBird;

            pointL = leftCatapultRay.GetPoint(catapultToBird.magnitude + passaroCol.radius);//magnitude comprimento vetor somar valor de um raio para cobrir o personagem

            lineFront.SetPosition(1, pointL);
            lineBack.SetPosition(1, pointL);
        }
    }

    void SpringEffect() {
        if (spring.enabled && GAME_MANAGER.instance.passarosEmCena > 0) {
            if (passaroRB.isKinematic == false) {
                if (preVel.sqrMagnitude > passaroRB.velocity.sqrMagnitude) {//retorno do comprimente do quadrado do vetor
                    lineFront.enabled = false;
                    lineBack.enabled  = false;
                    //Destroy(spring);
                    spring.enabled = false;
                    passaroRB.velocity = preVel;//ajuste de velocidade apos a destruicao do spring

                }
            }
            else if (passaroRB.isKinematic == true && transform.position == GAME_MANAGER.instance.pos.position) {
                lineFront.enabled = true;
                lineBack.enabled  = true;
            }
        }
    }

    void MataPassaro() {

        if (passaroRB.velocity.magnitude == 0 && !passaroRB.isKinematic) {
            StartCoroutine(TempoMorte());
        }
    }

    IEnumerator TempoMorte() {
        yield return new WaitForSeconds(2);
        Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Instantiate(AudioMortePassaro, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
        GAME_MANAGER.instance.passarosNum    -= 1;
        GAME_MANAGER.instance.passarosEmCena  = 0;
        estouPronto = false;
        GAME_MANAGER.instance.passaroLancado = false;
    }

    //mouse
    void Dragging() {

        if (passaroRB.isKinematic) {
            Vector3 mouseWP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWP.z = 0f;

            catapultToBird = mouseWP - catapult.position;
            if (catapultToBird.sqrMagnitude > 9f)
            {//3 para 3*3 valor limite
                rayToMT.direction = catapultToBird;
                mouseWP = rayToMT.GetPoint(3f);
            }

            transform.position = mouseWP;

            MataPassaro();
        }

        
    }

    void OnMouseDown()
    {
        if (transform.position == GAME_MANAGER.instance.pos.position) {
            clicked = true;
            rastro.enabled = false;
            estouPronto = true;
        }
        
    }

    void OnMouseUp()
    {
        if (estouPronto) {
            passaroRB.isKinematic = false;
            clicked = false;
            rastro.enabled = true;
            GAME_MANAGER.instance.passaroLancado = true;
            audioPassaro.Play();
        }

    }

}
