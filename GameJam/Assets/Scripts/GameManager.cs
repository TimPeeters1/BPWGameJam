using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum playerID
    {
        player1,
        player2
    }

    public playerID currentPlayer;

    bool doingTurn;

    public GameObject selectedShip;
    public Camera cam;

    public GameObject Player1UI;
    public GameObject Player2UI;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Player1UI.SetActive(true);
        Player2UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SelectShip();
    }

    void SelectShip()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.tag == "Spaceship" && !doingTurn && hit.collider.GetComponent<TurnbasedSpaceship>().playerID == currentPlayer)
            {
                hit.collider.GetComponent<TurnbasedSpaceship>().isSelected = true;
                selectedShip = hit.collider.gameObject;
                doingTurn = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.tag != "Spaceship" && doingTurn)
            {
                selectedShip.GetComponent<TurnbasedSpaceship>().isSelected = false;
                selectedShip = null;
                doingTurn = false;
            }
        }
    }

    public void doTurn()
    {
        switch (currentPlayer)
        {
            case playerID.player1:
                currentPlayer = playerID.player2;
                Player1UI.SetActive(false);
                Player2UI.SetActive(true);
                break;
            case playerID.player2:
                currentPlayer = playerID.player1;
                Player1UI.SetActive(true);
                Player2UI.SetActive(false);
                break;
            default:
                break;
        }

        selectedShip.GetComponent<TurnbasedSpaceship>().isSelected = false;
        selectedShip = null;
        doingTurn = false;

    }


}
