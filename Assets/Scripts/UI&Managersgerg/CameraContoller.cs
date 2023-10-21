using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was based of code in this video (Pandemonium (2021). Unity 2D Platformer for Complete Beginners - #6 CAMERA MOVEMENT. YouTube. Available at: https://www.youtube.com/watch?v=PA5DgZfRsAM&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7 [Accessed 18 Oct. 2023].)
public class CameraContoller : MonoBehaviour
{
    // Creates a transform in a serialize field so that the players transform can be attached in engine
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        // Sets the position of the camera to match that of the player so it followes them
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); 
    }
}