using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
	public abstract class IKLimb : MonoBehaviour
	{
		protected IKCore core;

		public void Initalize(IKCore c)
		{
			core = c;
			OnInitalize();
		}

		public abstract void OnInitalize();
		public abstract void Tick();
		public abstract bool IsBlocking { get; }
	}
}
