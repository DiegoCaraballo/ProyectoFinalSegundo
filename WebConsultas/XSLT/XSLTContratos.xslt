<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!--Determino que el formato se aplica a todo el documento-->
  <xsl:template match="/">

    <table>
      <xsl:for-each select="TiposDeContratos/TipoContrato">
      
        <tr>  
            <td style="background-color:#04B486;padding:4px;font-size:15pt;font-weight:bold;color:white">
                Codigo: <xsl:value-of select="CodContrato"/>
              </td>

              <td style="background-color:#04B486;padding:4px;font-size:15pt;font-weight:bold;color:white">
                Nombre: <xsl:value-of select="NomContrato"/>
              </td>
        </tr>

      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>