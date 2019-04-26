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
    public int shipDamage;
    [SerializeField] bool isFighter;

    [Space]
    public bool isSelected;

    [Space]
    [SerializeField] GameObject selectionUI;
    [SerializeField] GameObject ExplosionEffect;

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

        if(shipHealth <= 0)
        {
            Die();
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
        if(other.gameObject.tag == "Spaceship" && isFighter)
        {
            other.gameObject.GetComponent<TurnbasedSpaceship>().DoDamage(shipDamage);
            if (isFighter)
            {
                shipHealth -= shipDamage;
            }
        }
    }

    public void DoDamage(int damage)
    {
        shipHealth -= damage;
        
        //Debug.Log("Damage");
    }

    void Die()
    {
        GameObject explosion = Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
