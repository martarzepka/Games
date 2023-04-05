using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.IO;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class Game : MonoBehaviour
{
    public Canvas menu;
    public Canvas nickInput;
    public Canvas game;
    
    public Button A, B, C, D, ffButton, phoneButton;
    public TextMeshProUGUI textA, textB, textC, textD, textQ, message, hint, prompt;

    public Button k0, k1, k2, k3, k4, k5, k6, k7, k8, k9, k10, k11;
    private Button[] amountButtons = new Button[12];

    private Color red = Color.red, green = Color.green, blue, orange, disableHelpButtonColor, enableHelpButtonColor;

    private bool ffUsed = false, phoneUsed = false;
    
    private int level;
    private string ans, nick = "";
    private string[] amount= {"0","500","1000","2000","5000","10000","20000","50000","75000","150000","250000","500000","1000000"};

    

    // Start is called before the first frame update
    void Start()
    {
        menu.enabled = true;
        nickInput.enabled = false;
        game.enabled = false;
        level = 0;
        ColorBlock colors = A.colors;
        blue = colors.disabledColor;
        orange = colors.selectedColor;

        colors = ffButton.colors;
        disableHelpButtonColor = colors.disabledColor;
        enableHelpButtonColor = colors.normalColor;

        amountButtons[0] = k0;
        amountButtons[1] = k1;
        amountButtons[2] = k2;
        amountButtons[3] = k3;
        amountButtons[4] = k4;
        amountButtons[5] = k5;
        amountButtons[6] = k6;
        amountButtons[7] = k7;
        amountButtons[8] = k8;
        amountButtons[9] = k9;
        amountButtons[10] = k10;
        amountButtons[11] = k11;

    }

    IEnumerator ToMenu()
    {
        yield return new WaitForSeconds(5f);
        amountButtons[level - 1].interactable = true;
        menu.enabled = true;
        nickInput.enabled = false;
        game.enabled = false;
    }

    public void ToNickInput()
    {
        if(level!=0)
            amountButtons[level - 1].interactable = true;

        menu.enabled = false;
        nickInput.enabled = true;
        game.enabled = false;
    }

    public void ToGame()
    {
        menu.enabled = false;
        nickInput.enabled = false;
        game.enabled = true;
    }

    public void NewGame()
    {
        if (nick != "")
        {
            ToGame();
            level = 0;

            ColorBlock colors = ffButton.colors;
            colors.disabledColor = enableHelpButtonColor;
            ffButton.colors = colors;

            colors = phoneButton.colors;
            colors.disabledColor = enableHelpButtonColor;
            phoneButton.colors = colors;

            ffButton.interactable = true;
            phoneButton.interactable = true;
            ffUsed = false;
            phoneUsed = false;
            NewQuestion();
        }
        else
        {
            ToNickInput();
        }
    }

    public void Close()
    {
        message.text = "Rezygnujesz z gry \n Twoja wygrana to " + amount[level-1] + " zł";
        StartCoroutine(ToMenu());
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void LoadNick(string n)
    {
        nick = n.Trim();
    }

    private void DisplayMessage()
    {
        amountButtons[level - 1].interactable = false;
        if(level>1)
            amountButtons[level - 2].interactable = true;

        message.text = nick + " oto twoje pytanie za " + amount[level] + "zł";
        if(level == 2 || level == 7)
            message.text += "\nJeśli odpowiesz na to pytanie będziesz mieć gwarantowane " + amount[level] + " zł wygranej!";

        hint.text = "";
    }

    private void DefaultButtonSettings(Button button)
    {
        ColorBlock colors = button.colors;
        colors.disabledColor = blue;
        button.colors = colors;
        button.interactable = true;
    }

    public void NewQuestion()
    {

        level++;

        DefaultButtonSettings(A);
        DefaultButtonSettings(B);
        DefaultButtonSettings(C);
        DefaultButtonSettings(D);
        
        if(!ffUsed)
            ffButton.interactable = true;
        if (!phoneUsed)
            phoneButton.interactable = true;

        DisplayMessage();

        int nr = new System.Random().Next(1, 6);

        string filePath = Application.streamingAssetsPath + "/Baza/" + amount[level] + "/" + nr + ".txt"; // ścieżka do pliku tekstowego w katalogu Assets
        if (File.Exists(filePath)) // sprawdź, czy plik istnieje
        {
            using (StreamReader sr = new StreamReader(filePath)) // utwórz obiekt StreamReader
            {
                ans = sr.ReadLine();
                textQ.text = sr.ReadLine();
                textA.text = sr.ReadLine();
                textB.text = sr.ReadLine();
                textC.text = sr.ReadLine();
                textD.text = sr.ReadLine();
            }
        }
        else
        {
            Debug.LogError("Plik nie istnieje!"); // wyświetl błąd w konsoli, jeśli plik nie istnieje
        }
    }

    private void disableButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.disabledColor = disableHelpButtonColor;
        button.colors = colors;
        button.interactable = false;
    }

    public void Fiftyfifty()
    {
        int d1 = 0;
        int d2 = 0;
        int correct = 0;
        switch (ans)
        {
            case "1000":
                correct = 1;
                break;
            case "0100":
                correct = 2;
                break;
            case "0010":
                correct = 3;
                break;
            case "0001":
                correct = 4;
                break;
        }
        while(d2 == 0)
        {
            int x = new System.Random().Next(1, 5);
            if(x!=correct)
            {
                if(d1 == 0)
                    d1 = x;
                else
                {
                    if(x!=d1)
                        d2 = x;
                }
            }
            
        }

        if (d1==1 || d2==1)
            disableButton(A);
        if(d1==2 || d2==2)
            disableButton(B);
        if (d1==3 || d2==3)
            disableButton(C);
        if (d1==4 || d2==4)
            disableButton(D);

        disableButton(ffButton);
        ffUsed = true;

    }
   

    public void Phone()
    {
        bool chosen = false;
        while(!chosen)
        {
            switch(new System.Random().Next(0, 10))
            {
                case 0:
                    if(A.interactable)
                    {
                        hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź A";
                        chosen = true;
                    }
                    break;
                case 1:
                    if(B.interactable)
                    {
                        hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź B";
                        chosen = true;
                    }
                    break;
                case 2:
                    if(C.interactable)
                    {
                        hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź C";
                        chosen = true;
                    }
                    break;
                case 3:
                    if(D.interactable)
                    {
                        hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź D";
                        chosen = true;
                    }
                    break;
                default:
                    switch (ans)
                    {
                        case "1000":
                            hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź A";
                            chosen = true;
                            break;
                        case "0100":
                            hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź B";
                            chosen = true;
                            break;
                        case "0010":
                            hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź C";
                            chosen = true;
                            break;
                        case "0001":
                            hint.text = "Twój przyjaciel uważa, że poprawna jest odpowiedź D";
                            chosen = true;
                            break;
                    }
                    break; 
            }
        }

        disableButton(phoneButton);
        phoneUsed = true;
    }

    private void ButtonsOff()
    {
        A.interactable = false;
        B.interactable = false;
        C.interactable = false;
        D.interactable = false;
        ffButton.interactable = false;
        phoneButton.interactable = false;
    }

    public void Clicked(Button button)
    {
        ColorBlock colors = button.colors;
        colors.disabledColor = orange;
        button.colors = colors;
        ButtonsOff();
        switch (ans)
        {
            case "1000":
                if(button==A)
                    StartCoroutine(Correct(button));
                else
                    StartCoroutine(Incorrect(button));
                break;
            case "0100":
                if(button==B)
                    StartCoroutine(Correct(button));
                else
                    StartCoroutine(Incorrect(button));
                break;
            case "0010":
                if(button==C)
                    StartCoroutine(Correct(button));
                else
                    StartCoroutine(Incorrect(button));
                break;
            case "0001":
                if(button==D)
                    StartCoroutine(Correct(button));
                else
                    StartCoroutine(Incorrect(button));
                break;
        }
    }

    IEnumerator Correct(Button button)
    {
        yield return new WaitForSeconds(2f);
        ColorBlock colors = button.colors;
        colors.disabledColor = Color.green;
        button.colors = colors;
        yield return new WaitForSeconds(2f);
        if(level<12)
            NewQuestion();
        else
        {
            message.text = "Gratulacje, " + nick + " !!! \n Wygrywasz MILION zł!!!";
            StartCoroutine(ToMenu());
        }
    }

    IEnumerator Incorrect(Button button)
    {
        yield return new WaitForSeconds(2f);
        ColorBlock colors = button.colors;
        colors.disabledColor = Color.red;
        button.colors = colors;
        switch(level)
        {
            case 1:
            case 2:
                message.text = "Przegrałeś :(";
                break;
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                message.text = "Przegrałeś :( \n Jednak otrzymujesz gwarantowane 1000 zł :)";
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                message.text = "Przegrałeś :( \n Jednak otrzymujesz gwarantowane 50000 zł :)";
                break;
        }
        StartCoroutine(ToMenu());
    }

    public void OnRestart()
    {
        ToNickInput();
    }

    public void OnClose()
    {
        Close();
    }
}
