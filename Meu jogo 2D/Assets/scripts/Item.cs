﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public GameObject esquerdaItem;
    public GameObject direitaItem;
    public GameObject tinta;
    

    private Pontuacao pontos;
    private Vidas vida;

    public float forca;
    public float torque;
    private bool isdead;
    private Vector3 screen;

    public float alpha;
    public float red;
    public float green;
    public float blue;

    public float minY;
    public float maxY;

    private float rotDirecao = 50;


    // Use this for initialization
    void Start () {
        vida = GameObject.FindGameObjectWithTag("Vidas").GetComponent<Vidas>() as Vidas;
        pontos = GameObject.FindGameObjectWithTag("Pontos").GetComponent<Pontuacao>() as Pontuacao;
        minY = GerenciarCamera.MinY();
        maxY = GerenciarCamera.MaxY();
        
    }
	
	// Update is called once per frame
	void Update () {
        Remover();
	}

    public void Remover()
    {
        screen = Camera.main.WorldToScreenPoint(transform.position);
        if (isdead && screen.y<minY)
        {
            Destroy(gameObject);
            if (!vida.Remover())
            {
                GetComponent<Collider2D>().enabled = false;
                pontos.Recorde();
                
            }
        }
        else
        {
            isdead = true;
            
        }
       
    }

    
    public void InstanciarDestruir()
    {
        GameObject tempItem = null;
        GameObject tempTinta = null;

        tempItem = Instantiate(esquerdaItem, transform.position, transform.rotation) as GameObject;
        tempItem.GetComponent<Rigidbody2D>().AddForce(-transform.right * forca);
        
        tempItem.GetComponent<Rigidbody2D>().AddTorque(-torque);

        tempItem = Instantiate(direitaItem, transform.position, transform.rotation) as GameObject;
        tempItem.GetComponent<Rigidbody2D>().AddForce(transform.right * forca);
        tempItem.GetComponent<Rigidbody2D>().AddTorque(torque);

        tempTinta = Instantiate(tinta, new Vector2(transform.position.x, transform.position.y), transform.rotation) as GameObject;

        tempTinta.GetComponent<Tinta>().SetColor(red,alpha);

        Destroy(gameObject);

    }
}
