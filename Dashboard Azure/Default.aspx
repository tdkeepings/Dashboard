<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Dashboard_Azure.Default" %>

<!DOCTYPE html>
<head>
	<title>Dashboard</title>
	<link rel="icon" href="favicon.ico" type="image/x-icon" />
	<script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="/scripts/script.js"></script>
	<script type="text/javascript" src="/scripts/angular.js"></script>
    <script type="text/javascript" src="/scripts/app.js"></script>
	<link rel="stylesheet" href="/css/style.min.css">
</head>

<body ng-app="LinkPage">
	<div class="constrain"  ng-controller="PageController as page" ng-init="load()">
		<div class="content">
            <div class="centered">
                <h1>Dashboard</h1>
                <p>Ripping off Googles new tab page since 2015</p>
                <asp:Label runat="server" ID="Test"></asp:Label>
            </div>
			
            <div class="loader">Loading...</div>
            
			<div class="linkContainer" >
                <div class="buttonContainer col-md-12">
                    <button type="button" class="btn btn-success addSite">+ Add Site</button>
                    <button type="button" class="btn btn-success addColumn">+ Add Column</button>
                </div>
                <div id="controller">
                    <div ng-repeat="column in page.columns">
                        <div class="column col-md-2" >
                            <h3>{{ column.Name }}</h3>
                            <div ng-repeat="link in column.Sites[0]">
                                <div class="site" style="background-color: {{link.BgColour}}; color: {{link.Colour}};">
                                    <a href="{{link.Url}}" target="_blank">
                                        {{link.Name}}
                                    </a>
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

		</div>
	</div>
</body>
