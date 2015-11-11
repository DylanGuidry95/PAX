﻿using UnityEngine;
using System.Collections;

public class _Stats : MonoBehaviour
{
	// DEBUGGING // DEBUGGING // DEBUGGING // DEBUGGING // DEBUGGING 
	void Start()
	{
		Messenger.Broadcast("modstat" + GetInstanceID(), "health", -7f);
	}
	// DEBUGGING // DEBUGGING // DEBUGGING // DEBUGGING // DEBUGGING 

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		print(GetInstanceID());

		m_maxHealth = m_health;		// Sets the health limit to the value assigned at start
		m_maxMana = m_mana;			// Sets the mana limit to the value assigned at start

		Messenger.AddListener<string, float>("modstat" + GetInstanceID(), ModStat);	// Subscribes to the modifing of its own stats
		Messenger.MarkAsPermanent("modstat" + GetInstanceID());						// Prevents subscription loss during scene change
	}

	/// <summary>
	/// Mods the stat.
	/// </summary>
	/// <param name="a_stat">A_stat.</param>
	/// <param name="a_value">A_value.</param>
	void ModStat(string a_stat, float a_value)
	{
		if(a_stat.ToLower().Contains("health"))
		{
			health += a_value;
		}

		if(a_stat.ToLower().Contains("mana"))
		{
			mana += a_value;
		}
	}

	/// <summary>
	/// Raises the dead event.
	/// </summary>
	void OnDead()
	{
		Messenger.Broadcast("entitydied", + GetInstanceID());
		Messenger.RemoveListener<string, float>("modstat" + + GetInstanceID(), ModStat);
		transform.position = Vector3.zero;
		gameObject.SetActive(false);
	}

	/// <summary>
	/// The max health.
	/// </summary>
	private float m_maxHealth;
	/// <summary>
	/// The max mana.
	/// </summary>
	private float m_maxMana;

	[SerializeField] protected float m_strength;
	[SerializeField] protected float m_health;
	[SerializeField] protected float m_speed;
	[SerializeField] protected float m_mana;

	/// <summary>
	/// Gets or sets the health.
	/// </summary>
	/// <value>The health.</value>
	public float health
	{
		get
		{
			return m_health;
		}
		set
		{
			m_health = value;

			m_health = m_health >= m_maxHealth ? m_maxHealth : m_health;

			if(m_health <= 0f)
			{
				OnDead();
			}
		}
	}

	/// <summary>
	/// Gets or sets the mana.
	/// </summary>
	/// <value>The mana.</value>
	public float mana
	{
		get
		{
			return m_mana;
		}
		set
		{
			m_mana = value;

			m_mana = m_mana >= m_maxMana ? m_maxMana : m_mana;
		
		}
	}
}

/// Eric Mouledoux