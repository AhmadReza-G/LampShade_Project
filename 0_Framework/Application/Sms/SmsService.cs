//using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient;
using Microsoft.Extensions.Configuration;
//using SmsIrRestful;

namespace _0_Framework.Application.Sms;

public class SmsService : ISmsService
{
    private readonly IConfiguration _configuration;

    public SmsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async void Send(string number, string message)
    {
        //var token = GetToken();
        //var lines = new SmsLine().GetSmsLines(token);
        //if (lines == null) return;

        //var line = lines.SMSLines.Last().LineNumber.ToString();
        //var data = new MessageSendObject
        //{
        //    Messages = new List<string>
        //        {message}.ToArray(),
        //    MobileNumbers = new List<string> { number }.ToArray(),
        //    LineNumber = line,
        //    SendDateTime = DateTime.Now,
        //    CanContinueInCaseOfError = true
        //};
        //var messageSendResponseObject =
        //    new MessageSend().Send(token, data);

        //if (messageSendResponseObject.IsSuccessful) return;

        //line = lines.SMSLines.First().LineNumber.ToString();
        //data.LineNumber = line;
        //new MessageSend().Send(token, data);
        SmsIr smsIr = new SmsIr("zjh2y1fzjgfLTNwtRAuml4EOgSuSw3lROGS2dksqmBoGb7hCrrdQmN6VOLgkQXbH");

        var bulkSendResult = await smsIr.BulkSendAsync(30007732902587, message, new string[] { number });

    }

    //private string GetToken()
    //{
    //    var smsSecrets = _configuration.GetSection("SmsSecrets");
    //    var tokenService = new Token();
    //    var apiKey = _configuration.GetSection("SmsSecrets")["ApiKey"];
    //    var secretKey = _configuration.GetSection("SmsSecrets")["X-API-KEY"];
    //    var test = tokenService.GetToken(apiKey, secretKey);
    //    return test;
    //}
}