﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IndividualPage.aspx.vb" MasterPageFile="~/Site.Master" Inherits="Gedcom.Web.IndividualPage"  %>
<%@ Register src="IndividualUC.ascx" tagname="IndividualUC" tagprefix="uc1" %>
<%@ Register src="IndividualListUC.ascx" tagname="IndividualListUC" tagprefix="uc2" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title><%=FullName()%> - Genealogía Chilena en Red</title>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/stepcarousel.js">

        /***********************************************
        * Step Carousel Viewer script- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
        * Visit http://www.dynamicDrive.com for hundreds of DHTML scripts
        * This notice must stay intact for legal use
        ***********************************************/

    </script>

    <style type="text/css">

    .stepcarousel{
    position: relative; /*leave this value alone*/
    border: transparent;
    overflow: scroll; /*leave this value alone*/
    width: 372px; /*Width of Carousel Viewer itself*/
    height: 120px; /*Height should enough to fit largest content's height*/
    }

    .stepcarousel .belt{
    position: absolute; /*leave this value alone*/
    left: 0;
    top: 0;
    }

    .stepcarousel .panel{
    float: left; /*leave this value alone*/
    overflow: hidden; /*clip content that go outside dimensions of holding panel DIV*/
    margin: 4px; /*margin around each panel*/
    width: 120px;  /*Width of each panel holding each content. If removed, widths should be individually defined on each content DIV then. */
    }

    </style>



    <script type="text/javascript">

        var mediacount = <%=MediaCount()%>;

        if (mediacount > 0) {

            stepcarousel.setup({
                galleryid: 'mygallery', //id of carousel DIV
                beltclass: 'belt', //class of inner "belt" DIV containing all the panel DIVs
                panelclass: 'panel', //class of panel DIVs each holding content
                autostep: { enable: true, moveby: 1, pause: 3000 },
                panelbehavior: { speed: 500, wraparound: false, wrapbehavior: 'slide', persist: true },
                defaultbuttons: { enable: true, moveby: 1, leftnav: ['../img/leftnav.gif', -5, 50], rightnav: ['../img/rightnav.gif', -20, 50] },
                statusvars: ['statusA', 'statusB', 'statusC'], //register 3 variables that contain current panel (start), current panel (last), and total panels
                contenttype: ['inline'] //content setting ['inline'] or ['ajax', 'path_to_external_file']
            })

        }

    </script>

    <!-- colorbox --> 
    <link rel="stylesheet" href="../Styles/colorbox.css" />
    <script src="../Scripts/jquery.colorbox.js" type="text/javascript"></script>
    <style type="text/css">
        img {border:none;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //Examples of how to assign the Colorbox event to elements
            $(".gallery1").colorbox({ rel: 'gallery1', transition: "none", width: "75%", height: "75%" });
            $(".inline").colorbox({ inline: true, width: "75%" });
            $(".openpage").colorbox({ width: "75%" });
        });
    </script>

    <style type="text/css">
    .notes
    {
        font-size: 1.2em;
        font-family:  Arial; 
        font-weight: bolder;
    }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 style="text-align:left;">
    <asp:Label ID="lblNames" runat="server" Text=""></asp:Label>
</h2>
    <asp:Panel ID="pnlIndividual" runat="server">
    <table>
    <tr>
    <td style="width:50%"><h7><%=FullName()%></h7><asp:Panel ID="pnlAdmin" runat="server" Visible = "false">
    <asp:Button ID="btnDelete" runat="server" Text="Eliminar" />
    <asp:Button ID="btnRestore" runat="server" Text="Restaurar" />
</asp:Panel></td>
    <td style="width:50%"><div style="height:1px" class="fb-like" data-href="http://www.genealogiachilenaenred.cl/gcr/IndividualPage.aspx?ID=<%=Original_ID()%>" data-send="true" data-show-faces="false" data-font="arial"></div></td>
    </tr>
    </table>
    <table style="width: 100%;">
    <tr>
        <td>
            <table style="width: 100%;" >
                <tr>
                    <td style="vertical-align:top">
                        <uc1:IndividualUC ID="IndividualUC" runat="server" />
                            <a class="openpage" title="Ver notas" runat="Server" 
                            id="lnkNotes"><img src="../img/icon-notes.png" alt="Notas"/>&nbsp;Ver notas</a>
                    </td>
                    <td style="width:360px">
<asp:Panel ID="gallery" runat="server">
                    <div id="mygallery" class="stepcarousel" >
                        <div class="belt">
                            <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div class="panel" style="text-align:center;" >
                                <a class='gallery1' href='<%#Eval("image")%>' title='<%#Eval("note")%>'>
                                    <img src="<%#Eval("thumbnail")%>" alt="<%#Eval("note")%>" />
                                </a>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div id="mygallery-paginate" style="width: 372px; text-align:center">
                    <img src="../img/opencircle.png" data-over="../img/graycircle.png" data-select="../img/closedcircle.png" data-moveby="1" />
                    </div>
</asp:Panel>
                    </td>
                    <td style="width:30%">
                        <asp:Panel ID="pnlParents" runat="server" Visible="False">
                        <h2 style="text-align:left;">
                            Padres:
                        </h2>
                            
                            <uc1:IndividualUC ID="FatherUC" runat="server" /><br />
                            <uc1:IndividualUC ID="MotherUC" runat="server" />
                        </asp:Panel>
                            
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    <asp:Panel ID="pnlFamilies" runat="server" Visible="false">
                        <h2 style="text-align:left;">Cónyuges:<asp:DropDownList ID="ddlFamilies" runat="server" AutoPostBack="True"></asp:DropDownList></h2>
                        <uc1:IndividualUC ID="SpouseUC" runat="server" />
                        <a class='openpage' title='Ver notas familiares' runat="Server" id="lnkFamilyNotes"><img src="../img/icon-notes.png" alt="Notas"/>&nbsp;Ver notas familiares</a>
                    </asp:Panel>
                    <asp:Panel ID="pnlNoFamilies" runat="server" Visible="false">
                        Sin Familias.
                    </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlChildrens" runat="server" Visible="false">
                <h2 style="text-align:left;">
                Hijos:
                </h2>
        <uc2:IndividualListUC ID="ChildrenList" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlNoChildrens" runat="server" Visible="false">
                Sin Hijos.
            </asp:Panel>
        </td>
    </tr>
</table>
</asp:Panel>
<br />
<div class="fb-comments" data-href="http://www.genealogiachilenaenred.cl/gcr/IndividualPage.aspx?ID=<%=Original_Id()%>" data-width="600" data-num-posts="10"></div>
</asp:Content>

