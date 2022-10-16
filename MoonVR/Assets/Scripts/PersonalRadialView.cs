// using UnityEngine;

// namespace Microsoft.MixedReality.Toolkit.Utilities.Solvers
// {
//     public class PersonalRadialView : MonoBehaviour
//     {
//         public GameObject wristMenu;

//         [SerializeField]
//         [Tooltip("Min distance from eye to position element around, i.e. the sphere radius")]
//         private float minDistance = 1f;
//         public float value;

//         /// <summary>
//         /// Min distance from eye to position element around, i.e. the sphere radius.
//         /// </summary>
//         public float MinDistance
//         {
//             get => minDistance;
//             set => minDistance = value;
//         }

//         [SerializeField]
//         [Tooltip("Max distance from eye to element")]
//         private float maxDistance = 2f;

//         /// <summary>
//         /// Max distance from eye to element.
//         /// </summary>
//         public float MaxDistance
//         {
//             get => maxDistance;
//             set => maxDistance = value;
//         }
//     }
// }

using TMPro;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class PersonalRadialView : MonoBehaviour
{
    [SerializeField]
    public GameObject wristMenu;
    public TMP_Text Height_DeltaText;
    public TMP_Text Distance_DeltaText;
    float wristMaxDist;
    float wristMinDist;
    float wristMenuHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeScale(float scaleSize)
    {
        Vector3 wristMenu = transform.localScale;
        transform.localScale = new Vector3(wristMenu.x*scaleSize,wristMenu.y*scaleSize, wristMenu.z*scaleSize );
    }

    // public void distanceMenu(float delta)
    // {
    //     // access "radialsolver" and call 


    //     //Get function
    //     RadialView radialViewRef = wristMenu.GetComponent("RadialView") as RadialView;
    //     float currentMaxVal = radialViewRef.MaxDistance;
    //     float currentMinVal = radialViewRef.MinDistance;

    //     // Check if the user wants the menu to be closer or further away, to make sure the menu doesn't get stuck close to us
    //     if (delta > 0) {
    //         // Move the menu away from us
    //         // Later on, if we want a max menu distance value limit, we can set it here
    //         wristMaxDist = currentMaxVal - delta;
    //         wristMinDist = currentMinVal - delta;
    //     }   
    //     else if (delta < 0) {
    //         // Move the menu closer to us
    //         // Conditional statement to make sure the values don't go negative
    //         if (currentMaxVal > 0.21) {
    //             wristMaxDist = currentMaxVal + delta;
    //         }
    //         if (currentMinVal > 0.01) {
    //             wristMinDist = currentMinVal + delta;
    //         }
    //     }
    //     // wristMaxDist = currentMaxVal + delta;
    //     // wristMinDist = currentMinVal + delta;

    //     //Set Max & Min Distance based on our local wristMaxDist variable
    //     radialViewRef.MaxDistance = wristMaxDist;
    //     radialViewRef.MinDistance = wristMinDist;
    // } 


    public void distanceMenu(float delta)
    {
        // Get all property values from the RadialView class and assign them to "radialViewRef";
        // Get the MaxDistance and MinDistance values and assign them to our own variables
        RadialView radialViewRef = wristMenu.GetComponent("RadialView") as RadialView;
        float currentMaxVal = radialViewRef.MaxDistance;
        float currentMinVal = radialViewRef.MinDistance;

        // Have the delta value subtract from the distance values and assign them to floats
        wristMaxDist = currentMaxVal + delta;
        wristMinDist = currentMinVal + delta;

        // update the  Min and Max distances in the RadialView class
        radialViewRef.MaxDistance = wristMaxDist;
        radialViewRef.MinDistance = wristMinDist;

        //
        Distance_DeltaText.SetText(wristMaxDist.ToString());
    }

    public void verticalMenu(float delta)
    {
        // 
        RadialView radialViewRef = wristMenu.GetComponent("RadialView") as RadialView;
        float currentVerticalPos = radialViewRef.FixedVerticalPosition;
        
        // 
        wristMenuHeight = currentVerticalPos - delta;
        radialViewRef.FixedVerticalPosition = wristMenuHeight;

        // 
        Height_DeltaText.SetText(wristMenuHeight.ToString());
    }

    // public void verticalMenuHigher(float delta)
    // {
    //     //
    //     RadialView radialViewRef = wristMenu.GetComponent("RadialView") as RadialView;
    //     float currentVerticalPos = radialViewRef.FixedVerticalPosition;
        
    //     // 
    //     wristMenuHeight = currentVerticalPos + delta;
    //     radialViewRef.FixedVerticalPosition = wristMenuHeight;
    // }
}
    // public void closerMenu(float delta)
    // {
    //     // access "radialsolver" and call 
    //     float valueIntial = GetComponent<solver>().maxDistance;
        
    //     wristMenu.RadialView
    //     go.get.solver.RadialView.maxDistance ;
    //     delta = set.solver.RadialView.maxDistance ;
    // } 

