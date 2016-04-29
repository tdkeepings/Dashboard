<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Dashboard_Azure.Register" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div class="centered">
            <div class="registerForm">
                <div class="formField">
                    <asp:Label runat="server" id="ErrorLabel" name="ErrorLabel" type="text"></asp:Label>
                </div>
                <div class="formField">
                    <asp:TextBox runat="server" id="UsernameTextBox" name="UsernameTextBox" type="text" placeholder="Username" ></asp:TextBox>
                </div>
                <div class="formField">
                    <asp:TextBox runat="server" ID="PasswordTextBox" name="PasswordTextBox"  type="password" placeholder="Password"></asp:TextBox>
                </div>
                <div class="formField">
                    <asp:TextBox runat="server" ID="PasswordConfirmTextBox" name="PasswordConfirmTextBox"  type="password" placeholder="Password"></asp:TextBox>
                </div>
                <div class="formField">
                    <asp:Button type="submit" Text="Register" class="button accept" runat="server" id="RegisterButton" OnClick="RegisterClicked"/>
                </div>
                <div class="formField">
                    <a href="/Login.aspx">Login</a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
