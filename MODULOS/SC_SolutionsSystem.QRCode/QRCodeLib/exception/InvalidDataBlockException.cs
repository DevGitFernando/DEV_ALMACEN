using System;
namespace SC_SolutionsSystem.QRCode.ExceptionHandler
{
	[Serializable]
	public class InvalidDataBlockException:System.ArgumentException
	{
        internal String message = null;

		public override String Message
		{
			get
			{
				return message;
			}
			
		}
		
		public InvalidDataBlockException(String message)
		{
			this.message = message;
		}
	}
}