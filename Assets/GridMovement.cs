using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridMovement : MonoBehaviour
{
    public bool isMoving;
    public float timeToMove = 0.2f;
    public Vector3 origem, destino;

    public Vector3 startPosition;
    public bool death;
    public bool isVoiding;
    public bool AnotherLogic;
    public Tilemap background;
    public Tilemap wall;
    public Tilemap voiddeath;

    public SpriteRenderer sr;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        startPosition = transform.position;

    }
    void Awake()
    {
    }
    void Update()
    {
        ControlLogic();
        DeathLogic();
        // Screen.SetResolution(10,10,false,30);
    }

    private void DeathLogic(){
        if (!death)
        {
            if (isVoiding)
            {
                sr.color = Color.yellow;
            }
            else
            {
                sr.color = Color.white;
            }
        }
        else
        {
            sr.color = Color.red;
            StartCoroutine(Respawn());
        }
    }
    private void ControlLogic(){
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !isMoving)
        {
            MovePlayerLogic(Vector3.up);
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !isMoving)
        {
            MovePlayerLogic(Vector3.left);
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !isMoving)
        {
            MovePlayerLogic(Vector3.down);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !isMoving)
        {
            MovePlayerLogic(Vector3.right);
        }
    }
    private IEnumerator Respawn()
    {
        isMoving = true;
        float frame = 0;
        while (frame < 1f)
        {
            frame += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
        sr.color = Color.white;
        StopAllCoroutines();
        death = false;
        AnotherLogic = false;
        isVoiding = false;
        isMoving = false;
    }


    private void MovePlayerLogic(Vector3 direction)
    {
        if (canMove(direction))
        {

            StartCoroutine(Move(direction));
            GameController.instance.StepDecrease();
        }
        else
        {
            if (!isVoiding)
            {
                StartCoroutine(DontMove(direction));
            }
            else
            {

                StartCoroutine(VerifyVoidingAgain(direction));
            }

        }

    }

    private IEnumerator VerifyVoidingAgain(Vector3 direction)
    {
        Vector3Int grigpos = background.WorldToCell(transform.position + direction);
        if (voiddeath.HasTile(grigpos))
        {
            if (AnotherLogic)
            {
                Debug.Log("Ainda está rodando vc sfd");
                death = true;
            }
        }

        yield return null;
    }
    private IEnumerator DontMove(Vector3 direction)
    {
        Vector3Int grigpos = background.WorldToCell(transform.position + direction);
        Debug.Log("Não pode mover para: " + direction);
        if (voiddeath.HasTile(grigpos) && !isVoiding)
        {

            isVoiding = true;
            Debug.Log(isVoiding);

            float timevoiding = 0;
            while (timevoiding < 0.1f)
            {
                timevoiding += Time.deltaTime;
                Debug.Log(timevoiding);
                if (death)
                {
                    StopCoroutine(DontMove(direction));
                }
                yield return null;
            }
            timevoiding = 0;
            AnotherLogic = true;
            while (timevoiding < 2f)
            {
                timevoiding += Time.deltaTime;
                Debug.Log(timevoiding);
                if (death)
                {
                    StopCoroutine(DontMove(direction));
                }
                yield return null;
            }
            AnotherLogic = false;
            isVoiding = false;
            Debug.Log(isVoiding);
        }


    }

    private IEnumerator Move(Vector3 direction)
    {
        Debug.Log("Movendo para+ " + direction);
        isMoving = true;
        float elapseTime = 0;
        origem = transform.position;
        destino = origem + direction;

        while (elapseTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origem, destino, elapseTime / timeToMove);
            elapseTime += Time.deltaTime;
            yield return null;
        }
        transform.position = destino;
        isMoving = false;
    }

    private bool canMove(Vector3 direction)
    {
        Vector3Int grigpos = background.WorldToCell(transform.position + direction);
        if (wall.HasTile(grigpos))
        {
            return false;
        }
        if (voiddeath.HasTile(grigpos))
        {
            return false;
        }
        Debug.Log("Pode");
        return true;
    }
}
