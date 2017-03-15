using System.Collections.Generic;
using Episerver.Forms.CustomActor.Models.Properties;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace Episerver.Forms.CustomActor.Business.EditorDescriptors
{
    /// <summary>
    /// Editor descriptor class, for using Dojo widget CollectionEditor to render.
    /// Inherit from <see cref="CollectionEditorDescriptor{T}"/>, it will be rendered as a grid UI.
    /// </summary>
    [EditorDescriptorRegistration(TargetType = typeof(IEnumerable<CustomActorModel>), UIHint = "CustomActorModelPropertyHint")]
    public class CustomActorEditorDescriptor : CollectionEditorDescriptor<CustomActorModel>
    {
        public CustomActorEditorDescriptor()
        {
            ClientEditingClass = "epi-forms/contentediting/editors/CollectionEditor";
        }
    }
}