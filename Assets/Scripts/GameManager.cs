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

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    [Header("Arena Objects")]
    private GameObject playerTank;

    public Text CurrentPlayerNameText;
    public Text TextCurrentScore;

    public string UserOpenId;
    public string UserName;
    
    public static int WeakWallCount = 0;
    //private bool isLoginFinished = false;

    //Tiger
    /*private SendUserMessage m_PicoUserMessage;*/
    

    void Start()
    {
        

       /* m_PicoUserMessage = GameObject.Find("PicoPayment").GetComponent<SendUserMessage>();*/
        
        playerTank = GameObject.FindGameObjectWithTag("Player");
        /*m_PicoUserMessage.DELEGATE_GET_USER_INFO_RESULT += LoginToGameService;*/

        /*loadingScreen.SetActive(true);*/
        
        WeakWallCount = GameObject.FindGameObjectsWithTag("WeakWall").Length;

        // first login to Pico account
        /*Unity.XR.PXR.LoginSDK.Login();*/


        // login the with Pico openID, currently the input value is fake one        
    }
    

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (isPaused)
        //    {
        //        GameUIScreen.SetActive(true);
        //        pauseScreen.SetActive(false);
        //        isPaused = false;
        //        Time.timeScale = 1.0f;
        //    }
        //    else
        //    {
        //        GameUIScreen.SetActive(false);
        //        pauseScreen.SetActive(true);
        //        isPaused = true;
        //        Time.timeScale = 0.0f;
        //    }
        //}
    }

    public void LoginToGameService(string openid, string name)
    {
        this.UserOpenId = openid;
        this.UserName = name;
        CurrentPlayerNameText.text = this.UserName;
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MainScene"); 
        Time.timeScale = 1.0f;
        Rigidbody playerRB = playerTank.GetComponent<Rigidbody>();
        playerRB.isKinematic = true;
        playerRB.isKinematic = false;
    }

    private void UpdatePlayerName()
    {
        
        //string display_name = null;
        
        //if (result.InfoResultPayload.PlayerProfile != null)
        //{
        //    display_name = result.InfoResultPayload.PlayerProfile.DisplayName;
        //}
        
        //if (display_name == null)
        //{
            
        //}

        Debug.Log("PlayerProfile is null");
        //Display name must be between 3 and 25 characters
        

    }

    
    
}
