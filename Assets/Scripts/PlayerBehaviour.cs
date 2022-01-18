/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviourPunCallbacks
{
    public float scrollSpeed;
    public float speed = 12f;
    public float turnSpeed = 180f;
    public AudioSource movementAudio;
    public AudioClip engineIdling;
    public AudioClip engineDriving;
    public AudioClip exploding;
    public Renderer tankRenderer;
    private Rigidbody playerRigidbody;
    private float movementInputValue;
    private float turnInputValue;
    private Material tankTreadMaterial;
    public float textureOffset;
    public GameObject barrel;
    public GameObject projectile;
    public bool indexBool = true;


    private Vector2 m_JoystickAxisValue = Vector2.zero;

    public void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        tankTreadMaterial = tankRenderer.materials[0];
    }

    public void Start()
    {
        textureOffset = tankTreadMaterial.GetTextureOffset("_MainTex").x;
    }

    public void Update()
    {
        if (photonView.IsMine)
        {
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out m_JoystickAxisValue);
            movementInputValue = m_JoystickAxisValue.y;

            float offset = Time.deltaTime * (scrollSpeed * movementInputValue);
            textureOffset += offset;
            tankTreadMaterial.SetTextureOffset("_MainTex", new Vector2(textureOffset, 0));

            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out m_JoystickAxisValue);
            turnInputValue = m_JoystickAxisValue.x;

            if (XR_GetKeyDown(XRNode.RightHand, CommonUsages.triggerButton))
            {
                PhotonNetwork.Instantiate("Shell", barrel.transform.position, barrel.transform.rotation);
                Invoke("ReverseBool", 1);
            }

        }
        EngineAudio();
    }

    public void EngineAudio()
    {
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            if (movementAudio.clip == engineDriving)
            {
                movementAudio.clip = engineIdling;
                movementAudio.Play();
            }
        }
        else
        {
            if (movementAudio.clip == engineIdling)
            {
                movementAudio.clip = engineDriving;
                movementAudio.Play();
            }
        }
    }

    public void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Turn();
            Move();
        }
    }

    public void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }

    public void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
    }

    public void ReverseBool()
    {
        indexBool = true;
    }

    public bool XR_GetKeyDown(XRNode node, InputFeatureUsage<bool> usage)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        bool isDone;

        if (device.TryGetFeatureValue(usage, out isDone) && isDone)
        {
            if (indexBool)
            {
                indexBool = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {

            return false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillingPlane" || other.gameObject.tag == "KillingMine")
        {
            movementAudio.clip = exploding;
            movementAudio.Play();

            if (photonView.IsMine)
            {
                Transform canvas = transform.GetChild(2);
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
                //PhotonNetwork.LeaveRoom();
            }
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            //Hashtable "CustomProperties" only contains all of the changed walls.
            Hashtable propertiesToGet = PhotonNetwork.CurrentRoom.CustomProperties;

            for (int i = 0; i < WallManager.wallDic.Count; i++)
            {
                if (propertiesToGet.ContainsKey("FragileWall " + i.ToString()))
                {
                    object value = propertiesToGet["FragileWall " + i.ToString()];
                    WallManager.ApplyVisibility("FragileWall " + i.ToString(), (bool)value);
                }
                else
                {
                    Debug.Log("The unchanged walls are FragileWall " + i);
                }
            }
        }
    }

    /*public GameObject tankTurret;
    public float turnSpeed = 180f;
    private float turnInputValue;

    void Update()
    {
        if (photonView.IsMine)
        {
            turnInputValue = Input.GetAxis("TurretRotate");
            RotateTurret();
        }
    }

    void RotateTurret()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, 0f, turn);
        tankTurret.transform.rotation = (tankTurret.transform.rotation * turnRotation);
    }*/
}
