// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.11.1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MessageExtnSearch.Controllers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApiproj.Dtos;

namespace MessageExtnSearch.Bots
{
    public class EchoBot : TeamsActivityHandler
    {
      
        protected override async Task<MessagingExtensionResponse> OnTeamsMessagingExtensionQueryAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionQuery query, CancellationToken cancellationToken)
        {
            var text = query?.Parameters?[0]?.Value as string ?? string.Empty;
            var packages = await FindPackages(text);



            var attachments = packages.Select(package => {
                var previewCard = new ThumbnailCard
                {
                  Title = package.Name, 
                  Tap = new CardAction { Type = "invoke", Value = package },


                };



                var attachment = new MessagingExtensionAttachment
                {
                    ContentType = HeroCard.ContentType,
                    Content = new HeroCard { Title = package.Name },
                    Preview = previewCard.ToAttachment()
                };



                return attachment;
            }).ToList();




            return new MessagingExtensionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    Type = "result",
                    AttachmentLayout = "list",
                    Attachments = attachments
                }
            };
        }



        protected override Task<MessagingExtensionResponse> OnTeamsMessagingExtensionSelectItemAsync(ITurnContext<IInvokeActivity> turnContext, JObject query, CancellationToken cancellationToken)
        {
            var lead = query.ToObject<WebApiReadDto>();

            var card = new ThumbnailCard
            {
                Title = $"{lead.Name}",
                Subtitle = lead.Email,
                

                Buttons = new List<CardAction>
                {
                    
                    new CardAction { Type = ActionTypes.OpenUrl, Title = "Chat", Value ="https://teams.microsoft.com/l/chat/0/0?users=amit.kalal@celebaltech.com"},
                },
            };



            var attachment = new MessagingExtensionAttachment
            {
                ContentType = ThumbnailCard.ContentType,
                Content = card,
            };



            return Task.FromResult(new MessagingExtensionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    Type = "result",
                    AttachmentLayout = "list",
                    Attachments = new List<MessagingExtensionAttachment> { attachment }
                }
            });

           

        }
        private async Task<IEnumerable<WebApiReadDto>> FindPackages(string text)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var jsonchange = JsonConvert.SerializeObject(commandReadDto);
            HttpResponseMessage response = await client.GetAsync("api/WebApi");
            List<WebApiReadDto> result = new List<WebApiReadDto>();
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var data2 = JsonConvert.DeserializeObject<IEnumerable<WebApiReadDto>>(data);
                foreach(var item in data2)
                {
                    if(item.Name.Contains(text))
                    {
                        result.Add(item);
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }

            return result;
        }


    }
}
