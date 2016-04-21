<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Dashboard_Azure.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div ng-controller="PageController as page" ng-init="load()">
        <div class="loader">Loading...</div>
		<div class="linkContainer" >
            <div class="buttonContainer col-md-12">
                <button type="button" class="btn btn-success addSite">+ Add Site</button>
                <button type="button" class="btn btn-success addColumn">+ Add Column</button>
            </div>
            <div id="controller">
                <div ng-repeat="column in page.columns">
                    <div class="clearFloat" ng-if="$index % 6 == 0">&nbsp;</div>
                    <div class="column col-md-2" >
                        <h3>{{ column.Name }} <a ng-click='deleteColumn(column.Name)' class="close" href="#">&#10006;</a></h3>
                        <div ng-repeat="link in column.Sites[0]">
                            <div class="site" style="background-color: {{link.BgColour}}; color: {{link.Colour}};">
                                <a href="{{link.Url}}" target="_blank">
                                    {{link.Name}}
                                </a>
                                <a ng-click='deleteSite(link.Name)' class="close" href="#">&#10006;</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
		</div>

        <div class="overlay siteOverlay">
            <div class="content">
                <h2>Add new site</h2>
                <a class="close" href="#">&#10006;</a>
                <div class="formField"><label for="SiteName">Name: </label><input id="SiteName" type="text" /></div>
                <div class="formField">
                    <label for="ParentColumn">Parent Column: </label>
                    <select id="ColumnName">
                        <option>Choose a column for your new site...</option>
                        <option  ng-repeat="column in page.columns">{{ column.Name }}</option>
                    </select>
                </div>
                <div class="formField"><label for="SiteUrl">Url: </label><input id="SiteUrl" type="text" /></div>
                <div class="formField"><label for="SiteBgColour">Background color: </label><input id="SiteBgColour" type="text" /></div>
                <div class="formField"><label for="SiteColour">Font color: </label><input id="SiteColour" type="text" /></div>

                <div class="buttons">
                    <input class="button accept" type="button" value="Accept" ng-click="addSite()"/>
                    <input class="button cancel" type="button" value="Cancel" />
                </div>
            </div>
        </div>

        <div class="overlay columnOverlay">
            <div class="content">
                <h2>Add new column</h2>
                <a class="close" href="#">&#10006;</a>
                <div class="formField"><label for="ColumnName">Name: </label><input id="ColumnName" type="text" /></div>

                <div class="buttons">
                    <input class="button accept" type="button" value="Accept"  ng-click="addColumn()"/>
                    <input class="button cancel" type="button" value="Cancel" />
                </div>
            </div>
        </div>

        <div class="overlay login">
            <div class="content">
                <h2>Login</h2>

                <a class="close" href="#">&#10006;</a>
                <div class="formField">
                    <input type="text" placeholder="Username" />
                </div>
                <div class="formField">
                    <input type="password" placeholder="Password" />
                </div>

                <div class="buttons">
                    <input class="button accept" type="button" value="Accept"  ng-click="login()"/>
                    <input class="button cancel" type="button" value="Cancel" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
