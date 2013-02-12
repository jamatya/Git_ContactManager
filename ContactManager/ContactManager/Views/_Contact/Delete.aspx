<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Contact>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>
   
    <p>
        Are you sure you want to delete the entry for <%= Model.FirstName %> <%= Model.LastName %> ?
    </p>

    <% using (Html.BeginForm(new { Id = Model.Id })) %>
        <% { %>
            <p>
                <input type = "submit" value = "Delete" />
            </p>
        <% } %>
    <div>
        <%=Html.ActionLink("Back To List", "Index") %>
    </div>

</asp:Content>
 