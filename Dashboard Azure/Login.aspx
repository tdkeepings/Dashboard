<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="Dashboard_Azure.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div class="centered">
            <div class="loginForm">
                <div class="formField">
                    <asp:Label runat="server" ID="ErrorLabel"></asp:Label>
                </div>
                <div class="formField">
                    <asp:TextBox runat="server" id="UsernameTextBox" name="UsernameTextBox" type="text" placeholder="Username" ></asp:TextBox>
                </div>
                <div class="formField">
                    <asp:TextBox runat="server" ID="PasswordTextBox" name="PasswordTextBox"  type="password" placeholder="Password"></asp:TextBox>
                </div>
                <div class="formField">
                    <asp:Button type="submit" Text="Login" class="button accept" runat="server" id="LoginButton" OnClick="LoginClicked"/>
                </div>
                <a href="/Register.aspx">Register</a>
            </div>
        </div>
    </form>
</asp:Content>
