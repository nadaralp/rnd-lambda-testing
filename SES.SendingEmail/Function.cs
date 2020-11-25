using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

// This is sending emails using Simple Email Service SDk.
namespace SES.SendingEmail
{
    public class Function
    {
        private IAmazonSimpleEmailService _simpleEmailServiceClient;
        private const string htmlTemplatePath = @"D:\Test Projects\Lambda-testing\Lambda\SES.SendingEmail\EmailTemplates\template.html";

        public Function()
        {
            _simpleEmailServiceClient = new AmazonSimpleEmailServiceClient();
        }

        private const string htmlBody = @"<html>
                                        <head></head>
                                        <body>
                                          <h1>Hello there {0}</h1>
                                          <p>This email was sent with
                                            <a href='https://nadaralp.com'>Amazon SES </a>
                                            <p>Nice to meet you </p>
                                        </body>
                                        </html>";

        public async Task FunctionHandler(DataDto dataDto, ILambdaContext context)
        {
            try
            {
                string source = "nadaralp16@gmail.com";
                Destination destination = new Destination(new List<string> { "nadaralp16@gmail.com" });

                using (var sr = new StreamReader(htmlTemplatePath))
                {
                    string htmlContent = await sr.ReadToEndAsync();

                    // Message building
                    Content subject = new Content("Nadar Alpenidze Amazing Email - This is subject"); //subject is the title of the email
                    string htmlFormatted = string.Format(htmlContent, dataDto?.Name ?? "no name was provided", dataDto?.Email ?? "No provided email");
                    Content content = new Content(htmlFormatted);

                    Body body = new Body { Html = content };
                    Message message = new Message(subject, body);

                    // Mail request
                    SendEmailRequest sendEmailRequest = new SendEmailRequest(source, destination, message);
                    var res = await _simpleEmailServiceClient.SendEmailAsync(sendEmailRequest);
                }
            }
            catch (Exception e)
            {
                context.Logger.LogLine(e.Message);
            }
        }
    }
}