<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="E_Commerce.Carrello" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <main>

     <div class=" container">
         <div class="row border-3 border-bottom border-dark mt-2 mb-4">
             <div class="col-9">
                 <h2>Totale Carrello: </h2>
             </div>
             <div class="col-3">
                 <asp:Button CssClass="btn btn-danger" Text="Svuota carrello" ID="SvuotaCarrello" runat="server" OnClick="SvuotaCarrello_Click" />
             </div>
             <div class="col-12">
                 <h2 class="text-start" id="contenitoreTot" runat="server"></h2>
             </div>
         </div>
         <div class=" row">
             <div class="col">
                 <div id="ContenitoreProdottiCarrello" runat="server" class="row row-cols-3 g-3">
                     
                 </div>

             </div>
         </div>
     </div>


 </main>

</asp:Content>
