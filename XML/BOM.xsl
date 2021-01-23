<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet exclude-result-prefixes="xs b" version="2.0" xmlns:b="urn:sparesmanual.com:bom"
  xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output encoding="UTF-8" method="html" version="5" />
  <xsl:output encoding="UTF-8" method="html" name="html5" version="5" />

  <xsl:template match="/">
    <html>
      <head>
        <title>Bill of materials</title>
      </head>
      <body>
        <div class="container">
          <xsl:apply-templates select="b:bill" />
        </div>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="b:bill">
    <h1>Bill of materials</h1>
    <xsl:apply-templates select="b:client" />

    <table>
      <thead>
        <tr>
          <th>Part number</th>
          <th>Makers part number</th>
          <th>Quantity</th>
          <th>Descritpion</th>
        </tr>
      </thead>
      <tfoot>
        <tr>
          <td colspan="0">Total quantity: <xsl:value-of select="sum(/b:bill/b:material/@quantity)"
             /></td>
        </tr>
      </tfoot>
      <tbody>
        <xsl:apply-templates select="b:material" />
      </tbody>
    </table>
  </xsl:template>

  <xsl:template match="b:client">
    <h2>Client details</h2>
    <ul>
      <xsl:apply-templates />  
    </ul>    
  </xsl:template>
  
  <xsl:template match="b:name">
    <li><strong>Name: </strong><xsl:value-of select="."/></li>
  </xsl:template>
  
  <xsl:template match="b:email">
    <li><strong>Email: </strong><xsl:value-of select="."/></li>
  </xsl:template>
  
  <xsl:template match="b:message">
    <li><strong>Message: </strong><xsl:value-of select="."/></li>
  </xsl:template>

  <xsl:template match="b:material">
    <tr>
      <td>
        <xsl:value-of select="@partNumber" />
      </td>
      <td>
        <xsl:value-of select="@makersPartNumber" />
      </td>
      <td>
        <xsl:value-of select="@quantity" />
      </td>
      <td>
        <xsl:value-of select="." />
      </td>
    </tr>
  </xsl:template>

</xsl:stylesheet>