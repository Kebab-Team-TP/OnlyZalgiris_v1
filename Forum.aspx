<%@ Page Title="Forum" Language="C#" MasterPageFile="~/Site.Master"  enableEventValidation="false" Async="true" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="Zalgiris.Forum" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .forum-container {
        margin: auto;
        max-width: 500px;
        height: 500px;
        border: 1px solid black;
        border-radius: 12px;
        color: black;
        margin-bottom: 25px;
        padding: 10px;  /* Added padding for inner spacing */
        overflow-y: auto; /* Ensures content is scrollable if it exceeds the height */
    }

    .message-block {
        display: flex;
        align-items: top;
        margin: 10px 0;
        padding: 5px; /* Padding for each message block */
        background: #f2f2f2; /* Background color for visibility */
        border-radius: 5px; /* Rounded corners for message blocks */
    }

    .profile-pic {
        flex-shrink: 0;
        width: 50px; /* Fixed width for profile pictures */
        height: 50px; /* Fixed height for profile pictures */
        margin-right: 10px; /* Space between the picture and the text */
        border-radius: 50%; /* Makes the profile picture circular */
    }

    .message-content {
        flex-grow: 1; /* Allows the message content to fill the remaining space */
    }

    .date {
        font-size: 0.8em; /* Smaller font size for date */
        color: #666; /* Grey color for date */
        margin-bottom: 5px; /* Space between date and message text */
    }

    .text {
        background: white; /* White background for text bubble */
        padding: 8px; /* Padding around text */
        border-radius: 5px; /* Rounded corners for text bubble */
        box-shadow: 0 2px 2px rgba(0,0,0,0.2); /* Subtle shadow for depth */
    }

    /* Specific styles for author messages */
    .author {
        justify-content: flex-end; /* Aligns author messages to the right */
    }

    .author .profile-pic {
        order: 1; /* Ensures the picture is on the right */
        margin-left: 10px; /* Left margin for the picture when on the right */
        margin-right: 0; /* Removes the right margin */
    }

    .author .message-content {
        text-align: right; /* Right-aligns the text content */
        order: 0; /* Ensures the text content appears before the picture */
    }

    /* Styling for the forum input field */
    .forum-input {
        max-width: 100%;
        width: 100%;
        height: 35px;
        border: none;
        border-top: 3px solid black;
        padding: 12px;
        border-radius: 0;
        border-bottom-right-radius: 12px;
        border-bottom-left-radius: 12px;
        font-size: 15px;
    }

    /* Utility classes */
    .d-none {
        display: none;
    }
    .profile-picture-section {
    margin-top: 20px;
    border-top: 1px solid #ccc;
    padding-top: 20px;
}

.profile-picture-section h3 {
    color: #333;
}

/* Styles for RadioButtonList and images within it */
.profile-picture-section .RadioButtonList {
    padding: 10px 0;
}

.profile-picture-section .RadioButtonList img {
    margin-right: 10px;
    border-radius: 5px;
    transition: transform 0.3s ease;
}

.profile-picture-section .RadioButtonList img:hover {
    transform: scale(1.1);
    cursor: pointer;
}
/* Button Styles */
button, .aspButton {
    background-color: #4CAF50; /* Green background */
    border: none;
    color: white;
    padding: 12px 24px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 4px 2px;
    transition: background-color 0.3s ease, box-shadow 0.3s ease; /* Smooth transition for hover effects */
    cursor: pointer;
    border-radius: 4px; /* Rounded corners */
    box-shadow: 0 4px #999; /* Simulated depth */
}

button:hover, .aspButton:hover {
    background-color: #45a049; /* Darker shade of button color on hover */
    box-shadow: 0 8px #666; /* Shadow gets darker and seems to 'lift' */
    transform: translateY(-2px); /* Button moves up slightly on hover */
}

button:active, .aspButton:active {
    background-color: #3e8e41;
    box-shadow: 0 2px #666;
    transform: translateY(4px); /* Button seems to be pressed down */
}
</style>

<div class="container">
    <h2 class="forum-header"><%: Title %></h2>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="dropdown-style"></asp:DropDownList>

    <div class="forum-container">
        <asp:Label ID="MessagesField" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="MessageText" CssClass="forum-input" runat="server" OnTextChanged="MessageText_TextChanged"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="d-none" />
    </div>

    <div class="profile-picture-section">
        <h3>Change Profile Picture</h3>
        <asp:RadioButtonList ID="ProfilePictureSelection" runat="server" RepeatDirection="Horizontal">
    <asp:ListItem Value="Images/1367304-161641-1287x836.jpg" Text="<img src='Images/1367304-161641-1287x836.jpg' alt='Profile Picture' style='width:60px;height:60px;'>" />
    <asp:ListItem Value="Images/1683138729890-1024x683.jpg" Text="<img src='Images/1683138729890-1024x683.jpg' alt='Profile Picture' style='width:60px;height:60px;'>" />
    <asp:ListItem Value="Images/zal1_13.jpg" Text="<img src='Images/zal1_13.jpg' alt='Profile Picture' style='width:60px;height:60px;'>" />
    <asp:ListItem Value="Images/kauno-zalgirispristate-nauja-logotipa-64f9ee6196407.jpg" Text="<img src='Images/kauno-zalgirispristate-nauja-logotipa-64f9ee6196407.jpg' alt='Profile Picture' style='width:60px;height:60px;'>" />
    <asp:ListItem Value="Images/paveikslelis-393083-crop700x700.jpg" Text="<img src='Images/paveikslelis-393083-crop700x700.jpg' alt='Profile Picture' style='width:60px;height:60px;'>" />
</asp:RadioButtonList>

        <asp:Button ID="SaveProfilePicture" runat="server" Text="Save Selection" OnClick="SaveProfilePicture_Click" CssClass="aspButton" />

    </div>
</div>
</asp:Content>