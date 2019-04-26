using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TurnbasedSpaceship : MonoBehaviour
{
    [Space]
    public GameManager.playerID playerID;

    [Space]
    public float moveRange;

    [Space]
    public int shipHealth;

    [Space]
    public bool isSelected;

    [Space]
    [SerializeField] GameObject selectionUI;

    GameManager mgr;
    NavMeshAgent agent;
    Camera cam;
    
    [SerializeField] float Distance;

    // Start is called before the first frame update
    void Start()
    {
        mgr = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        selectionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DoMove();

        if (isSelected)
        {
            selectionUI.SetActive(true);
        }
        else
        {
            selectionUI.SetActive(false);
        }
    }

    void DoMove()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            Distance = Vector3.Distance(transform.position, hit.point);

            if (Input.GetKeyDown(KeyCode.Mouse1) && isSelected && Distance < moveRange)
            {
                agent.destination = hit.point;

                mgr.doTurn();
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag)
    }
}
