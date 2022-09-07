// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/telemetry.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace SecondVariety {
  public static partial class TelemetryServ
  {
    static readonly string __ServiceName = "secondvariety.TelemetryServ";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Timestamp> __Marshaller_google_protobuf_Timestamp = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Timestamp.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SecondVariety.GTelemetrys> __Marshaller_secondvariety_GTelemetrys = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SecondVariety.GTelemetrys.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SecondVariety.GObjectId> __Marshaller_secondvariety_GObjectId = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SecondVariety.GObjectId.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SecondVariety.GTelemetry> __Marshaller_secondvariety_GTelemetry = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SecondVariety.GTelemetry.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::SecondVariety.Empty> __Marshaller_secondvariety_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::SecondVariety.Empty.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Timestamp, global::SecondVariety.GTelemetrys> __Method_GetForDate = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Timestamp, global::SecondVariety.GTelemetrys>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetForDate",
        __Marshaller_google_protobuf_Timestamp,
        __Marshaller_secondvariety_GTelemetrys);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::SecondVariety.GObjectId, global::SecondVariety.GTelemetry> __Method_GetById = new grpc::Method<global::SecondVariety.GObjectId, global::SecondVariety.GTelemetry>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetById",
        __Marshaller_secondvariety_GObjectId,
        __Marshaller_secondvariety_GTelemetry);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::SecondVariety.GTelemetry, global::SecondVariety.GTelemetry> __Method_Post = new grpc::Method<global::SecondVariety.GTelemetry, global::SecondVariety.GTelemetry>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Post",
        __Marshaller_secondvariety_GTelemetry,
        __Marshaller_secondvariety_GTelemetry);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::SecondVariety.GTelemetry, global::SecondVariety.GTelemetry> __Method_Put = new grpc::Method<global::SecondVariety.GTelemetry, global::SecondVariety.GTelemetry>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Put",
        __Marshaller_secondvariety_GTelemetry,
        __Marshaller_secondvariety_GTelemetry);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::SecondVariety.GObjectId, global::SecondVariety.Empty> __Method_Delete = new grpc::Method<global::SecondVariety.GObjectId, global::SecondVariety.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Delete",
        __Marshaller_secondvariety_GObjectId,
        __Marshaller_secondvariety_Empty);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::SecondVariety.TelemetryReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for TelemetryServ</summary>
    public partial class TelemetryServClient : grpc::ClientBase<TelemetryServClient>
    {
      /// <summary>Creates a new client for TelemetryServ</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public TelemetryServClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for TelemetryServ that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public TelemetryServClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected TelemetryServClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected TelemetryServClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetrys GetForDate(global::Google.Protobuf.WellKnownTypes.Timestamp request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetForDate(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetrys GetForDate(global::Google.Protobuf.WellKnownTypes.Timestamp request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetForDate, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetrys> GetForDateAsync(global::Google.Protobuf.WellKnownTypes.Timestamp request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetForDateAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetrys> GetForDateAsync(global::Google.Protobuf.WellKnownTypes.Timestamp request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetForDate, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetry GetById(global::SecondVariety.GObjectId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetry GetById(global::SecondVariety.GObjectId request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetry> GetByIdAsync(global::SecondVariety.GObjectId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetry> GetByIdAsync(global::SecondVariety.GObjectId request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetry Post(global::SecondVariety.GTelemetry request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Post(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetry Post(global::SecondVariety.GTelemetry request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Post, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetry> PostAsync(global::SecondVariety.GTelemetry request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetry> PostAsync(global::SecondVariety.GTelemetry request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Post, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetry Put(global::SecondVariety.GTelemetry request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Put(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.GTelemetry Put(global::SecondVariety.GTelemetry request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Put, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetry> PutAsync(global::SecondVariety.GTelemetry request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PutAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.GTelemetry> PutAsync(global::SecondVariety.GTelemetry request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Put, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.Empty Delete(global::SecondVariety.GObjectId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Delete(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::SecondVariety.Empty Delete(global::SecondVariety.GObjectId request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.Empty> DeleteAsync(global::SecondVariety.GObjectId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::SecondVariety.Empty> DeleteAsync(global::SecondVariety.GObjectId request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override TelemetryServClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new TelemetryServClient(configuration);
      }
    }

  }
}
#endregion