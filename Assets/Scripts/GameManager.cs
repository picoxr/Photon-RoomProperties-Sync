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

using Pico.Platform;
using Pico.Platform.Models;
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
    private User myUser;
    
    void Start()
    {
        playerTank = GameObject.FindGameObjectWithTag("Player");
        WeakWallCount = GameObject.FindGameObjectsWithTag("WeakWall").Length;
    }
    

    void Update()
    {
    
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
       // Debug.Log("PlayerProfile is null");       
    }

    
    
}
