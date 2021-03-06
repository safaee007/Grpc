﻿using System;
using Bond;
using Bond.IO.Safe;
using Bond.Protocols;

namespace CodingMilitia.Grpc.Serializers
{
    public class BondSerializer : ISerializer
    {
        //From: https://github.com/Horusiath/GrpcSample/blob/master/GrpcSample.Shared/Serializer.cs
        public byte[] ToBytes<T>(T input)
        {
            var buffer = new OutputBuffer();
            var writer = new FastBinaryWriter<OutputBuffer>(buffer);
            Serialize.To(writer, input);
            var output = new byte[buffer.Data.Count];
            Array.Copy(buffer.Data.Array, 0, output, 0, (int)buffer.Position);
            return output;
        }

        public T FromBytes<T>(byte[] input)
        {
            var buffer = new InputBuffer(input);
            var data = Deserialize<T>.From(new FastBinaryReader<InputBuffer>(buffer));
            return data;
        }
    }
}