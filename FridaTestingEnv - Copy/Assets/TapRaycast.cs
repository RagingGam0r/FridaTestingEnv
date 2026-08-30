using UnityEngine;

public class TapRaycast : MonoBehaviour
{
    string handleClick() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            // Debug.Log(hit.collider.gameObject.name);
            return hit.collider.gameObject.name;
        else
            // Debug.Log("Clicked but not hit");
            return "Clicked but not hit";
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(handleClick());
        }
    }
}