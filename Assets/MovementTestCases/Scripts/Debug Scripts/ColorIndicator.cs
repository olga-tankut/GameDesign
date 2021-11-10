using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIndicator : MonoBehaviour
{
    [SerializeField]private Material move;
    [SerializeField]private Material dash;
    [SerializeField]private Material slide;
    [SerializeField]private Material jump;
    [SerializeField]private Material error;
    private SpriteRenderer spriteRenderer;
    //private bool isOnGround = false;
    [SerializeField]private bool ColorCodingIsActive = false;
    private KeyCode overRideKey;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI() 
    {
        ProcessKeyPress(Event.current);
    }

    private void ProcessKeyPress(Event e)
    {
        if(e.isKey && ColorCodingIsActive)
        {
            switch(e.keyCode)
            {
                case KeyCode.A:
                case KeyCode.D:
                case KeyCode.LeftArrow:
                case KeyCode.RightArrow:
                    ChangeMaterial(move);
                    break;

                case KeyCode.S:
                case KeyCode.DownArrow:
                    ChangeMaterial(slide);
                    break;

                case KeyCode.LeftShift:
                    ChangeMaterial(dash);
                    break;

                case KeyCode.Space:
                    ChangeMaterial(jump);
                    break;

                default:
                    Debug.Log("Unused key was pressed: " + e.keyCode);
                    ChangeMaterial(error);
                    break;
            }
        }
    }

    private void ChangeMaterial(Material material)
    {
        spriteRenderer.material = material;
    }
}
