<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<html>
			<body>
				<h2>Моя Бібліотека</h2>
				<table border="1">
					<tr bgcolor="#9acd32">
						<th>Назва</th>
						<th>Автор</th>
						<th>Рік</th>
					</tr>
					<xsl:for-each select="library/book">
						<tr>
							<td>
								<xsl:value-of select="title"/>
							</td>
							<td>
								<xsl:value-of select="author"/>
							</td>
							<td>
								<xsl:value-of select="publicationQualities/year"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>

</xsl:stylesheet>