﻿using System;
using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Orchard.Core.Containers.Extensions;
using Orchard.Core.Containers.Models;
using Orchard.Core.Containers.ViewModels;
using Orchard.Data;
using Orchard.Localization;

namespace Orchard.Core.Containers.Drivers {
    public class ContainerWidgetPartDriver : ContentPartDriver<ContainerWidgetPart> {
        private readonly IContentManager _contentManager;

        public ContainerWidgetPartDriver(IContentManager contentManager) {
            _contentManager = contentManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ContainerWidgetPart part, string displayType, dynamic shapeHelper) {
            return ContentShape(
                "Parts_ContainerWidget",
                () => {
                    var container = _contentManager.Get(part.Record.ContainerId);

                    IContentQuery<ContentItem> query = _contentManager
                        .Query(VersionOptions.Published)
                        .Join<CommonPartRecord>().Where(cr => cr.Container.Id == container.Id);

                    var descendingOrder = part.Record.OrderByDirection == (int)OrderByDirection.Descending;
                    query = query.OrderBy(part.Record.OrderByProperty, descendingOrder);

                    if (part.Record.ApplyFilter)
                        query = query.Where(part.Record.FilterByProperty, part.Record.FilterByOperator, part.Record.FilterByValue);

                    var pageOfItems = query.Slice(0, part.Record.PageSize).ToList();

                    var list = shapeHelper.List();
                    list.AddRange(pageOfItems.Select(item => _contentManager.BuildDisplay(item, "Summary")));

                    return shapeHelper.Parts_ContainerWidget(ContentItems: list);
                });
        }

        protected override DriverResult Editor(ContainerWidgetPart part, dynamic shapeHelper) {
            return Editor(part, null, shapeHelper);
        }

        protected override DriverResult Editor(ContainerWidgetPart part, IUpdateModel updater, dynamic shapeHelper) {
            return ContentShape(
                "Parts_ContainerWidget_Edit",
                () => {
                    var model = new ContainerWidgetViewModel {Part = part};

                    if (updater != null) {
                        updater.TryUpdateModel(model, "ContainerWidget", null, null);
                    }

                    var containers = _contentManager.Query<ContainerPart, ContainerPartRecord>(VersionOptions.Latest).List();
                    var listItems = containers.Count() < 1
                        ? new[] {new SelectListItem {Text = T("(None - create container enabled items first)").Text, Value = "0"}}
                        : containers.Select(x => new SelectListItem {
                                Value = Convert.ToString(x.Id),
                                Text = x.ContentItem.TypeDefinition.DisplayName + ": " + x.As<IRoutableAspect>().Title,
                                Selected = x.Id == model.Part.Record.ContainerId,
                            });

                    model.AvailableContainers = new SelectList(listItems, "Value", "Text", model.Part.Record.ContainerId);

                    return shapeHelper.EditorTemplate(TemplateName: "ContainerWidget", Model: model, Prefix: "ContainerWidget");
                });
        }
    }

    public class ContainerWidgetPartHandler : ContentHandler {
        public ContainerWidgetPartHandler(IRepository<ContainerWidgetPartRecord> repository, IOrchardServices orchardServices) {
            Filters.Add(StorageFilter.For(repository));
            OnInitializing<ContainerWidgetPart>((context, part) => {
                part.Record.ContainerId = 0;
                part.Record.PageSize = 5;
                part.Record.OrderByProperty = part.Is<CommonPart>() ? "CommonPart.PublishedUtc" : "";
                part.Record.OrderByDirection = (int)OrderByDirection.Descending;
                part.Record.FilterByProperty = "CustomPropertiesPart.CustomOne";
                part.Record.FilterByOperator = "=";
            });
        }
    }
}