<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <header class="bg-primary text-white">
      <div class="container text-center">
        <h1>Bienvenido a BiosMoney</h1>
        <p class="lead">En nuestra pagina podra consultar si sus facturas se encuentran 
            abonadas, ademas podra consultar nuestras empresas afiliadas.</p>
      </div>
      

    </header>
              <div >
              <asp:Image ID="Image1" runat="server" 
                  DescriptionUrl="~/Imagenes/Logo Bios Money.jpg" Height="100%" 
                  ImageAlign="Left" ImageUrl="~/Imagenes/Logo Bios Money.jpg" Width="100%" />
          </div>
   
  
    <!-- Footer -->
    <footer class="py-5 bg-dark">
      <div class="container">
        <p class="m-0 text-center text-white">Copyright &copy; BiosMoney </p>
      </div>
      <!-- /.container -->
    </footer>

    <!-- Bootstrap core JavaScript -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom JavaScript for this theme -->
    <script src="js/scrolling-nav.js"></script>
</asp:Content>

