using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JointComparer : MonoBehaviour
{
    public Transform savedPose; // Reference to the saved pose GameObject
    public Transform currentPose; // Reference to the current pose GameObject
    public float threshold = 0.1f; // Threshold for joint position comparison
    public GameObject text; // Reference to the text GameObject

    void Update()
    {
        if(savedPose == null)
        {
            ChooseRandomPose();
        }


        bool allJointsClose = true; // Flag to track if all joints are close to their saved positions

        // Loop through all child joints of the saved pose
        foreach (Transform savedJoint in savedPose)
        {
            // Find the corresponding joint in the current pose
            Transform currentJoint = FindJointByName(savedJoint.name, currentPose);

            if (currentJoint != null)
            {
                // Compare the positions of the joints
                float distance = Vector3.Distance(savedJoint.position, currentJoint.position);

                // If the distance is within the threshold, they are considered close
                if (distance > threshold)
                {
                    allJointsClose = false; // At least one joint is not close to its saved position
                    Debug.Log(savedJoint.name + " is not close to its saved position.");
                }
            }
            else
            {
                Debug.LogWarning("Joint " + savedJoint.name + " not found in current pose.");
            }
        }

        if (allJointsClose)
        {
            Debug.Log("All joints are close to their saved positions.");
            savedPose.gameObject.SetActive(false);
            savedPose = null;
        }
    }

    // Function to find a joint by name under a parent transform
    Transform FindJointByName(string name, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null;
    }

    public void ChooseRandomPose()
    {
        //get a random pose from the saved poses in the poses array
        int randomPoseIndex = Random.Range(0, gameObject.GetComponent<Poses>().poses.Length);
        savedPose = gameObject.GetComponent<Poses>().poses[randomPoseIndex].transform;
        savedPose.gameObject.SetActive(true);
        text.GetComponent<Text>().text = "Pose to make: " + savedPose.name;
        Debug.Log("Pose to make: " + savedPose.name);
    }
}
