using UnityEngine;
using System.Collections;
using System.Reflection;
using System.IO;
using System;
public class RefDataBase :MonoBehaviour {

	// Use this for initialization
	void Start () {


        Type type = "RefDataBase".GetType();

        Debug.Log(type);
	}
	


	// Update is called once per frame
	void Update () {
	
	}


}
