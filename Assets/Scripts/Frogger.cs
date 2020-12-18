using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Frogger : MonoBehaviour
{
    public GameObject car;
    public GameObject Truck;
    public GameObject player;
    public Transform carStart;
    public Transform carEnd;


    public List<GameObject> Vehicles;

    // Start is called before the first frame update
    void Start()
    {
        Vehicles.Add(car);
        Vehicles.Add(Truck);

        car = Instantiate(car, carStart.position, Quaternion.identity);
    }
    
    // Update is called once per frame
    void Update()
    {
       

    }
}
