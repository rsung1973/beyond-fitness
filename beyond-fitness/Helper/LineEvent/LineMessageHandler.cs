﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LineMessagingAPISDK;
using LineMessagingAPISDK.Models;
using Newtonsoft.Json;
using WebHome.Properties;

namespace WebHome.Helper.LineEvent
{
    public class LineMessageHandler
    {
        private Event lineEvent;
        private LineClient lineClient = new LineClient(Settings.Default.ChannelToken);

        public LineMessageHandler(Event lineEvent)
        {
            this.lineEvent = lineEvent;
        }

        public async Task HandleBeaconEvent()
        {
        }

        public async Task HandleFollowEvent()
        {
        }

        public async Task HandleJoinEvent()
        {
        }

        public async Task HandleLeaveEvent()
        {
        }

        public async Task HandlePostbackEvent()
        {
            string reply;
            // Handle DateTimePicker postback
            if (lineEvent.Postback?.Params != null)
            {
                var dateTime = lineEvent.Postback?.Params;
                reply = $"DateTime: {dateTime.DateTime}, Date: {dateTime.Date}, Time: {dateTime.Time}";
            }
            else
            {
                reply = lineEvent.Postback.Data;
            }
            await Reply(new TextMessage(reply));
        }

        public async Task HandleUnfollowEvent()
        {
        }

        public async Task<Profile> GetProfile(string mid)
        {
            return await lineClient.GetProfile(mid);
        }

        public async Task HandleTextMessage()
        {
            var textMessage = JsonConvert.DeserializeObject<TextMessage>(lineEvent.Message.ToString());
            Message replyMessage = null;
            var message = textMessage.Text.ToLower();
            if (textMessage.Text.ToLower() == "buttons")
            {
                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("Message Label", "sample data"));
                actions.Add(new PostbackTemplateAction("Postback Label", "sample data", "sample data"));
                actions.Add(new UriTemplateAction("Uri Label", "https://github.com/kenakamu"));
                ButtonsTemplate buttonsTemplate = new ButtonsTemplate("https://github.com/apple-touch-icon.png", "Sample Title", "Sample Text", actions);

                replyMessage = new TemplateMessage("Buttons", buttonsTemplate);
            }
            else if (message.StartsWith("goto bf:"))
            {
                List<TemplateAction> actions = new List<TemplateAction>();
                //actions.Add(new MessageTemplateAction("Beyond Fitness", "Welcome"));
                //actions.Add(new PostbackTemplateAction("Postback Label", "sample data", "sample data"));
                actions.Add(new UriTemplateAction("Quick Login", "https://www.worthitstudio.idv.tw/Account/QuickLogin?pid=" + message.Substring(8).Trim()));
                actions.Add(new UriTemplateAction("Quick Login", "https://www.worthitstudio.idv.tw/Account/QuickLogin?pid=" + message.Substring(8).Trim()));
                actions.Add(new UriTemplateAction("Quick Login", "https://www.worthitstudio.idv.tw/Account/QuickLogin?pid=" + message.Substring(8).Trim()));
                actions.Add(new UriTemplateAction("Quick Login", "https://www.worthitstudio.idv.tw/Account/QuickLogin?pid=" + message.Substring(8).Trim()));
                ButtonsTemplate buttonsTemplate = new ButtonsTemplate("https://github.com/apple-touch-icon.png", "Beyond Fitness", "Welcome!!", actions);

                replyMessage = new TemplateMessage("Buttons", buttonsTemplate);
            }
            else if (textMessage.Text.ToLower() == "confirm")
            {
                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("Yes", "yes"));
                actions.Add(new MessageTemplateAction("No", "no"));
                ConfirmTemplate confirmTemplate = new ConfirmTemplate("Confirm Test", actions);
                replyMessage = new TemplateMessage("Confirm", confirmTemplate);
            }
            else if (textMessage.Text.ToLower() == "carousel")
            {
                List<TemplateColumn> columns = new List<TemplateColumn>();
                List<TemplateAction> actions1 = new List<TemplateAction>();
                List<TemplateAction> actions2 = new List<TemplateAction>();

                // Add actions.
                actions1.Add(new MessageTemplateAction("Message Label", "sample data"));
                actions1.Add(new PostbackTemplateAction("Postback Label", "sample data", "sample data"));
                actions1.Add(new UriTemplateAction("Uri Label", "https://github.com/kenakamu"));

                // Add datetime picker actions
                actions2.Add(new DatetimePickerTemplateAction("DateTime Picker", "DateTime", DatetimePickerMode.Datetime, "2017-07-21T13:00"));
                actions2.Add(new DatetimePickerTemplateAction("Date Picker", "Date", DatetimePickerMode.Date, "2017-07-21"));
                actions2.Add(new DatetimePickerTemplateAction("Time Picker", "Time", DatetimePickerMode.Time, "13:00"));

                columns.Add(new TemplateColumn() { Title = "Casousel 1 Title", Text = "Casousel 1 Text", ThumbnailImageUrl = "https://github.com/apple-touch-icon.png", Actions = actions1 });
                columns.Add(new TemplateColumn() { Title = "Casousel 2 Title", Text = "Casousel 2 Text", ThumbnailImageUrl = "https://github.com/apple-touch-icon.png", Actions = actions2 });
                CarouselTemplate carouselTemplate = new CarouselTemplate(columns);
                replyMessage = new TemplateMessage("Carousel", carouselTemplate);
            }
            else if (textMessage.Text.ToLower() == "imagecarousel")
            {
                List<ImageColumn> columns = new List<ImageColumn>();
                UriTemplateAction action = new UriTemplateAction("Uri Label", "https://github.com/kenakamu");

                columns.Add(new ImageColumn("https://github.com/apple-touch-icon.png", action));
                columns.Add(new ImageColumn("https://github.com/apple-touch-icon.png", action));
                columns.Add(new ImageColumn("https://github.com/apple-touch-icon.png", action));
                columns.Add(new ImageColumn("https://github.com/apple-touch-icon.png", action));
                columns.Add(new ImageColumn("https://github.com/apple-touch-icon.png", action));

                ImageCarouselTemplate carouselTemplate = new ImageCarouselTemplate(columns);

                replyMessage = new TemplateMessage("Carousel", carouselTemplate);
            }
            else if (textMessage.Text.ToLower() == "imagemap")
            {
                var url = HttpContext.Current.Request.Url;
                var imageUrl = $"{url.Scheme}://{url.Host}:{url.Port}/LineEvents/GetIcon";
                List<ImageMapAction> actions = new List<ImageMapAction>();
                actions.Add(new UriImageMapAction("http://github.com", new ImageMapArea(0, 0, 520, 1040)));
                actions.Add(new MessageImageMapAction("I love LINE!", new ImageMapArea(520, 0, 520, 1040)));
                replyMessage = new ImageMapMessage(imageUrl, "GitHub", new BaseSize(1040, 1040), actions);
            }
            else if (textMessage.Text.ToLower() == "addrichmenu")
            {
                // Create Rich Menu
                RichMenu richMenu = new RichMenu()
                {
                    Size = new RichMenuSize(1686),
                    Selected = false,
                    Name = "nice richmenu",
                    ChatBarText = "touch me",
                    Areas = new List<RichMenuArea>()
                        {
                            new RichMenuArea()
                            {
                                Action = new PostbackTemplateAction("action=buy&itemid=123"),
                                Bounds = new RichMenuBounds(0, 0, 2500, 1686)
                            }
                        }
                };


                var richMenuId = await lineClient.CreateRichMenu(richMenu);
                var image = new MemoryStream(File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~\Images\richmenu.PNG")));
                // Upload Image
                await lineClient.UploadRichMenuImage(richMenuId, image);
                // Link to user
                await lineClient.LinkRichMenuToUser(lineEvent.Source.UserId, richMenuId);
            }
            else if (textMessage.Text.ToLower() == "deleterichmenu")
            {
                // Get Rich Menu for the user
                var richMenuId = await lineClient.GetRichMenuIdForUser(lineEvent.Source.UserId);

                await lineClient.UnlinkRichMenuToUser(lineEvent.Source.UserId);
                await lineClient.DeleteRichMenu(richMenuId);
            }
            else if (textMessage.Text.ToLower() == "deleteallrichmenu")
            {
                // Get Rich Menu for the user
                var richMenuList = await lineClient.GetRichMenuList();
                foreach (var richMenu in richMenuList)
                {
                    await lineClient.DeleteRichMenu(richMenu["richMenuId"].ToString());
                }
            }
            else if(textMessage.Text.ToLower() == "劉加菲")
            {
                replyMessage = new TextMessage("是個白爛女人!!");
            }
            else
            {
                replyMessage = new TextMessage(textMessage.Text);
            }

            if (replyMessage != null)
                await Reply(replyMessage);
        }

