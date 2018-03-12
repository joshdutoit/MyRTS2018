using System.Collections;
using System.Collections.Generic;
using MyRTS2018;
using UnityEngine;

namespace MyRTS2018
{

    public class camscript : MonoBehaviour
    {

        float speed = 15.0f;
        //   [SerializeField] private float verticalSpeed = 1f;
        //  [SerializeField] private float horizontalSpeed = 1f;
        // [SerializeField] private float mapSizeX = 1f;
        //  [SerializeField] private float mapSizeZ = 1f;
        //  [SerializeField] private float mapSizeY = 1f;
        // Use this for initialization
        void Start()
        {
            transform.position = new Vector3(-0.0f, -0.0f, -100.0f);
        }

        // Update is called once per frame
        void Update()
        {
             transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0.0f);         
        }
    }
}