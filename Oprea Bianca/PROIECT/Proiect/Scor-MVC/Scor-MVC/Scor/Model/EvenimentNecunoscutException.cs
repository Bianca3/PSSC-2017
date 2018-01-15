using System;
using System.Runtime.Serialization;

namespace Scor.Model
{
	[Serializable]
	internal class EvenimentNecunoscutException : Exception
	{
		public EvenimentNecunoscutException()
		{
		}

		public EvenimentNecunoscutException(string message) : base(message)
		{
		}

		public EvenimentNecunoscutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected EvenimentNecunoscutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}