        public async Task HandleMediaMessage()
        {
            Message message = JsonConvert.DeserializeObject<Message>(lineEvent.Message.ToString());
            // Get media from Line server.
            Media media = await lineClient.GetContent(message.Id);
            Message replyMessage = null;

            // Reply Image 
            switch (message.Type)
            {
                case MessageType.Image:
                case MessageType.Video:
                case MessageType.Audio:
                    replyMessage = new ImageMessage("https://github.com/apple-touch-icon.png", "https://github.com/apple-touch-icon.png");
                    break;
            }

            await Reply(replyMessage);
        }

        public async Task HandleStickerMessage()
        {
            //https://devdocs.line.me/files/sticker_list.pdf
            var stickerMessage = JsonConvert.DeserializeObject<StickerMessage>(lineEvent.Message.ToString());
            var replyMessage = new StickerMessage("1", "1");
            await Reply(replyMessage);
        }

        public async Task HandleLocationMessage()
        {
            var locationMessage = JsonConvert.DeserializeObject<LocationMessage>(lineEvent.Message.ToString());
            LocationMessage replyMessage = new LocationMessage(
                locationMessage.Title,
                locationMessage.Address,
                locationMessage.Latitude,
                locationMessage.Longitude);
            await Reply(replyMessage);
        }

        private async Task Reply(Message replyMessage)
        {
            try
            {
                await lineClient.ReplyToActivityAsync(lineEvent.CreateReply(message: replyMessage));
            }
            catch
            {
                await lineClient.PushAsync(lineEvent.CreatePush(message: replyMessage));
            }
        }
    }

}