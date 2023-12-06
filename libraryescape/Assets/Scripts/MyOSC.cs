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
    public float prevValue;
    public float prevValue1;
    public float prevValue2;
    public float prevValue3;
    public static int spinLockNb = 0;
    public static float spinLockXpos = -2f;
    public TextMeshProUGUI spinLockText1;
    public TextMeshProUGUI spinLockText2;
    public TextMeshProUGUI spinLockText3;

    public Transform spinLockSelector;


    public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/button", numberUp);
        oscReceiver.Bind("/atombtn", numberUpConfirm);
        oscReceiver.Bind("/encoderbtn", spinLockxposChange);
        oscReceiver.Bind("/encoderrotation", spinLockNbChange);
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
            Debug.Log(currentValue1);
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
            Debug.Log(currentValue2);

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
            Debug.Log(currentValue3);

        }
            prevValue3 = value;
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
            spinLockXpos += 2;
            if (spinLockNb > 2)
            {
                spinLockNb = 0;
                spinLockXpos = -2f;
            }
        }

        prevValue2 = value;
        spinLockSelector.transform.position = new Vector3(spinLockXpos, -3f, 0);

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
            if (count == 7)
            {
                number.text = "Correct!";
                count = 0;
            } else
            {
                number.text = "Incorrect!";
                count = 0;
            }
        }

        prevValue1 = value;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
