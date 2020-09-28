using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeltObject : MonoBehaviour
{
    public ObjectProperties objectProperties;
    private Slider timeSlider;

    private GameController gameController;
    public GameController GC
    {
        set
        {
            if (gameController == null) 
                gameController = value;
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        timeSlider = gameController.timeSlider;
        objectProperties = gameController.SelectedObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameController.meltPhase) {
            //print(timeSlider.value);
            if (transform.position.y >= 0) {
                transform.position = new Vector3(transform.position.x, (timeSlider.value * objectProperties.meltFactor), transform.position.z);
            }
            
        }
    }
    
}
