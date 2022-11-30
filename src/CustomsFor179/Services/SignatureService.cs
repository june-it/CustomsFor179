namespace CustomsFor179.Services
{
    public class SignatureService : ISignatureService
    {
        private readonly ILogger _logger;
        public SignatureService(ILogger<SignatureService> logger)
        {
            _logger = logger;
        }
        public async Task<string?> Sign(RealTimeDataUp request)
        {
            StringBuilder messageBuilder = new();
            messageBuilder.Append($"\"sessionID\":\"{request.SessionId}\"");
            messageBuilder.Append($"||");
            messageBuilder.Append($"\"payExchangeInfoHead\":\"{JsonConvert.SerializeObject(request.PayExchangeInfoHead)}\"");
            messageBuilder.Append($"||");
            messageBuilder.Append($"\"payExchangeInfoLists\":\"{JsonConvert.SerializeObject(request.PayExchangeInfoLists)}\"");
            messageBuilder.Append($"||");
            messageBuilder.Append($"\"serviceTime\":\"{request.ServiceTime}\"");
            string cipertext = messageBuilder.ToString();

            // 创建WebSocket请求签名
            CancellationTokenSource cancellationToken = new();
            ClientWebSocket socketClient = new();
            await socketClient.ConnectAsync(new Uri("ws://127.0.0.1:61232"), cancellationToken.Token);
            if (socketClient.State == WebSocketState.Open)
            {
                while (socketClient.State == WebSocketState.Open)
                {
                    ArraySegment<byte> receiveBuffer = new(new byte[1024 * 1024 * 5]);
                    WebSocketReceiveResult receiveResult = await socketClient.ReceiveAsync(receiveBuffer, cancellationToken.Token);
                    if (receiveResult.Count == 0 || receiveBuffer.Array == null) 
                        continue; 

                    string response = Encoding.UTF8.GetString(receiveBuffer.Array, 0, receiveResult.Count);
                    _logger.LogInformation($"接收到签名客户端数据：{response}");
                    string error = Regex.Match(response, @"Error"":\[(\S+)\]").Groups[1].Value;
                    if (response.Contains("握手成功"))
                    {
                        // 发送待签名数据
                        string sendDataStr = "{\"_method\":\"cus-sec_SpcSignDataAsPEM\",\"_id\":1,\"args\":{\"inData\":\"" + cipertext.Replace("\"", "\\\"") + "\",\"passwd\":\"88888888\"}}";
                        await socketClient.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(sendDataStr)), WebSocketMessageType.Text, true, CancellationToken.None);
                        _logger.LogInformation($"发送签名数据：{sendDataStr}");
                    }
                    else if (error.Length > 0)
                    {
                        cancellationToken.Cancel();
                        throw new InvalidOperationException($"签名客户端出现错误：{error}");
                    }
                    else
                    {
                        string signValue = Regex.Match(response, @"Data"":\[""(\S+?)""").Groups[1].Value;
                        cancellationToken.Cancel();
                        return signValue;
                    } 
                }
            }
            return null;
        }
    }
}
