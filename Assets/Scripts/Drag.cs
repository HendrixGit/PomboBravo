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

    void Start()
    {
        drag = GetComponent<Collider2D>();
        SetupLine();

        leftCatapultRay = new Ray(lineFront.transform.position, Vector3.zero);
        passaroCol      = GetComponent<CircleCollider2D>();

        spring    = GetComponent<SpringJoint2D>();
        passaroRB = GetComponent<Rigidbody2D>();

        clicked = false;

        catapult = spring.connectedBody.transform;
        rayToMT  = new Ray(catapult.position, Vector3.zero);
    }

    void Update()
    {
        LineUpdate();
        SpringEffect();

        preVel = passaroRB.velocity;

#if UNITY_ANDROID

        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
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
    }

    void SetupLine() {
        lineFront.SetPosition(0, lineFront.transform.position);
        lineBack.SetPosition(0, lineBack.transform.position);
    }

    void LineUpdate() {

        catapultToBird = transform.position - lineFront.transform.position;
        leftCatapultRay.direction = catapultToBird;

        pointL = leftCatapultRay.GetPoint(catapultToBird.magnitude + passaroCol.radius);//magnitude comprimento vetor somar valor de um raio para cobrir o personagem

        lineFront.SetPosition(1, pointL);
        lineBack.SetPosition(1, pointL);
    }

    void SpringEffect() {
        if (spring.enabled) {
            if (passaroRB.isKinematic == false) {
                if (preVel.sqrMagnitude > passaroRB.velocity.sqrMagnitude) {//retorno do comprimente do quadrado do vetor
                    lineFront.enabled = false;
                    lineBack.enabled = false;
                    //Destroy(spring);
                    spring.enabled = false;
                    passaroRB.velocity = preVel;//ajuste de velocidade apos a destruicao do spring

                }
            }
        }
    }

    void MataPassaro() {
        if (passaroRB.velocity.magnitude == 0) {
            StartCoroutine(TempoMorte());
        }
    }

    IEnumerator TempoMorte() {
        yield return new WaitForSeconds(5);
        Instantiate(bomb, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }

    //mouse
    void Dragging() {
        Vector3 mouseWP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWP.z = 0f;

        catapultToBird = mouseWP - catapult.position;
        if (catapultToBird.sqrMagnitude > 9f) {//3 para 3*3 valor limite
            rayToMT.direction = catapultToBird;
            mouseWP = rayToMT.GetPoint(3f);
        }

        transform.position = mouseWP;

        MataPassaro();
    }

    void OnMouseDown()
    {
        clicked = true;
    }

    void OnMouseUp()
    {
        passaroRB.isKinematic = false;
        clicked = false;
    }

}
