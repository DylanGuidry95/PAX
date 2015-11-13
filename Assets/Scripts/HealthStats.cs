/// <summary>
/// Developed by: Quinton "Kiro" Baudoin.
/// 
/// Purpose: To act as a health counter.
/// 
/// Usage: Attach to desired gameobject and use the Messanger System
///         to broadcast "takedamage", with name of gameobject and this
///         script will check to see if it got hit and then reduce health.
///         When health hits 0 or less "playerdied" with name of gameobject this is
///         attached to.
/// 
/// </summary>
///////////////////////////////////

using UnityEngine;
using System.Collections;

public class HealthStats : MonoBehaviour
{
 
    /// <summary>
    /// public
    /// Getter/Setter for _health
    /// </summary>
    public int health
    {
        get { return _health; } //returns _health
        protected set           // Protected Set
        {
            if (value < maxHealth || maxHealth == 0)
                _health = value;      //Sets _health to Value
            else
            {
                _health = maxHealth;
            }

            if (_health <= 0)     //If health is less then or equal to 0
                OnDead();         //Triggers OnDead()
        }

     }

    protected virtual void TakeDamage(string message)
    {
        if (message == name) // Checks to see if message is equal to gameObject.name
        { health--; }       // reduces health by 1
    }
    protected virtual void GetHealed(string message)
    {
        if (message == name)
            health++;
    }

    protected virtual void OnDead()
    {
        Messenger.Broadcast("entitydied", name); //Broacasts "playerdied" with gameObject.name
    }

    public virtual void OnEnable()
    {
        Messenger.AddListener<string>("takedamage", TakeDamage); //AddListener of "takedamage" with TakeDamage function
        Messenger.MarkAsPermanent("takedamage");
        Messenger.AddListener<string>("gethealed", GetHealed);
        Messenger.MarkAsPermanent("gethealed");
        
    }
    public virtual void OnDisable()
    {
        Messenger.RemoveListener<string>("takedamage", TakeDamage); //Removes listener of "takedamage" with TakeDamage function
        Messenger.RemoveListener<string>("gethealed", GetHealed);
    }

    //public virtual void OnDestroy()
    //{
    //    Messenger.RemoveListener<string>("takedamage", TakeDamage); //Removes listener of "takedamage" with TakeDamage function
    //    Messenger.RemoveListener<string>("gethealed", GetHealed);
    //}
   [SerializeField]
   protected int _health = 3;
    /// <summary>
    /// The max amout of Health this script can have.
    /// If health exedes maxhealth then the health is set to the max health.
    /// Set maxHealth to 0 if you do not want max health
    /// </summary>
   [SerializeField]
   protected int maxHealth = 3;
}
