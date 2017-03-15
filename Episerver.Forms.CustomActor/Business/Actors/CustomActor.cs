using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Episerver.Forms.CustomActor.Business.Extensions;
using EPiServer.Forms.Core.Data;
using EPiServer.Forms.Core.PostSubmissionActor;
using EPiServer.Forms.EditView;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using Episerver.Forms.CustomActor.Models.Properties;

namespace Episerver.Forms.CustomActor.Business.Actors
{
    public class CustomActor : PostSubmissionActorBase, IUIPropertyCustomCollection
    {
        private readonly Injected<IFormDataRepository> _formDataRepository;

        public override object Run(object input)
        {
            var ret = string.Empty;

            var actorModel = ((IEnumerable<CustomActorModel>)this.Model).SingleOrDefault();
            var webhook = actorModel?.NoTrackingWebHook ?? "";
            var trackingType = "";

            if (actorModel != null && !string.IsNullOrEmpty(this.HttpRequestContext.Cookies.Get("trackingCookie")?.Value))
            {
                switch (this.HttpRequestContext.Cookies.Get("trackingCookie")?.Value.ToLower())
                {
                    case "payperclick":
                        webhook = actorModel.PaidSearchWebHook;
                        trackingType = "ppc";
                        break;
                    case "socialnetwork":
                        webhook = actorModel.SocialNetworkWebHook;
                        trackingType = "sn";
                        break;
                }
            }

            if (!string.IsNullOrEmpty(webhook))
            {
                var submittedData = _formDataRepository.Service.TransformSubmissionDataWithFriendlyName(
                    SubmissionData.Data, SubmissionFriendlyNameInfos, true).Where(x => x.Key.StartsWith("customField_")).ToNameValueCollection();
                submittedData.Add("tracking_type", trackingType);

                var webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                webClient.UploadValues(webhook, submittedData);
            }

            return ret;
        }

        public virtual Type PropertyType => typeof(PropertyForDisplayingCustomActor);
    }

    /// <summary>
    /// Property definition for the Actor
    /// </summary>
    [EditorHint("CustomActorModelPropertyHint")]
    [PropertyDefinitionTypePlugIn(DisplayName = "CustomActorModelProp")]
    public class PropertyForDisplayingCustomActor : PropertyGenericList<CustomActorModel> { }
}