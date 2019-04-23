using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Collider2D drag;
    public  LayerMask layer;
    [SerializeField]
    private bool clicked;
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

    void Start()
    {
        drag = GetComponent<Collider2D>();
        SetupLine();
 
        leftCatapultRay = new Ray(lineFront.transform.position, Vector3.zero);
        passaroCol = GetComponent<CircleCollider2D>();

        spring    = GetComponent<SpringJoint2D>();
        passaroRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LineUpdate();
        SpringEffect();

        preVel = passaroRB.velocity;

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
                    transform.position = tpos;
                    LineUpdate();
                }
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                passaroRB.isKinematic = false;
                clicked = false;
                MataPassaro();
            }
        }
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
        if (spring != null) {
            if (passaroRB.isKinematic == false) {
                if (preVel.sqrMagnitude > passaroRB.velocity.sqrMagnitude) {//retorno do comprimente do quadrado do vetor
                    lineFront.enabled = false;
                    lineBack.enabled  = false;
                    //Destroy(spring);
                    spring.enabled = false;
                    passaroRB.velocity = preVel;//ajuste de velocidade apos a destruicao do spring
                    
                }
            }
        }
    }

    void MataPassaro() {
        if (passaroRB.velocity.magnitude < 0.5f) {
            StartCoroutine(TempoMorte());
        }
    }

    IEnumerator TempoMorte() {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
