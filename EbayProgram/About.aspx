<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EbayProgram.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblName" runat="server" Text="Item Name: "></asp:Label>
    <asp:TextBox ID="txtCardName" runat="server"></asp:TextBox>
    <asp:Label ID="lblCollectionNum" runat="server" Text="Collection Number: "></asp:Label>
    <asp:TextBox ID="TxtCollectionNum" runat="server"></asp:TextBox>
    <asp:Label ID="lblListed" runat="server" Text="Listed Value: "></asp:Label>
    <asp:TextBox ID="txtListValue" runat="server"></asp:TextBox><br><br>
    <asp:Label ID="lblSold" runat="server" Text="Sold Value: "></asp:Label>
    <asp:TextBox ID="txtSoldValue" runat="server"></asp:TextBox>
    <asp:Label ID="lblStyle" runat="server" Text="Listing Style: "></asp:Label>
    <asp:TextBox ID="txtListStyle" runat="server"></asp:TextBox>
    <asp:Label ID="lblShipping" runat="server" Text="Shipping: "></asp:Label>
    <asp:TextBox ID="txtShipping" runat="server"></asp:TextBox><br><br>
    <asp:Button ID="btnSave" runat="server" Text="Save Item" OnClick="btnSave_Click" />
    <asp:Button ID="btnShow" runat="server" Text="Display Items" OnClick="btnShow_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete Item" OnClick="btnDelete_Click" />
    <asp:Button ID="btnUpdate" runat="server" Text="Update Item" OnClick="btnUpdate_Click" />
	<asp:Label ID="lblSearch" runat="server" Text="Select an Item Number for Delete or Update: "></asp:Label>
	<asp:ListBox ID="listBoxFill" runat="server" Height="30px" Width="75px"></asp:ListBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
	<br><br>
    <asp:TextBox ID="txtTotalActiveCards" runat="server"></asp:TextBox>
    <asp:Button ID="btnTotalActiveCards" runat="server" Text="Display Total" OnClick="btnTotalActiveCards_Click" />
    <asp:TextBox ID="txtTotalSold" runat="server"></asp:TextBox>
    <asp:Button ID="btnDisplayTotal" runat="server" Text="Display Total" OnClick="btnDisplayTotal_Click" />
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>Item Name</asp:ListItem>
        <asp:ListItem>Collection Number</asp:ListItem>
        <asp:ListItem>Sold Items</asp:ListItem>
        <asp:ListItem>Unsold Items</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtSearchBox" runat="server"></asp:TextBox>
    <asp:Button ID="btnDisplaySearch" runat="server" OnClick="btnDisplaySearch_Click" Text="Button" />
    <br><br>

    <asp:GridView ID="GridView1" runat="server" Height="292px" Width="750px" AutoGenerateColumns="True" BorderStyle="Solid"> 
    </asp:GridView>
    
</asp:Content>
