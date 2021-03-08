<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet exclude-result-prefixes="xs b" version="2.0" xmlns:b="urn:sparesmanual.com:bom"
                xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output encoding="UTF-8" method="html" version="5"/>
  <xsl:output encoding="UTF-8" method="html" name="html5" version="5"/>

  <xsl:template match="/">
    <html>
      <body>
        <xsl:apply-templates select="b:bill"/>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="b:bill">
    <h1>Bill of materials</h1>
    <br/>
    <xsl:apply-templates select="b:client"/>

    <table border='1' style='border-collapse:collapse'>
      <tr>
        <th bgcolor="#adadad" color="white" align="center">
          <strong>Part number</strong>
        </th>
        <th bgcolor="#adadad" color="white" align="center">
          <strong>Makers number</strong>
        </th>
        <th bgcolor="#adadad" color="white" align="center">
          <strong>Quantity</strong>
        </th>
        <th bgcolor="#adadad" color="white" align="center">
          <strong>Descritpion</strong>
        </th>
      </tr>
      <xsl:apply-templates select="b:material"/>
    </table>

    <p>
      <em>Total quantity:
        <xsl:value-of select="sum(/b:bill/b:material/@b:quantity)"/>
      </em>
    </p>
  </xsl:template>

  <xsl:template match="b:client">
    <br/>
    <h2>Client details</h2>
    <br/>
    <ul>
      <xsl:apply-templates/>
    </ul>
  </xsl:template>

  <xsl:template match="b:name">
    <li>
      <strong>Name:</strong>
      <xsl:value-of select="."/>
    </li>
  </xsl:template>

  <xsl:template match="b:email">
    <li>
      <strong>Email:</strong>
      <xsl:value-of select="."/>
    </li>
  </xsl:template>

  <xsl:template match="b:message">
    <li>
      <strong>Message:</strong>
      <xsl:value-of select="."/>
    </li>
  </xsl:template>

  <xsl:template match="b:material">
    <tr>
      <td>
        <xsl:value-of select="@b:partNumber"/>
      </td>
      <td>
        <xsl:value-of select="@b:makersPartNumber"/>
      </td>
      <td>
        <xsl:value-of select="@b:quantity"/>
      </td>
      <td>
        <xsl:value-of select="."/>
      </td>
    </tr>
  </xsl:template>

</xsl:stylesheet>
