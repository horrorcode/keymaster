<xsl:stylesheet
  version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns="http://www.w3.org/1999/xhtml">

 <!-- include default formatting templates from svn2cl.xsl -->
 <xsl:include href="svn2cl.xsl" />

 <xsl:output
   method="xml"
   encoding="utf-8"
   media-type="text/html"
   omit-xml-declaration="no"
   standalone="yes"
   indent="yes"
   doctype-public="-//W3C//DTD XHTML 1.1//EN"
   doctype-system="http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd" />

 <!-- title of the report -->
 <xsl:param name="title" select="'ChangeLog'" />

 <!-- link to use for linking revision numbers -->
 <xsl:param name="revision-link" select="'#r'" />

 <!-- match toplevel element -->
 <xsl:template match="log">
  <html>
   <head>
    <title><xsl:value-of select="string($title)" /></title>
    <link rel="stylesheet" href="svn2html.css" type="text/css" />
		<style type="text/css">
	body {
  background-color: white;
  color: black;
  margin-left: 1.5em;
  margin-right: 1.5em;
  margin-top: 1.5em;
  margin-bottom: 1em;
}

ul.changelog_entries {
  margin-left: 0.7em;
  margin-right: 0.7em;
  padding-left: 0.7em;
  padding-right: 0.7em;
  padding-bottom: 0.7em;
  background: #fefefe;
  border: 1px dashed #88aa88;
}

li.changelog_entry {
  list-style-type: none;
  margin-left: 0px;
  padding-left: 0px;
  margin-top: 0.8em;
  border-top: 1px solid #dddddd;
  background: #f8f8f8;
}

li.changelog_change {
  list-style-type: circle;
  margin-left: 4em;
}

span.changelog_date {
  color: black;
}

span.changelog_author {
  color: #111188;
}

.changelog_revision {
  font-size: 80%;
  color: #881111;
  background: #fff4f4;
}

.changelog_revision a {
  color: inherit;
}

.changelog_files {
  font-size: 80%;
  font-family: monospace;
  color: #116611;
}

.changelog_files:after {
  content: ':';
}

.changelog_message {
  display: block;
  color: #220000;
}

p.changelog_footer {
  margin-top: 1.5em;
  margin-left: 0.7em;
  font-style: italic;
  line-height: 90%;
  color: gray;
  font-family: Helvetica, Arial, sans-serif;
}

p.changelog_footer a {
  text-decoration: none;
  color: inherit;
}

	</style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   </head>
   <body>
    <xsl:if test="$title">
     <h1><xsl:value-of select="string($title)" /></h1>
    </xsl:if>
    <ul class="changelog_entries">
     <xsl:choose>
      <xsl:when test="$ignore-message-starting != ''">
       <!-- only handle logentries with don't contain the string -->
       <xsl:apply-templates select="logentry[not(starts-with(msg,$ignore-message-starting))]" />
      </xsl:when>
      <xsl:otherwise>
       <xsl:apply-templates select="logentry" />
      </xsl:otherwise>
     </xsl:choose>
    </ul>
    <p class="changelog_footer">
     <xsl:text>Generated by </xsl:text><a href="http://ch.tudelft.nl/~arthur/svn2cl/">svn2cl 0.11</a>
    </p>
   </body>
  </html>
 </xsl:template>

 <!-- format one entry from the log -->
 <xsl:template match="logentry">
  <xsl:choose>
   <!-- if we're grouping we should omit some headers -->
   <xsl:when test="$groupbyday='yes'">
    <!-- save log entry number -->
    <xsl:variable name="pos" select="position()" />
    <!-- fetch previous entry's date -->
    <xsl:variable name="prevdate">
     <xsl:apply-templates select="../logentry[position()=(($pos)-1)]/date" />
    </xsl:variable>
    <!-- fetch previous entry's author -->
    <xsl:variable name="prevauthor">
     <xsl:value-of select="normalize-space(../logentry[position()=(($pos)-1)]/author)" />
    </xsl:variable>
    <!-- fetch this entry's date -->
    <xsl:variable name="date">
     <xsl:apply-templates select="date" />
    </xsl:variable>
    <!-- fetch this entry's author -->
    <xsl:variable name="author">
     <xsl:value-of select="normalize-space(author)" />
    </xsl:variable>
    <!-- check if header is changed -->
    <xsl:if test="($prevdate!=$date) or ($prevauthor!=$author)">
     <li class="changelog_entry">
      <!-- date -->
      <span class="changelog_date"><xsl:value-of select="$date" /></span>
      <xsl:text>&#x20;</xsl:text>
      <!-- author's name -->
      <span class="changelog_author"><xsl:apply-templates select="author" /></span>
     </li>
    </xsl:if>
   </xsl:when>
   <!-- write the log header -->
   <xsl:otherwise>
    <li class="changelog_entry">
     <!-- date -->
     <span class="changelog_date"><xsl:apply-templates select="date" /></span>
     <xsl:text>&#x20;</xsl:text>
     <!-- author's name -->
     <span class="changelog_author"><xsl:apply-templates select="author" /></span>
    </li>
   </xsl:otherwise>
  </xsl:choose>
  <!-- entry -->
  <li class="changelog_change">
   <!-- get revision number -->
   <xsl:variable name="revlink">
    <xsl:choose>
     <xsl:when test="contains($revision-link,'##')">
      <xsl:value-of select="concat(substring-before($revision-link,'##'),@revision,substring-after($revision-link,'##'))" />
     </xsl:when>
     <xsl:otherwise>
      <xsl:value-of select="concat($revision-link,@revision)" />
     </xsl:otherwise>
    </xsl:choose>
   </xsl:variable>
   <span class="changelog_revision">
    <a id="r{@revision}" href="{$revlink}">[r<xsl:value-of select="@revision" />]</a>
   </span>
   <xsl:text>&#x20;</xsl:text>
   <!-- get paths string -->
   <span class="changelog_files"><xsl:apply-templates select="paths" /></span>
   <xsl:text>&#x20;</xsl:text>
   <!-- get message text -->
   <xsl:variable name="msg">
    <xsl:call-template name="trim-newln">
     <xsl:with-param name="txt" select="msg" />
    </xsl:call-template>
   </xsl:variable>
   <span class="changelog_message">
    <xsl:call-template name="newlinestobr">
     <xsl:with-param name="txt" select="$msg" />
    </xsl:call-template>
   </span>
  </li>
 </xsl:template>

 <!-- template to replace line breaks with <br /> tags -->
 <xsl:template name="newlinestobr">
  <xsl:param name="txt" />
  <xsl:choose>
   <xsl:when test="contains($txt,'&#xa;')">
    <!-- text contains newlines, do the first line -->
    <xsl:value-of select="substring-before($txt,'&#xa;')" />
    <!-- print new line -->
    <br />
    <!-- wrap the rest of the text -->
    <xsl:call-template name="newlinestobr">
     <xsl:with-param name="txt" select="substring-after($txt,'&#xa;')" />
    </xsl:call-template>
   </xsl:when>
   <xsl:otherwise>
    <xsl:value-of select="$txt" />
   </xsl:otherwise>
  </xsl:choose>
 </xsl:template>

</xsl:stylesheet>