<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="Gedcom.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Genealogía Chilena en Red</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
    <tr>
    <td style="width:50%"><h7>I N T R O D U C C I Ó N</h7></td>
    <td style="width:50%"><div style="height:1px" class="fb-like" data-href="http://www.genealogiachilenaenred.cl" data-send="true" data-show-faces="false" data-font="arial"></div></td>
    </tr>
    </table>
    <p>
        &nbsp;</p>
    <h3  >
        INVESTIGADOR Y RECOPILADOR: JUAN DALMATI CARRASCO
    </h3>
    <h3 >
        COLABORADOR: JOSÉ URZÚA PRIETO, GENEALOGISTA</h3>
    <p >
        &nbsp;</p>
    <h4>
        Este ensayo genealógico, producto de largos años de trabajo, a diferencia de 
        otras interesantes publicaciones, que abordan uno o varios linajes en forma 
        lineal, busca desarrollar el concepto de la gran familia chilena, tomada como un 
        todo.<br />
        <br />
        Facilita esta labor la alta endogamia existente por siglos en esta aislada, 
        larga y angosta faja de tierra. No es aventurado decir que en Chile todos somos 
        parientes, carnales o políticos. Una muestra de esto se encuentra en la obra 
        &quot;Familias Fundadoras de Chile&quot;, en la que, tomando como referencia la familia 
        Gaete, se expone una amplia muestra del parentesco, no detectable a primera 
        vista, que liga a una apreciable cantidad de personajes chilenos.<br />
        <br />
        El presente ensayo pretende, en una escala mayor, confirmar la realidad 
        contenida en el concepto de &quot;la gran familia chilena&quot;, tomando como columna 
        vertebral el linaje que se originó en Chile en el siglo XVI con el matrimonio de 
        <a href="gcr/IndividualPage.aspx?ID=I1">FRANCISCO RIQUELME DE LA BARRERA</a>, español, con&nbsp; <a href="gcr/IndividualPage.aspx?ID=I2">LEONOR ÁLVAREZ DE TOLEDO 
        ALFARO.</a><br />
        <br />
        En su descendencia se encontrará a Gobernadores del Reino de Chile, Directores 
        Supremos y Presidentes de la República, miembros de los poderes Legislativo y 
        Judicial, agricultores, mineros, eclesiásticos, militares, abogados, médicos, 
        ingenieros e individuos de otras profesiones liberales, inquilinos, empresarios, 
        obreros, empleados y un largo etcétera, todos parientes en algún grado, que con 
        el andar del tiempo fueron formando lo que es la sociedad chilena de hoy.<br />
        <br />
        La base documental que sustenta este trabajo está constituida por los archivos 
        notariales y judiciales que se custodian en el Archivo Nacional de Chile; los 
        libros parroquiales de la Iglesia Católica (de bautismos, matrimonios y 
        defunciones), microfilmados por la Iglesia de los Santos de los Últimos Días 
        (Mormones), que pueden consultarse en los templos de éstos y en el Archivo 
        Histórico del Arzobispado de Santiago; el Registro Civil, que comenzó a 
        funcionar el 1ð de enero de 1885 y cuyos libros, desde esa fecha hasta 1903, han 
        sido microfilmados por los mormones y los han subido a Internet; libros y 
        revistas de genealogía, publicaciones en periódicos, páginas web y datos orales 
        o manuscritos proporcionados por personas a quienes se agradece su aporte y que 
        se mencionan en su lugar. Todas las fuentes usadas son públicas o han sido 
        proporcionadas por los interesados. En todo caso, existe la opción de retirarse 
        para los que no deseen figurar en este estudio.<br />
        <br />
        El autor no persigue fines de lucro y su propósito es contribuir con un modesto 
        aporte histórico-cultural al conocimiento de nuestra realidad social. Agradece 
        la colaboración del genealogista José Urzúa Prieto Y AL WEB MASTER.<br />
        <br />
        Juan Dalmati C.</h4>
        <h7>
    <br />
    <br />
        M E M O R Á N D U M<br />
        </h7>
        <h4>        
IMÁGENES O NOTAS CON DERECHOS RESERVADOS, QUE EVENTUAL E INVOLUNTARIAMENTE, HAYAMOS PUBLICADO, POR FAVOR, DANOS EL PRIVILEGIO DE ELLO O REPORTA EL DESLÍZ, PARA SU EXCLUSIÓN Y TE ROGAMOS EXCUSARNOS POR LA FALTA DE PROLIGIDAD.<br />
        </h4>
    <h4>        
        AGRADECEMOS A TODAS NUESTRAS VISITAS
COLABORAR CON FOTOS FECHAS Y DATOS, REPORTA ERRORES, ETC.</h4>
    <h4>        
SI NO ERES PARTE DE LA RED Y DESEAS INGRESAR GRATUITAMENTE, INDÍCANOS PARIENTE (S) EXISTENTE (S), HACER REFERENCIA A SUS NOMBRES Y LA SECUENCIA GENEALÓGICA HASTA TUS PROPIOS DATOS. (SI LOS INCLUÍMOS PASARÁS A SER PARTE DE LA  RED)<br />
        </h4>
    <h4>        
        SELECCIONA LA OPCIÖN &quot;CONTACTO&quot; EN LA PARTE SUPERIOR DE ESTA PÁGINA PARA ACOJER 
        LAS OPCIONES OFRECIDAS &nbsp;EN LOS ACÁPITES ANTERIORES<br />
        </h4>
     <h7>
    <br />
    <br />
        B Ú S Q U E D A<br />
    </h7>
    <h4>
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                Nombres</td>
            <td>
                Apellidos</td>
            <td>
                </td>
        </tr>
        <tr>
            <td class="style1">
                Ingrese los nombres y/o apellidos de la persona que busca:</td>
            <td class="style1">
                <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox></td>
            <td class="style1">
                <asp:TextBox ID="txtSurName" runat="server" Width="200px"></asp:TextBox></td>
            <td class="style1">
                <asp:Button ID="btnSearch" runat="server" Text="Buscar" /></td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
            <td class="style1" style="font-size:10px" >
                EJEMPLO: MAZA, de la, BARRERA, de la; CASTRO, de; PINO, del</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>
    </h4>
</asp:Content>
