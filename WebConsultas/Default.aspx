<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <header class="bg-primary text-white">
      <div class="container text-center">
      <div class="row">
          <div class="col-lg-8">
            <br><br><br><br>
            <h1>BiosMoney</h1>
            <p class="lead">En nuestra pagina podra consultar si sus facturas se encuentran 
                abonadas, ademas podra consultar nuestras empresas afiliadas.</p>
          </div>
              <div class="col-lg-4">
                <div>
                    <asp:Image ID="Image2" 
                               runat="server" 
                               Height="100%" 
                               Width="100%" 
                               ImageUrl="~/Imagenes/Logo Bios Money.jpg" />
                </div>
              </div>
      </div>
      </div>

    </header>

    <!-- Sobre la Empresa -->
    <section id="services" class="bg-light">
      <div class="container">
        <div class="row">
          <div class="col-lg-8 mx-auto">
            <h2>Nuestra Empresa</h2>
            <p class="lead">Somos una empresa uruguaya cuyo principal objetivo es brindar servicios de calidad en el mercado transaccional de cobranzas y pagos. Trabajamos día a día con honestidad, responsabilidad, eficiencia y vocación, con el propósito de estar en constante desarrollo e innovar en nuevos mercados y tecnologías.</p>
          </div>
        </div>
      </div>
    </section>

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

