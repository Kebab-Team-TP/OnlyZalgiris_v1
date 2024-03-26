<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Gallery.aspx.cs" Inherits="Zalgiris.Gallery" %>

<asp:Content ID="GalleryContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .gallery-container {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 15px;
            padding: 20px;
        }
        .gallery-item {
            position: relative;
            overflow: hidden;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .gallery-item img {
            width: 100%;
            height: auto;
            transition: transform 0.5s ease, opacity 0.5s ease;
        }
        .gallery-item:hover img {
            transform: scale(1.05);
            opacity: 0.9;
        }
        @media (max-width: 600px) {
            .gallery-container {
                grid-template-columns: 1fr;
            }
        }
    </style>

    <div class="gallery-container">
        <!-- Image 1 -->
        <div class="gallery-item">
            <img src="Images/1367304-161641-1287x836.jpg" alt="zalgiris">
        </div>
        <!-- Image 2 -->
        <div class="gallery-item">
            <img src="Images/1683138729890-1024x683.jpg" alt="zalgiris">
        </div>
        <!-- Image 3 -->
        <div class="gallery-item">
            <img src="Images/1686070211549-1024x684.jpg" alt="zalgiris">
        </div>
        <!-- Image 4 -->
        <div class="gallery-item">
            <img src="Images/1686070255323-1024x683.jpg" alt="zalgiris">
        </div>
        <!-- Image 5 -->
        <div class="gallery-item">
            <img src="Images/6bc58ca0-7dfd-11ed-ab81-a71b60a80a19.jpg" alt="zalgiris">
        </div>
        <!-- Image 6 -->
        <div class="gallery-item">
            <img src="Images/acbe203e2fca0aaf17cc29402ff7c3dd.jpg" alt="zalgiris">
        </div>
        <!-- Image 7 -->
        <div class="gallery-item">
            <img src="Images/ad318d629cf51b6c11f09f63296b0268.jpg"alt="zalgiris">
        </div>
        <!-- Image 8 -->
        <div class="gallery-item">
            <img src="Images/F3zZR3NWYAAP4Ph.jpeg" alt="zalgiris">
        </div>
        <!-- Image 9 -->
        <div class="gallery-item">
            <img src="Images/3dec5e00-e9d5-11ed-be0d-01badd26f64f.jpg" alt="zalgiris">
        </div>
        <!-- Image 10 -->
        <div class="gallery-item">
            <img src="Images/ZalgirioArena20160420-1.jpg" alt="zalgiris">
        </div>
    </div>
</asp:Content>



