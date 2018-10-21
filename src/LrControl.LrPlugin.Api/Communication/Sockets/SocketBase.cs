﻿using System;
using System.Net.Sockets;
using System.Threading;
using LrControl.LrPlugin.Api.Common;
using Serilog;

namespace LrControl.LrPlugin.Api.Communication.Sockets
{
    public delegate void ConnectionHandler(bool connected);

    internal abstract class SocketBase : IDisposable
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<SocketBase>();
        
        public const int SocketTimeout = 1000;
        
        private readonly int _port;
        private readonly string _hostName;
        private readonly ProcessingThread _reconnectThread;
        protected Socket Socket;

        protected SocketBase(string hostName, int port)
        {
            _hostName = hostName;
            _port = port;
            _reconnectThread = new ProcessingThread($"Socket Reconnect Thread (port {_port})", ReconnectIteration);
        }

        public bool IsOpen { get; private set; }
        public bool IsConnected { get; private set; }
        public event ConnectionHandler Connection;
        
        public bool Open()
        {
            if (IsOpen)
                throw new InvalidOperationException("Cannot open, already open");

            // Try to create socket
            if (!TryCreateSocket())
                return false;

            // Initial state
            IsOpen = true;
            IsConnected = false;
            _reconnectThread.Start();

            return true;
        }

        public void Close()
        {
            if (!IsOpen)
                throw new InvalidOperationException("Cannot close, is not open");

            _reconnectThread.Stop();

            CloseSocket();

            IsOpen = false;
            IsConnected = false;

            Log.Debug("Socket connected to {HostName}:{Port} has been closed", _hostName, _port);
        }

        public void Reconnect(bool fireEvent = true)
        {
            if (!IsOpen)
                throw new InvalidOperationException("Cannot reconnect, is not open");

            if (fireEvent) OnConnection(false);

            IsConnected = false;
            _reconnectThread.Start();
        }
        
        public void Dispose()
        {
            _reconnectThread.Dispose();
            
            if (IsOpen)
            {
                CloseSocket();
            }
            
            OnDispose();
        }
        
        private void ReconnectIteration(CancellationToken cancellationToken, Action stop)
        {
            BeforeReconnect();
            
            Log.Verbose("Trying to reconnect to {HostName}:{Port}", _hostName, _port);

            if (TryReconnect())
            {
                Log.Debug("Successfully reconnected to {HostName}:{Port}", _hostName, _port);

                IsConnected = true;
                OnConnection(true);

                AfterReconnect();

                // Stop reconnect loop, successfully reconnected
                stop();
            }
            else
            {
                var duration = TimeSpan.FromSeconds(5);
                Log.Verbose("Waiting {Duration} before retrying...", duration);
                cancellationToken.WaitHandle.WaitOne(duration);
            }
        }

        private bool TryReconnect()
        {
            CloseSocket();

            if (!TryCreateSocket())
                return false;

            // Try to connect
            try
            {
                Socket.Connect(_hostName, _port);
                return true;
            }
            catch (SocketException e)
            {
                Log.Verbose(e, "Unable to connect to {HostName}:{Port}", _hostName, _port);
                return false;
            }
        }

        private bool TryCreateSocket()
        {
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    SendTimeout = SocketTimeout,
                    ReceiveTimeout = SocketTimeout,
                    SendBufferSize = 8192,
                    ReceiveBufferSize = 8192
                };

                return true;
            }
            catch (SocketException e)
            {
                Log.Error(e, "An error occurred while trying to allocate a socket");
                return false;
            }
        }

        private void CloseSocket()
        {
            try
            {
                if (Socket.Connected)
                {
                    Socket.Disconnect(false);
                }
                Socket.Close();
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to close socket");
            }
            finally
            {
                Socket.Dispose();
            }
            Socket = null;
        }
        
        private void OnConnection(bool connected)
        {
            Connection?.Invoke(connected);
        }
        
        protected virtual void BeforeReconnect()
        {
        }

        protected virtual void AfterReconnect()
        {
        }

        protected virtual void OnDispose()
        {
        }
    }
}