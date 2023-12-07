using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using extOSC;
using TMPro;

public class MyOSC : MonoBehaviour

{    
    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;
    public GameObject myTarget;
    public TextMeshProUGUI number;
    public int count;
    public float currentValue1;
    public float currentValue2;
    public float currentValue3;
    public float currentValue4;
    public float currentValue5;
    public float currentValue6;
    public float currentValuePad;
    public float prevValue;
    public float prevValue1;
    public float prevValue2;
    public float prevValue3;
    public float prevValueRight;
    public float prevValueLeft;
    public static int spinLockNb = 0;
    public float spinLockXpos;
    public float spinLockYpos;
    public TextMeshProUGUI spinLockText1;
    public TextMeshProUGUI spinLockText2;
    public TextMeshProUGUI spinLockText3;
    public TextMeshProUGUI spinLockText4;
    public TextMeshProUGUI spinLockText5;
    public TextMeshProUGUI spinLockText6;
    public GameObject Answer1;
    public GameObject safeNumPad;
    public TextMeshProUGUI safeNumPadInput;
    public GameObject win;
    public GameObject feu;
    public GameObject black;
    public GameObject cadenas;
    public GameObject coffre;
    public GameObject openSafe;
    public GameObject dimmer;
    public Transform spinLockSelector;
    float red;
    float green;
    float right;
    float left;
    float yellow1;
    float yellow2;


    public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/key", numberUp);
        oscReceiver.Bind("/atombtn", numberUpConfirm);
        oscReceiver.Bind("/encoderbtn", spinLockxposChange);
        oscReceiver.Bind("/encoderrotation", spinLockNbChange);
        oscReceiver.Bind("/tof", fireScale);
        oscReceiver.Bind("/right", goRight);
        oscReceiver.Bind("/gauche", goLeft);
        oscReceiver.Bind("/pir", openBook);
        oscReceiver.Bind("/photo", lightsDown);
    }

    void lightsDown(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        if (value <= 800)
        {
            dimmer.SetActive(true);
        } else
        {
            dimmer.SetActive(false);
        }

    }

    void goLeft(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        if (value == 1 && value != prevValueLeft)
        {
            if (coffre.activeSelf == true)
            {
                coffre.SetActive(false);
                openSafe.SetActive(false);
            }
            else if (coffre.activeSelf == false)
            {
                coffre.SetActive(true);
            }
        }

        prevValueLeft = value;

    }

    void goRight(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        if (value == 1 && value != prevValueRight)
        {
            if (cadenas.activeSelf == true)
            {
                cadenas.SetActive(false);
            } else if (cadenas.activeSelf == false) 
            { 
                cadenas.SetActive(true); 
            }
        }

        prevValueRight = value;


    }

    void openBook(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        if (value == 1)
        {
            black.gameObject.SetActive(false);
        }

    }

    void fireScale(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        float yScale = ScaleValue(value, 0, 400, 0, 0.4f);
        feu.transform.localScale = new Vector3(0.2736726f, yScale, 0.2736726f);
    }

    void spinLockNbChange(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        if (spinLockNb == 0)
        {
            currentValue1 += value;
            
            if (currentValue1 > 9) {
                currentValue1 = 0;
            } else if (currentValue1 < 0) {
                currentValue1 = 9;
            }
            spinLockText1.text = currentValue1.ToString();
        }
        else if (spinLockNb == 1)
        {
            currentValue2 += value;
            
            if (currentValue2 > 9) {
                currentValue2 = 0;
            } else if (currentValue2 < 0) {
                currentValue2 = 9;
            }
            spinLockText2.text = currentValue2.ToString();

        }
        else if (spinLockNb == 2)
        {
            currentValue3 += value;
            
            if (currentValue3 > 9) {
                currentValue3 = 0;
            } else if (currentValue3 < 0) {
                currentValue3 = 9;
            }
            spinLockText3.text = currentValue3.ToString();

        }
        else if (spinLockNb == 3)
        {
            currentValue4 += value;

            if (currentValue4 > 9)
            {
                currentValue4 = 0;
            }
            else if (currentValue4 < 0)
            {
                currentValue4 = 9;
            }
            spinLockText4.text = currentValue4.ToString();

        }
        else if (spinLockNb == 4)
        {
            currentValue5 += value;

            if (currentValue5 > 9)
            {
                currentValue5 = 0;
            }
            else if (currentValue5 < 0)
            {
                currentValue5 = 9;
            }
            spinLockText5.text = currentValue5.ToString();

        }
        else if (spinLockNb == 5)
        {
            currentValue6 += value;

            if (currentValue6 > 9)
            {
                currentValue6 = 0;
            }
            else if (currentValue6 < 0)
            {
                currentValue6 = 9;
            }
            spinLockText6.text = currentValue6.ToString();

        }

        prevValue3 = value;

        currentValuePad += value;
        safeNumPadInput.text = currentValuePad.ToString();
    
    }

    void spinLockxposChange(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        if (value == 1 && value != prevValue2)
        {
            spinLockNb += 1;
            spinLockXpos += 165;
            if (spinLockNb > 5)
            {
                spinLockNb = 0;
                spinLockXpos = 533.83f;
            }
            if (currentValue4 == 10)
            {
                Debug.Log("Pad Correct");
            }
        }

        prevValue2 = value;
        spinLockSelector.transform.position = new Vector3(spinLockXpos, spinLockYpos, 0);

    }


    void numberUp(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }
        
        if (value == 1 && value != prevValue)
        {
            count++;
            number.text = count.ToString();
        }

        prevValue = value;
    }

    void numberUpConfirm(OSCMessage oscMessage)
    {
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            return;
        }

        if (value == 1 && value != prevValue1)
        {
            if (count == 9)
            {
                Answer1.gameObject.SetActive(true);
                count = 0;
            } else
            {
                count = 0;
            }

            if (currentValue1 == 1 &&  currentValue2 == 1 && currentValue3 == 1 && currentValue4 == 1 && currentValue5 == 1 && currentValue6 == 1) {
                win.gameObject.SetActive(true );
            }

            if (currentValuePad == 598)
            {
                openSafe.gameObject.SetActive(true);
            }
        }



        prevValue1 = value;
    }

    float myChronoStart;

    // Update is called once per frame
    void LateUpdate()
    {


        if (black.activeSelf == false)
        {
            red = 255;
            green = 255;
            right = 255;
            left = 255;
            yellow1 = 255;
            yellow2 = 255;

        } else
        {
            red = 0;
            green = 0;
            right = 0;
            left = 0;
        }

        if (Time.realtimeSinceStartup - myChronoStart >= 0.05f)
        {
            myChronoStart = Time.realtimeSinceStartup;

            // Créer le message
            var myOscMessage = new OSCMessage("/unity");

            // Aller chercher une valeur:
            //float myPositionX = myTarget.transform.position.x;
            // Changer l'échelle de la valeur:
            //float myScaledPositionX = ScaleValue(myPositionX, -7, 7, 0, 255);

            // Ajouter la valeur au message
            myOscMessage.AddValue(OSCValue.Int((int)red));
            myOscMessage.AddValue(OSCValue.Int((int)green));
            myOscMessage.AddValue(OSCValue.Int((int)right));
            myOscMessage.AddValue(OSCValue.Int((int)left));
            myOscMessage.AddValue(OSCValue.Int((int)yellow1));
            myOscMessage.AddValue(OSCValue.Int((int)yellow2));

            // Envoyer le message
            oscTransmitter.Send(myOscMessage);
        }
    }
}
