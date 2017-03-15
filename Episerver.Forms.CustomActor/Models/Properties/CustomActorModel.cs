using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Forms.Core.PostSubmissionActor;

namespace Episerver.Forms.CustomActor.Models.Properties
{
    /// <summary>
    /// This will be the model for Actor config, it is a row in the Actor configuration UI in the EditView
    /// </summary>
    [Serializable]
    public class CustomActorModel : IPostSubmissionActorModel, ICloneable
    {
        [Display(Name = "Pay Per Click Web Hook", Order = 10)]
        public virtual string PaidSearchWebHook { get; set; }

        [Display(Name = "Social Network Web Hook", Order = 20)]
        public virtual string SocialNetworkWebHook { get; set; }

        [Display(Name = "No Tracking Cookie Web Hook", Order = 30)]
        public virtual string NoTrackingWebHook { get; set; }

        public object Clone()
        {
            return new CustomActorModel
            {
                PaidSearchWebHook = this.PaidSearchWebHook,
                SocialNetworkWebHook = this.SocialNetworkWebHook,
                NoTrackingWebHook = this.NoTrackingWebHook
            };
        }

    }
}