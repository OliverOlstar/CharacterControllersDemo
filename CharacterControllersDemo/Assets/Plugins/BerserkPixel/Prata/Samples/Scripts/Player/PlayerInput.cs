using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 Move => move;
        public bool Jump
		{ 
			get
			{
				//if (jump)
				//{
				//	jump = false;
				//	return true;
				//}
				return jump;
			}
		}
        public bool JumpHold => jumpHold;
        public bool Climb
		{
			get
			{
				if (climb)
				{
					climb = false;
					return true;
				}
				return false;
			}
		}
		public bool Interact
		{
			get
			{
				if (interact)
				{
					interact = false;
					return true;
				}
				return false;
			}
		}

		private Vector2 move;
        private bool jump;
        private bool jumpHold;
        private bool climb;
        private bool interact;

		public void SetMoveVector(Vector2 pInput) => move = pInput;
		public void SetJump(bool pInput)
		{
			if (pInput)
			{
				jumpHold = true;
				jump = false;
			}
			else
			{
				jumpHold = false;
				jump = true;
			}
		}
		public void SetClimb(bool pInput) => climb = pInput ? true : climb;
		public void SetInteract(bool pInput) => interact = pInput ? true : interact;
	}
}