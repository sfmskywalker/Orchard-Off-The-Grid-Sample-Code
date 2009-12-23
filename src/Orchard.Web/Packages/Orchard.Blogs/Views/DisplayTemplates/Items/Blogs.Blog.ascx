﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ItemDisplayModel<Blog>>" %>
<%@ Import Namespace="Orchard.Blogs.Extensions"%>
<%@ Import Namespace="Orchard.Blogs.Models"%>
<%@ Import Namespace="Orchard.ContentManagement.ViewModels"%>
<div class="manage"><a href="<%=Url.BlogEdit(Model.Item.Slug) %>" class="ibutton edit">edit</a></div>
<h1><%=Html.TitleForPage(Model.Item.Name) %></h1>
<div><%=Html.Encode(Model.Item.Description) %></div>
<%--TODO: (erikpo) Need to figure out which zones should be displayed in this template--%>
<%=Html.DisplayZonesAny() %>