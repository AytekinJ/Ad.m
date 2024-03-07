using UnityEngine;

public class RectangularPlatform : MonoBehaviour
{
    public GameObject R1Platform1, R1Platform2, R2Platform1, R2Platform2;
    bool r1X, r1Y;
    bool r2X, r2Y;
    float speed = 4f;

    void Update() {
        PlatformPosCheck();
        PlatformMovement();

    }

    private void FixedUpdate() {

    }

    void PlatformPosCheck()
    {
        #region Platform 1
        if(R1Platform1.transform.position.x < 147.1f && R1Platform1.transform.position.y < -6.99f)
        {
            r1X = false;
            r1Y = false;
        }
        else if(R1Platform1.transform.position.x > 152.99f && R1Platform1.transform.position.y < -6.99f)
        {
            r1X = true;
            r1Y = false;
        }
        else if(R1Platform1.transform.position.x > 152.99f && R1Platform1.transform.position.y > 0.99f)
        {
            r1X = true;
            r1Y = true;
        }
        else if(R1Platform1.transform.position.x < 147.1f && R1Platform1.transform.position.y > 0.99f)
        {
            r1X = false;
            r1Y = true;
        }
        #endregion

        #region Platform 2
        if(R2Platform1.transform.position.x < 159.1f && R2Platform1.transform.position.y < -6.99f)
        {
            r2X = false;
            r2Y = false;
        }
        else if(R2Platform1.transform.position.x < 159.1f && R2Platform1.transform.position.y > 0.99f)
        {
            r2X = false;
            r2Y = true;
        }
        else if(R2Platform1.transform.position.x > 164.99f && R2Platform1.transform.position.y > 0.99f)
        {
            r2X = true;
            r2Y = true;
        }
        else if(R2Platform1.transform.position.x > 164.99f && R2Platform1.transform.position.y < -6.99f)
        {
            r2X = true;
            r2Y = false;
        }
        #endregion
    }

    void PlatformMovement()
    {
        #region Platform Force 1
        if(r1X == false && r1Y == false)
        {
            R1Platform1.transform.position += new Vector3(speed * Time.deltaTime, 0);
            R1Platform2.transform.position -=  new Vector3(speed * Time.deltaTime, 0);
        }
        else if(r1X == true && r1Y == false)
        {
            R1Platform1.transform.position += new Vector3(0, speed * Time.deltaTime);
            R1Platform2.transform.position -= new Vector3(0, speed * Time.deltaTime);

        }
        else if(r1X == true && r1Y == true)
        {
            R1Platform1.transform.position -= new Vector3(speed * Time.deltaTime, 0);
            R1Platform2.transform.position += new Vector3(speed * Time.deltaTime, 0);
        }
        else if(r1X == false && r1Y == true)
        {
            R1Platform1.transform.position -= new Vector3(0, speed * Time.deltaTime);
            R1Platform2.transform.position += new Vector3(0, speed * Time.deltaTime);
        }
        #endregion

        #region Platform Force 2
        if(r2X == false && r2Y == false)
        {
            R2Platform1.transform.position += new Vector3(0, speed * Time.deltaTime);
            R2Platform2.transform.position -= new Vector3(0, speed * Time.deltaTime);
        }
        else if(r2X == false && r2Y == true)
        {
            R2Platform1.transform.position += new Vector3(speed * Time.deltaTime, 0);
            R2Platform2.transform.position -= new Vector3(speed * Time.deltaTime, 0);
        }
        else if(r2X == true && r2Y == true)
        {
            R2Platform1.transform.position -= new Vector3(0, speed * Time.deltaTime);
            R2Platform2.transform.position += new Vector3(0, speed * Time.deltaTime);
        }
        else if(r2X == true && r2Y == false)
        {
            R2Platform1.transform.position -= new Vector3(speed * Time.deltaTime, 0);
            R2Platform2.transform.position += new Vector3(speed * Time.deltaTime, 0);
        }
        #endregion  
    }


}
