using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolCryptographyC
{
	internal class Segment
	{
		private TypeSegment type;
		private byte numSegment;
		private byte[] length;
		private byte[]? payload;
		public static int lengthBlockFile = 16777199;
		private static int lengthBlockFileCrypto = 16777200;

		public TypeSegment Type { get { return type; } }
		public byte NumSegment { get { return numSegment; } }
		public byte[] Length { get { return length; } }
		public byte[]? Payload { get { return payload; } }

		public static byte[]? PackSegment(TypeSegment type, byte numSegment, byte[]? payload)
		{
			if(payload == null)
				throw new ArgumentNullException(nameof(payload));

			byte[] length = new byte[3];
            byte[] lengthBuffer = BitConverter.GetBytes(payload.Length);
            if (lengthBuffer[3] == (byte)0)
            {
				Array.Copy(lengthBuffer, length, 3);
            }
            else
            {
                return null;
            }

            return PackSegment(type, numSegment, length, payload);
		}

		private static byte[]? PackSegment(TypeSegment type, byte numSegment, byte[] length, byte[]? payload)
		{
			int size = 0;
			if (length.Length > 3)
			{
				return null;
			}
			if (length.Length < 3)
			{
				byte[] lengthBuffer = new byte[3];
				for(int i=0;i<length.Length;i++)
				{
					lengthBuffer[i] = length[i];
				}
				length = lengthBuffer;
			}
			if (payload != null)
			{
				if (payload.Length > lengthBlockFileCrypto)
				{
					return null;
				}
				size = 2 + length.Length + payload.Length;
			}
			else
            {
				size = 2 + length.Length;
            }

            byte[] segment = new byte[size];

            segment[0] = (byte)type;
            segment[1] = numSegment;

            for (int i = 2; i < length.Length + 2; i++)
            {
                segment[i] = length[i - 2];
            }

            if (payload != null)
			{
				for (int i = length.Length + 2; i < size; i++)
				{
					segment[i] = payload[i - (length.Length + 2)];
				}
			}

			return segment;
		}

		public static Segment? ParseSegment(Socket socket)
        {
			Segment segment = new Segment();
			int lengthInt;
			byte[] header = new byte[5];
			int lengthRead = 0;
			int lengthReadOld = 0;
			try
			{
				socket.Receive(header);

				segment.type = (TypeSegment)header[0];
				segment.numSegment = header[1];
				segment.length = new byte[3];
				for (int i = 0; i < segment.length.Length; i++)
				{
					segment.length[i] = header[2 + i];
				}
				byte[] lengthBuffer = new byte[4];
				segment.length.CopyTo(lengthBuffer, 0);
				lengthBuffer[3] = (byte)0;
				lengthInt = BitConverter.ToInt32(lengthBuffer, 0);
				if (lengthInt > 0)
				{
					segment.payload = new byte[lengthInt];
				}

				if (segment.payload != null)
				{
					if (segment.payload.Length > 0)
					{
						lengthRead = 0;
						lengthReadOld = 0;
						while (lengthRead < segment.payload.Length)
						{
							lengthRead += socket.Receive(segment.payload, lengthReadOld, segment.payload.Length - lengthReadOld, SocketFlags.None);
							lengthReadOld = lengthRead;
						}
					}
				}

				return segment;
			}
			catch
            {
				return null;
            }
        }
	}
}
