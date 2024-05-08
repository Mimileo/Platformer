using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLogic : MonoBehaviour
{
    public GameObject package;
    public GameObject parachute;
    public float deploymentHeight = 7.5f;
    public float parachuteDrag = 5f;
    public float landingHeight = 1f;
    public float chuteOpenDuration = 0.5f;

    float originalDrag;

    // Start is called before the first frame update
    void Start()
    {
        // originalDrag = package.GetComponent<Rigidbody>().drag;
        // parachute.SetActive(false);
        parachute.SetActive(true);
        StartCoroutine(ExpandParachute());
    }

    IEnumerator ExpandParachute()
    {
        parachute.transform.localScale = Vector3.zero;
        float timeElapsed = 0f;
      

        while (timeElapsed < chuteOpenDuration)
        {
            float newScale = timeElapsed / chuteOpenDuration;
            parachute.transform.localScale = new Vector3(newScale, newScale, newScale);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        parachute.transform.localScale = Vector3.one;
        Debug.Log("about to open parachute");
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("it's open");
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("We landed");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Physics.Raycast(package.transform.position, Vector3.down, out var hitInfo, deploymentHeight))
        {
            //change the drag value on the package's rigidbody
            package.GetComponent<Rigidbody>().drag = parachuteDrag;
            parachute.SetActive(true);
            // package position, Vector3.down, zcolor.cyan
            Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.red);
            Debug.Log($"distance: {hitInfo.distance}");

            if (hitInfo.distance < landingHeight)
            {
                Debug.Log("Landed");
                parachute.SetActive(false);
            }
        }*/
    /*
        else
        {
            parachute.SetActive(false);
            package.GetComponent<Rigidbody>().drag = parachuteDrag;
            Debug.DrawRay(package.transform.position, Vector3.down * deploymentHeight, Color.cyan);

        }
        */
    }
}
